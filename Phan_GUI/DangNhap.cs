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
    public partial class FormDangNhap : Form
    {
        public FormDangNhap()
        {
            InitializeComponent();
        }

        private void FormDangNhap_Load(object sender, EventArgs e)
        {

        }

        private void cboLoaiNguoiDung_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedItem = cboLoaiNguoiDung.SelectedItem.ToString();

            if (selectedItem == "Độc giả")
            {
                // 1. Nếu là Độc giả: Cần thấy các lựa chọn đăng ký
                label3.Visible = true;
                lnkDangKy.Visible = true;
            }
            else if (selectedItem == "Thủ thư")
            {
                // 2. Nếu là Thủ thư: Không cần thấy các lựa chọn đăng ký
                label3.Visible = false;
                lnkDangKy.Visible = false;
            }
            else if (selectedItem == "Admin")
            {
                // 2. Nếu là Thủ thư: Không cần thấy các lựa chọn đăng ký
                label3.Visible = false;
                lnkDangKy.Visible = false;
            }
        }

        private void txtMatKhau_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void chkHienThiMatKhau_CheckedChanged(object sender, EventArgs e)
        {
            if (chkHienThiMatKhau.Checked)
            {
                txtMatKhau.PasswordChar = '\0';
            }
            else
            {
                txtMatKhau.PasswordChar = '*';
            }
        }

        private void lnkQuenMatKhau_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FormDoiMatKhau formDoiMatKhau = new FormDoiMatKhau();
            formDoiMatKhau.FormClosed += (s, args) => this.Show();
            formDoiMatKhau.Show();
            this.Hide();
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            FormTrangChuDocGia formtrangchu = new FormTrangChuDocGia();
            formtrangchu.FormClosed += (s, args) => this.Show();
            this.Hide();
            formtrangchu.Show();
        }

        private void lnkDangKy_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FormDangKyTheMoi formDangKyTheMoi = new FormDangKyTheMoi();
            formDangKyTheMoi.FormClosed += (s, args) => this.Show();
            formDangKyTheMoi.Show();
            this.Hide();
        }
    }
}
