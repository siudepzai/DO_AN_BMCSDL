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
    public partial class Thongtinphieu_phong : Form
    {
       
        private string _maPhieu;
        private Label lblTrangThaiXuLy; 

        public Thongtinphieu_phong()
        {
            InitializeComponent();
        }

      
        public Thongtinphieu_phong(string maPhieu)
        {
            InitializeComponent();
            _maPhieu = maPhieu;

          
            lblTrangThaiXuLy = this.Controls.Find("lbl_trangthaiphieu_control_name", true).FirstOrDefault() as Label;

            
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

        private void Thongtinphieu_phong_Load(object sender, EventArgs e)
        {
            LoadChiTietPhieu();
        }

        private void LoadChiTietPhieu()
        {
            string sql = @"
        SELECT 
            T1.MADATPHONG AS MaPhieu,
            T1.YEUCAU AS YeuCau,
            T1.TRANGTHAI AS TrangThaiXuLy,
            -- T1.SOLANGIAHAN AS SoLanGiaHan, <-- Bị xóa
            T1.THOIGIANBATDAU AS ThoiGianMuon, 
            T1.THOIGIANKETTHUC AS ThoiGianTra,
            
            T2.MATHANHVIEN AS MaDocGia,
            
            T4.TENTV AS TenDocGia,      
            T4.VAITRO AS VaiTro,
            T4.KHOAHOC AS Khoa,         
            T4.TENTRUONG AS Lop,        
            
            T3.TENPHONG AS TenPhong,
            T3.MAPHONG AS MaPhong,
            T3.TRANGBI AS ViTriPhong,  -- 🛠️ SỬA: Lấy TRANGBI làm Vị trí/Trang bị phòng (Vì PHONGHOC không có cột VITRI)
            T3.TRANGTHAI AS TrangThaiPhong
            
        FROM DATPHONG T1
        JOIN THEBANDOC T2 ON T1.MASOTHE = T2.MASOTHE
        JOIN PHONGHOC T3 ON T1.MAPHONG = T3.MAPHONG
        JOIN DOCGIA T4 ON T2.MATHANHVIEN = T4.MATHANHVIEN 
        WHERE T1.MADATPHONG = :maPhieu";

            try
            {
                if (Database.Connect())
                {
                    DataTable dt = Database.ExecuteQuery(sql, new OracleParameter("maPhieu", _maPhieu));

                    if (dt.Rows.Count > 0)
                    {
                        DataRow row = dt.Rows[0];
                        string trangThai = row["TrangThaiXuLy"].ToString().Trim();

                        txtMaPhieu.Text = row["MaPhieu"].ToString();
                        txtYeuCau.Text = row["YeuCau"].ToString();

                        txtSoLanGiaHang.Text = "";

                        txt_TGMuon.Text = ((DateTime)row["ThoiGianMuon"]).ToString("dd/MM/yyyy HH:mm");
                        textBox14.Text = ((DateTime)row["ThoiGianTra"]).ToString("dd/MM/yyyy HH:mm");

                        txtMaDocGia.Text = row["MaDocGia"].ToString();
                        txtTenDocGia.Text = row["TenDocGia"].ToString();
                        txtVaiTro.Text = row["VaiTro"].ToString();
                        txtKhoa.Text = row["Khoa"].ToString();
                        txtLop.Text = row["Lop"].ToString();

                        txtMaPhong.Text = row["MaPhong"].ToString();
                        txtTenPhong.Text = row["TenPhong"].ToString();
                        
                        txtTrangThai.Text = row["TrangThaiPhong"].ToString();

                        if (lblTrangThaiXuLy != null) lblTrangThaiXuLy.Text = trangThai;

                        if (trangThai == "Dong y" || trangThai == "Tu choi")
                        {
                            btn_dongy.Visible = false;
                            btn_tuchoi.Visible = false;
                            BackColor = (trangThai == "Dong y") ? Color.Green : (trangThai == "Tu choi" ? Color.Red : Color.LightGray);
                            
                        }
                        else
                        {
                            btn_dongy.Visible = true;
                            btn_tuchoi.Visible = true;
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
        private void btnDongY_Click(object sender, EventArgs e)
        {
            CapNhatTrangThai("Dong y");
        }
        private void btnTuChoi_Click(object sender, EventArgs e)
        {
            CapNhatTrangThai("Tu choi");
        }

        private void CapNhatTrangThai(string trangThaiMoi)
        {
            string sql = "UPDATE DATPHONG SET TRANGTHAI = :trangThai WHERE MADATPHONG = :maPhieu";

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

        private void btn_X_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}