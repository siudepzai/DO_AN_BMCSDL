using _40_caesarOracle;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace DO_AN_BMCSDL.Phan_GUI
{
    public partial class QL_the : Form
    {
        
        private bool _isAddingNew = false;
        private const string KEY_DES = "HUITCNTT";
       
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
               
                Database.Set_Database("localhost", "1521", "ORCL", "C##DO_AN", "12345");
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show("Lỗi thiết lập thông tin DB: " + ex.Message, "Lỗi nghiêm trọng");
                return;
            }

            dgv_thongtinthe.Font = new Font("Times New Roman", 12, FontStyle.Regular);
            dgv_thongtinthe.ColumnHeadersDefaultCellStyle.Font = new Font("Times New Roman", 12, FontStyle.Bold);
            dgv_thongtinthe.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            LoadDataTheDocGia();
            dgv_thongtinthe.CellClick += dgv_thongtinthe_CellClick;

            LoadDataTheDocGia();

            SetFormMode(false);
        }

        private void SetFormMode(bool isEditable)
        {
            _isAddingNew = isEditable;

           
            txt_masothe.ReadOnly = true;
            txt_hoten.ReadOnly = true;

            txt_tinhtrang.ReadOnly = !isEditable; 

            if (!_isAddingNew)
            {
                txt_diachi.ReadOnly = false;
                txt_vaitro.ReadOnly = false;
            }
            else
            {
                
                txt_masothe.ReadOnly = false;
                txt_hoten.ReadOnly = false;
                txt_diachi.ReadOnly = true;
                txt_vaitro.ReadOnly = true;
            }

           
            btn_capnhat.Text = _isAddingNew ? "Lưu Thẻ" : "Cập nhật";

         }

        private void ClearFormForNewEntry()
        {
            txt_masothe.Clear();
            txt_hoten.Clear();
            txt_diachi.Clear();
            txt_vaitro.Clear();
            txt_tinhtrang.Text = "Chưa cấp"; 
        }

        private void btn_capnhat_Click(object sender, EventArgs e)
        {
            if (_isAddingNew)
            {
               
                HandleInsertNewThe();
            }
            else
            {
                
                HandleUpdateThe();
            }
        }

        private void HandleInsertNewThe()
        {
            string maThe = txt_masothe.Text.Trim();
            string maTV = txt_hoten.Text.Trim();
            string tinhTrang = txt_tinhtrang.Text.Trim();
            string hanSD = DateTime.Now.AddYears(5).ToString("yyyy-MM-dd"); 

            if (string.IsNullOrEmpty(maThe) || string.IsNullOrEmpty(maTV))
            {
                MessageBox.Show("Mã số thẻ và Mã thành viên (ô Họ tên) không được để trống.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!CheckBeforeInsert(maThe, maTV))
            {
                return;
            }

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
                SetFormMode(false); 
            }
        }

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
                int countThe = Convert.ToInt32(Database.ExecuteQuery(sqlCheckThe, new OracleParameter("maThe", maThe)).Rows[0][0]);
                if (countThe > 0)
                {
                    MessageBox.Show($"Lỗi: Mã số thẻ '{maThe}' đã tồn tại.", "Lỗi trùng lặp");
                    return false;
                }

                Database.Close();
                if (!Database.Connect())
                {
                    MessageBox.Show("Mất kết nối giữa các truy vấn.", "Lỗi kết nối");
                    return false;
                }

               
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

        private void HandleUpdateThe()
        {
            string maSoThe = txt_masothe.Text.Trim();
            string tinhTrang = txt_tinhtrang.Text.Trim();
            string diaChi = Encrypt_DES(txt_diachi.Text.Trim(), KEY_DES);
            string vaiTro = txt_vaitro.Text.Trim();


            if (string.IsNullOrEmpty(maSoThe))
            {
                MessageBox.Show("Vui lòng chọn Mã số thẻ cần cập nhật.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            
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
                   
                    using (OracleCommand cmdDocGia = new OracleCommand(sqlDocGia, Database.Get_Connection()))
                    {
                        cmdDocGia.Parameters.Add(new OracleParameter("diaChiMoi", diaChi));
                        cmdDocGia.Parameters.Add(new OracleParameter("vaiTroMoi", vaiTro));
                        cmdDocGia.Parameters.Add(new OracleParameter("maTheCu", maSoThe));

                        cmdDocGia.ExecuteNonQuery();
                    }

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



        private void dgv_thongtinthe_CellClick(object sender, DataGridViewCellEventArgs e)
        {
         
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

                   
                    SetFormMode(false); 
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
                    foreach (DataRow r in dt.Rows)
                    {
                        try
                        {
                            r["Dia chi"] = Encrypt_DES(r["Dia chi"].ToString(), KEY_DES);
                        }
                        catch { }
                    }

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
        private void LoadDataGoc()
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
        ORDER BY T2.MASOTHE";

            if (!Database.Connect())
            {
                MessageBox.Show("Không kết nối được Oracle.");
                return;
            }

            DataTable dt = Database.ExecuteQuery(sql);
            
            foreach (DataRow r in dt.Rows)
            {
                try
                {
                    r["Dia chi"] = Decrypt_DES(r["Dia chi"].ToString(), KEY_DES);
                }
                catch { }
            }

            dgv_thongtinthe.DataSource = dt;
        }

        private void btn_thongtingoc_Click(object sender, EventArgs e)
        {
            string mk = Microsoft.VisualBasic.Interaction.InputBox(
        "Nhập mật khẩu để xem địa chỉ gốc:", "Xác thực", "");

            if (mk != "HUITCNTT") 
            {
                MessageBox.Show("Sai mật khẩu! Không thể xem thông tin gốc.");
                return;
            }

            LoadDataGoc();
        }
        // hàm mã hóa và giải mã
        public static string Encrypt_DES(string plainText, string key)
        {
            using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
            {
                des.Key = Encoding.UTF8.GetBytes(key);
                des.IV = Encoding.UTF8.GetBytes(key);

                byte[] data = Encoding.UTF8.GetBytes(plainText);

                using (var ms = new MemoryStream())
                using (var cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(data, 0, data.Length);
                    cs.FlushFinalBlock();
                    return Convert.ToBase64String(ms.ToArray());
                }
            }
        }

        public static string Decrypt_DES(string cipherText, string key)
        {
            using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
            {
                des.Key = Encoding.UTF8.GetBytes(key);
                des.IV = Encoding.UTF8.GetBytes(key);

                byte[] buffer = Convert.FromBase64String(cipherText);

                using (var ms = new MemoryStream())
                using (var cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(buffer, 0, buffer.Length);
                    cs.FlushFinalBlock();
                    return Encoding.UTF8.GetString(ms.ToArray());
                }
            }
        }

        private void dgv_thongtinthe_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}