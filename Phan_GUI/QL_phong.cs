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

                LoadDataPhongHoc();
            }
        }

        private void LoadDataPhongHoc(string searchTerm = "")
        {
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
                MessageBox.Show("Lỗi khi tải dữ liệu Phiếu Yêu Cầu Phòng:\n" + ex.Message, "Lỗi SQL");
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

                    Thongtinphieu_phong formXuLy = new Thongtinphieu_phong(maPhieu);

                    formXuLy.FormClosed += FormXuLy_FormClosed;

                    formXuLy.ShowDialog();
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
                else if (trangThai == "Ket thuc muon") 
                {
                    e.CellStyle.ForeColor = Color.Blue;
                    e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                }
                else { 
                    e.CellStyle.ForeColor = Color.Orange;
                }
            }
        }

       
        private void FormXuLy_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (sender is Thongtinphieu_phong form && form.DialogResult == DialogResult.OK)
            {
                LoadDataPhongHoc(); 
            }

            if (sender is Thongtinphieu_phong formToUnsubscribe)
            {
                formToUnsubscribe.FormClosed -= FormXuLy_FormClosed;
            }
        }
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
            LoadDataPhongHoc();
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}