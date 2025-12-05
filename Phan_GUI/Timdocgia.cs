using _40_caesarOracle;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace DO_AN_BMCSDL.Phan_GUI
{
    public partial class Timdocgia : Form
    {

        public Timdocgia()
        {
            InitializeComponent();
        }

        private void btn_thoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Timdocgia_Load(object sender, EventArgs e)
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

            if (dgvDocGia != null)
            {
                dgvDocGia.Font = new Font("Times New Roman", 12, FontStyle.Regular);
                dgvDocGia.ColumnHeadersDefaultCellStyle.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                dgvDocGia.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                LoadDataDocGia();
            }
        }

        private void LoadDataDocGia(string searchTerm = "")
        {
            string sql = @"
        SELECT
            ROWNUM AS STT,
            TRIM(T1.MASOTHE) AS ""Ma so the"",
            TRIM(T2.TENTV) AS ""Ho ten"",
            T2.NGSINH AS ""Ngay sinh"",
            T2.KHOAHOC AS ""Nien khoa"",
            TRIM(T2.DIACHI) AS ""Dia chi"",
            TRIM(T2.SODIENTHOAI) AS ""So dien thoai"",
            TRIM(T2.MATHANHVIEN) AS ""Ma thanh vien""
        FROM THEBANDOC T1
        JOIN DOCGIA T2 ON T1.MATHANHVIEN = T2.MATHANHVIEN
        WHERE (LOWER(TRIM(T1.MASOTHE)) LIKE '%' || :searchTerm || '%' OR
               LOWER(TRIM(T2.TENTV)) LIKE '%' || :searchTerm || '%' OR
               LOWER(TRIM(T2.MATHANHVIEN)) LIKE '%' || :searchTerm || '%')
        
        -- 🛠️ THAY ĐỔI: Sắp xếp theo Ma so the để ROWNUM ổn định, sau đó sắp xếp theo ROWNUM (STT)
        ORDER BY ""Ma so the"", STT ASC";

            try
            {
                if (Database.Connect())
                {
                    OracleParameter param = new OracleParameter("searchTerm", searchTerm.ToLower());
                    DataTable dt = Database.ExecuteQuery(sql, param);
                    dgvDocGia.DataSource = dt;

                    if (dgvDocGia.Columns.Contains("Ma thanh vien"))
                    {
                        dgvDocGia.Columns["Ma thanh vien"].Visible = false;
                    }
                    if (dgvDocGia.Columns.Contains("Ngay sinh"))
                    {
                        dgvDocGia.Columns["Ngay sinh"].DefaultCellStyle.Format = "dd/MM/yyyy";
                    }
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

        private void btn_TK_Click(object sender, EventArgs e)
        {
            if (txt_timkiem == null)
            {
                MessageBox.Show("Lỗi: Không tìm thấy TextBox tìm kiếm (txtTimKiem).", "Lỗi hệ thống");
                return;
            }

            string searchTerm = txt_timkiem.Text.Trim();

            LoadDataDocGia(searchTerm);
        }
    }
}