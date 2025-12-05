using _40_caesarOracle;
using DO_AN_BMCSDL.Phan_xu_ly;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using System.Text;
using DO_AN_BMCSDL.Phan_GUI;

namespace DO_AN_BMCSDL.Phan_GUI
{
    public partial class QL_tailieu : Form
    {

        private bool _isAddingNew = false;
        private byte[] _aesKey; // Khóa AES (Giải mã) chung
        private byte[] _aesIV;  // IV (Giải mã) chung
        private string _rsaPublicKey;
        private string _rsaPrivateKey;

        public QL_tailieu()
        {
            InitializeComponent();
        }

        private void QL_tailieu_Load(object sender, EventArgs e)
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

            dgv_tailieu.Font = new Font("Times New Roman", 12, FontStyle.Regular);
            dgv_tailieu.ColumnHeadersDefaultCellStyle.Font = new Font("Times New Roman", 12, FontStyle.Bold);
            dgv_tailieu.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;


            dgv_tailieu.CellClick += dgv_tailieu_CellClick;

            // Tải khóa RSA và Khóa AES chung
            if (!LoadRSATKeys())
            {
                MessageBox.Show("Không thể tải khóa RSA hoặc Khóa AES chung. Vui lòng kiểm tra bảng SECURITY_INFO.", "Lỗi Khóa RSA/AES");
            }

            LoadDataTaiLieu();


            SetFormMode(false);
        }

        // --- HÀM ĐÃ SỬA: TẠO, LƯU VÀ CHUYỂN ĐỔI DỮ LIỆU TÊN TÁC GIẢ SANG AES ---
        private void CreateAndSaveAesKey(string publicKey, string privateKey)
        {
            // 1. Tạo Khóa AES và IV mới
            (byte[] newAesKey, byte[] newAesIV) = Thuat_toan_AES.GenerateAES();

            // 2. Mã hóa Khóa AES bằng RSA Public Key
            Encrypt_RSA encryptRsa = new Encrypt_RSA(publicKey);
            string aesKeyBase64 = Convert.ToBase64String(newAesKey);
            string encryptedKeyBase64 = encryptRsa.Encrypt(aesKeyBase64);
            string ivBase64 = Convert.ToBase64String(newAesIV);

            // 3. XÓA KHÓA CŨ VÀ LƯU KHÓA AES MỚI VÀO SECURITY_INFO
            string sqlDeleteOldKeys = "DELETE FROM SECURITY_INFO WHERE TRIM(SECURITY_TYPE) IN ('AES_KEY_RSA', 'AES_IV')";
            string sqlInsertKey = "INSERT INTO SECURITY_INFO (SECURITY_TYPE, SECURITY_KEY) VALUES ('AES_KEY_RSA', :key_rsa)";
            string sqlInsertIV = "INSERT INTO SECURITY_INFO (SECURITY_TYPE, SECURITY_KEY) VALUES ('AES_IV', :iv_base64)";

            // SQL này dùng TENTACGIA (cột gốc) để mã hóa lại bằng AES
            string sqlSelect = "SELECT TRIM(MATAILIEU) AS MATAILIEU, TRIM(TENTACGIA) AS TENTACGIA_ORIGINAL FROM TAILIEU";
            string sqlUpdate = "UPDATE TAILIEU SET TENTACGIA_ENC = HEXTORAW(:encrypted_tacgia) WHERE TRIM(MATAILIEU) = :ma";

            try
            {
                if (Database.Connect())
                {
                    // Xóa khóa AES/IV cũ (nếu có)
                    Database.ExecuteNonQuery(sqlDeleteOldKeys);

                    // Lưu khóa AES/IV mới
                    Database.ExecuteNonQuery(sqlInsertKey, new OracleParameter("key_rsa", encryptedKeyBase64));
                    Database.ExecuteNonQuery(sqlInsertIV, new OracleParameter("iv_base64", ivBase64));

                    // COMMIT để lưu khóa mới trước
                    Database.ExecuteNonQuery("COMMIT");


                    // --- BƯỚC 4: MÃ HÓA LẠI DỮ LIỆU TENTACGIA TỪ GỐC SANG AES THỰC TẾ ---

                    DataTable dtTaiLieu = Database.ExecuteQuery(sqlSelect);

                    foreach (DataRow row in dtTaiLieu.Rows)
                    {
                        string maTL = row["MATAILIEU"].ToString();
                        string originalTacGia = row["TENTACGIA_ORIGINAL"].ToString().Trim();

                        if (string.IsNullOrEmpty(originalTacGia)) continue;

                        // Mã hóa bằng AES
                        byte[] encryptedTacGiaBytes = Thuat_toan_AES.EncryptAES(originalTacGia, newAesKey, newAesIV);
                        string encryptedTacGiaHex = BytesToHex(encryptedTacGiaBytes);

                        // Cập nhật TENTACGIA_ENC với giá trị đã mã hóa AES
                        using (OracleCommand cmd = new OracleCommand(sqlUpdate, Database.Get_Connection()))
                        {
                            cmd.Parameters.Add(new OracleParameter("encrypted_tacgia", encryptedTacGiaHex));
                            cmd.Parameters.Add(new OracleParameter("ma", maTL));
                            cmd.ExecuteNonQuery();
                        }
                    }

                    // COMMIT để lưu các thay đổi UPDATE dữ liệu
                    Database.ExecuteNonQuery("COMMIT");

                    MessageBox.Show("Đã tạo và lưu Khóa AES chung mới thành công. Dữ liệu Tên Tác Giả đã được mã hóa lại bằng AES.", "Thông báo");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi CSDL khi tạo/lưu/chuyển đổi Khóa AES chung: " + ex.Message, "Lỗi SQL");
            }
            finally
            {
                Database.Close();
            }
        }

        // --- PHƯƠNG THỨC ĐÃ SỬA: TẢI KHÓA RSA VÀ KHÓA AES CHUNG (Truyền privateKey) ---
        private bool LoadRSATKeys()
        {
            string sql = "SELECT SECURITY_KEY FROM SECURITY_INFO WHERE SECURITY_TYPE = :type";
            string sqlAes = "SELECT SECURITY_KEY FROM SECURITY_INFO WHERE TRIM(SECURITY_TYPE) = 'AES_KEY_RSA'";
            string sqlIv = "SELECT SECURITY_KEY FROM SECURITY_INFO WHERE TRIM(SECURITY_TYPE) = 'AES_IV'";
            bool success = true;

            string rsaPrivateKeyLocal = null;
            string rsaPublicKeyLocal = null;

            try
            {
                if (Database.Connect())
                {
                    // TẠM THỜI: Buộc xóa khóa AES/IV cũ để kích hoạt lại quá trình migration
                    // XÓA DÒNG NÀY SAU KHI DỮ LIỆU ĐƯỢC MÃ HÓA THÀNH CÔNG!
                    Database.ExecuteNonQuery("DELETE FROM SECURITY_INFO WHERE TRIM(SECURITY_TYPE) IN ('AES_KEY_RSA', 'AES_IV')");
                    Database.ExecuteNonQuery("COMMIT");

                    // 1. Tải Public/Private Key
                    DataTable dtPrivate = Database.ExecuteQuery(sql, new OracleParameter("type", "RSA_PRIVATE"));
                    if (dtPrivate.Rows.Count > 0) rsaPrivateKeyLocal = dtPrivate.Rows[0]["SECURITY_KEY"].ToString().Trim();
                    else success = false;

                    DataTable dtPublic = Database.ExecuteQuery(sql, new OracleParameter("type", "RSA_PUBLIC"));
                    if (dtPublic.Rows.Count > 0) rsaPublicKeyLocal = dtPublic.Rows[0]["SECURITY_KEY"].ToString().Trim();
                    else success = false;

                    if (!success) return false; // Không có cặp khóa RSA thì không thể tiếp tục

                    _rsaPrivateKey = rsaPrivateKeyLocal;
                    _rsaPublicKey = rsaPublicKeyLocal;

                    // 2. Tải Khóa AES (RSA Encrypted) và IV
                    DataTable dtAesKeyRsa = Database.ExecuteQuery(sqlAes);
                    DataTable dtAesIv = Database.ExecuteQuery(sqlIv);

                    bool aesKeyExists = dtAesKeyRsa.Rows.Count > 0;
                    bool aesIvExists = dtAesIv.Rows.Count > 0;

                    if (!aesKeyExists || !aesIvExists)
                    {
                        // Khóa chung chưa được tạo, tiến hành tạo và lưu vào DB
                        CreateAndSaveAesKey(_rsaPublicKey, _rsaPrivateKey);

                        // Sau khi tạo, tải lại dữ liệu khóa (để gán vào _aesKey và _aesIV)
                        dtAesKeyRsa = Database.ExecuteQuery(sqlAes);
                        dtAesIv = Database.ExecuteQuery(sqlIv);
                    }

                    // 3. Giải mã Khóa AES bằng RSA Private Key (Chỉ xảy ra 1 lần)
                    if (dtAesKeyRsa.Rows.Count > 0 && dtAesIv.Rows.Count > 0)
                    {
                        string encryptedKeyBase64 = dtAesKeyRsa.Rows[0]["SECURITY_KEY"].ToString().Trim();
                        string ivBase64 = dtAesIv.Rows[0]["SECURITY_KEY"].ToString().Trim();

                        Decrypt_RSA decryptRsa = new Decrypt_RSA(_rsaPrivateKey);
                        string decryptedKeyBase64 = decryptRsa.Decrypt(encryptedKeyBase64);

                        _aesKey = Convert.FromBase64String(decryptedKeyBase64);
                        _aesIV = Convert.FromBase64String(ivBase64);

                        return true;
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải/giải mã Khóa chung: " + ex.Message, "Lỗi SQL/Crypto");
                success = false;
            }
            finally
            {
                Database.Close();
            }
            return success;
        }

        // --- PHƯƠNG THỨC GIỮ NGUYÊN: LOAD DATA (Dùng khóa chung) ---
        private void LoadDataTaiLieu(string searchTerm = "")
        {
            string sql = @"
                SELECT 
                    ROWNUM AS STT, 
                    TRIM(MATAILIEU) AS ""Ma tai lieu"", 
                    TRIM(TENSACH) AS ""Ten tai lieu"", 
                    TRIM(NGONNGU) AS ""Ngon ngu"", 
                    PHIMUON AS ""Chi phi"", 
                    TENTACGIA_ENC, -- Chỉ lấy cột mã hóa
                    TRIM(THELOAI) AS ""The loai"", 
                    TRIM(TINHTRANG) AS ""Tinh trang""
                FROM TAILIEU
                WHERE (LOWER(TRIM(MATAILIEU)) LIKE '%' || :searchTerm || '%' OR
                       LOWER(TRIM(TENSACH)) LIKE '%' || :searchTerm || '%')
                ORDER BY MATAILIEU";

            try
            {
                if (Database.Connect())
                {
                    DataTable dt = Database.ExecuteQuery(sql, new OracleParameter("searchTerm", searchTerm.ToLower()));

                    // --- BƯỚC 1: GIẢI MÃ TÊN TÁC GIẢ BẰNG AES KHÓA CHUNG ---
                    // Kiểm tra xem khóa chung đã được tải và giải mã thành công chưa
                    if (dt.Rows.Count > 0 && _aesKey != null && _aesIV != null)
                    {
                        dt.Columns.Add("Tac gia", typeof(string));

                        foreach (DataRow row in dt.Rows)
                        {
                            string encryptedTacGiaHex = row["TENTACGIA_ENC"].ToString();

                            if (!string.IsNullOrEmpty(encryptedTacGiaHex))
                            {
                                try
                                {
                                    // SỬ DỤNG KHÓA VÀ IV CHUNG ĐÃ GIẢI MÃ TỪ _aesKey, _aesIV
                                    byte[] cipherText = HexToBytes(encryptedTacGiaHex);
                                    string decryptedTacGia = Thuat_toan_AES.DecryptAES(cipherText, _aesKey, _aesIV);

                                    row["Tac gia"] = decryptedTacGia;
                                }
                                catch (Exception ex)
                                {
                                    // Báo lỗi giải mã (có thể do dữ liệu cũ còn sót, hoặc lỗi khóa)
                                    row["Tac gia"] = "LỖI GIẢ MÃ";
                                }
                            }
                            else
                            {
                                row["Tac gia"] = "Chưa mã hóa";
                            }
                        }
                    }

                    dgv_tailieu.DataSource = dt;

                    // Ẩn các cột không cần thiết
                    if (dgv_tailieu.Columns.Contains("TENTACGIA_ENC")) dgv_tailieu.Columns["TENTACGIA_ENC"].Visible = false;

                    if (dgv_tailieu.Columns.Contains("Tac gia"))
                    {
                        dgv_tailieu.Columns["Tac gia"].HeaderText = "TÁC GIẢ";
                    }

                    dgv_tailieu.Columns["Ma tai lieu"].HeaderText = "MÃ TÀI LIỆU";
                    dgv_tailieu.Columns["Ten tai lieu"].HeaderText = "TÊN TÀI LIỆU";
                    dgv_tailieu.Columns["Ngon ngu"].HeaderText = "NGÔN NGỮ";
                    dgv_tailieu.Columns["Chi phi"].HeaderText = "CHI PHÍ";
                    dgv_tailieu.Columns["The loai"].HeaderText = "THỂ LOẠI";
                    dgv_tailieu.Columns["Tinh trang"].HeaderText = "TÌNH TRẠNG";

                    foreach (DataGridViewColumn col in dgv_tailieu.Columns)
                    {
                        col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu Tài liệu:\n" + ex.Message, "Lỗi SQL");
            }
            finally
            {
                Database.Close();
            }
        }

        // --- PHƯƠNG THỨC CHUYỂN ĐỔI GIỮ NGUYÊN ---
        private static byte[] HexToBytes(string hex)
        {
            if (hex.Length % 2 != 0)
            {
                hex = "0" + hex;
            }
            byte[] bytes = new byte[hex.Length / 2];
            for (int i = 0; i < hex.Length; i += 2)
            {
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            }
            return bytes;
        }

        private static string BytesToHex(byte[] bytes)
        {
            return BitConverter.ToString(bytes).Replace("-", "");
        }
        // --- END PHƯƠNG THỨC CHUYỂN ĐỔI ---

        // --- PHẦN LOGIC THAO TÁC CƠ SỞ DỮ LIỆU ---

        private void SetFormMode(bool isAdding)
        {
            _isAddingNew = isAdding;

            txt_matailieu.ReadOnly = !isAdding;

            txt_tentailieu.ReadOnly = false;
            txt_ngonngu.ReadOnly = false;
            txt_chiphi.ReadOnly = false;
            txt_tinhtrang.ReadOnly = false;
            txt_tentacgia.ReadOnly = false;
            txt_theloai.ReadOnly = false;

            btn_capnhattailieu.Text = isAdding ? "Lưu" : "Cập nhật";
        }

        private void ClearFormControls()
        {
            txt_matailieu.Clear();
            txt_tentailieu.Clear();
            txt_ngonngu.Clear();
            txt_chiphi.Clear();
            txt_tinhtrang.Clear();
            txt_tentacgia.Clear();
            txt_theloai.Clear();
        }

        private void dgv_tailieu_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (_isAddingNew) return;

            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgv_tailieu.Rows[e.RowIndex];

                try
                {
                    txt_matailieu.Text = row.Cells["Ma tai lieu"].Value.ToString();
                    txt_tentailieu.Text = row.Cells["Ten tai lieu"].Value.ToString();
                    txt_ngonngu.Text = row.Cells["Ngon ngu"].Value.ToString();
                    txt_chiphi.Text = row.Cells["Chi phi"].Value.ToString();
                    txt_tentacgia.Text = row.Cells["Tac gia"].Value.ToString();
                    txt_theloai.Text = row.Cells["The loai"].Value.ToString();
                    txt_tinhtrang.Text = row.Cells["Tinh trang"].Value.ToString();

                    SetFormMode(false);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi điền dữ liệu vào TextBox: Vui lòng kiểm tra lại tên cột trong SQL. Chi tiết: " + ex.Message, "Lỗi");
                }
            }
        }

        private void btn_them_Click(object sender, EventArgs e)
        {
            ClearFormControls();
            SetFormMode(true);
            txt_matailieu.Focus();
        }

        private void btn_capnhat_Click(object sender, EventArgs e)
        {
            if (_isAddingNew)
            {
                HandleInsert();
            }
            else
            {
                HandleUpdate();
            }
        }

        // --- PHƯƠNG THỨC GIỮ NGUYÊN: XỬ LÝ MÃ HÓA (CHỈ TRẢ VỀ CIPHERTEXT) ---
        private string EncryptHybrid(string plaintext)
        {
            if (_aesKey == null || _aesIV == null)
            {
                // Gọi hàm này chỉ xảy ra nếu LoadRSATKeys() thất bại, nhưng là fail-safe
                throw new InvalidOperationException("Khóa AES chung chưa được tải hoặc giải mã thành công. Không thể mã hóa.");
            }

            // 2. Mã hóa Tên Tác giả bằng AES (dùng khóa chung)
            byte[] encryptedTacGiaBytes = Thuat_toan_AES.EncryptAES(plaintext, _aesKey, _aesIV);
            string encryptedTacGiaHex = BytesToHex(encryptedTacGiaBytes);

            // Chỉ trả về giá trị mã hóa.
            return encryptedTacGiaHex;
        }

        private void HandleInsert()
        {
            string maTL = txt_matailieu.Text.Trim();
            string tenTL = txt_tentailieu.Text.Trim();
            string tenTacGia = txt_tentacgia.Text.Trim();

            if (string.IsNullOrEmpty(maTL) || string.IsNullOrEmpty(tenTL) || string.IsNullOrEmpty(tenTacGia))
            {
                MessageBox.Show("Mã tài liệu, Tên tài liệu và Tên tác giả không được để trống.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            float chiPhi = 0;
            if (!float.TryParse(txt_chiphi.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out chiPhi))
            {
                MessageBox.Show("Chi phí (phí mượn) không hợp lệ.", "Lỗi định dạng");
                return;
            }

            // --- MÃ HÓA DỮ LIỆU VỚI KHÓA CHUNG ---
            string encryptedTacGiaHex;
            try
            {
                encryptedTacGiaHex = EncryptHybrid(tenTacGia);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi mã hóa Hybrid (RSA/AES): " + ex.Message, "Lỗi Mã hóa");
                return;
            }

            // SQL chỉ chèn TENTACGIA_ENC và BỎ CÁC CỘT KHÓA AES/IV
            string sql = @"INSERT INTO TAILIEU (MATAILIEU, TENSACH, NGONNGU, PHIMUON, TINHTRANG, THELOAI, TENTACGIA_ENC) 
                           VALUES (:ma, :ten, :ngonngu, :chiphi, :tinhtrang, :theloai, HEXTORAW(:encrypted_tacgia))";

            try
            {
                if (Database.Connect())
                {
                    using (OracleCommand cmd = new OracleCommand(sql, Database.Get_Connection()))
                    {
                        cmd.Parameters.Add(new OracleParameter("ma", maTL));
                        cmd.Parameters.Add(new OracleParameter("ten", tenTL));
                        cmd.Parameters.Add(new OracleParameter("ngonngu", txt_ngonngu.Text.Trim()));
                        cmd.Parameters.Add(new OracleParameter("chiphi", chiPhi));
                        cmd.Parameters.Add(new OracleParameter("tinhtrang", txt_tinhtrang.Text.Trim()));
                        cmd.Parameters.Add(new OracleParameter("theloai", txt_theloai.Text.Trim()));

                        // Chỉ chèn giá trị đã mã hóa
                        cmd.Parameters.Add(new OracleParameter("encrypted_tacgia", encryptedTacGiaHex));

                        cmd.ExecuteNonQuery();

                        // COMMIT để lưu dữ liệu thêm vào
                        Database.ExecuteNonQuery("COMMIT");

                        MessageBox.Show("Thêm tài liệu thành công!", "Thành công");
                        LoadDataTaiLieu();
                    }
                }
            }
            catch (OracleException ex)
            {
                if (ex.Number == 1)
                {
                    MessageBox.Show($"Lỗi: Mã tài liệu '{maTL}' đã tồn tại.", "Lỗi trùng lặp");
                }
                else
                {
                    MessageBox.Show("Lỗi CSDL khi thêm: " + ex.Message, "Lỗi SQL");
                }
            }
            finally
            {
                Database.Close();
                SetFormMode(false);
            }
        }

        private void HandleUpdate()
        {
            string maTL = txt_matailieu.Text.Trim();
            string tenTacGia = txt_tentacgia.Text.Trim();

            if (string.IsNullOrEmpty(maTL) || string.IsNullOrEmpty(tenTacGia))
            {
                MessageBox.Show("Vui lòng chọn tài liệu và nhập Tên tác giả cần cập nhật.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            float chiPhi = 0;
            if (!float.TryParse(txt_chiphi.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out chiPhi))
            {
                MessageBox.Show("Chi phí (phí mượn) không hợp lệ.", "Lỗi định dạng");
                return;
            }

            // --- MÃ HÓA DỮ LIỆU VỚI KHÓA CHUNG ---
            string encryptedTacGiaHex;
            try
            {
                encryptedTacGiaHex = EncryptHybrid(tenTacGia);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi mã hóa Hybrid (RSA/AES): " + ex.Message, "Lỗi Mã hóa");
                return;
            }

            // SQL chỉ cập nhật TENTACGIA_ENC và BỎ CÁC CỘT KHÓA AES/IV
            string sql = @"UPDATE TAILIEU SET 
                           TENSACH = :ten, 
                           NGONNGU = :ngonngu, 
                           PHIMUON = :chiphi, 
                           TINHTRANG = :tinhtrang, 
                           THELOAI = :theloai,
                           TENTACGIA_ENC = HEXTORAW(:encrypted_tacgia)
                           WHERE TRIM(MATAILIEU) = :ma";

            try
            {
                if (Database.Connect())
                {
                    using (OracleCommand cmd = new OracleCommand(sql, Database.Get_Connection()))
                    {
                        cmd.Parameters.Add(new OracleParameter("ten", txt_tentailieu.Text.Trim()));
                        cmd.Parameters.Add(new OracleParameter("ngonngu", txt_ngonngu.Text.Trim()));
                        cmd.Parameters.Add(new OracleParameter("chiphi", chiPhi));
                        cmd.Parameters.Add(new OracleParameter("tinhtrang", txt_tinhtrang.Text.Trim()));
                        cmd.Parameters.Add(new OracleParameter("theloai", txt_theloai.Text.Trim()));

                        // Chỉ cập nhật giá trị đã mã hóa
                        cmd.Parameters.Add(new OracleParameter("encrypted_tacgia", encryptedTacGiaHex));
                        cmd.Parameters.Add(new OracleParameter("ma", maTL));

                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            // COMMIT để lưu dữ liệu cập nhật
                            Database.ExecuteNonQuery("COMMIT");

                            MessageBox.Show("Cập nhật tài liệu thành công!", "Thành công");
                            LoadDataTaiLieu();
                        }
                        else
                        {
                            MessageBox.Show("Mã tài liệu không tồn tại hoặc không có thay đổi.", "Thông báo");
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

        private void btn_xoa_Click(object sender, EventArgs e)
        {
            string maTL = txt_matailieu.Text.Trim();
            if (string.IsNullOrEmpty(maTL))
            {
                MessageBox.Show("Vui lòng chọn tài liệu cần xóa.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show(
                $"Bạn có chắc chắn muốn xóa tài liệu có Mã: {maTL} không?",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                // Xóa bản ghi
                string sql = "DELETE FROM TAILIEU WHERE TRIM(MATAILIEU) = :ma";

                try
                {
                    if (Database.Connect())
                    {
                        using (OracleCommand cmd = new OracleCommand(sql, Database.Get_Connection()))
                        {
                            cmd.Parameters.Add(new OracleParameter("ma", maTL));
                            int rowsAffected = cmd.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                // COMMIT để lưu dữ liệu xóa
                                Database.ExecuteNonQuery("COMMIT");

                                MessageBox.Show("Xóa tài liệu thành công!", "Thành công");
                                ClearFormControls();
                                LoadDataTaiLieu();
                            }
                            else
                            {
                                MessageBox.Show("Không tìm thấy tài liệu cần xóa.", "Thông báo");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    if (ex.Message.Contains("integrity constraint") && ex.Message.Contains("violated"))
                    {
                        MessageBox.Show($"Lỗi Khóa ngoại: Không thể xóa tài liệu này vì nó đang được sử dụng trong các phiếu mượn.", "Lỗi nghiêm trọng", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void btn_thoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_TK_Click(object sender, EventArgs e)
        {
            string searchTerm = txt_timkiem.Text.Trim();
            LoadDataTaiLieu(searchTerm);
        }

        private void dgv_tailieu_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btn_thongtingoc_Click(object sender, EventArgs e)
        {
            string maTL = txt_matailieu.Text.Trim();
            if (string.IsNullOrEmpty(maTL))
            {
                MessageBox.Show("Vui lòng chọn tài liệu để xem thông tin gốc.", "Lỗi");
                return;
            }

            // --- BƯỚC 1: YÊU CẦU NHẬP MÃ XÁC THỰC ---
            string enteredPassword = Prompt.ShowDialog("Nhập mật khẩu xác thực:", "Yêu cầu Xác thực");

            string requiredAuthCode = GetAuthCode();

            if (string.IsNullOrEmpty(requiredAuthCode))
            {
                MessageBox.Show("Không tìm thấy mã xác thực (AUTH_CODE) trong database.", "Lỗi Cấu hình");
                return;
            }

            // --- BƯỚC 2: SO SÁNH VÀ XÁC THỰC ---
            if (enteredPassword.Trim() != requiredAuthCode.Trim())
            {
                MessageBox.Show("Mật khẩu không đúng. Không thể xem thông tin gốc.", "Lỗi Xác thực");
                return;
            }
            // --- KẾT THÚC XÁC THỰC ---


            // --- BƯỚC 3: TRUY VẤN VÀ GIẢI MÃ (Chỉ chạy khi xác thực thành công) ---
            string sql = "SELECT RAWTOHEX(TENTACGIA_ENC) AS TACGIA_HEX FROM TAILIEU WHERE TRIM(MATAILIEU) = :ma";
            string tacGiaHex = "";

            try
            {
                if (Database.Connect())
                {
                    DataTable dt = Database.ExecuteQuery(sql, new OracleParameter("ma", maTL));
                    if (dt.Rows.Count > 0)
                    {
                        tacGiaHex = dt.Rows[0]["TACGIA_HEX"].ToString();

                        string decryptedTacGia = "Không có dữ liệu/Lỗi giải mã";
                        if (!string.IsNullOrEmpty(tacGiaHex) && _aesKey != null && _aesIV != null)
                        {
                            try
                            {
                                // Giải mã Tên Tác giả bằng AES Key CHUNG
                                byte[] cipherText = HexToBytes(tacGiaHex);
                                decryptedTacGia = Thuat_toan_AES.DecryptAES(cipherText, _aesKey, _aesIV);
                            }
                            catch (Exception ex)
                            {
                                decryptedTacGia = $"LỖI GIẢ MÃ: {ex.Message}";
                            }
                        }

                        MessageBox.Show($"--- THÔNG TIN GỐC ĐÃ XÁC THỰC ---\n" +
                                        $"Tên Tác giả GỐC (Đã giải mã): {decryptedTacGia}\n" +
                                        $"Tên Tác giả (AES Ciphertext HEX): {tacGiaHex}",
                                        "Thông tin gốc (Đã Mã hóa)");
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy dữ liệu gốc.", "Thông báo");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải thông tin gốc: " + ex.Message, "Lỗi SQL");
            }
            finally
            {
                Database.Close();
            }
        }

        private string GetAuthCode()
        {
            string authCode = "";
            // SỬ DỤNG TRIM() TRONG SQL ĐỂ TRÁNH LỖI KHOẢNG TRẮNG CỦA CỘT CHAR/VARCHAR2
            string sql = "SELECT SECURITY_KEY FROM SECURITY_INFO WHERE TRIM(SECURITY_TYPE) = 'AUTH_CODE'";

            try
            {
                if (Database.Connect())
                {
                    DataTable dt = Database.ExecuteQuery(sql);
                    if (dt.Rows.Count > 0)
                    {
                        authCode = dt.Rows[0]["SECURITY_KEY"].ToString().Trim();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lấy AUTH_CODE từ DB: " + ex.Message, "Lỗi Database");
            }
            finally
            {
                Database.Close();
            }
            return authCode;
        }
    }
    // Đặt lớp này bên ngoài class QL_tailieu (ví dụ: ở cuối file QL_tailieu.cs)

    public static class Prompt
    {
        public static string ShowDialog(string text, string caption)
        {
            Form prompt = new Form()
            {
                Width = 500,
                Height = 150,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = caption,
                StartPosition = FormStartPosition.CenterScreen
            };
            Label textLabel = new Label() { Left = 50, Top = 20, Text = text };
            // Sử dụng PasswordChar = '*' để ẩn ký tự mật khẩu
            TextBox textBox = new TextBox() { Left = 50, Top = 50, Width = 400, PasswordChar = '*' };
            Button confirmation = new Button() { Text = "Ok", Left = 350, Width = 100, Top = 70, DialogResult = DialogResult.OK };
            confirmation.Click += (sender, e) => { prompt.Close(); };
            prompt.Controls.Add(textBox);
            prompt.Controls.Add(confirmation);
            prompt.Controls.Add(textLabel);
            prompt.AcceptButton = confirmation;

            return prompt.ShowDialog() == DialogResult.OK ? textBox.Text : "";
        }
    }
}