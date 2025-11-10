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
    public partial class QL_phong : Form
    {
        public QL_phong()
        {
            InitializeComponent();
        }

        private void btn_thoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        
        // Hàm tải dữ liệu Phòng học
        private DataTable LoadDataPhongHoc()
        {
            // ... Thực hiện truy vấn Oracle SELECT * FROM PHIEU_YEU_CAU ... (như bảng đã thiết kế trước đó)
            // Trả về DataTable chứa dữ liệu mượn/trả phòng
            // Ví dụ: return GetOracleData("SELECT MaPhong, MaDocGia, TrangThaiXuLy FROM...");
            return new DataTable(); // Placeholder
        }

        private void QL_phong_Load(object sender, EventArgs e)
        {

        }

        private void btn_thongtinphong_Click(object sender, EventArgs e)
        {
            // 1. Cập nhật màu sắc Button
            btn_thongtinphong.BackColor = Color.FromArgb(170, 170, 170); // Xám đậm

            // 2. Tải và gán nguồn dữ liệu mới
            dgvMuonTra.DataSource = LoadDataPhongHoc();

            // 3. Cập nhật Tiêu đề cột
            dgvMuonTra.Columns["MaPhong"].HeaderText = "Mã phòng"; // Đảm bảo tiêu đề cột đúng

            // 4. Kích hoạt lại định dạng màu (Cần thiết cho CellFormatting)
            dgvMuonTra.Invalidate();
        }
    }
}
