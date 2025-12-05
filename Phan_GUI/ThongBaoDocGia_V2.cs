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

namespace DO_AN_BMCSDL
{
    public partial class FormThongBaoDocGia_V2 : Form
    {
        private string _tenTaiKhoan;
        public FormThongBaoDocGia_V2(string tenTaiKhoan)
        {
            InitializeComponent();
            _tenTaiKhoan = tenTaiKhoan;
        }

        private void FormThongBaoDocGia_V2_Load(object sender, EventArgs e)
        {
            LoadPhieuPhat();
        }
        private void LoadPhieuPhat()
        {
            try
            {
                DataTable dt = ThongBao.GetPhieuPhatByUsername(_tenTaiKhoan);

                dgvThongBao.DataSource = dt;

                if (dt != null && dt.Rows.Count > 0)
                {
                    // Thiết lập Header Text theo yêu cầu
                    // MAPHIEUPHAT, MANV, MAPHIEUMUON, PHIPHAT, LYDOPHAT, SACHTHAYTHE, TINHTRANGTAILIEU, LYDO, NGTAO, NGUOILAP, SOLAN
                    dgvThongBao.Columns["MAPHIEUPHAT"].HeaderText = "Mã Phiếu Phạt";
                    dgvThongBao.Columns["MANV"].HeaderText = "Mã NV Lập";
                    dgvThongBao.Columns["MAPHIEUMUON"].HeaderText = "Mã Phiếu Mượn";
                    dgvThongBao.Columns["PHIPHAT"].HeaderText = "Phí Phạt (VNĐ)";
                    dgvThongBao.Columns["LYDOPHAT"].HeaderText = "Lý Do Phạt";
                    dgvThongBao.Columns["SACHTHAYTHE"].HeaderText = "Sách Thay Thế";
                    dgvThongBao.Columns["TINHTRANGTAILIEU"].HeaderText = "Tình Trạng TL";
                    dgvThongBao.Columns["LYDO"].HeaderText = "Lý Do Hủy"; // Giả định đây là lý do phụ
                    dgvThongBao.Columns["NGTAO"].HeaderText = "Ngày Lập";
                    dgvThongBao.Columns["NGUOILAP"].HeaderText = "Người Lập";
                    dgvThongBao.Columns["SOLAN"].HeaderText = "Số Lần";

                    // Định dạng cột tiền tệ
                    if (dgvThongBao.Columns.Contains("PHIPHAT"))
                    {
                        dgvThongBao.Columns["PHIPHAT"].DefaultCellStyle.Format = "N0";
                        dgvThongBao.Columns["PHIPHAT"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    }

                    dgvThongBao.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
                    dgvThongBao.ReadOnly = true;
                }
                else
                {
                    MessageBox.Show("Bạn không có phiếu phạt nào.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dgvThongBao.DataSource = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải thông báo phiếu phạt: " + ex.Message, "Lỗi Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dgvThongBao.DataSource = null;
            }
        }

        private void lnkTroLai_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Close();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
