using _40_caesarOracle;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace DO_AN_BMCSDL.Phan_GUI
{
    public partial class QL_phong : Form
    {

        public QL_phong()
        {
            InitializeComponent();
        }

        private void btn_thoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void QL_phong_Load(object sender, EventArgs e)
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

            // Thiết lập Font và Style cho DataGridView
            if (dgvMuonTra != null)
            {
                dgvMuonTra.Font = new Font("Times New Roman", 12, FontStyle.Regular);
                dgvMuonTra.ColumnHeadersDefaultCellStyle.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                dgvMuonTra.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                // Gán sự kiện CellClick để mở Form xử lý phiếu
                dgvMuonTra.CellContentClick -= dgvPhieuYeuCau_CellContentClick; // Tránh gán lại
                dgvMuonTra.CellContentClick += dgvPhieuYeuCau_CellContentClick;

                // Gán sự kiện định dạng ô để tô màu trạng thái
                dgvMuonTra.CellFormatting -= dgvPhieuYeuCau_CellFormatting; // Tránh gán lại
                dgvMuonTra.CellFormatting += dgvPhieuYeuCau_CellFormatting;

                // Tải dữ liệu ban đầu
                LoadDataPhongHoc();
            }
        }

        // --- HÀM TẢI DỮ LIỆU PHIẾU YÊU CẦU PHÒNG HỌC (Có hỗ trợ tìm kiếm) ---
        private void LoadDataPhongHoc(string searchTerm = "")
        {
            // Truy vấn lấy dữ liệu PHIEU_YEU_CAU (DATPHONG)
            string sql = @"
                SELECT 
                    ROWNUM AS STT, 
                    TRIM(T1.MAPHONG) AS ""Ma phong"", 
                    TRIM(T2.MATHANHVIEN) AS ""Ma doc gia"",
                    TRIM(T1.YEUCAU) AS ""Yeu cau"",
                    TRIM(T1.TRANGTHAI) AS ""Trang thai xu ly"",
                    TRIM(T1.MADATPHONG) AS ""Ma phieu"" -- Ma phieu ẩn để xử lý
                FROM DATPHONG T1
                JOIN THEBANDOC T2 ON T1.MASOTHE = T2.MASOTHE
                WHERE (LOWER(TRIM(T1.MAPHONG)) LIKE '%' || :searchTerm || '%' OR
                       LOWER(TRIM(T2.MATHANHVIEN)) LIKE '%' || :searchTerm || '%')
                ORDER BY T1.NGAYDAT DESC";

            try
            {
                if (Database.Connect())
                {
                    DataTable dt = Database.ExecuteQuery(sql, new OracleParameter("searchTerm", searchTerm.ToLower()));
                    dgvMuonTra.DataSource = dt;

                    // 1. Thêm cột nút bấm nếu chưa có
                    SetupButtonColumn();

                    // 2. Ẩn cột Ma phieu (Ma phieu)
                    if (dgvMuonTra.Columns.Contains("Ma phieu"))
                    {
                        dgvMuonTra.Columns["Ma phieu"].Visible = false;
                    }

                    // Căn giữa các cột
                    foreach (DataGridViewColumn col in dgvMuonTra.Columns)
                    {
                        col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu Phiếu Yêu Cầu Phòng:\n" + ex.Message, "Lỗi SQL");
            }
            finally
            {
                Database.Close();
            }
        }

        // --- CÁC HÀM XỬ LÝ DATAGRIDVIEW ---

        // Thêm cột nút bấm "Thông tin phiếu"
        private void SetupButtonColumn()
        {
            // Chỉ thêm nếu cột chưa tồn tại
            if (!dgvMuonTra.Columns.Contains("btnThongTinPhieu"))
            {
                DataGridViewButtonColumn btnColumn = new DataGridViewButtonColumn();
                btnColumn.Name = "btnThongTinPhieu";
                btnColumn.HeaderText = "";
                btnColumn.Text = "Thông tin phiếu";
                btnColumn.UseColumnTextForButtonValue = true;
                btnColumn.DefaultCellStyle.BackColor = Color.LightGray;
                btnColumn.DefaultCellStyle.SelectionBackColor = Color.Gray;

                dgvMuonTra.Columns.Add(btnColumn);
            }
        }

        // 🔹 Xử lý click vào nút "Thông tin phiếu" (ĐÃ CẬP NHẬT)
        private void dgvPhieuYeuCau_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Kiểm tra xem có phải cột nút bấm không
            if (e.RowIndex >= 0 && dgvMuonTra.Columns[e.ColumnIndex].Name == "btnThongTinPhieu")
            {
                try
                {
                    // Lấy Ma phieu từ cột ẩn (Ma phieu)
                    string maPhieu = dgvMuonTra.Rows[e.RowIndex].Cells["Ma phieu"].Value.ToString().Trim();

                    // Mở Form xử lý phiếu
                    Thongtinphieu_phong formXuLy = new Thongtinphieu_phong(maPhieu);

                    // Gán sự kiện FormClosed để tải lại DataGridView khi Form con đóng
                    formXuLy.FormClosed += FormXuLy_FormClosed;

                    formXuLy.ShowDialog();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi mở Form chi tiết phiếu: " + ex.Message, "Lỗi");
                }
            }
        }

        // 🔹 Định dạng màu sắc cho cột "Trạng thái xử lý"
        private void dgvPhieuYeuCau_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // Lấy tên cột "Trạng thái xử lý"
            if (dgvMuonTra != null && dgvMuonTra.Columns[e.ColumnIndex].HeaderText == "Trạng thái xử lý")
            {
                string trangThai = e.Value?.ToString().Trim();

                if (trangThai == "Dong y")
                {
                    e.CellStyle.ForeColor = Color.DarkGreen;
                    e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                }
                else if (trangThai == "Tu choi")
                {
                    e.CellStyle.ForeColor = Color.Red;
                    e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                }
                else if (trangThai == "Ket thuc muon") // Giả sử đây là trạng thái hoàn tất cũ
                {
                    e.CellStyle.ForeColor = Color.Blue;
                    e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                }
                else // Chờ xác nhận
                {
                    e.CellStyle.ForeColor = Color.Orange;
                }
            }
        }

        // 🔹 Xử lý khi Form xử lý phiếu con đóng (để tải lại DGV) (ĐÃ CẬP NHẬT)
        private void FormXuLy_FormClosed(object sender, FormClosedEventArgs e)
        {
            // Kiểm tra DialogResult.OK để chỉ tải lại khi có thay đổi thành công
            if (sender is Thongtinphieu_phong form && form.DialogResult == DialogResult.OK)
            {
                LoadDataPhongHoc(); // Tải lại dữ liệu sau khi Form xử lý đóng
            }

            // Hủy đăng ký sự kiện để tránh rò rỉ bộ nhớ
            if (sender is Thongtinphieu_phong formToUnsubscribe)
            {
                formToUnsubscribe.FormClosed -= FormXuLy_FormClosed;
            }
        }

        // 🔹 Xử lý nút TÌM KIẾM
        private void btn_Tim_Click(object sender, EventArgs e)
        {
            if (txt_timkiem == null)
            {
                MessageBox.Show("Lỗi: Không tìm thấy TextBox tìm kiếm.", "Lỗi hệ thống");
                return;
            }
            string searchTerm = txt_timkiem.Text.Trim();
            LoadDataPhongHoc(searchTerm);
        }

        private void btn_thongtinphong_Click(object sender, EventArgs e)
        {
            // Giữ nguyên logic cũ hoặc tải lại dữ liệu phòng học
            LoadDataPhongHoc();
        }
    }
}