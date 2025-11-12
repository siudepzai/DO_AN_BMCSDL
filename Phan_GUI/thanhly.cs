using _40_caesarOracle;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace DO_AN_BMCSDL.Phan_GUI
{
    public partial class thanhly : Form
    {
        // 🛠️ Giả định tên DataGridView là dgvThanhLy
        // Giả định tên các Form con là themtailieu_thanhly và suathanhlytailieu

        public thanhly()
        {
            InitializeComponent();
        }

        private void btn_thoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void LoadDataThanhLy()
        {
            string sql = @"
                SELECT
                    ROWNUM AS STT,
                    TRIM(T1.MAHOADON) AS ""Ma phieu thanh ly"",
                    T1.NGAYTHANHLY AS ""Ngay lap phieu"",
                    TRIM(T1.GHICHU) AS ""Ghi chu"",
                    TRIM(T1.MANV) AS ""Ma NV lap"" -- Mã NV ẩn để xử lý
                FROM THANHLYTAILIEU T1
                ORDER BY STT ASC";

            try
            {
                if (Database.Connect())
                {
                    DataTable dt = Database.ExecuteQuery(sql);
                    dgvThanhLy.DataSource = dt;

                    // Ẩn cột Ma NV lap
                    if (dgvThanhLy.Columns.Contains("Ma NV lap"))
                    {
                        dgvThanhLy.Columns["Ma NV lap"].Visible = false;
                    }
                    // Định dạng cột Ngày lập
                    if (dgvThanhLy.Columns.Contains("Ngay lap phieu"))
                    {
                        dgvThanhLy.Columns["Ngay lap phieu"].DefaultCellStyle.Format = "dd/MM/yyyy";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu Thanh lý:\n" + ex.Message, "Lỗi SQL");
            }
            finally
            {
                Database.Close();
            }
        }

        // --- XỬ LÝ NÚT THÊM ---
        private void btn_them_Click(object sender, EventArgs e)
        {
            themtailieu_thanhly formThem = new themtailieu_thanhly();
            
            if (formThem.ShowDialog() == DialogResult.OK)
            {
               
                LoadDataThanhLy();
            }
        }

        // --- XỬ LÝ NÚT SỬA ---
        private void btn_sua_Click(object sender, EventArgs e)
        {
            if (dgvThanhLy.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn phiếu thanh lý cần sửa.", "Thông báo");
                return;
            }

            // Lấy Ma phieu thanh ly từ dòng được chọn
            string maHoaDon = dgvThanhLy.CurrentRow.Cells["Ma phieu thanh ly"].Value.ToString().Trim();

            suathanhlytailieu formSua = new suathanhlytailieu(maHoaDon); // Truyền Mã hóa đơn vào constructor

            // Tải lại dữ liệu khi Form con đóng thành công
            if (formSua.ShowDialog() == DialogResult.OK)
            {
                LoadDataThanhLy();
            }
        }

        // --- XỬ LÝ NÚT XÓA ---
        private void btn_xoa_Click(object sender, EventArgs e)
        {
            if (dgvThanhLy.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn phiếu thanh lý cần xóa.", "Thông báo");
                return;
            }

            string maHoaDon = dgvThanhLy.CurrentRow.Cells["Ma phieu thanh ly"].Value.ToString().Trim();
            DialogResult confirm = MessageBox.Show($"Xác nhận xóa phiếu thanh lý {maHoaDon}?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (confirm == DialogResult.Yes)
            {
                HandleDeleteThanhLy(maHoaDon);
            }
        }

        // --- LOGIC XÓA DỮ LIỆU ---
        private void HandleDeleteThanhLy(string maHoaDon)
        {
            // Cần có bảng chi tiết để xóa chi tiết trước (nếu có)
            string sqlDelete = "DELETE FROM THANHLYTAILIEU WHERE TRIM(MAHOADON) = :maHoaDon";

            try
            {
                if (Database.Connect())
                {
                    int rowsAffected = Database.ExecuteNonQuery(sqlDelete, new OracleParameter("maHoaDon", maHoaDon));

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Xóa phiếu thanh lý thành công!", "Thành công");
                        LoadDataThanhLy();
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy phiếu thanh lý này.", "Lỗi");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi CSDL khi xóa phiếu thanh lý: " + ex.Message, "Lỗi SQL");
            }
            finally
            {
                Database.Close();
            }
        }

        private void dgvThanhLy_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void thanhly_Load_1(object sender, EventArgs e)
        {
            try
            {
                // Thiết lập Database
                Database.Set_Database("localhost", "1521", "ORCL", "C##DO_AN", "12345");
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show("Lỗi thiết lập thông tin DB: " + ex.Message, "Lỗi nghiêm trọng");
                return;
            }

            // Thiết lập DataGridView
            if (dgvThanhLy != null)
            {
                dgvThanhLy.Font = new Font("Times New Roman", 12, FontStyle.Regular);
                dgvThanhLy.ColumnHeadersDefaultCellStyle.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                dgvThanhLy.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                LoadDataThanhLy();
            }
        }
    }
}