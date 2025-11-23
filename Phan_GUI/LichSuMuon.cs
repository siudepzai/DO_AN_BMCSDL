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
    public partial class FormLichSuMuon : Form
    {
        private string _tenTaiKhoan;
        public FormLichSuMuon(string tenTaiKhoan)
        {
            InitializeComponent();
            _tenTaiKhoan = tenTaiKhoan;
        }

        private void btnTrangChu_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void LoadLichSuMuonData(DataTable dt, string type)
        {
            dataGridView1.DataSource = dt;

            // Đảm bảo DataGridView có cột nếu có dữ liệu
            if (dt == null || dt.Rows.Count == 0)
            {
                dataGridView1.Columns.Clear();
                MessageBox.Show($"Không có lịch sử {type} nào được tìm thấy.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Thiết lập Header Text
            if (type == "Mượn Tài liệu")
            {
                dataGridView1.Columns["MATAILIEU"].HeaderText = "Mã Tài Liệu";
                dataGridView1.Columns["TENTAILIEU"].HeaderText = "Tên Tài Liệu";
                dataGridView1.Columns["NXB"].HeaderText = "Nhà Xuất Bản";
                dataGridView1.Columns["PHIMUON"].HeaderText = "Phí Mượn";
                dataGridView1.Columns["SOLUONG"].HeaderText = "Số Lượng";
            }
            else if (type == "Mượn Phòng Học")
            {
                dataGridView1.Columns["MAPHONG"].HeaderText = "Mã Phòng";
                dataGridView1.Columns["TENPHONG"].HeaderText = "Tên Phòng";
                dataGridView1.Columns["THOIGIANBATDAU"].HeaderText = "Thời Gian Bắt Đầu";
                dataGridView1.Columns["THOIGIANKETTHUC"].HeaderText = "Thời Gian Kết Thúc";
                dataGridView1.Columns["TRANGTHAI"].HeaderText = "Trạng Thái";
            }

            dataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            dataGridView1.ReadOnly = true;
        }

        private void LoadLichSuTaiLieu()
        {
            try
            {
                DataTable dt = LichSuMuon.GetLichSuMuonTaiLieu(_tenTaiKhoan);
                LoadLichSuMuonData(dt, "Mượn Tài liệu");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải lịch sử mượn tài liệu: " + ex.Message, "Lỗi Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dataGridView1.DataSource = null;
            }
        }
        private void LoadLichSuPhong()
        {
            try
            {
                DataTable dt = LichSuMuon.GetLichSuMuonPhong(_tenTaiKhoan);
                LoadLichSuMuonData(dt, "Mượn Phòng Học");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải lịch sử mượn phòng: " + ex.Message, "Lỗi Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dataGridView1.DataSource = null;
            }
        }
        private void FormLichSuMuon_Load(object sender, EventArgs e)
        {
            if (radLichSuMuonTaiLieu.Checked)
            {
                LoadLichSuTaiLieu();
            }
        }
        private void radLichSuMuonTaiLieu_CheckedChanged(object sender, EventArgs e)
        {
            if (radLichSuMuonTaiLieu.Checked)
            {
                LoadLichSuTaiLieu();
            }
        }
        private void radLichSuMuonPhong_CheckedChanged(object sender, EventArgs e)
        {
            if (radLichSuMuonPhong.Checked)
            {
                LoadLichSuPhong();
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
