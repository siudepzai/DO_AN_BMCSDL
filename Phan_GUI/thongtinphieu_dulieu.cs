using _40_caesarOracle;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DO_AN_BMCSDL.Phan_GUI
{
    public partial class thongtinphieu_dulieu : Form
    {
        private string _maPhieu;
        private Label lblTrangThaiXuLy;

        public thongtinphieu_dulieu()
        {
            InitializeComponent();
        }

      
        public thongtinphieu_dulieu(string maPhieu)
        {
            InitializeComponent();
            _maPhieu = maPhieu; 

            if (btn_dongy != null)
            {
                btn_dongy.Click -= btnDongY_Click;
                btn_dongy.Click += btnDongY_Click;
            }
            if (btn_tuchoi != null)
            {
                btn_tuchoi.Click -= btnTuChoi_Click;
                btn_tuchoi.Click += btnTuChoi_Click;
            }
        }

        private void thongtinphieu_dulieu_Load(object sender, EventArgs e)
        {
            LoadChiTietPhieuMuon();
        }

      
        private void LoadChiTietPhieuMuon()
        {
          
            string sql = @"
                SELECT 
                    T1.MAPHIEUMUON AS MaPhieu,
                    T2.MATAILIEU AS MaTaiLieu,
                    T3.TENSACH AS TenTaiLieu,
                    T4.MATHANHVIEN AS MaDocGia,
                    T5.TENTV AS TenDocGia,
                    T5.VAITRO AS VaiTro,
                    T2.HIENTRANG AS TrangThaiXuLy, -- Dùng HIENTRANG làm trạng thái xử lý
                    T1.NGAYMUON AS NgayMuon,
                    T1.NGAYTRA AS NgayTra,
                    'Yeu cau' AS YeuCau -- Cần xác định yêu cầu thực tế
                FROM PHIEUMUON T1
                JOIN CHITIETPHIEUMUON T2 ON T1.MAPHIEUMUON = T2.MAPHIEUMUON
                JOIN TAILIEU T3 ON T2.MATAILIEU = T3.MATAILIEU
                JOIN THEBANDOC T4 ON T1.MASOTHE = T4.MASOTHE
                JOIN DOCGIA T5 ON T4.MATHANHVIEN = T5.MATHANHVIEN
                WHERE T1.MAPHIEUMUON = :maPhieu";

            try
            {
                if (Database.Connect())
                {
                    DataTable dt = Database.ExecuteQuery(sql, new OracleParameter("maPhieu", _maPhieu));

                    if (dt.Rows.Count > 0)
                    {
                        DataRow row = dt.Rows[0];
                        string trangThai = row["TrangThaiXuLy"].ToString().Trim();

                      
                        if (txtMaPhieu != null) txtMaPhieu.Text = row["MaPhieu"].ToString();
                        if (txtYeuCau != null) txtYeuCau.Text = row["YeuCau"].ToString();
                        if (txtMaTL != null) txtMaTL.Text = row["MaTaiLieu"].ToString();
                        if (txtTenTL != null) txtTenTL.Text = row["TenTaiLieu"].ToString();
                       
                        if (txtMaDocGia != null) txtMaDocGia.Text = row["MaDocGia"].ToString();
                        if (txtTenDocGia != null) txtTenDocGia.Text = row["TenDocGia"].ToString();
                        if (txtVaiTro != null) txtVaiTro.Text = row["VaiTro"].ToString();

                        if (txt_TGMuon != null) txt_TGMuon.Text = ((DateTime)row["NgayMuon"]).ToString("dd/MM/yyyy HH:mm");
                        if (txt_thoigiantra != null) txt_thoigiantra.Text = ((DateTime)row["NgayTra"]).ToString("dd/MM/yyyy HH:mm");

                        if (lblTrangThaiXuLy != null) lblTrangThaiXuLy.Text = trangThai;


                        if (trangThai == "Dong y" || trangThai == "Tu choi" || trangThai == "Cho duyet mat") 
                        {
                            if (btn_dongy != null) btn_dongy.Visible = false;
                            if (btn_tuchoi != null) btn_tuchoi.Visible = false;

                            
                            this.BackColor = (trangThai == "Dong y") ? Color.Green : (trangThai == "Tu choi" ? Color.Red : this.BackColor);
                        }
                        else
                        {
                            if (btn_dongy != null) btn_dongy.Visible = true;
                            if (btn_tuchoi != null) btn_tuchoi.Visible = true;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy thông tin phiếu.", "Lỗi dữ liệu");
                        this.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải chi tiết phiếu: " + ex.Message, "Lỗi SQL");
            }
            finally
            {
                Database.Close();
            }
        }

     
        private void CapNhatTrangThai(string trangThaiMoi)
        {
            
            string sql = "UPDATE CHITIETPHIEUMUON SET HIENTRANG = :trangThai WHERE MAPHIEUMUON = :maPhieu";

            try
            {
                if (Database.Connect())
                {
                    int rowsAffected = Database.ExecuteNonQuery(sql,
                        new OracleParameter("trangThai", trangThaiMoi),
                        new OracleParameter("maPhieu", _maPhieu));

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show($"Đã cập nhật trạng thái phiếu {_maPhieu} thành '{trangThaiMoi}'.", "Thành công");
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Cập nhật trạng thái không thành công.", "Lỗi");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi cập nhật trạng thái: " + ex.Message, "Lỗi SQL");
            }
            finally
            {
                Database.Close();
            }
        }

        private void btnDongY_Click(object sender, EventArgs e)
        {
            CapNhatTrangThai("Dong y");
        }

        private void btnTuChoi_Click(object sender, EventArgs e)
        {
            CapNhatTrangThai("Tu choi");
        }

        private void btn_X_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}