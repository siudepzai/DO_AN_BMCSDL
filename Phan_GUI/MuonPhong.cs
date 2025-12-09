using System;
using System.Data;
using System.Windows.Forms;
using DO_AN_BMCSDL.Phan_xu_ly;

namespace DO_AN_BMCSDL.Phan_GUI
{
    public partial class FormMuonPhong : Form
    {
        private DataTable dtTatCaPhong;
        private string _tenTaiKhoan;
        public FormMuonPhong(string tenTaiKhoan)
        {
            InitializeComponent();
            _tenTaiKhoan = tenTaiKhoan;
            this.Load += FormMuonPhong_Load;
            this.btnTim.Click += btnTim_Click;

            this.dgvTraCuuPhong.CellClick += dgvTraCuuPhong_CellClick;
            this.dgvPhongDaMuon.CellClick += dgvPhongDaMuon_CellClick;
        }

        public FormMuonPhong() : this("Unknown") { }

        private void HienThiPhong(DataTable dt, DataGridView dgv)
        {
            dgv.DataSource = dt;

            if (dt != null && dt.Rows.Count > 0)
            {
                if (dgv == dgvTraCuuPhong)
                {
                    dgv.Columns["MAPHONG"].HeaderText = "Mã Phòng";
                    dgv.Columns["TENPHONG"].HeaderText = "Tên Phòng";
                    dgv.Columns["SUCCHUA"].HeaderText = "Sức Chứa";
                    dgv.Columns["TRANGBI"].HeaderText = "Trang Bị";
                    dgv.Columns["TRANGTHAI"].HeaderText = "Trạng Thái";
                }
                else if (dgv == dgvPhongDaMuon)
                {
                    dgv.Columns["MAPHONG"].HeaderText = "Mã Phòng";
                    dgv.Columns["TENPHONG"].HeaderText = "Tên Phòng";
                    dgv.Columns["THOIGIANBATDAU"].HeaderText = "Bắt Đầu";
                    dgv.Columns["THOIGIANKETTHUC"].HeaderText = "Kết Thúc";
                    dgv.Columns["TINHTRANG"].HeaderText = "Tình Trạng";
                }

                dgv.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
                dgv.ReadOnly = true;
            }
        }

        private void LoadPhongDaDat()
        {
            try
            {
                DataTable dtPhongDat = MuonPhong.GetPhongDangDat(_tenTaiKhoan);
                HienThiPhong(dtPhongDat, dgvPhongDaMuon);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách phòng đang đặt: " + ex.Message, "Lỗi Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dgvPhongDaMuon.DataSource = null;
            }
        }

        private void btnTrangChu_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvTraCuuPhong_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void dgvTraCuuPhong_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                DataGridViewRow row = dgvTraCuuPhong.Rows[e.RowIndex];
                if (dgvTraCuuPhong.Columns.Contains("MAPHONG"))
                {
                    txtNhapMaPhong.Text = row.Cells["MAPHONG"].Value.ToString().Trim();
                }
            }
        }

        private void dgvPhongDaMuon_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                DataGridViewRow row = dgvPhongDaMuon.Rows[e.RowIndex];
                if (dgvPhongDaMuon.Columns.Contains("MAPHONG"))
                {
                    txtNhapMaPhong.Text = row.Cells["MAPHONG"].Value.ToString().Trim();
                }
            }
        }

        private void FormMuonPhong_Load(object sender, EventArgs e)
        {
            try
            {
                dtTatCaPhong = TraCuu.GetTatCaPhong();

                HienThiPhong(dtTatCaPhong, dgvTraCuuPhong);

                LoadPhongDaDat();
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
                HienThiPhong(dtTatCaPhong, dgvTraCuuPhong);
            }
            else
            {
                try
                {
                    DataTable dtFiltered = TraCuu.FilterData(dtTatCaPhong.Copy(), keyword);
                    HienThiPhong(dtFiltered, dgvTraCuuPhong);

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
            if (string.IsNullOrWhiteSpace(txtTimKiem.Text))
            {
                if (dtTatCaPhong != null)
                {
                    HienThiPhong(dtTatCaPhong, dgvTraCuuPhong);
                }
            }
        }

        private void btnDangKyMuon_Click(object sender, EventArgs e)
        {
            string maPhong = txtNhapMaPhong.Text.Trim();
            string soGioStr = txtNhapSoGioMuon.Text.Trim();
            int soGio = 0;

            if (string.IsNullOrEmpty(maPhong))
            {
                MessageBox.Show("Vui lòng nhập Mã Phòng hoặc chọn từ danh sách.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(soGioStr, out soGio) || soGio <= 0)
            {
                MessageBox.Show("Số giờ mượn không hợp lệ. Vui lòng nhập một số nguyên dương.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string ketQua = MuonPhong.DangKyDatPhong(_tenTaiKhoan, maPhong, soGio);

            if (ketQua.StartsWith("INVALID_CARD:"))
            {
                string statusCode = ketQua.Substring("INVALID_CARD:".Length).Trim();
                switch (statusCode)
                {
                    case "NO_CARD_INFO":
                        MessageBox.Show("Thông tin thẻ bạn đọc bị trống. Vui lòng đăng ký thẻ.", "Lỗi nghiệp vụ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    case "NOT_ISSUED":
                        MessageBox.Show("Thẻ bạn đọc chưa được cấp. Vui lòng liên hệ thủ thư.", "Lỗi nghiệp vụ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    case "RENEWAL_PENDING":
                        MessageBox.Show("Thẻ bạn đọc đang chờ gia hạn. Vui lòng hoàn thành thủ tục gia hạn.", "Lỗi nghiệp vụ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    case "LOCKED":
                        MessageBox.Show("Thẻ bạn đọc đã bị khóa. Vui lòng liên hệ thủ thư.", "Lỗi nghiệp vụ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    default:
                        MessageBox.Show("Thẻ bạn đọc đang ở trạng thái không hợp lệ. Vui lòng liên hệ thủ thư.", "Lỗi nghiệp vụ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                }
            }
            else
            {
                switch (ketQua)
                {
                    case "SUCCESS":
                        MessageBox.Show("Đăng ký đặt phòng thành công! (Phòng đang chờ duyệt)", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        FormMuonPhong_Load(null, null);

                        txtNhapMaPhong.Clear();
                        txtNhapSoGioMuon.Clear();
                        break;
                    case "ROOM_NOT_AVAILABLE":
                        MessageBox.Show("Phòng học đã bị trùng lịch đặt trong khoảng thời gian yêu cầu.", "Lỗi nghiệp vụ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    case "INVALID_HOURS":
                        MessageBox.Show("Số giờ mượn phải lớn hơn 0.", "Lỗi nghiệp vụ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    case "ERROR: USER_NOT_FOUND":
                        MessageBox.Show("Lỗi hệ thống: Tài khoản độc giả không tồn tại.", "Lỗi hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    default:
                        MessageBox.Show("Đăng ký thất bại. Lỗi: " + ketQua, "Lỗi hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                }
            }
        }

        private void btnTraPhongSom_Click(object sender, EventArgs e)
        {
            string maPhong = txtNhapMaPhong.Text.Trim();

            if (string.IsNullOrEmpty(maPhong))
            {
                MessageBox.Show("Vui lòng chọn phòng cần trả trong danh sách phòng đã đặt.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show($"Bạn có chắc chắn muốn trả phòng {maPhong} sớm?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            string ketQua = MuonPhong.TraPhongSom(_tenTaiKhoan, maPhong);

            switch (ketQua)
            {
                case "SUCCESS":
                    MessageBox.Show($"Phòng {maPhong} đã được trả sớm thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    FormMuonPhong_Load(null, null);
                    txtNhapMaPhong.Clear();
                    txtNhapSoGioMuon.Clear();
                    break;
                case "NO_BOOKING":
                    MessageBox.Show($"Không tìm thấy phòng {maPhong} đang đặt/mượn để trả sớm.", "Lỗi nghiệp vụ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                default:
                    MessageBox.Show("Trả phòng sớm thất bại. Lỗi: " + ketQua, "Lỗi hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
            }
        }

        private void btnGiaHan_Click(object sender, EventArgs e)
        {
            string maPhong = txtNhapMaPhong.Text.Trim();
            string soGioStr = txtNhapSoGioMuon.Text.Trim();
            int soGioGiaHan = 0;

            if (string.IsNullOrEmpty(maPhong))
            {
                MessageBox.Show("Vui lòng chọn phòng cần gia hạn trong danh sách phòng đã đặt.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(soGioStr, out soGioGiaHan) || soGioGiaHan <= 0)
            {
                MessageBox.Show("Số giờ gia hạn không hợp lệ. Vui lòng nhập một số nguyên dương.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string ketQua = MuonPhong.GiaHanDatPhong(_tenTaiKhoan, maPhong, soGioGiaHan);

            switch (ketQua)
            {
                case "SUCCESS":
                    MessageBox.Show($"Gia hạn phòng {maPhong} thêm {soGioGiaHan} giờ thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    FormMuonPhong_Load(null, null);
                    txtNhapMaPhong.Clear();
                    txtNhapSoGioMuon.Clear();
                    break;
                case "ROOM_NOT_AVAILABLE":
                    MessageBox.Show("Phòng bị trùng lịch đặt sau khi gia hạn.", "Lỗi nghiệp vụ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                case "NO_BOOKING":
                    MessageBox.Show($"Không tìm thấy phòng {maPhong} đang đặt/mượn để gia hạn.", "Lỗi nghiệp vụ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                default:
                    MessageBox.Show("Gia hạn thất bại. Lỗi: " + ketQua, "Lỗi hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
            }
        }

        private void txtNhapMaPhong_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtNhapSoGioMuon_TextChanged(object sender, EventArgs e)
        {

        }

        private void dgvPhongDaMuon_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvPhongDaMuon_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}