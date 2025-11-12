using _40_caesarOracle;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Data;
using System.Globalization;
using System.Windows.Forms;

namespace DO_AN_BMCSDL.Phan_GUI
{
    public partial class suathongtindocgia : Form
    {
        private string _maDocGiaHienTai;

        public suathongtindocgia()
        {
            InitializeComponent();
        }

        public suathongtindocgia(string maDocGia) : this()
        {
            _maDocGiaHienTai = maDocGia?.Trim();
        }

        private void suathongtindocgia_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(_maDocGiaHienTai))
            {
                txt_timkiem.Text = _maDocGiaHienTai;
                LoadData(_maDocGiaHienTai);
            }
        }

        // Xóa dữ liệu cũ trên form
        private void ClearFormControls()
        {
            txt_tendocgia.Clear();
            txt_madocgia.Clear();
            txt_ngaysinh.Clear();
            txt_gioitinh.Clear();
            txt_chucvu.Clear();
            txt_sdt.Clear();
            txt_email.Clear();
            txtdiachi.Clear();
            _maDocGiaHienTai = string.Empty;
        }

        // LOAD dữ liệu từ DB
        private void LoadData(string maDocGia)
        {
            ClearFormControls();

            string sql = @"SELECT MATHANHVIEN, TENTV, NGSINH, GIOITINH, VAITRO,
                                  SODIENTHOAI, EMAIL, DIACHI
                           FROM DOCGIA
                           WHERE TRIM(MATHANHVIEN) = :maDocGia";

            try
            {
                if (!Database.Connect())
                {
                    MessageBox.Show("Không thể kết nối CSDL.", "Lỗi kết nối");
                    return;
                }

                DataTable dt = Database.ExecuteQuery(sql, new OracleParameter("maDocGia", maDocGia));

                if (dt == null)
                {
                    MessageBox.Show("Không thể truy vấn dữ liệu (dt = null).", "Lỗi");
                    return;
                }

                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];

                    txt_tendocgia.Text = row["TENTV"]?.ToString().Trim();
                    txt_madocgia.Text = row["MATHANHVIEN"]?.ToString().Trim();

                    // Xử lý ngày sinh an toàn
                    string ngaySinhText = row["NGSINH"]?.ToString();
                    if (!string.IsNullOrWhiteSpace(ngaySinhText) &&
                        DateTime.TryParse(ngaySinhText, out DateTime ngSinh))
                    {
                        txt_ngaysinh.Text = ngSinh.ToString("dd/MM/yyyy");
                    }

                    txt_gioitinh.Text = row["GIOITINH"]?.ToString().Trim();
                    txt_chucvu.Text = row["VAITRO"]?.ToString().Trim();
                    txt_sdt.Text = row["SODIENTHOAI"]?.ToString().Trim();
                    txt_email.Text = row["EMAIL"]?.ToString().Trim();
                    txtdiachi.Text = row["DIACHI"]?.ToString().Trim();

                    _maDocGiaHienTai = txt_madocgia.Text;
                }
                else
                {
                    MessageBox.Show("❌ Không tìm thấy độc giả có mã: " + maDocGia, "Kết quả tìm kiếm");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu:\n" + ex.Message, "Lỗi");
            }
            finally
            {
                Database.Close();
            }
        }

        // Nút TÌM
       

        // Nút LƯU (Cập nhật DB)
        private void btn_luu_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_maDocGiaHienTai))
            {
                MessageBox.Show("Vui lòng tìm độc giả trước khi lưu.", "Lỗi");
                return;
            }

            string tenTV = txt_tendocgia.Text.Trim();
            string vaiTro = txt_chucvu.Text.Trim();
            string ngaySinhString = txt_ngaysinh.Text.Trim();
            string gioiTinh = txt_gioitinh.Text.Trim();
            string sdt = txt_sdt.Text.Trim();
            string email = txt_email.Text.Trim();
            string diaChi = txtdiachi.Text.Trim();

            // Chuyển ngày sinh
            if (!DateTime.TryParseExact(ngaySinhString, "dd/MM/yyyy",
                CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime ngaySinhDate))
            {
                MessageBox.Show("Ngày sinh không hợp lệ (định dạng DD/MM/YYYY).", "Lỗi định dạng");
                return;
            }

            string sql = @"UPDATE DOCGIA SET
                               TENTV = :tenTV,
                               VAITRO = :vaiTro,
                               NGSINH = :ngSinh,
                               GIOITINH = :gt,
                               SODIENTHOAI = :sdt,
                               EMAIL = :email,
                               DIACHI = :dc
                           WHERE TRIM(MATHANHVIEN) = :maDocGia";

            try
            {
                if (!Database.Connect())
                {
                    MessageBox.Show("Không thể kết nối CSDL.", "Lỗi kết nối");
                    return;
                }

                using (OracleCommand cmd = new OracleCommand(sql, Database.Get_Connection()))
                {
                    cmd.Parameters.Add(new OracleParameter("tenTV", tenTV));
                    cmd.Parameters.Add(new OracleParameter("vaiTro", vaiTro));
                    cmd.Parameters.Add(new OracleParameter("ngSinh", OracleDbType.Date) { Value = ngaySinhDate });
                    cmd.Parameters.Add(new OracleParameter("gt", gioiTinh));
                    cmd.Parameters.Add(new OracleParameter("sdt", sdt));
                    cmd.Parameters.Add(new OracleParameter("email", email));
                    cmd.Parameters.Add(new OracleParameter("dc", diaChi));
                    cmd.Parameters.Add(new OracleParameter("maDocGia", _maDocGiaHienTai));

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("✅ Cập nhật độc giả thành công!", "Thông báo");
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Không có thay đổi nào được lưu.", "Thông báo");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi cập nhật CSDL:\n" + ex.Message, "Lỗi SQL");
            }
            finally
            {
                Database.Close();
            }
        }

        private void btn_huy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_TK_Click(object sender, EventArgs e)
        {
            string maDocGia = txt_timkiem.Text.Trim();

            if (string.IsNullOrEmpty(maDocGia))
            {
                MessageBox.Show("Vui lòng nhập mã độc giả cần tìm.", "Thông báo");
                return;
            }

            LoadData(maDocGia);
        }
        // Trong file suathongtindocgia.cs
        public bool IsReadOnlyMode
        {
            set
            {
                // Ví dụ:
                txt_tendocgia.ReadOnly = value;
                // ... (khóa tất cả các điều khiển nhập liệu)

                btn_luu.Visible = !value; // Ẩn nút lưu
                btn_huy.Visible = !value; // Ẩn nút hủy
            }
        }
    }
}
