using _40_caesarOracle;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace DO_AN_BMCSDL.Phan_GUI
{
    public partial class timtailieu : Form
    {
        

        public timtailieu()
        {
            InitializeComponent();
        }

        private void btn_thoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private T FindControl<T>(string name) where T : Control
        {
            if (name == "txtTimKiem" && this.Controls.Find("txt_timkiem", true).FirstOrDefault() is TextBox t)
            {
                return t as T;
            }
            Control[] controls = this.Controls.Find(name, true);
            return controls.FirstOrDefault(c => c is T) as T;
        }
        private void LoadDataTaiLieu(string searchTerm = "")
        {
            string sql = @"
                SELECT
                    ROWNUM AS STT,
                    TRIM(MATAILIEU) AS ""Mã tài liệu"",
                    TENSACH AS ""Tên tài liệu"",
                    NGONNGU AS ""Ngôn ngữ"",
                    PHIMUON AS ""Chi phí"",
                    TENTACGIA AS ""Tác giả"",
                    THELOAI AS ""Thể loại"",
                    TINHTRANG AS ""Tình trạng""
                FROM TAILIEU
                WHERE (LOWER(TRIM(MATAILIEU)) LIKE '%' || :searchTerm || '%' OR
                       LOWER(TENSACH) LIKE '%' || :searchTerm || '%' OR
                       LOWER(TENTACGIA) LIKE '%' || :searchTerm || '%')
                ORDER BY STT ASC";

            try
            {
                if (Database.Connect())
                {
                    OracleParameter param = new OracleParameter("searchTerm", searchTerm.ToLower());
                    DataTable dt = Database.ExecuteQuery(sql, param);

                    dgvTaiLieu.DataSource = dt;
                    if (dgvTaiLieu.Columns.Contains("Chi phí"))
                    {
                        dgvTaiLieu.Columns["Chi phí"].DefaultCellStyle.Format = "N0";
                        dgvTaiLieu.Columns["Chi phí"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    }
                    foreach (DataGridViewColumn col in dgvTaiLieu.Columns)
                    {
                        col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu Tài liệu:\n" + ex.Message, "Lỗi SQL");
            }
            finally
            {
                Database.Close();
            }
        }
        private void btn_TK_Click(object sender, EventArgs e)
        {
            if (txt_timkiem == null)
            {
                MessageBox.Show("Lỗi: Không tìm thấy TextBox tìm kiếm. Vui lòng kiểm tra tên control trong Designer.", "Lỗi hệ thống");
                return;
            }

            string searchTerm = txt_timkiem.Text.Trim();

            LoadDataTaiLieu(searchTerm);
        }

        private void timtailieu_Load_1(object sender, EventArgs e)
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

            txt_timkiem = FindControl<TextBox>("txtTimKiem");
            if (dgvTaiLieu != null) 
            {
                dgvTaiLieu.Font = new Font("Times New Roman", 12, FontStyle.Regular);
                dgvTaiLieu.ColumnHeadersDefaultCellStyle.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                dgvTaiLieu.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                LoadDataTaiLieu();
            }
        }
    }
}