using _40_caesarOracle;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace DO_AN_BMCSDL.Phan_GUI
{
    public partial class QL_phieumuon : Form
    {
       
        public QL_phieumuon()
        {
            InitializeComponent();
        }

        private void btn_thoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void QL_phieumuon_Load(object sender, EventArgs e)
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

            if (dgvMuonTra != null)
            {
                dgvMuonTra.Font = new Font("Times New Roman", 12, FontStyle.Regular);
                dgvMuonTra.ColumnHeadersDefaultCellStyle.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                dgvMuonTra.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                dgvMuonTra.CellContentClick -= dgvPhieuYeuCau_CellContentClick;
                dgvMuonTra.CellContentClick += dgvPhieuYeuCau_CellContentClick;

                dgvMuonTra.CellFormatting -= dgvPhieuYeuCau_CellFormatting;
                dgvMuonTra.CellFormatting += dgvPhieuYeuCau_CellFormatting;

                LoadDataPhieuMuon();
            }
        }

        private void LoadDataPhieuMuon(string searchTerm = "")
        {
            string sql = @"
                SELECT
                    ROWNUM AS STT,
                    TRIM(T2.MATAILIEU) AS ""Ma tai lieu - sach"",
                    TRIM(T3.MATHANHVIEN) AS ""Ma doc gia"",
                    'Muon sach' AS ""Yeu cau"", 
                    TRIM(T2.HIENTRANG) AS ""Trang thai xu ly"",  -- 🛠️ SỬA LỖI: Lấy HIENTRANG từ CHITIETPHIEUMUON (T2)
                    TRIM(T1.MAPHIEUMUON) AS ""Ma phieu"" 
                FROM PHIEUMUON T1
                JOIN CHITIETPHIEUMUON T2 ON T1.MAPHIEUMUON = T2.MAPHIEUMUON
                JOIN THEBANDOC T4 ON T1.MASOTHE = T4.MASOTHE
                JOIN DOCGIA T3 ON T4.MATHANHVIEN = T3.MATHANHVIEN
                WHERE (LOWER(TRIM(T2.MATAILIEU)) LIKE '%' || :searchTerm || '%' OR
                       LOWER(TRIM(T3.MATHANHVIEN)) LIKE '%' || :searchTerm || '%')
                ORDER BY T1.NGAYMUON DESC";

            try
            {
                if (Database.Connect())
                {
                    OracleParameter param = new OracleParameter("searchTerm", searchTerm.ToLower());
                    DataTable dt = Database.ExecuteQuery(sql, param);
                    dgvMuonTra.DataSource = dt;

                    SetupButtonColumn();

                    if (dgvMuonTra.Columns.Contains("Ma phieu"))
                    {
                        dgvMuonTra.Columns["Ma phieu"].Visible = false;
                    }
                    foreach (DataGridViewColumn col in dgvMuonTra.Columns)
                    {
                        col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu Phiếu Mượn/Trả Sách:\n" + ex.Message, "Lỗi SQL");
            }
            finally
            {
                Database.Close();
            }
        }

        private void SetupButtonColumn()
        {
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
        private void dgvPhieuYeuCau_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvMuonTra.Columns[e.ColumnIndex].Name == "btnThongTinPhieu")
            {
                try
                {
                    string maPhieu = dgvMuonTra.Rows[e.RowIndex].Cells["Ma phieu"].Value.ToString().Trim();

                    thongtinphieu_dulieu formXuLy = new thongtinphieu_dulieu(maPhieu);
                    formXuLy.FormClosed += FormXuLy_FormClosed;
                    formXuLy.ShowDialog();
                    MessageBox.Show($"Mở Form xử lý chi tiết cho Phiếu: {maPhieu}", "Thông tin phiếu");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi mở Form chi tiết phiếu: " + ex.Message, "Lỗi");
                }
            }
        }
        private void dgvPhieuYeuCau_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvMuonTra != null && dgvMuonTra.Columns[e.ColumnIndex].HeaderText == "Trạng thái xử lý")
            {
                string trangThai = e.Value?.ToString().Trim();

                if (trangThai == "Đồng ý" || trangThai == "Dong y")
                {
                    e.CellStyle.ForeColor = Color.DarkGreen;
                    e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                }
                else if (trangThai == "Từ chối" || trangThai == "Tu choi" || trangThai == "Cho duyet mat") 
                {
                    e.CellStyle.ForeColor = Color.Red;
                    e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                }
                else 
                {
                    e.CellStyle.ForeColor = Color.Orange;
                }
            }
        }

       
        private void FormXuLy_FormClosed(object sender, FormClosedEventArgs e)
        {
            
            LoadDataPhieuMuon();
        }


        private void btn_TK_Click(object sender, EventArgs e)
        {

            
            string searchTerm = txt_timkiem.Text.Trim();
            LoadDataPhieuMuon(searchTerm);
            MessageBox.Show("Đã thực hiện tìm kiếm.", "Tìm kiếm");
        }
    }
}