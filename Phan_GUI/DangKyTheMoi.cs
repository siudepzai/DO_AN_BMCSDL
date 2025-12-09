using DO_AN_BMCSDL.Phan_xu_ly;
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
    public partial class FormDangKyTheMoi : Form
    {
        public FormDangKyTheMoi()
        {
            InitializeComponent();
            lblDangKyThanhCong.Visible = false;
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDangKyThe_Click(object sender, EventArgs e)
        {
            string hoTen = txtHoTen.Text.Trim();
            DateTime ngaySinh = dateTimePicker1.Value.Date;
            string gioiTinh = radNam.Checked ? "Nam" : (radNu.Checked ? "Nu" : null);
            string taiKhoan = txtTaiKhoan.Text.Trim();
            string matKhau = txtMatKhau.Text;
            string vaiTro = cboVaiTro.SelectedItem?.ToString() ?? "Doc gia";
            string ngheNghiep = cboNgheNghiep.SelectedItem?.ToString() ?? "Students";
            string diaChi = txtDiaChi.Text.Trim();
            string khoaHoc = txtKhoaHoc.Text.Trim();
            string email = txtEmail.Text.Trim();
            string sdt = txtDienThoai.Text.Trim();
            string ghiChu = txtGhiChu.Text.Trim();

            if (string.IsNullOrEmpty(hoTen) || string.IsNullOrEmpty(taiKhoan) || string.IsNullOrEmpty(matKhau) || gioiTinh == null)
            {
                MessageBox.Show("Vui lòng điền đầy đủ Họ tên, Tài khoản, Mật khẩu và Giới tính.", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                DangKyTheMoi.DangKyDocGiaVaThe(hoTen, ngaySinh, gioiTinh, ngheNghiep, vaiTro, diaChi, khoaHoc, email, sdt, ghiChu, taiKhoan, matKhau);


                lblDangKyThanhCong.Visible = true;

            }
            catch (Exception ex)
            {
                lblDangKyThanhCong.Text = "Đăng ký thất bại!";
                lblDangKyThanhCong.BackColor = Color.Red;
                lblDangKyThanhCong.Visible = true;
                MessageBox.Show(ex.Message, "Lỗi Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lblDangKyThanhCong_Click(object sender, EventArgs e)
        {

        }

        private void cboVaiTro_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
