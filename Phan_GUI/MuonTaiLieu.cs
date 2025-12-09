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
        private string _tenTaiKhoan;
        public FormMuonTaiLieu(string tenTaiKhoan)
        {
            InitializeComponent();
            _tenTaiKhoan = tenTaiKhoan;
            this.Load += FormMuonTaiLieu_Load;
            this.btnTim.Click += btnTim_Click;
            this.dgvTraCuuTaiLieu.CellClick += dgvTraCuuTaiLieu_CellClick;
        }

        public FormMuonTaiLieu() : this("Unknown") { }

        private void HienThiTaiLieu(DataTable dt, DataGridView dgv)
        {
            dgv.DataSource = dt;

            if (dt != null && dt.Rows.Count > 0)
            {
                if (dgv == dgvTraCuuTaiLieu)
                {
                    dgv.Columns["MATAILIEU"].HeaderText = "Mã Tài Liệu";
                    dgv.Columns["TENTAILIEU"].HeaderText = "Tên Tài Liệu";
                    dgv.Columns["NXB"].HeaderText = "NXB";
                    dgv.Columns["PHIMUON"].HeaderText = "Phí Mượn";
                    dgv.Columns["SOLUONG"].HeaderText = "Số Lượng Tồn";
                }
                else if (dgv == dgvTaiLieuDaMuon)
                {
                    dgv.Columns["MATAILIEU"].HeaderText = "Mã Tài Liệu";
                    dgv.Columns["TENTAILIEU"].HeaderText = "Tên Tài Liệu";
                    dgv.Columns["NXB"].HeaderText = "NXB";
                    dgv.Columns["TRANGTHAI"].HeaderText = "Trạng Thái";
                    dgv.Columns["SOLUONG"].HeaderText = "Số Lượng Mượn";
                }

                dgv.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
                dgv.ReadOnly = true;
            }
        }

        private void LoadTaiLieuDaMuon()
        {
            try
            {
                DataTable dtMuonPending = MuonTaiLieu.GetTaiLieuDaMuon(_tenTaiKhoan);
                HienThiTaiLieu(dtMuonPending, dgvTaiLieuDaMuon);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách tài liệu đang mượn: " + ex.Message, "Lỗi Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dgvTaiLieuDaMuon.DataSource = null;
            }
        }


        private void btnTrangChu_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvTraCuuTaiLieu_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void dgvTraCuuTaiLieu_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                DataGridViewRow row = dgvTraCuuTaiLieu.Rows[e.RowIndex];
                if (dgvTraCuuTaiLieu.Columns.Contains("MATAILIEU"))
                {
                    txtNhapMaTaiLieu.Text = row.Cells["MATAILIEU"].Value.ToString().Trim();
                }
            }
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTimKiem.Text))
            {
                if (dtTatCaTaiLieu != null)
                {
                    HienThiTaiLieu(dtTatCaTaiLieu, dgvTraCuuTaiLieu);
                }
            }
        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            if (dtTatCaTaiLieu == null) return;

            string keyword = txtTimKiem.Text.Trim();

            if (string.IsNullOrWhiteSpace(keyword))
            {
                HienThiTaiLieu(dtTatCaTaiLieu, dgvTraCuuTaiLieu);
            }
            else
            {
                try
                {
                    DataTable dtFiltered = TraCuu.FilterData(dtTatCaTaiLieu.Copy(), keyword);
                    HienThiTaiLieu(dtFiltered, dgvTraCuuTaiLieu);

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
                dtTatCaTaiLieu = TraCuu.GetTatCaTaiLieu();
                HienThiTaiLieu(dtTatCaTaiLieu, dgvTraCuuTaiLieu);

                LoadTaiLieuDaMuon();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách tài liệu: " + ex.Message, "Lỗi Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dgvTraCuuTaiLieu.DataSource = null;
            }
        }

        private void btnDangKyMuon_Click(object sender, EventArgs e)
        {

            string maTaiLieu = txtNhapMaTaiLieu.Text.Trim();
            string soLuongStr = txtNhapSoLuongMuon.Text.Trim();
            int soLuongMuon = 0;

            if (string.IsNullOrEmpty(maTaiLieu))
            {
                MessageBox.Show("Vui lòng nhập Mã Tài Liệu hoặc chọn từ danh sách.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(soLuongStr, out soLuongMuon) || soLuongMuon <= 0)
            {
                MessageBox.Show("Số lượng mượn không hợp lệ. Vui lòng nhập một số nguyên dương.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string ketQua = MuonTaiLieu.DangKyMuon(_tenTaiKhoan, maTaiLieu, soLuongMuon);

            switch (ketQua)
            {
                case "SUCCESS":
                    MessageBox.Show("Đăng ký mượn tài liệu thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    LoadTaiLieuDaMuon();

                    FormMuonTaiLieu_Load(null, null);

                    txtNhapMaTaiLieu.Clear();
                    txtNhapSoLuongMuon.Clear();
                    break;
                case "NO_CARD":
                    MessageBox.Show("Độc giả không có thẻ bạn đọc hợp lệ. Vui lòng đăng ký thẻ.", "Lỗi nghiệp vụ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                case "INVALID_CARD_STATUS":
                    MessageBox.Show("Thẻ bạn đọc đang ở trạng thái không hợp lệ (ví dụ: bị khóa).", "Lỗi nghiệp vụ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                case "NO_DOCUMENT":
                    MessageBox.Show("Mã Tài Liệu không tồn tại.", "Lỗi nghiệp vụ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                case "INSUFFICIENT_STOCK":
                    MessageBox.Show("Số lượng tài liệu không đủ cung cấp.", "Lỗi nghiệp vụ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                default:
                    MessageBox.Show("Đăng ký thất bại. Lỗi: " + ketQua, "Lỗi hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
            }
        }

        private void dgvTaiLieuDaMuon_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvTaiLieuDaMuon_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}