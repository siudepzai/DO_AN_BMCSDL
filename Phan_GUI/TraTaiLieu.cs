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
    public partial class FormTraTaiLieu : Form
    {
        private string _tenTaiKhoan;
        public FormTraTaiLieu(string tenTaiKhoan)
        {
            InitializeComponent();
            _tenTaiKhoan = tenTaiKhoan;
            LoadPhieuMuonData();
        }
        private void FormTraTaiLieu_Load(object sender, EventArgs e)
        {
            LoadPhieuMuonData();
        }
        private void LoadPhieuMuonData()
        {
            DataTable dt = XL_PhieuMuon.GetPhieuMuonByTaiKhoan(_tenTaiKhoan);

            dgvPhieuMuon.DataSource = dt;

            if (dt.Rows.Count > 0)
            {
                dgvPhieuMuon.Columns["MAPHIEUMUON"].HeaderText = "Mã Phiếu";
                dgvPhieuMuon.Columns["MATAILIEU"].HeaderText = "Mã Tài Liệu";
                dgvPhieuMuon.Columns["TENSACH"].HeaderText = "Tên Tài Liệu";
                dgvPhieuMuon.Columns["SOLUONG"].HeaderText = "SL Mượn";
                dgvPhieuMuon.Columns["NGAYMUON"].HeaderText = "Ngày Mượn";
                dgvPhieuMuon.Columns["HIENTRANG"].HeaderText = "Trạng Thái";

                dgvPhieuMuon.AutoResizeColumns();
                dgvPhieuMuon.ReadOnly = true;
            }
        }


        private void btnTrangChu_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvPhieuMuon_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnTraTaiLieu_Click(object sender, EventArgs e)
        {
            if (dgvPhieuMuon.SelectedRows.Count == 0)
            {
                MessageBox.Show("Hãy chọn tài liệu cần trả!");
                return;
            }

            string maPhieu = dgvPhieuMuon.SelectedRows[0].Cells["MAPHIEUMUON"].Value.ToString();
            string maTaiLieu = dgvPhieuMuon.SelectedRows[0].Cells["MATAILIEU"].Value.ToString();

            DialogResult dr = MessageBox.Show(
                $"Bạn chắc chắn muốn trả tài liệu {maTaiLieu}?",
                "Xác nhận",
                MessageBoxButtons.YesNo);

            if (dr == DialogResult.No) return;

            string kq = XL_PhieuMuon.TraTaiLieu(maPhieu, maTaiLieu);

            if (kq == "SUCCESS")
            {
                MessageBox.Show("Trả tài liệu thành công!");
                LoadPhieuMuonData();
            }
            else
            {
                MessageBox.Show("Lỗi: " + kq);
            }
        }

        private void dgvPhieuMuon_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
