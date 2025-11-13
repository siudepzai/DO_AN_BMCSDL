using _40_caesarOracle;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace DO_AN_BMCSDL.Phan_GUI
{
    public partial class THONGTINVIPHAM : Form
    {
        private string _maPhieuPhat;

        public THONGTINVIPHAM()
        {
            InitializeComponent();
        }

        public THONGTINVIPHAM(string maPhieuPhat)
        {
            InitializeComponent();
            _maPhieuPhat = maPhieuPhat;
        }

     
        private T FindControl<T>(string name) where T : Control
        {
            Control[] controls = this.Controls.Find(name, true);
            return controls.FirstOrDefault(c => c is T) as T;
        }

        private void THONGTINVIPHAM_Load(object sender, EventArgs e)
        {
            LoadChiTietViPham();
        }

        private void LoadChiTietViPham()
        {
            if (string.IsNullOrEmpty(_maPhieuPhat)) return;

            string sql = @"
                SELECT 
                    T1.LYDOPHAT AS LyDo,
                    T1.SOLAN AS SoLanViPham,
                    T1.LYDO AS HinhPhatText, 
                    T1.NGTAO AS ThoiGianTre, 
                    T3.MATHANHVIEN AS MaDocGia,
                    T3.TENTV AS TenDocGia,
                    T3.VAITRO AS VaiTro
                FROM PHIEUPHAT T1
                JOIN PHIEUMUON T2 ON T1.MAPHIEUMUON = T2.MAPHIEUMUON
                JOIN THEBANDOC T4 ON T2.MASOTHE = T4.MASOTHE
                JOIN DOCGIA T3 ON T4.MATHANHVIEN = T3.MATHANHVIEN
                WHERE TRIM(T1.MAPHIEUPHAT) = :maPhat";

            try
            {
                if (Database.Connect())
                {
                    DataTable dt = Database.ExecuteQuery(sql, new OracleParameter("maPhat", _maPhieuPhat));
                    if (dt.Rows.Count > 0)
                    {
                        DataRow row = dt.Rows[0];
                        if (txt_ma != null) txt_ma.Text = row["MaDocGia"].ToString().Trim();
                        if (txt_ten != null) txt_ten.Text = row["TenDocGia"].ToString().Trim();
                        if (txt_vt != null) txt_vt.Text = row["VaiTro"].ToString().Trim();
                        if (txt_ld != null) txt_ld.Text = row["LyDo"].ToString().Trim();

                        if (txt_tgt != null && row["ThoiGianTre"] != DBNull.Value)
                        {
                            txt_tgt.Text = ((DateTime)row["ThoiGianTre"]).ToString("dd/MM/yyyy HH:mm:ss");
                        }

                        if (txt_HP != null) txt_HP.Text = row["HinhPhatText"].ToString().Trim();
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy dữ liệu vi phạm cho phiếu này.", "Lỗi");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải chi tiết vi phạm: " + ex.Message, "Lỗi SQL");
            }
            finally
            {
                Database.Close();
            }
        }
    }
}