using _40_caesarOracle;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Oracle.ManagedDataAccess.Client;

namespace DO_AN_BMCSDL.Phan_GUI
{
    public partial class QL_the : Form
    {
        
        private bool _isAddingNew = false;


        public QL_the()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e) { }
        private void label2_Click(object sender, EventArgs e) { }

        private void btn_thoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void QL_the_Load(object sender, EventArgs e)
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

            // Thiết lập Font và Style cho DataGridView
            dgv_thongtinthe.Font = new Font("Times New Roman", 12, FontStyle.Regular);
            dgv_thongtinthe.ColumnHeadersDefaultCellStyle.Font = new Font("Times New Roman", 12, FontStyle.Bold);
            dgv_thongtinthe.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            // Tải dữ liệu lần đầu (Không cần tham số, dùng mặc định)
            LoadDataTheDocGia();
            // Gán sự kiện CellClick
            dgv_thongtinthe.CellClick += dgv_thongtinthe_CellClick;

            LoadDataTheDocGia();

            // Thiết lập trạng thái ban đầu: Chỉ xem/chỉnh sửa
            SetFormMode(false);
        }

        // --- HÀM THIẾT LẬP TRẠNG THÁI FORM (Mở khóa Địa chỉ, Vai trò, Tình trạng) ---
        private void SetFormMode(bool isEditable)
        {
            _isAddingNew = isEditable;

            //  Khóa các trường KHÔNG THỂ sửa (Mã thẻ, Mã TV/Họ tên)
            txt_masothe.ReadOnly = true;
            txt_hoten.ReadOnly = true;

            //  Mở khóa các trường CÓ THỂ sửa
            txt_tinhtrang.ReadOnly = !isEditable; // Mở khóa Tình trạng

            // Chế độ Cập nhật/Xem thẻ cũ
            if (!_isAddingNew)
            {
                txt_diachi.ReadOnly = false;
                txt_vaitro.ReadOnly = false;
            }
            else
            {
                // Chế độ Thêm mới: Chỉ mở Mã thẻ và Họ tên (Mã TV)
                txt_masothe.ReadOnly = false;
                txt_hoten.ReadOnly = false;
                txt_diachi.ReadOnly = true;
                txt_vaitro.ReadOnly = true;
            }

            // Cập nhật nút dựa trên trạng thái
            btn_capnhat.Text = _isAddingNew ? "Lưu Thẻ" : "Cập nhật";

         }

        // --- HÀM DỌN DẸP FORM CHO THÊM MỚI ---
        private void ClearFormForNewEntry()
        {
            txt_masothe.Clear();
            txt_hoten.Clear();
            txt_diachi.Clear();
            txt_vaitro.Clear();
            txt_tinhtrang.Text = "Chưa cấp"; // Giá trị mặc định
        }

        // 🔹 Nút CẬP NHẬT/LƯU
        private void btn_capnhat_Click(object sender, EventArgs e)
        {
            if (_isAddingNew)
            {
                // THỰC HIỆN INSERT (LƯU THẺ MỚI)
                HandleInsertNewThe();
            }
            else
            {
                // THỰC HIỆN UPDATE (CẬP NHẬT THẺ CŨ)
                HandleUpdateThe();
            }
        }

        // --- LOGIC INSERT THẺ MỚI (Kiểm tra trùng Mã thẻ) ---
        private void HandleInsertNewThe()
        {
            string maThe = txt_masothe.Text.Trim();
            string maTV = txt_hoten.Text.Trim();
            string tinhTrang = txt_tinhtrang.Text.Trim();
            string hanSD = DateTime.Now.AddYears(5).ToString("yyyy-MM-dd"); // Giả định hạn sử dụng 5 năm

            if (string.IsNullOrEmpty(maThe) || string.IsNullOrEmpty(maTV))
            {
                MessageBox.Show("Mã số thẻ và Mã thành viên (ô Họ tên) không được để trống.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!CheckBeforeInsert(maThe, maTV))
            {
                return;
            }

            // Câu truy vấn INSERT vào THEBANDOC
            string sql = @"INSERT INTO THEBANDOC (MASOTHE, MATHANHVIEN, TINHTRANGTHE, HANSUDUNG)
                           VALUES (:maThe, :maTV, :tinhTrang, :hanSD)";

            try
            {
                if (Database.Connect())
                {
                    using (OracleCommand cmd = new OracleCommand(sql, Database.Get_Connection()))
                    {
                        cmd.Parameters.Add(new OracleParameter("maThe", maThe));
                        cmd.Parameters.Add(new OracleParameter("maTV", maTV));
                        cmd.Parameters.Add(new OracleParameter("tinhTrang", tinhTrang));
                        cmd.Parameters.Add(new OracleParameter("hanSD", hanSD));

                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Thêm thẻ độc giả thành công!", "Thành công");
                        LoadDataTheDocGia();
                    }
                }
            }
            catch (OracleException ex)
            {
                MessageBox.Show("Lỗi CSDL khi thêm thẻ: " + ex.Message, "Lỗi SQL");
            }
            finally
            {
                Database.Close();
                SetFormMode(false); // Quay về chế độ mặc định sau khi lưu
            }
        }

        // --- HÀM KIỂM TRA TRƯỚC KHI INSERT ---
        private bool CheckBeforeInsert(string maThe, string maTV)
        {
            string sqlCheckThe = "SELECT COUNT(*) FROM THEBANDOC WHERE TRIM(MASOTHE) = :maThe";
            string sqlCheckTV = "SELECT COUNT(*) FROM DOCGIA WHERE TRIM(MATHANHVIEN) = :maTV";

            if (!Database.Connect())
            {
                MessageBox.Show("Không thể kết nối CSDL để kiểm tra trùng lặp.", "Lỗi kết nối");
                return false;
            }

            try
            {
                // Kiểm tra trùng Mã số thẻ
                int countThe = Convert.ToInt32(Database.ExecuteQuery(sqlCheckThe, new OracleParameter("maThe", maThe)).Rows[0][0]);
                if (countThe > 0)
                {
                    MessageBox.Show($"Lỗi: Mã số thẻ '{maThe}' đã tồn tại.", "Lỗi trùng lặp");
                    return false;
                }

                // Đóng và mở lại kết nối để thực hiện truy vấn thứ hai an toàn hơn
                Database.Close();
                if (!Database.Connect())
                {
                    MessageBox.Show("Mất kết nối giữa các truy vấn.", "Lỗi kết nối");
                    return false;
                }

                // Kiểm tra Mã thành viên có tồn tại trong DOCGIA không
                int countTV = Convert.ToInt32(Database.ExecuteQuery(sqlCheckTV, new OracleParameter("maTV", maTV)).Rows[0][0]);
                if (countTV == 0)
                {
                    MessageBox.Show($"Lỗi: Mã thành viên '{maTV}' không tồn tại trong danh sách độc giả.", "Lỗi Khóa ngoại");
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kiểm tra trùng lặp: " + ex.Message, "Lỗi");
                return false;
            }
            finally
            {
                Database.Close();
            }
            return true;
        }

        // --- LOGIC UPDATE THẺ CŨ ---
        private void HandleUpdateThe()
        {
            string maSoThe = txt_masothe.Text.Trim();
            string tinhTrang = txt_tinhtrang.Text.Trim();
            string diaChi = txt_diachi.Text.Trim();
            string vaiTro = txt_vaitro.Text.Trim();


            if (string.IsNullOrEmpty(maSoThe))
            {
                MessageBox.Show("Vui lòng chọn Mã số thẻ cần cập nhật.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Câu truy vấn UPDATE: Cập nhật DOCGIA (Địa chỉ, Vai trò) và THEBANDOC (Tình trạng)
            string sqlDocGia = @"UPDATE DOCGIA T1 
                           SET T1.DIACHI = :diaChiMoi, 
                               T1.VAITRO = :vaiTroMoi 
                           WHERE T1.MATHANHVIEN = (SELECT T2.MATHANHVIEN FROM THEBANDOC T2 WHERE TRIM(T2.MASOTHE) = :maTheCu)";

            string sqlThe = @"UPDATE THEBANDOC SET 
                              TINHTRANGTHE = :tinhTrangMoi 
                              WHERE TRIM(MASOTHE) = :maTheCu";


            try
            {
                if (Database.Connect())
                {
                    // 1. Cập nhật DOCGIA (Địa chỉ, Vai trò)
                    using (OracleCommand cmdDocGia = new OracleCommand(sqlDocGia, Database.Get_Connection()))
                    {
                        cmdDocGia.Parameters.Add(new OracleParameter("diaChiMoi", diaChi));
                        cmdDocGia.Parameters.Add(new OracleParameter("vaiTroMoi", vaiTro));
                        cmdDocGia.Parameters.Add(new OracleParameter("maTheCu", maSoThe));

                        cmdDocGia.ExecuteNonQuery();
                    }

                    // 2. Cập nhật THEBANDOC (Tình trạng)
                    Database.Close();
                    if (!Database.Connect())
                    {
                        MessageBox.Show("Lỗi cập nhật: Mất kết nối CSDL giữa các truy vấn.", "Lỗi");
                        return;
                    }

                    using (OracleCommand cmdThe = new OracleCommand(sqlThe, Database.Get_Connection()))
                    {
                        cmdThe.Parameters.Add(new OracleParameter("tinhTrangMoi", tinhTrang));
                        cmdThe.Parameters.Add(new OracleParameter("maTheCu", maSoThe));

                        int rowsAffected = cmdThe.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Cập nhật thông tin thẻ thành công!", "Thành công");
                            LoadDataTheDocGia();
                        }
                        else
                        {
                            MessageBox.Show("Mã số thẻ không tồn tại hoặc không có thay đổi nào được thực hiện.", "Thông báo");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi cập nhật CSDL: " + ex.Message, "Lỗi SQL");
            }
            finally
            {
                Database.Close();
                SetFormMode(false);
            }
        }


        // --- CÁC HÀM XỬ LÝ KHÁC ---

        private void dgv_thongtinthe_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Chỉ cho phép click để xem/sửa nếu không ở chế độ Thêm mới
            if (_isAddingNew) return;

            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgv_thongtinthe.Rows[e.RowIndex];

                try
                {
                    txt_masothe.Text = row.Cells["Ma so the"].Value.ToString().Trim();
                    txt_hoten.Text = row.Cells["Ho ten"].Value.ToString().Trim();
                    txt_diachi.Text = row.Cells["Dia chi"].Value.ToString().Trim();
                    txt_vaitro.Text = row.Cells["Vai tro"].Value.ToString().Trim();
                    txt_tinhtrang.Text = row.Cells["Tinh trang"].Value.ToString().Trim();

                    // Chuyển Form về chế độ chỉnh sửa/xem
                    SetFormMode(false); // Gọi SetFormMode(false) để khóa Mã thẻ/Họ tên nhưng mở khóa Địa chỉ/Vai trò/Tình trạng
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi điền dữ liệu vào TextBox: " + ex.Message, "Lỗi");
                }
            }
        }

        private void btn_xoa_Click(object sender, EventArgs e)
        {
            string maSoThe = txt_masothe.Text.Trim();

            if (string.IsNullOrEmpty(maSoThe))
            {
                MessageBox.Show("Vui lòng chọn thẻ cần xóa.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show(
                $"Bạn có chắc chắn muốn xóa thẻ có Mã số: {maSoThe} không? (Lưu ý: Nếu thẻ đang có giao dịch liên quan, việc xóa có thể thất bại).",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                string sql = "DELETE FROM THEBANDOC WHERE TRIM(MASOTHE) = :maThe";

                try
                {
                    if (Database.Connect())
                    {
                        using (OracleCommand cmd = new OracleCommand(sql, Database.Get_Connection()))
                        {
                            cmd.Parameters.Add(new OracleParameter("maThe", maSoThe));

                            int rowsAffected = cmd.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Xóa thẻ thành công!", "Thành công");

                                txt_masothe.Clear();
                                txt_hoten.Clear();
                                txt_diachi.Clear();
                                txt_vaitro.Clear();
                                txt_tinhtrang.Clear();

                                LoadDataTheDocGia();
                            }
                            else
                            {
                                MessageBox.Show("Không tìm thấy Mã số thẻ cần xóa hoặc thẻ không tồn tại.", "Thông báo");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    if (ex.Message.Contains("integrity constraint") && ex.Message.Contains("violated"))
                    {
                        MessageBox.Show($"Lỗi Khóa ngoại: Không thể xóa thẻ '{maSoThe}' vì nó đang liên quan đến các dữ liệu khác (ví dụ: Phiếu mượn). Vui lòng xử lý dữ liệu liên quan trước.", "Lỗi nghiêm trọng", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        MessageBox.Show("Lỗi xóa CSDL: " + ex.Message, "Lỗi SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                finally
                {
                    Database.Close();
                }
            }
        }


        private void LoadDataTheDocGia(string searchTerm = "")
        {
            string sql = @"
                SELECT 
                    ROWNUM AS STT, 
                    TRIM(T2.MASOTHE) AS ""Ma so the"", 
                    TRIM(T1.TENTV) AS ""Ho ten"", 
                    TRIM(T1.DIACHI) AS ""Dia chi"", 
                    TRIM(T1.VAITRO) AS ""Vai tro"", 
                    TRIM(T2.TINHTRANGTHE) AS ""Tinh trang""
                FROM DOCGIA T1
                JOIN THEBANDOC T2 ON T1.MATHANHVIEN = T2.MATHANHVIEN
                WHERE (LOWER(TRIM(T2.MASOTHE)) LIKE '%' || :searchTerm || '%' OR
                       LOWER(TRIM(T1.TENTV)) LIKE '%' || :searchTerm || '%')
                ORDER BY T2.MASOTHE";

            try
            {
                if (Database.Connect())
                {
                    // Tham số tìm kiếm
                    DataTable dt = Database.ExecuteQuery(sql, new OracleParameter("searchTerm", searchTerm.ToLower()));
                    dgv_thongtinthe.DataSource = dt;

                    // Cập nhật HeaderText hiển thị Tiếng Việt
                    dgv_thongtinthe.Columns["Ma so the"].HeaderText = "MÃ SỐ THẺ";
                    dgv_thongtinthe.Columns["Ho ten"].HeaderText = "HỌ TÊN";
                    dgv_thongtinthe.Columns["Dia chi"].HeaderText = "ĐỊA CHỈ";
                    dgv_thongtinthe.Columns["Vai tro"].HeaderText = "VAI TRÒ";
                    dgv_thongtinthe.Columns["Tinh trang"].HeaderText = "TÌNH TRẠNG";

                    // Căn giữa các cột
                    foreach (DataGridViewColumn col in dgv_thongtinthe.Columns)
                    {
                        col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    }
                }
                else
                {
                    MessageBox.Show("Không thể kết nối đến Oracle.", "Lỗi kết nối");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu Thẻ độc giả:\n" + ex.Message, "Lỗi SQL");
            }
            finally
            {
                Database.Close();
            }
        }

        private void btn_TK_Click(object sender, EventArgs e)
        {
            // Giả định tên TextBox tìm kiếm là txt_timkiem
            if (txt_timkiem == null)
            {
                MessageBox.Show("Lỗi: Không tìm thấy TextBox tìm kiếm (txt_timkiem).", "Lỗi hệ thống");
                return;
            }
            string searchTerm = txt_timkiem.Text.Trim();
            LoadDataTheDocGia(searchTerm);
        }

        private void btn_X_Click(object sender, EventArgs e)
        {
            txt_masothe.Clear();
            txt_hoten.Clear();
            txt_timkiem.Clear();
            txt_vaitro.Clear();
            txt_diachi.Clear();
            txt_tinhtrang.Clear();
            LoadDataTheDocGia();
        }
    }
}