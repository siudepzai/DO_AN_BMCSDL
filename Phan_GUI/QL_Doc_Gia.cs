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
                Database.Set_Database("localhost", "1521", "ORCL", "C##DO_AN", "12345");
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show("Lỗi thiết lập thông tin DB: " + ex.Message, "Lỗi nghiêm trọng");
                return;
            }

            dgvDocGia.Font = new Font("Times New Roman", 12, FontStyle.Regular);
            dgvDocGia.ColumnHeadersDefaultCellStyle.Font = new Font("Times New Roman", 12, FontStyle.Bold);

            dgvDocGia.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            LoadDataDocGia();
        }

        private void btn_them_Click(object sender, EventArgs e)
        {
            Them_doc_gia formThem = new Them_doc_gia();

            if (formThem.ShowDialog() == DialogResult.OK)
            {
                LoadDataDocGia();
            }
        }

        private void btn_sua_Click(object sender, EventArgs e)
        {
            if (dgvDocGia.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn độc giả cần sửa!", "Thông báo");
                return;
            }

            string maDocGia = GetSelectedMaDocGia();
            if (string.IsNullOrEmpty(maDocGia))
            {
                MessageBox.Show("Không thể lấy MA DOC GIA từ dòng đã chọn.", "Lỗi dữ liệu");
                return;
            }

            suathongtindocgia formSua = new suathongtindocgia(maDocGia);

            if (formSua.ShowDialog() == DialogResult.OK)
            {
                LoadDataDocGia();
            }
        }

        private void btn_thoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_xoa_Click(object sender, EventArgs e)
        {
            if (dgvDocGia.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn một Độc giả để xóa.", "Thông báo",
                                 MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string maDocGia = GetSelectedMaDocGia();
            if (string.IsNullOrEmpty(maDocGia))
            {
                MessageBox.Show("Không thể lấy MA DOC GIA từ dòng đã chọn.", "Lỗi dữ liệu");
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
                if (ex.Number == 2292) 
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

        private void btn_thongtin_Click(object sender, EventArgs e)
        {
            if (dgvDocGia.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn một Độc giả để xem thông tin chi tiết.", "Thông báo",
                                 MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string maDocGia = GetSelectedMaDocGia();

            if (string.IsNullOrEmpty(maDocGia))
            {
                MessageBox.Show("Không thể lấy MA DOC GIA từ dòng đã chọn.", "Lỗi dữ liệu");
                return;
            }

            suathongtindocgia formThongTin = new suathongtindocgia(maDocGia);

            
            formThongTin.IsReadOnlyMode = true;
            formThongTin.ShowOriginalInfoButton = true;

            formThongTin.ShowDialog();
        }

        private string GetSelectedMaDocGia()
        {
            try
            {
                if (dgvDocGia.CurrentRow != null)
                {
                    object value = dgvDocGia.CurrentRow.Cells[1].Value;

                    if (value != null)
                    {
                        return value.ToString().Trim();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi lấy MA DOC GIA từ DGV: " + ex.Message);
                return string.Empty;
            }
            return string.Empty;
        }


        private void LoadDataDocGia()
        {
            string sql = @"
                SELECT 
                    ROWNUM AS STT, 
                    TRIM(T1.MATHANHVIEN) AS ""MA DOC GIA"", 
                    TRIM(T1.TENTV) AS ""TEN DOC GIA"", 
                    TRIM(T1.VAITRO) AS ""VAI TRO"", 
                    TO_CHAR(T1.NGSINH, 'DD/MM/YYYY') AS ""NGAY THAM GIA""
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