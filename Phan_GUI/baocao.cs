using _40_caesarOracle;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace DO_AN_BMCSDL.Phan_GUI
{
    public partial class baocao : Form
    {
        
        public baocao()
        {
            InitializeComponent();
        }

        private void baocao_Load(object sender, EventArgs e)
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

            if (dgvBaoCao != null)
            {
                dgvBaoCao.Font = new Font("Times New Roman", 12, FontStyle.Regular);
                dgvBaoCao.ColumnHeadersDefaultCellStyle.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                dgvBaoCao.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }

            if (cb_luachon != null && cb_luachon.Items.Count > 0)
            {
                cb_luachon.SelectedIndex = 0; // Mặc định là "Tất cả dữ liệu"
            }

            // Tải dữ liệu ban đầu (Chi tiết Tài liệu)
            LoadBaoCaoThongKe("ALL");
        }

        // --- HÀM HỖ TRỢ XÁC ĐỊNH PHẠM VI THỜI GIAN ---
        private bool TryGetDateRange(string scope, out DateTime startDate, out DateTime endDate)
        {
            startDate = DateTime.MinValue;
            endDate = DateTime.Today.AddDays(1).AddSeconds(-1);
            DateTime now = DateTime.Today;

            switch (scope)
            {
                case "MONTHLY":
                    startDate = new DateTime(now.Year, now.Month, 1);
                    break;
                case "QUARTERLY":
                    int currentQuarter = (now.Month - 1) / 3 + 1;
                    int startMonth = 3 * currentQuarter - 2;
                    startDate = new DateTime(now.Year, startMonth, 1);
                    break;
                case "YEARLY":
                    startDate = new DateTime(now.Year, 1, 1);
                    break;
                default:
                    return false;
            }
            return true;
        }

        private void LoadBaoCaoThongKe(string scope, string searchTerm = "")
        {
            string sql;
            bool isTimeScope = (scope == "MONTHLY" || scope == "QUARTERLY" || scope == "YEARLY");

            if (isTimeScope)
            {
                // CHẾ ĐỘ 1: THỐNG KÊ PHIẾU MƯỢN THEO THỜI GIAN (Có thể lọc được)
                sql = @"
                    SELECT
                        TRIM(T4.TENNV) AS ""Nguoi lap"",
                        COUNT(T1.MAPHIEUMUON) AS ""So luong phieu muon"",
                        TRIM(T1.MANV) AS ""Ma NV""
                    FROM PHIEUMUON T1
                    JOIN NHANVIEN T4 ON T1.MANV = T4.MANV
                    WHERE 1=1 ";

                DateTime startDate, endDate;
                if (TryGetDateRange(scope, out startDate, out endDate))
                {
                    sql += " AND T1.NGAYMUON >= :startDate AND T1.NGAYMUON <= :endDate ";
                }

                // Lọc tìm kiếm theo Mã NV
                if (!string.IsNullOrWhiteSpace(searchTerm))
                {
                    sql += " AND LOWER(TRIM(T1.MANV)) LIKE '%' || :searchTerm || '%' ";
                }

                sql += " GROUP BY T4.TENNV, T1.MANV ORDER BY COUNT(T1.MAPHIEUMUON) DESC";
            }
            else // CHẾ ĐỘ 2: DANH SÁCH CHI TIẾT TÀI LIỆU (Theo yêu cầu hình ảnh)
            {
                sql = @"
                    SELECT
                        TRIM(MATAILIEU) AS ""Ma tai lieu"",
                        TENSACH AS ""Ten tai lieu"",
                        NXB AS ""Nha xuat ban"",
                        NGONNGU AS ""Ngon ngu"",
                        TENTACGIA AS ""Tac gia"",
                        TINHTRANG AS ""Tinh trang""
                    FROM TAILIEU T1
                    WHERE 1=1 ";

                // Lọc tìm kiếm theo Mã, Tên, hoặc Tác giả
                if (!string.IsNullOrWhiteSpace(searchTerm))
                {
                    sql += @" AND (LOWER(TRIM(T1.MATAILIEU)) LIKE '%' || :searchTerm || '%' OR 
                                    LOWER(T1.TENSACH) LIKE '%' || :searchTerm || '%' OR
                                    LOWER(T1.TENTACGIA) LIKE '%' || :searchTerm || '%') ";
                }

                sql += " ORDER BY T1.TENSACH ASC";
            }

            try
            {
                if (Database.Connect())
                {
                    OracleCommand cmd = new OracleCommand(sql, Database.Get_Connection());

                    // Thêm tham số
                    if (isTimeScope && TryGetDateRange(scope, out DateTime startDate, out DateTime endDate))
                    {
                        cmd.Parameters.Add(new OracleParameter("startDate", startDate));
                        cmd.Parameters.Add(new OracleParameter("endDate", endDate));
                    }
                    if (!string.IsNullOrWhiteSpace(searchTerm))
                    {
                        cmd.Parameters.Add(new OracleParameter("searchTerm", searchTerm.ToLower()));
                    }

                    OracleDataAdapter da = new OracleDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    if (dgvBaoCao != null)
                    {
                        dgvBaoCao.DataSource = dt;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải báo cáo:\n" + ex.Message, "Lỗi SQL");
            }
            finally
            {
                Database.Close();
            }
        }

        // --- XỬ LÝ NÚT THỐNG KÊ (Kích hoạt chế độ lọc thời gian) ---
        private void btn_Thongke_Click(object sender, EventArgs e)
        {
            if (cb_luachon == null || cb_luachon.SelectedItem == null) return;

            string luaChon = cb_luachon.SelectedItem.ToString();
            string scope = (luaChon == "Tất cả dữ liệu") ? "ALL" :
                           (luaChon == "Dữ liệu theo tháng" ? "MONTHLY" :
                            (luaChon == "Dữ liệu theo quý" ? "QUARTERLY" :
                             (luaChon == "Dữ liệu theo năm" ? "YEARLY" : "ALL")));

            LoadBaoCaoThongKe(scope);
        }

        // --- XỬ LÝ NÚT TÌM KIẾM (Kích hoạt tìm kiếm, ưu tiên hiển thị chi tiết sách) ---
        private void btn_TK_Click(object sender, EventArgs e)
        {
            string searchTerm = txt_TK?.Text.Trim() ?? "";

            // Khi nhấn Tìm kiếm, luôn tải danh sách chi tiết sách (scope = ALL) để tìm kiếm trực tiếp trên sách
            LoadBaoCaoThongKe("ALL", searchTerm);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // --- CÁC HÀM KHÔNG DÙNG TRỰC TIẾP ---
        private void cb_luachon_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Không làm gì
        }

        private void dgvchitiet_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Không làm gì
        }
    }
}