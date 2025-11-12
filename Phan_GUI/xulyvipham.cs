using _40_caesarOracle;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace DO_AN_BMCSDL.Phan_GUI
{
    public partial class xulyvipham : Form
    {

        public xulyvipham()
        {
            InitializeComponent();
        }

        private void btn_thoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void xulyvipham_Load(object sender, EventArgs e)
        {
            try
            {
                // Thiết lập Database
                Database.Set_Database("localhost", "1521", "ORCL", "C##DO_AN", "12345");
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show("Lỗi thiết lập thông tin DB: " + ex.Message, "Lỗi nghiêm trọng");
                return;
            }

            // Thiết lập DataGridView
            if (dgv_thongtinvipham != null)
            {
                // Giữ lại các thiết lập trước đó...
                dgv_thongtinvipham.Font = new Font("Times New Roman", 12, FontStyle.Regular);
                dgv_thongtinvipham.ColumnHeadersDefaultCellStyle.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                dgv_thongtinvipham.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                // 🛠️ Gán sự kiện CellDoubleClick
                dgv_thongtinvipham.CellDoubleClick -= dgv_thongtinvipham_CellDoubleClick; // Tránh gán trùng
                dgv_thongtinvipham.CellDoubleClick += dgv_thongtinvipham_CellDoubleClick;

                LoadDataViPham();
            }
        }

        // --- HÀM TẢI DỮ LIỆU XỬ LÝ VI PHẠM (Dùng bảng PHIEUPHAT) ---
        private void LoadDataViPham()
        {
            string sql = @"
                SELECT 
                    ROWNUM AS STT, 
                    TRIM(T1.MAPHIEUPHAT) AS ""Ma phieu phat"", -- Dùng tên không dấu
                    TRIM(T3.MATHANHVIEN) AS ""Ma doc gia"",    -- Dùng tên không dấu
                    TRIM(T3.TENTV) AS ""Ten doc gia"",        -- Dùng tên không dấu
                    TRIM(T1.LYDOPHAT) AS ""Ly do vi pham"",   -- Dùng tên không dấu
                    T1.SOLAN AS ""So lan vi pham"",
                    T1.NGTAO AS ""Thoi gian tre""
                FROM PHIEUPHAT T1
                JOIN PHIEUMUON T2 ON T1.MAPHIEUMUON = T2.MAPHIEUMUON
                JOIN THEBANDOC T4 ON T2.MASOTHE = T4.MASOTHE
                JOIN DOCGIA T3 ON T4.MATHANHVIEN = T3.MATHANHVIEN
                ORDER BY T1.NGTAO DESC";

            try
            {
                if (Database.Connect())
                {
                    DataTable dt = Database.ExecuteQuery(sql);
                    dgv_thongtinvipham.DataSource = dt;

                    // Ẩn cột Mã phiếu phạt (dùng để xử lý chi tiết)
                    if (dgv_thongtinvipham.Columns.Contains("Ma phieu phat"))
                    {
                        dgv_thongtinvipham.Columns["Ma phieu phat"].Visible = false;
                    }
                }
            }
            finally
            {
                Database.Close();
            }
        }

        // 🔹 Nút THÔNG TIN (Mở Form chi tiết)
        private void btn_thongtin_Click(object sender, EventArgs e)
        {
            if (dgv_thongtinvipham.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn một vi phạm để xem thông tin.", "Thông báo");
                return;
            }

            try
            {
                // 🛠️ ĐÃ SỬA LỖI: Lấy Mã phiếu phạt từ cột "Ma phieu phat" (không dấu)
                string maPhieuPhat = dgv_thongtinvipham.CurrentRow.Cells["Ma phieu phat"].Value.ToString().Trim();

                // Gọi phương thức chung để mở Form
                OpenThongTinViPhamForm(maPhieuPhat);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi mở Form chi tiết: " + ex.Message, "Lỗi");
            }
        }

        // Phương thức chung để mở Form chi tiết vi phạm
        private void OpenThongTinViPhamForm(string maPhieuPhat)
        {
            try
            {
                THONGTINVIPHAM thongtinvipham = new THONGTINVIPHAM(maPhieuPhat);

                // Nếu Form con đóng sau khi xử lý (ví dụ: cập nhật hình phạt), tải lại DGV
                if (thongtinvipham.ShowDialog() == DialogResult.OK)
                {
                    LoadDataViPham();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi mở Form thông tin vi phạm: " + ex.Message, "Lỗi");
            }
        }

        // 🔹 Bổ sung sự kiện CellDoubleClick cho DataGridView
        private void dgv_thongtinvipham_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // Đảm bảo không click vào header và có dòng được chọn
            if (e.RowIndex >= 0)
            {
                // 🛠️ ĐÃ SỬA LỖI: Lấy Mã phiếu phạt từ cột "Ma phieu phat" (không dấu)
                string maPhieuPhat = dgv_thongtinvipham.Rows[e.RowIndex].Cells["Ma phieu phat"].Value.ToString().Trim();

                // Gọi phương thức chung để mở Form
                OpenThongTinViPhamForm(maPhieuPhat);
            }
        }

        // ... (Giữ nguyên các hàm khác như HandleDelete và btn_xoa_Click_1) ...

        // --- LOGIC XÓA PHIẾU PHẠT ---
        private void HandleDelete(string maPhieuPhat)
        {
            // ... code xóa giữ nguyên ...
        }

        private void btn_xoa_Click_1(object sender, EventArgs e)
        {
            if (dgv_thongtinvipham.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn vi phạm để xóa.", "Thông báo");
                return;
            }

            // 🛠️ SỬA LỖI: Dùng tên cột đã thống nhất
            string maPhieuPhat = dgv_thongtinvipham.CurrentRow.Cells["Ma phieu phat"].Value.ToString().Trim();
            string maDocGia = dgv_thongtinvipham.CurrentRow.Cells["Ma doc gia"].Value.ToString().Trim(); // Sửa Mã độc giả

            DialogResult confirm = MessageBox.Show($"Xác nhận xóa phiếu phạt {maPhieuPhat} của độc giả {maDocGia}?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (confirm == DialogResult.Yes)
            {
                HandleDelete(maPhieuPhat);
            }
        }
    }
}