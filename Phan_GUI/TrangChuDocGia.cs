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
    public partial class FormTrangChuDocGia : Form
    {
        public FormTrangChuDocGia()
        {
            InitializeComponent();
            // Trang chủ
            menuHideTimer.Tick += new EventHandler(menuHideTimer_Tick);

            // Đảm bảo các Panel con bị ẩn khi Form khởi động
            HideAllMenuPanels();
        }

        private void HideAllMenuPanels()
        {
            // Thay thế bằng TÊN PANEL menu con của bạn
            pnlTrangChu.Visible = false;
            pnlDichVu.Visible = false;
            pnlTaiNguyen.Visible = false;

            // Đảm bảo Timer luôn dừng lại sau khi ẩn menu
            menuHideTimer.Stop();
        }

        private void menuHideTimer_Tick(object sender, EventArgs e)
        {
            // Khi timer chạy hết, nghĩa là chuột đã rời khỏi khu vực menu
            // Tiến hành ẩn tất cả menu con.
            HideAllMenuPanels();
        }
        private void btnMuonTaiLieu_Click(object sender, EventArgs e)
        {
            FormMuonTaiLieu formMuonTaiLieu = new FormMuonTaiLieu();
            formMuonTaiLieu.FormClosed += (s, args) => this.Show();
            formMuonTaiLieu.Show();
            this.Hide();
        }
        private void FormTrangChuDocGia_Load(object sender, EventArgs e)
        {

        }

        private void button13_Click(object sender, EventArgs e)
        {

        }

        private void btnGioiThieu_MouseLeave(object sender, EventArgs e)
        {

        }

        private void btnTrangChu_MouseEnter(object sender, EventArgs e)
        {
            HideAllMenuPanels(); // Ẩn các menu khác trước
            pnlTrangChu.Visible = true; // Hiện menu con Trang Chủ
            menuHideTimer.Stop(); // Dừng timer ẩn
        }

        private void btnTrangChu_MouseLeave(object sender, EventArgs e)
        {
            menuHideTimer.Start(); // Bật timer đếm ngược
        }

        private void pnlTrangChu_MouseEnter(object sender, EventArgs e)
        {
            menuHideTimer.Stop(); // Hủy lệnh ẩn menu khi chuột đi vào Panel con
        }

        private void pnlTrangChu_MouseLeave(object sender, EventArgs e)
        {
            menuHideTimer.Start(); // Bật lại lệnh ẩn menu khi chuột rời khỏi Panel con
        }

        private void btnDichVu_MouseEnter(object sender, EventArgs e)
        {
            HideAllMenuPanels(); // Ẩn các menu khác trước
            pnlDichVu.Visible = true; // Hiện menu con Trang Chủ
            menuHideTimer.Stop(); // Dừng timer ẩn
        }

        private void pnlDichVu_MouseEnter(object sender, EventArgs e)
        {
            menuHideTimer.Stop();
        }

        private void pnlDichVu_MouseLeave(object sender, EventArgs e)
        {
            menuHideTimer.Start();
        }

        private void btnDichVu_MouseLeave(object sender, EventArgs e)
        {
            menuHideTimer.Start();
        }

        private void btnTaiNguyen_MouseEnter(object sender, EventArgs e)
        {
            HideAllMenuPanels(); // Ẩn các menu khác trước
            pnlTaiNguyen.Visible = true; // Hiện menu con Trang Chủ
            menuHideTimer.Stop(); // Dừng timer ẩn
        }

        private void btnTaiNguyen_MouseLeave(object sender, EventArgs e)
        {
            menuHideTimer.Start();
        }

        private void pnlTaiNguyen_MouseEnter(object sender, EventArgs e)
        {
            menuHideTimer.Stop();
        }

        private void pnlTaiNguyen_MouseLeave(object sender, EventArgs e)
        {
            menuHideTimer.Start();
        }

        private void btnThongTinThe_Click(object sender, EventArgs e)
        {
            FormThongTinThe formthongtinthe = new FormThongTinThe();
            formthongtinthe.FormClosed += (s, args) => this.Show();
            formthongtinthe.Show();
            this.Hide();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            FormThongBaoDocGia formThongBaoDocGia = new FormThongBaoDocGia();
            formThongBaoDocGia.FormClosed += (s, args) => this.Show();
            formThongBaoDocGia.Show();
        }

        private void btnLichSuMuon_Click(object sender, EventArgs e)
        {
            FormLichSuMuon formLichSuMuon = new FormLichSuMuon();
            formLichSuMuon.FormClosed += (s, args) => this.Show();
            formLichSuMuon.Show();
            this.Hide();
        }

        private void btnMuonPhongHoc_Click(object sender, EventArgs e)
        {
            FormMuonPhong formMuonPhong = new FormMuonPhong();
            formMuonPhong.FormClosed += (s, args) => this.Show();
            formMuonPhong.Show();
            this.Hide();
        }

        private void btnTraTaiLieu_Click(object sender, EventArgs e)
        {
            FormTraTaiLieu formTraTaiLieu = new FormTraTaiLieu();
            formTraTaiLieu.FormClosed += (s, args) => this.Show();
            formTraTaiLieu.Show();
            this.Hide();
        }

        private void btnGiaHanTaiLieu_Click(object sender, EventArgs e)
        {
            FormGiaHanTaiLieu formGiaHanTaiLieu = new FormGiaHanTaiLieu();
            formGiaHanTaiLieu.FormClosed += (s, args) => this.Show();
            formGiaHanTaiLieu.Show();
            this.Hide();
        }

        private void btnTraCuuTaiLieu_Click(object sender, EventArgs e)
        {

        }

        private void btnTraCuuPhongHoc_Click(object sender, EventArgs e)
        {

        }
    }
}
