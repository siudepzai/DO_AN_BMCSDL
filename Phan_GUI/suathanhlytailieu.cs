using _40_caesarOracle;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace DO_AN_BMCSDL.Phan_GUI
{
    public partial class suathanhlytailieu : Form
    {
        private string _maHoaDon;

        // Giả định tên controls: txtMaPhieu, txtNgayLap, txtGhiChu

        // Constructor mặc định (giữ nguyên)
        public suathanhlytailieu()
        {
            InitializeComponent();
        }

        // 🛠️ BỔ SUNG: Constructor nhận Mã hóa đơn để sửa
        public suathanhlytailieu(string maHoaDon)
        {
            InitializeComponent();
            _maHoaDon = maHoaDon;
            this.Load += suathanhlytailieu_Load; // Gán sự kiện Load
        }

        private void suathanhlytailieu_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(_maHoaDon))
            {
                LoadChiTietThanhLy();
            }
        }
        private void CreateDataGridViewColumns()
        {
            // Giả định tên DGV là dgvChiTiet
            if (dgvChiTiet == null || dgvChiTiet.Columns.Count > 0) return;

            // Tắt chế độ tự tạo cột (nếu có)
            dgvChiTiet.AutoGenerateColumns = false;

            // Thêm các cột theo thiết kế của bạn
            dgvChiTiet.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "ColMaTaiLieu",
                HeaderText = "Mã tài liệu - sách"
            });

            dgvChiTiet.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "ColTenTaiLieu",
                HeaderText = "Tên tài liệu - sách"
            });

            // Nếu Kho là ComboBox (tùy chọn), bạn dùng DataGridViewComboBoxColumn
            dgvChiTiet.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "ColKho",
                HeaderText = "Kho"
            });

            // Tùy chọn: Thêm cột nút Xóa dòng
            DataGridViewButtonColumn deleteCol = new DataGridViewButtonColumn();
            deleteCol.HeaderText = "Xóa";
            deleteCol.Text = "Xóa";
            deleteCol.UseColumnTextForButtonValue = true;
            deleteCol.Width = 50;
            dgvChiTiet.Columns.Add(deleteCol);

            // Điều chỉnh cột # (STT) nếu cần
            // dgvChiTiet.Columns[0].ReadOnly = true; 
        }

        // --- Tải chi tiết phiếu thanh lý cũ ---
        private void LoadChiTietThanhLy()
        {
            string sql = @"
                SELECT
                    TRIM(MAHOADON) AS MaPhieu,
                    NGAYTHANHLY AS NgayLap,
                    TRIM(GHICHU) AS GhiChu,
                    TRIM(MANV) AS MaNVLap
                FROM THANHLYTAILIEU
                WHERE TRIM(MAHOADON) = :maHoaDon";

            try
            {
                if (Database.Connect())
                {
                    DataTable dt = Database.ExecuteQuery(sql, new OracleParameter("maHoaDon", _maHoaDon));
                    if (dt.Rows.Count > 0)
                    {
                        DataRow row = dt.Rows[0];

                        // 🛠️ Gán dữ liệu vào các controls (Bạn cần thay tên control thực tế)
                        txt_maphieu.Text = row["MaPhieu"].ToString();
                        txt_ngay.Text = ((DateTime)row["NgayLap"]).ToString("dd/MM/yyyy");
                        txt_ghichu.Text = row["GhiChu"].ToString();

                        // Khóa Mã phiếu và Ngày lập
                        txt_maphieu.ReadOnly = true;
                        txt_ngay.ReadOnly = true;
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy dữ liệu phiếu thanh lý này.", "Lỗi");
                        this.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải chi tiết phiếu: " + ex.Message, "Lỗi SQL");
                this.Close();
            }
            finally
            {
                Database.Close();
            }
        }

        private void btn_them_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Chức năng thêm chi tiết tài liệu (DGV) đang được triển khai.", "Thông báo");
        }

        private void btn_luu_Click(object sender, EventArgs e)
        {
            
            string ghiChuMoi = txt_ghichu.Text.Trim();
            string sqlUpdate = "UPDATE THANHLYTAILIEU SET GHICHU = :ghiChu WHERE TRIM(MAHOADON) = :maHoaDon";

            try
            {
                if (Database.Connect())
                {
                    int rowsAffected = Database.ExecuteNonQuery(sqlUpdate,
                        new OracleParameter("ghiChu", ghiChuMoi),
                        new OracleParameter("maHoaDon", _maHoaDon));

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Sửa phiếu thanh lý thành công!", "Thành công");
                        this.DialogResult = DialogResult.OK; // Báo cho Form cha tải lại
                        this.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lưu dữ liệu: " + ex.Message, "Lỗi SQL");
            }
            finally
            {
                Database.Close();
            }
        }

        private void btn_huy_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Hủy lưu tài liệu thanh lý!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }

        private void btn_TK_Click(object sender, EventArgs e)
        {
            // Logic tìm kiếm tài liệu (nếu có DGV)
        }

        private void dgvChiTiet_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void suathanhlytailieu_Load_1(object sender, EventArgs e)
        {

        }
    }
}