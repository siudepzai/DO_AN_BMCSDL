using _40_caesarOracle;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace DO_AN_BMCSDL.Phan_GUI
{
    public partial class xulyvipham : Form
    {

        public xulyvipham()
        {
            InitializeComponent();
        }

        private void btn_thoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void xulyvipham_Load(object sender, EventArgs e)
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
            if (dgv_thongtinvipham != null)
            {
                dgv_thongtinvipham.Font = new Font("Times New Roman", 12, FontStyle.Regular);
                dgv_thongtinvipham.ColumnHeadersDefaultCellStyle.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                dgv_thongtinvipham.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgv_thongtinvipham.CellDoubleClick -= dgv_thongtinvipham_CellDoubleClick; 
                dgv_thongtinvipham.CellDoubleClick += dgv_thongtinvipham_CellDoubleClick;

                LoadDataViPham();
            }
        }
        private void LoadDataViPham()
        {
            string sql = @"
                SELECT 
                    ROWNUM AS STT, 
                    TRIM(T1.MAPHIEUPHAT) AS ""Ma phieu phat"", -- Dùng tên không dấu
                    TRIM(T3.MATHANHVIEN) AS ""Ma doc gia"",    -- Dùng tên không dấu
                    TRIM(T3.TENTV) AS ""Ten doc gia"",        -- Dùng tên không dấu
                    TRIM(T1.LYDOPHAT) AS ""Ly do vi pham"",   -- Dùng tên không dấu
                    T1.SOLAN AS ""So lan vi pham"",
                    T1.NGTAO AS ""Thoi gian tre""
                FROM PHIEUPHAT T1
                JOIN PHIEUMUON T2 ON T1.MAPHIEUMUON = T2.MAPHIEUMUON
                JOIN THEBANDOC T4 ON T2.MASOTHE = T4.MASOTHE
                JOIN DOCGIA T3 ON T4.MATHANHVIEN = T3.MATHANHVIEN
                ORDER BY T1.NGTAO DESC";

            try
            {
                if (Database.Connect())
                {
                    DataTable dt = Database.ExecuteQuery(sql);
                    dgv_thongtinvipham.DataSource = dt;

                    if (dgv_thongtinvipham.Columns.Contains("Ma phieu phat"))
                    {
                        dgv_thongtinvipham.Columns["Ma phieu phat"].Visible = false;
                    }
                }
            }
            finally
            {
                Database.Close();
            }
        }

        private void btn_thongtin_Click(object sender, EventArgs e)
        {
            if (dgv_thongtinvipham.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn một vi phạm để xem thông tin.", "Thông báo");
                return;
            }

            try
            {
                string maPhieuPhat = dgv_thongtinvipham.CurrentRow.Cells["Ma phieu phat"].Value.ToString().Trim();

                OpenThongTinViPhamForm(maPhieuPhat);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi mở Form chi tiết: " + ex.Message, "Lỗi");
            }
        }

        private void OpenThongTinViPhamForm(string maPhieuPhat)
        {
            try
            {
                THONGTINVIPHAM thongtinvipham = new THONGTINVIPHAM(maPhieuPhat);

                if (thongtinvipham.ShowDialog() == DialogResult.OK)
                {
                    LoadDataViPham();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi mở Form thông tin vi phạm: " + ex.Message, "Lỗi");
            }
        }
        private void dgv_thongtinvipham_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                string maPhieuPhat = dgv_thongtinvipham.Rows[e.RowIndex].Cells["Ma phieu phat"].Value.ToString().Trim();

                OpenThongTinViPhamForm(maPhieuPhat);
            }
        }
        private void HandleDelete(string maPhieuPhat)
        {
           
        }

        private void btn_xoa_Click_1(object sender, EventArgs e)
        {
            if (dgv_thongtinvipham.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn vi phạm để xóa.", "Thông báo");
                return;
            }

            
            string maPhieuPhat = dgv_thongtinvipham.CurrentRow.Cells["Ma phieu phat"].Value.ToString().Trim();
            string maDocGia = dgv_thongtinvipham.CurrentRow.Cells["Ma doc gia"].Value.ToString().Trim(); 

            DialogResult confirm = MessageBox.Show($"Xác nhận xóa phiếu phạt {maPhieuPhat} của độc giả {maDocGia}?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (confirm == DialogResult.Yes)
            {
                HandleDelete(maPhieuPhat);
            }
        }
    }
}