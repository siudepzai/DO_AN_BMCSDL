using _40_caesarOracle;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace DO_AN_BMCSDL.Phan_GUI
{
    public partial class lapphieumuon : Form
    {

        public lapphieumuon()
        {
            InitializeComponent();
        }

        private void btn_thoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lapphieumuon_Load(object sender, EventArgs e)
        {
            if (dgvDanhSachDocGia != null)
            {
                dgvDanhSachDocGia.Font = new Font("Times New Roman", 12, FontStyle.Regular);
                dgvDanhSachDocGia.ColumnHeadersDefaultCellStyle.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                dgvDanhSachDocGia.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                dgvDanhSachDocGia.CellClick -= dgvDanhSachDocGia_CellClick;
                dgvDanhSachDocGia.CellClick += dgvDanhSachDocGia_CellClick;

                dgvDanhSachDocGia.CellDoubleClick -= dgvDanhSachDocGia_CellDoubleClick;
                dgvDanhSachDocGia.CellDoubleClick += dgvDanhSachDocGia_CellDoubleClick;
            }

            try
            {
                Database.Set_Database("localhost", "1521", "ORCL", "C##DO_AN", "12345");
                LoadDanhSachDocGia();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khởi tạo hoặc kết nối DB: " + ex.Message, "Lỗi");
            }

            txtNgayLap.Clear();
            txtNgayLap.ReadOnly = true;
        }
        private void LoadDanhSachDocGia()
        {
            string sql = @"
                SELECT
                    TRIM(T2.MAPHIEUMUON) AS ""Ma phieu muon"",
                    TRIM(T3.MATHANHVIEN) AS ""Ma doc gia"",
                    TRIM(T4.MANV) AS ""Nguoi lap"",  -- 🛠️ ĐÃ SỬA: Lấy Mã NV (MANV) thay vì Tên NV (TENNV)
                    T2.NGAYMUON AS ""Ngay lap""
                FROM THEBANDOC T1
                JOIN PHIEUMUON T2 ON T1.MASOTHE = T2.MASOTHE
                JOIN DOCGIA T3 ON T1.MATHANHVIEN = T3.MATHANHVIEN
                LEFT JOIN NHANVIEN T4 ON T2.MANV = T4.MANV
                ORDER BY T2.NGAYMUON DESC";

            try
            {
                if (Database.Connect())
                {
                    DataTable dt = Database.ExecuteQuery(sql);
                    dgvDanhSachDocGia.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải Danh sách độc giả/Phiếu mượn: " + ex.Message, "Lỗi SQL");
            }
            finally
            {
                Database.Close();
            }
        }
        private void dgvDanhSachDocGia_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvDanhSachDocGia.Rows[e.RowIndex];

                if (dgvDanhSachDocGia.Columns.Contains("Ma doc gia"))
                {
                    txtMaDocGia.Text = row.Cells["Ma doc gia"].Value.ToString().Trim();
                }

                txtNgayLap.Clear();
            }
        }
        private void dgvDanhSachDocGia_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvDanhSachDocGia.Rows[e.RowIndex];

                if (dgvDanhSachDocGia.Columns.Contains("Ma phieu muon"))
                    txtMaPhieuMuon.Text = row.Cells["Ma phieu muon"].Value.ToString().Trim();

                if (dgvDanhSachDocGia.Columns.Contains("Ma doc gia"))
                    txtMaDocGia.Text = row.Cells["Ma doc gia"].Value.ToString().Trim();

                if (dgvDanhSachDocGia.Columns.Contains("Nguoi lap"))
                    txtNguoiLap.Text = row.Cells["Nguoi lap"].Value.ToString().Trim();

                if (dgvDanhSachDocGia.Columns.Contains("Ngay lap") && row.Cells["Ngay lap"].Value != DBNull.Value)
                {
                    DateTime ngayLapCu = (DateTime)row.Cells["Ngay lap"].Value;
                    txtNgayLap.Text = ngayLapCu.ToString("dd/MM/yyyy HH:mm");
                }
            }
        }

        private void btn_Them_Click_1(object sender, EventArgs e)
        {
            DateTime ngayMuon = DateTime.Now;
            txtNgayLap.Text = ngayMuon.ToString("dd/MM/yyyy HH:mm");

            if (string.IsNullOrWhiteSpace(txtMaPhieuMuon.Text) ||
                string.IsNullOrWhiteSpace(txtMaDocGia.Text) ||
                string.IsNullOrWhiteSpace(txtNguoiLap.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ Mã phiếu mượn, Mã độc giả và Người lập.", "Thiếu thông tin");
                return;
            }

            string maPhieu = txtMaPhieuMuon.Text.Trim();
            string maDocGia = txtMaDocGia.Text.Trim();
            string maNV = txtNguoiLap.Text.Trim(); 
            string sqlInsert = @"
                INSERT INTO PHIEUMUON (MAPHIEUMUON, MANV, MASOTHE, NGAYMUON, NGAYTRA)
                VALUES (
                    :maPhieu, 
                    :maNV, 
                    (SELECT T1.MASOTHE FROM THEBANDOC T1 JOIN DOCGIA T2 ON T1.MATHANHVIEN = T2.MATHANHVIEN WHERE TRIM(T2.MATHANHVIEN) = :maDocGia),
                    :ngayMuon, 
                    :ngayTra)";

            DateTime ngayTraMacDinh = ngayMuon.AddDays(14);

            try
            {
                if (Database.Connect())
                {
                    OracleParameter[] parameters = new OracleParameter[]
                    {
                        new OracleParameter("maPhieu", maPhieu),
                        new OracleParameter("maNV", maNV),
                        new OracleParameter("maDocGia", maDocGia),
                        new OracleParameter("ngayMuon", ngayMuon),
                        new OracleParameter("ngayTra", ngayTraMacDinh)
                    };

                    int rowsAffected = Database.ExecuteNonQuery(sqlInsert, parameters);

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Lập phiếu mượn thành công!", "Thành công");
                        LoadDanhSachDocGia();
                        txtMaPhieuMuon.Clear();
                        txtMaDocGia.Clear();
                        txtNguoiLap.Clear();
                        txtNgayLap.Clear();
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy Mã thẻ/Mã độc giả hợp lệ để lập phiếu.", "Lỗi nhập liệu");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lập phiếu mượn: " + ex.Message, "Lỗi SQL");
            }
            finally
            {
                Database.Close();
            }
        }
    }
    
}