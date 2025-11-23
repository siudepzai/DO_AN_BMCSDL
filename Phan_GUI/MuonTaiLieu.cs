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
    public partial class FormMuonTaiLieu : Form
    {
        private DataTable dtTatCaTaiLieu;
        public FormMuonTaiLieu()
        {
            InitializeComponent();
            this.Load += FormMuonTaiLieu_Load; // <== Đảm bảo có sự kiện Load
            this.btnTim.Click += btnTim_Click;
        }
        private void HienThiTaiLieu(DataTable dt)
        {
            dgvTraCuuTaiLieu.DataSource = dt;

            if (dt != null && dt.Rows.Count > 0)
            {
                // Thiết lập Header Text theo yêu cầu: MATAILIEU, TENTAILIEU, NXB, PHIMUON, SOLUONG
                dgvTraCuuTaiLieu.Columns["MATAILIEU"].HeaderText = "Mã Tài Liệu";
                dgvTraCuuTaiLieu.Columns["TENTAILIEU"].HeaderText = "Tên Tài Liệu";
                dgvTraCuuTaiLieu.Columns["NXB"].HeaderText = "NXB";
                dgvTraCuuTaiLieu.Columns["PHIMUON"].HeaderText = "Phí Mượn";
                dgvTraCuuTaiLieu.Columns["SOLUONG"].HeaderText = "Số Lượng";

                dgvTraCuuTaiLieu.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
                dgvTraCuuTaiLieu.ReadOnly = true;
            }
        }
        private void btnTrangChu_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvTraCuuTaiLieu_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            // Tự động quay lại danh sách gốc khi ô tìm kiếm rỗng
            if (string.IsNullOrWhiteSpace(txtTimKiem.Text))
            {
                if (dtTatCaTaiLieu != null)
                {
                    HienThiTaiLieu(dtTatCaTaiLieu);
                }
            }
        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            if (dtTatCaTaiLieu == null) return;

            string keyword = txtTimKiem.Text.Trim();

            if (string.IsNullOrWhiteSpace(keyword))
            {
                // TRƯỜNG HỢP 1: Ô tìm kiếm rỗng => Luôn hiển thị bảng dữ liệu gốc
                HienThiTaiLieu(dtTatCaTaiLieu);
            }
            else
            {
                // TRƯỜNG HỢP 2: Có từ khóa => Tiến hành lọc
                try
                {
                    DataTable dtFiltered = TraCuu.FilterData(dtTatCaTaiLieu.Copy(), keyword); // Dùng .Copy() để tránh thay đổi bảng gốc
                    HienThiTaiLieu(dtFiltered);

                    if (dtFiltered.Rows.Count == 0)
                    {
                        MessageBox.Show("Không tìm thấy tài liệu nào khớp với từ khóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi lọc dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void FormMuonTaiLieu_Load(object sender, EventArgs e)
        {
            try
            {
                // 1. Tải toàn bộ danh sách tài liệu ban đầu
                dtTatCaTaiLieu = TraCuu.GetTatCaTaiLieu();
                HienThiTaiLieu(dtTatCaTaiLieu);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách tài liệu: " + ex.Message, "Lỗi Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dgvTraCuuTaiLieu.DataSource = null;
            }
        }
    }
}
