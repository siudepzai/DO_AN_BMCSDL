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
            // Lấy thông tin từ controls
            string username = txtTenDangNhap.Text.Trim();
            string password = txtMatKhau.Text; // Password không cần Trim nếu bạn không Trim khi lưu
            string selectedRole = cboLoaiNguoiDung.SelectedItem?.ToString();
            string actualRole = string.Empty;

            // 1. Kiểm tra dữ liệu nhập đầy đủ
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(selectedRole))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ Tên đăng nhập, Mật khẩu và chọn Loại người dùng.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // 2. Gọi hàm Login từ lớp Data
                // Cần thêm using để gọi đến namespace của class Data
                

                if (DangNhap.Login(username, password, selectedRole, out actualRole))
                {
                    MessageBox.Show($"Đăng nhập thành công với vai trò: {actualRole}", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // 3. Xử lý chuyển Form dựa trên vai trò thực tế
                    Form nextForm = null;

                    username = txtTenDangNhap.Text;

                    if (selectedRole == "Độc giả")
                    {
                        // Giả định bạn có FormTrangChuDocGia
                        // nextForm = new FormTrangChuDocGia(); 
                        // Sử dụng FormTrangChuDocGia theo code cũ của bạn
                        nextForm = new FormTrangChuDocGia(username);
                    }
                    else if (selectedRole == "Thủ thư")
                    {
                        // Giả định bạn có FormTrangChuThuThu cho vai trò "Nhan vien"
                        // Bạn cần tự tạo Form này
                        // nextForm = new FormTrangChuThuThu();
                        nextForm = new thu_thu();
                        nextForm.FormClosed += (s, args) => this.Show();
                        this.Hide();
                        nextForm.Show();
                        // Tạm thời hiển thị thông báo nếu chưa có Form
                        MessageBox.Show("Đã đăng nhập vai trò Thủ thư. Cần mở Form Trang Chủ Thủ Thư (chưa có).", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    if (nextForm != null)
                    {
                        // Xử lý ẩn Form hiện tại và mở Form mới
                        nextForm.FormClosed += (s, args) => this.Show();
                        this.Hide();
                        nextForm.Show();
                    }
                }
                else
                {
                    MessageBox.Show("Tên đăng nhập, Mật khẩu hoặc Loại người dùng không chính xác.", "Lỗi đăng nhập", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                // Xử lý lỗi kết nối từ CreateOpenConnection()
                MessageBox.Show("Lỗi hệ thống: " + ex.Message, "Lỗi Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            //FormTrangChuDocGia formtrangchu = new FormTrangChuDocGia();
            //formtrangchu.FormClosed += (s, args) => this.Show();
            //this.Hide();
            //formtrangchu.Show();
        }

        private void lnkDangKy_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FormDangKyTheMoi formDangKyTheMoi = new FormDangKyTheMoi();
            formDangKyTheMoi.FormClosed += (s, args) => this.Show();
            formDangKyTheMoi.Show();
            this.Hide();
        }

        private void txtTenDangNhap_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
