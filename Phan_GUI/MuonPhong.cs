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
    public partial class FormMuonPhong : Form
    {
        private DataTable dtTatCaPhong;
        public FormMuonPhong()
        {
            InitializeComponent();
            this.Load += FormMuonPhong_Load; // <== Đảm bảo có sự kiện Load
            this.btnTim.Click += btnTim_Click;
        }
        private void HienThiPhong(DataTable dt)
        {
            dgvTraCuuPhong.DataSource = dt;

            if (dt != null && dt.Rows.Count > 0)
            {
                // Thiết lập Header Text theo yêu cầu: MAPHONG, TENPHONG, SUCCHUA, TRANGBI, TRANGTHAI
                dgvTraCuuPhong.Columns["MAPHONG"].HeaderText = "Mã Phòng";
                dgvTraCuuPhong.Columns["TENPHONG"].HeaderText = "Tên Phòng";
                dgvTraCuuPhong.Columns["SUCCHUA"].HeaderText = "Sức Chứa";
                dgvTraCuuPhong.Columns["TRANGBI"].HeaderText = "Trang Bị";
                dgvTraCuuPhong.Columns["TRANGTHAI"].HeaderText = "Trạng Thái";

                dgvTraCuuPhong.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
                dgvTraCuuPhong.ReadOnly = true;
            }
        }
        private void btnDangKyMuon_Click(object sender, EventArgs e)
        {
            FormThongBaoDangKy formThongBaoDangKy = new FormThongBaoDangKy();
            formThongBaoDangKy.FormClosed += (s, args) => this.Show();
            formThongBaoDangKy.Show();
        }

        private void btnTrangChu_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvTraCuuPhong_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void FormMuonPhong_Load(object sender, EventArgs e)
        {
            try
            {
                // 1. Tải toàn bộ danh sách phòng ban đầu
                dtTatCaPhong = TraCuu.GetTatCaPhong();
                HienThiPhong(dtTatCaPhong);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách phòng: " + ex.Message, "Lỗi Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dgvTraCuuPhong.DataSource = null;
            }
        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            if (dtTatCaPhong == null) return;

            string keyword = txtTimKiem.Text.Trim();

            if (string.IsNullOrWhiteSpace(keyword))
            {
                // TRƯỜNG HỢP 1: Ô tìm kiếm rỗng => Luôn hiển thị bảng dữ liệu gốc
                HienThiPhong(dtTatCaPhong);
            }
            else
            {
                // TRƯỜNG HỢP 2: Có từ khóa => Tiến hành lọc
                try
                {
                    DataTable dtFiltered = TraCuu.FilterData(dtTatCaPhong.Copy(), keyword); // Dùng .Copy() để tránh thay đổi bảng gốc
                    HienThiPhong(dtFiltered);

                    if (dtFiltered.Rows.Count == 0)
                    {
                        MessageBox.Show("Không tìm thấy phòng nào khớp với từ khóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi lọc dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            // Tự động quay lại danh sách gốc khi ô tìm kiếm rỗng
            if (string.IsNullOrWhiteSpace(txtTimKiem.Text))
            {
                if (dtTatCaPhong != null)
                {
                    HienThiPhong(dtTatCaPhong);
                }
            }
        }
    }
}
