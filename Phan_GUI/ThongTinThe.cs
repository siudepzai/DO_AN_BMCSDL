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
    public partial class FormThongTinThe : Form
    {
        private string _tenTaiKhoan;
        public FormThongTinThe(string tenTaiKhoan)
        {
            InitializeComponent();
            _tenTaiKhoan = tenTaiKhoan;
            LoadDocGiaData();
        }

        public FormThongTinThe()
        {
            InitializeComponent();
        }

        private void LoadDocGiaData()
        {
            try
            {
                ThongTinThe.DocGiaInfo docGia = ThongTinThe.GetDocGiaInfo(_tenTaiKhoan);

                if (docGia != null)
                {
                    textBox1.Text = docGia.MaThanhVien;
                    textBox2.Text = docGia.TenThanhVien;
                    textBox3.Text = docGia.NgaySinh?.ToShortDateString(); textBox4.Text = docGia.Email;
                    textBox5.Text = docGia.NgheNghiep;
                    textBox6.Text = docGia.GioiTinh;
                    textBox7.Text = docGia.KhoaHoc;
                    textBox8.Text = docGia.DiaChi;

                    textBox9.Text = docGia.MaSoThe;
                    textBox10.Text = docGia.TinhTrangThe;
                    textBox11.Text = docGia.VaiTro;
                    textBox12.Text = docGia.HanSuDung?.ToShortDateString();
                }
                else
                {
                    MessageBox.Show("Không tìm thấy thông tin độc giả này.", "Lỗi dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải thông tin độc giả: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void lnkTroLai_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Close();
        }

        private void btnDangKyCapLaiThe_Click(object sender, EventArgs e)
        {
            FormDangKyCapLaiThe formDangKyCapLaiThe = new FormDangKyCapLaiThe();
            formDangKyCapLaiThe.FormClosed += (s, args) => this.Show();
            formDangKyCapLaiThe.Show();
            this.Hide();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnCapNhatThongTin_Click(object sender, EventArgs e)
        {

        }
    }
}
