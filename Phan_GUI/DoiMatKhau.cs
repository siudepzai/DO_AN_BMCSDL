using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DO_AN_BMCSDL.Phan_xu_ly;

namespace DO_AN_BMCSDL.Phan_GUI
{
    public partial class FormDoiMatKhau : Form
    {
        public FormDoiMatKhau()
        {
            InitializeComponent();
        }

        private void btnHienMatKhau_Click(object sender, EventArgs e)
        {
            if (txtMatKhauCu.PasswordChar == '*')
            {
                txtMatKhauCu.PasswordChar = '\0';
            }
            else
            {
                txtMatKhauCu.PasswordChar = '*';
            }
        }

        private void btnHienMatKhau1_Click(object sender, EventArgs e)
        {
            if (txtMatKhauMoi.PasswordChar == '*')
            {
                txtMatKhauMoi.PasswordChar = '\0';
            }
            else
            {
                txtMatKhauMoi.PasswordChar = '*';
            }
        }

        private void btnDoiMatKhau_Click(object sender, EventArgs e)
        {
            lblDoiMatKhauThanhCong.Visible = false;

            string taiKhoan = txtThongTinTaiKhoan.Text.Trim();
            string matKhauCu = txtMatKhauCu.Text;
            string matKhauMoi = txtMatKhauMoi.Text;

            if (string.IsNullOrWhiteSpace(taiKhoan) || string.IsNullOrWhiteSpace(matKhauCu) || string.IsNullOrWhiteSpace(matKhauMoi))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ Tài khoản, Mật khẩu cũ và Mật khẩu mới.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (matKhauCu == matKhauMoi)
            {
                MessageBox.Show("Mật khẩu mới không được trùng với mật khẩu cũ.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string result = DoiMatKhau.ChangePassword(taiKhoan, matKhauCu, matKhauMoi);

                if (result.StartsWith("SUCCESS"))
                {
                    lblDoiMatKhauThanhCong.Text = "Đổi mật khẩu thành công";
                    lblDoiMatKhauThanhCong.Visible = true;

                    txtMatKhauCu.Clear();
                    txtMatKhauMoi.Clear();
                }
                else if (result.StartsWith("INVALID_CREDENTIALS"))
                {
                    MessageBox.Show("Mật khẩu cũ không chính xác. Vui lòng kiểm tra lại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (result.StartsWith("USER_NOT_FOUND"))
                {
                    MessageBox.Show("Tài khoản không tồn tại. Vui lòng kiểm tra lại tên tài khoản.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show("Lỗi hệ thống khi đổi mật khẩu. Chi tiết: " + result, "Lỗi Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối hoặc xử lý: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lnkQuayLaiDangNhap_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FormDangNhap formDangNhap = new FormDangNhap();
            formDangNhap.Show();
            this.Hide();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtThongTinTaiKhoan_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtMatKhauCu_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtMatKhauMoi_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
