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
    public partial class thu_thu : Form
    {
        public thu_thu()
        {
            InitializeComponent();

            // Gán sự kiện (Nếu bạn chưa gán trong Designer)
            // Đảm bảo các sự kiện này được gán trong Designer, nếu không hãy mở Designer lên và gán.

            // Hệ Thống
            btn_hethong.MouseEnter += new EventHandler(btn_hethong_MouseEnter);
            btn_hethong.MouseLeave += new EventHandler(btn_hethong_MouseLeave);
            pnl_hethongmenu.MouseEnter += new EventHandler(pnl_hethongmenu_MouseEnter);
            pnl_hethongmenu.MouseLeave += new EventHandler(pnl_hethongmenu_MouseLeave);

            // Danh Mục
            btn_danhmuc.MouseEnter += new EventHandler(btn_danhmuc_MouseEnter);
            btn_danhmuc.MouseLeave += new EventHandler(btn_danhmuc_MouseLeave);
            panel4.MouseEnter += new EventHandler(panel4_MouseEnter);
            panel4.MouseLeave += new EventHandler(panel4_MouseLeave);

            // Mượn-Trả
            btn_muontra.MouseEnter += new EventHandler(btn_muontra_MouseEnter);
            btn_muontra.MouseLeave += new EventHandler(btn_muontra_MouseLeave);
            panel5.MouseEnter += new EventHandler(panel5_MouseEnter);
            panel5.MouseLeave += new EventHandler(panel5_MouseLeave);

            // Tìm Kiếm
            btn_timkiem.MouseEnter += new EventHandler(btn_timkiem_MouseEnter);
            btn_timkiem.MouseLeave += new EventHandler(btn_timkiem_MouseLeave);
            panel6.MouseEnter += new EventHandler(panel6_MouseEnter);
            panel6.MouseLeave += new EventHandler(panel6_MouseLeave);

            // Xử lý Timer (Nhớ tạo Timer trong Designer)
            menuHideTimer.Tick += new EventHandler(menuHideTimer_Tick);

            // Ẩn tất cả Panel ngay khi Form tải (chỉ để đảm bảo)
            pnl_hethongmenu.Visible = false;
            panel4.Visible = false;
            panel5.Visible = false;
            panel6.Visible = false;
        }

       

        // Phương thức ẩn tất cả menu (giúp code sạch hơn)
        private void HideAllMenuPanels()
        {
            pnl_hethongmenu.Visible = false;
            panel4.Visible = false;
            panel5.Visible = false;
            panel6.Visible = false;
            menuHideTimer.Stop(); // Dừng timer nếu đang chạy
        }

        // === Logic cho Hệ Thống (Đã có sẵn, chỉ cần bổ sung Timer logic) ===

        private void btn_hethong_MouseEnter(object sender, EventArgs e)
        {
            HideAllMenuPanels(); // Ẩn các menu khác trước
            pnl_hethongmenu.Visible = true;
            menuHideTimer.Stop();
        }
        private void btn_hethong_MouseLeave(object sender, EventArgs e)
        {
            menuHideTimer.Start();
        }
        private void pnl_hethongmenu_MouseEnter(object sender, EventArgs e)
        {
            menuHideTimer.Stop();
        }
        private void pnl_hethongmenu_MouseLeave(object sender, EventArgs e)
        {
            menuHideTimer.Start();
        }

        // === Logic cho Danh Mục ===

        private void btn_danhmuc_MouseEnter(object sender, EventArgs e)
        {
            HideAllMenuPanels();
            panel4.Visible = true;
            menuHideTimer.Stop();
        }
        private void btn_danhmuc_MouseLeave(object sender, EventArgs e)
        {
            menuHideTimer.Start();
        }
        private void panel4_MouseEnter(object sender, EventArgs e)
        {
            menuHideTimer.Stop();
        }
        private void panel4_MouseLeave(object sender, EventArgs e)
        {
            menuHideTimer.Start();
        }

        // === Logic cho Mượn-Trả ===

        private void btn_muontra_MouseEnter(object sender, EventArgs e)
        {
            HideAllMenuPanels();
            panel5.Visible = true;
            menuHideTimer.Stop();
        }
        private void btn_muontra_MouseLeave(object sender, EventArgs e)
        {
            menuHideTimer.Start();
        }
        private void panel5_MouseEnter(object sender, EventArgs e)
        {
            menuHideTimer.Stop();
        }
        private void panel5_MouseLeave(object sender, EventArgs e)
        {
            menuHideTimer.Start();
        }

        // === Logic cho Tìm Kiếm ===

        private void btn_timkiem_MouseEnter(object sender, EventArgs e)
        {
            HideAllMenuPanels();
            panel6.Visible = true;
            menuHideTimer.Stop();
        }
        private void btn_timkiem_MouseLeave(object sender, EventArgs e)
        {
            menuHideTimer.Start();
        }
        private void panel6_MouseEnter(object sender, EventArgs e)
        {
            menuHideTimer.Stop();
        }
        private void panel6_MouseLeave(object sender, EventArgs e)
        {
            menuHideTimer.Start();
        }

        // === Sự kiện Timer (Ẩn tất cả menu) ===

        private void menuHideTimer_Tick(object sender, EventArgs e)
        {
            // Khi timer hết giờ (150ms), chuột đã không vào bất kỳ khu vực menu nào khác, 
            // tiến hành ẩn tất cả menu con.
            HideAllMenuPanels();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lab__Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btn_thongke_Click(object sender, EventArgs e)
        {
            baocao baocao = new baocao();
            baocao.FormClosed += (s, args) => this.Show(); // Hiện lại form chính khi đóng baocao
            this.Hide();
            baocao.Show();
        }

        private void btn_timkiem_Click(object sender, EventArgs e)
        {

        }

        private void btn_thanhly_Click(object sender, EventArgs e)
        {
            thanhly thanhly = new thanhly();
            thanhly.FormClosed += (s, args) => this.Show(); // Hiện lại form chính khi đóng thanhly
            this.Hide();
            thanhly.Show();
        }

        private void btn_muontra_Click(object sender, EventArgs e)
        {

        }

        private void btn_danhmuc_Click(object sender, EventArgs e)
        {

        }

        private void btn_hethong_Click(object sender, EventArgs e)
        {

        }

        private void pnl_hethongmenu_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btn_dangxuat_Click(object sender, EventArgs e)
        {

        }

        private void btn_doimk_Click(object sender, EventArgs e)
        {

        }

        private void btn_thongtincanhan_Click(object sender, EventArgs e)
        {

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btn_QL_xuphat_Click(object sender, EventArgs e)
        {
            xulyvipham xulyvipham = new xulyvipham();
            xulyvipham.FormClosed += (s, args) => this.Show(); // Hiện lại form chính khi đóng xulyvipham
            this.Hide();
            xulyvipham.Show();
        }

        private void btn_QLthe_Click(object sender, EventArgs e)
        {
            QL_the qL_The = new QL_the();
            qL_The.FormClosed += (s, args) => this.Show(); // Hiện lại form chính khi đóng QL_the
            this.Hide();
            qL_The.Show();
        }

        private void btn_QL_tailieu_Click(object sender, EventArgs e)
        {
            QL_tailieu qL_Tailieu = new QL_tailieu();
            qL_Tailieu.FormClosed += (s, args) => this.Show(); // Hiện lại form chính khi đóng QL_tailieu
            this.Hide();
            qL_Tailieu.Show();
        }

        private void btn_QL_docgia_Click(object sender, EventArgs e)
        {
            QL_Doc_Gia ql_Doc_Gia = new QL_Doc_Gia();
            ql_Doc_Gia.FormClosed += (s, args) => this.Show(); // Hiện lại form chính khi đóng QL_Doc_Gia
            this.Hide();
            ql_Doc_Gia.Show();
        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btn_q_Click(object sender, EventArgs e)
        {
            QL_phieumuon  phieumuonsach = new QL_phieumuon();
            phieumuonsach.FormClosed += (s, args) => this.Show(); // Hiện lại form chính khi đóng QL_phieumuon
            this.Hide();
            phieumuonsach.Show();
        }

        private void btn_lapphieumuon_Click(object sender, EventArgs e)
        {
            lapphieumuon lapphieumuon = new lapphieumuon();
            lapphieumuon.FormClosed += (s, args) => this.Show(); // Hiện lại form chính khi đóng lapphieumuon
            this.Hide();
            lapphieumuon.Show();
        }

        private void btn_tracuuphieumuon_Click(object sender, EventArgs e)
        {

        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btn_timdocgia_Click(object sender, EventArgs e)
        {
            Timdocgia timdocgia = new Timdocgia();
            timdocgia.FormClosed += (s, args) => this.Show(); // Hiện lại form chính khi đóng Timdocgia
            this.Hide();
            timdocgia.Show();
        }

        private void btn_timtailieu_Click(object sender, EventArgs e)
        {
            timtailieu timtailieu = new timtailieu();
            timtailieu.FormClosed += (s, args) => this.Show(); // Hiện lại form chính khi đóng timtailieu
            this.Hide();
            timtailieu.Show();
        }

        private void logo_Click(object sender, EventArgs e)
        {

        }

        private void menuHideTimer_Tick_1(object sender, EventArgs e)
        {

        }

        private void btn_QL_phonghoc_Click(object sender, EventArgs e)
        {
            QL_phong qL_Phong = new QL_phong();
            qL_Phong.FormClosed += (s, args) => this.Show(); // Hiện lại form chính khi đóng QL_phong
            this.Hide();
            qL_Phong.Show();
        }
    }
}