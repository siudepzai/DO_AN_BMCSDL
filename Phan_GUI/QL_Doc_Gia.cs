using _40_caesarOracle;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace DO_AN_BMCSDL.Phan_GUI
{
    public partial class QL_Doc_Gia : Form
    {
        public QL_Doc_Gia()
        {
            InitializeComponent();
        }

        private void QL_Doc_Gia_Load(object sender, EventArgs e)
        {
            try
            {
                // Giả định bạn gọi Set_Database ở đây hoặc ở điểm khởi đầu ứng dụng
                Database.Set_Database("localhost", "1521", "ORCL", "C##DO_AN", "12345");
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show("Lỗi thiết lập thông tin DB: " + ex.Message, "Lỗi nghiêm trọng");
                return;
            }

            dgvDocGia.Font = new Font("Times New Roman", 12, FontStyle.Regular);
            dgvDocGia.ColumnHeadersDefaultCellStyle.Font = new Font("Times New Roman", 12, FontStyle.Bold);

            // Thiết lập AutoSizeColumnsMode
            dgvDocGia.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            LoadDataDocGia();
        }

        // 🔹 Nút THÊM
        private void btn_them_Click(object sender, EventArgs e)
        {
            Them_doc_gia formThem = new Them_doc_gia();

            if (formThem.ShowDialog() == DialogResult.OK)
            {
                LoadDataDocGia();
            }
        }

        // 🔹 Nút SỬA
        private void btn_sua_Click(object sender, EventArgs e)
        {
            if (dgvDocGia.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn độc giả cần sửa!", "Thông báo");
                return;
            }

            // Lấy Mã độc giả an toàn bằng chỉ số cột
            string maDocGia = GetSelectedMaDocGia();
            if (string.IsNullOrEmpty(maDocGia))
            {
                MessageBox.Show("Không thể lấy Mã Độc giả từ dòng đã chọn.", "Lỗi dữ liệu");
                return;
            }

            suathongtindocgia formSua = new suathongtindocgia(maDocGia);

            if (formSua.ShowDialog() == DialogResult.OK)
            {
                LoadDataDocGia();
            }
        }

        // 🔹 Nút THOÁT
        private void btn_thoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // 🔹 Nút XÓA (Đã sửa lỗi TRIM)
        private void btn_xoa_Click(object sender, EventArgs e)
        {
            if (dgvDocGia.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn một Độc giả để xóa.", "Thông báo",
                                 MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Lấy Mã độc giả an toàn
            string maDocGia = GetSelectedMaDocGia();
            if (string.IsNullOrEmpty(maDocGia))
            {
                MessageBox.Show("Không thể lấy Mã Độc giả từ dòng đã chọn.", "Lỗi dữ liệu");
                return;
            }

            DialogResult confirm = MessageBox.Show($"Bạn có chắc chắn muốn xóa Độc giả có mã {maDocGia} không?\n" +
                                                  "Thao tác này không thể hoàn tác.",
                                                  "Xác nhận xóa",
                                                  MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (confirm == DialogResult.No)
            {
                return;
            }

            // ✅ SỬA LỖI: TRIM() cột MATHANHVIEN trong DB để so sánh an toàn
            string sql = "DELETE FROM DOCGIA WHERE TRIM(MATHANHVIEN) = :maDocGia";

            try
            {
                if (Database.Connect())
                {
                    using (OracleCommand cmd = new OracleCommand(sql, Database.Get_Connection()))
                    {
                        cmd.Parameters.Add(new OracleParameter("maDocGia", maDocGia));
                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Xóa độc giả thành công!", "Thành công");
                            LoadDataDocGia();
                        }
                        else
                        {
                            MessageBox.Show("Không tìm thấy Độc giả này trong cơ sở dữ liệu.", "Lỗi");
                        }
                    }
                }
            }
            catch (OracleException ex)
            {
                if (ex.Number == 2292) // Mã lỗi Khóa ngoại: ORA-02292
                {
                    MessageBox.Show("Lỗi: Không thể xóa vì Độc giả này còn liên kết dữ liệu (thẻ, phiếu mượn, phòng học) trong hệ thống. Vui lòng xử lý dữ liệu liên quan trước.",
                                    "Lỗi ràng buộc", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show("Lỗi CSDL khi xóa: " + ex.Message, "Lỗi SQL");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi hệ thống: " + ex.Message, "Lỗi");
            }
            finally
            {
                Database.Close();
            }
        }

        // 🔹 Nút THÔNG TIN (Đã hoàn thiện)
        private void btn_thongtin_Click(object sender, EventArgs e)
        {
            // 1. Kiểm tra lựa chọn dòng
            if (dgvDocGia.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn một Độc giả để xem thông tin chi tiết.", "Thông báo",
                                 MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // 2. Lấy Mã độc giả an toàn
            string maDocGia = GetSelectedMaDocGia();

            if (string.IsNullOrEmpty(maDocGia))
            {
                MessageBox.Show("Không thể lấy Mã Độc giả từ dòng đã chọn.", "Lỗi dữ liệu");
                return;
            }

            // 3. Mở Form Sửa Thông tin (suathongtindocgia)
            suathongtindocgia formThongTin = new suathongtindocgia(maDocGia);

            // 4. THIẾT LẬP CHẾ ĐỘ CHỈ ĐỌC
            // CHÚ Ý: Đảm bảo Form suathongtindocgia có thuộc tính IsReadOnlyMode
            formThongTin.IsReadOnlyMode = true;

            // 5. Hiển thị Form
            formThongTin.ShowDialog();
        }

        // --- HÀM TIỆN ÍCH: Lấy Mã Độc giả từ DGV an toàn hơn (Bằng chỉ số cột) ---
        private string GetSelectedMaDocGia()
        {
            try
            {
                if (dgvDocGia.CurrentRow != null)
                {
                    // Truy cập cột Mã độc giả bằng chỉ số 1 (Cột STT là 0)
                    object value = dgvDocGia.CurrentRow.Cells[1].Value;

                    if (value != null)
                    {
                        return value.ToString().Trim();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi lấy Mã Độc giả từ DGV: " + ex.Message);
                return string.Empty;
            }
            return string.Empty;
        }


        // 🔹 Load dữ liệu lên DataGridView
        private void LoadDataDocGia()
        {
            string sql = @"
                SELECT 
                    ROWNUM AS STT, 
                    TRIM(T1.MATHANHVIEN) AS ""MÃ ĐỘC GIẢ"", 
                    TRIM(T1.TENTV) AS ""TÊN ĐỘC GIẢ"", 
                    TRIM(T1.VAITRO) AS ""VAI TRÒ"", 
                    TO_CHAR(T1.NGSINH, 'DD/MM/YYYY') AS ""NGÀY THAM GIA""
                FROM DOCGIA T1
                ORDER BY T1.MATHANHVIEN";

            try
            {
                if (Database.Connect())
                {
                    DataTable dt = Database.ExecuteQuery(sql);
                    dgvDocGia.DataSource = dt;

                    // Căn giữa các cột
                    foreach (DataGridViewColumn col in dgvDocGia.Columns)
                    {
                        col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    }
                }
                else
                {
                    MessageBox.Show("Không thể kết nối đến Oracle.", "Lỗi kết nối");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu Độc giả:\n" + ex.Message, "Lỗi SQL");
            }
            finally
            {
                Database.Close();
            }
        }
    }
}