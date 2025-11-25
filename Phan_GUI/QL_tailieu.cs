using _40_caesarOracle;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace DO_AN_BMCSDL.Phan_GUI
{
    public partial class QL_tailieu : Form
    {

        private bool _isAddingNew = false;

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

            LoadDataTaiLieu();


            SetFormMode(false);
        }


        private void LoadDataTaiLieu(string searchTerm = "")
        {

            string sql = @"
                SELECT 
                    ROWNUM AS STT, 
                    TRIM(MATAILIEU) AS ""Ma tai lieu"", 
                    TRIM(TENSACH) AS ""Ten tai lieu"", 
                    TRIM(NGONNGU) AS ""Ngon ngu"", 
                    PHIMUON AS ""Chi phi"", 
                    TRIM(TENTACGIA) AS ""Tac gia"", 
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
                    dgv_tailieu.DataSource = dt;

                    dgv_tailieu.Columns["Ma tai lieu"].HeaderText = "MÃ TÀI LIỆU";
                    dgv_tailieu.Columns["Ten tai lieu"].HeaderText = "TÊN TÀI LIỆU";
                    dgv_tailieu.Columns["Ngon ngu"].HeaderText = "NGÔN NGỮ";
                    dgv_tailieu.Columns["Chi phi"].HeaderText = "CHI PHÍ";
                    dgv_tailieu.Columns["Tac gia"].HeaderText = "TÁC GIẢ";
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

        private void HandleInsert()
        {
            string maTL = txt_matailieu.Text.Trim();
            string tenTL = txt_tentailieu.Text.Trim();

            if (string.IsNullOrEmpty(maTL) || string.IsNullOrEmpty(tenTL))
            {
                MessageBox.Show("Mã tài liệu và Tên tài liệu không được để trống.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            float chiPhi = 0;
            if (!float.TryParse(txt_chiphi.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out chiPhi))
            {
                MessageBox.Show("Chi phí (phí mượn) không hợp lệ.", "Lỗi định dạng");
                return;
            }

            string sql = @"INSERT INTO TAILIEU (MATAILIEU, TENSACH, NGONNGU, PHIMUON, TINHTRANG, TENTACGIA, THELOAI) 
                           VALUES (:ma, :ten, :ngonngu, :chiphi, :tinhtrang, :tacgia, :theloai)";

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
                        cmd.Parameters.Add(new OracleParameter("tacgia", txt_tentacgia.Text.Trim()));
                        cmd.Parameters.Add(new OracleParameter("theloai", txt_theloai.Text.Trim()));

                        cmd.ExecuteNonQuery();
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
            if (string.IsNullOrEmpty(maTL))
            {
                MessageBox.Show("Vui lòng chọn tài liệu cần cập nhật từ danh sách.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            float chiPhi = 0;
            if (!float.TryParse(txt_chiphi.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out chiPhi))
            {
                MessageBox.Show("Chi phí (phí mượn) không hợp lệ.", "Lỗi định dạng");
                return;
            }

            string sql = @"UPDATE TAILIEU SET 
                           TENSACH = :ten, 
                           NGONNGU = :ngonngu, 
                           PHIMUON = :chiphi, 
                           TINHTRANG = :tinhtrang, 
                           TENTACGIA = :tacgia, 
                           THELOAI = :theloai 
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
                        cmd.Parameters.Add(new OracleParameter("tacgia", txt_tentacgia.Text.Trim()));
                        cmd.Parameters.Add(new OracleParameter("theloai", txt_theloai.Text.Trim()));
                        cmd.Parameters.Add(new OracleParameter("ma", maTL));

                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
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
    }
}