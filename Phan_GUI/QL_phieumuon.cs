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
    public partial class QL_phieumuon : Form
    {
        public QL_phieumuon()
        {
            InitializeComponent();
        }

        private void btn_thoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        // Hàm tải dữ liệu Tài liệu/Sách
        private DataTable LoadDataTaiLieuSach()
        {
            // ... Thực hiện truy vấn Oracle SELECT * FROM PHIEU_MUON_SACH ...
            // Trả về DataTable chứa dữ liệu mượn/trả sách
            // Ví dụ: return GetOracleData("SELECT MaSach, MaDocGia, TinhTrang FROM...");
            return new DataTable(); // Placeholder
        }

        private void btn_thongtintailieu_Click(object sender, EventArgs e)
        {
            // 1. Cập nhật màu sắc Button (Đánh dấu nút đang được chọn)
            btn_thongtintailieu.BackColor = Color.FromArgb(170, 170, 170); // Xám đậm

            // 2. Tải và gán nguồn dữ liệu mới
            dgvMuonTra.DataSource = LoadDataTaiLieuSach();

            // 3. Cập nhật Tiêu đề cột (Tùy chọn, nếu tên cột trong CSDL khác nhau)
            dgvMuonTra.Columns["MaPhong"].HeaderText = "Mã tài liệu"; // Ví dụ: đổi tên cột Mã phòng thành Mã tài liệu

            // 4. Kích hoạt lại định dạng màu (Nếu cần)
            dgvMuonTra.Invalidate();
        }
    }
}
