using _40_caesarOracle;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Collections.Generic;

namespace DO_AN_BMCSDL.Phan_GUI
{
    public partial class themtailieu_thanhly : Form
    {
       
        private const string MANV_LAP = "0000000001";

        public themtailieu_thanhly()
        {
            InitializeComponent();

          
            txt_maphieu = this.Controls.Find("txt_maphieu", true).FirstOrDefault() as TextBox;
            txt_ngay = this.Controls.Find("txt_ngay", true).FirstOrDefault() as TextBox;
            txt_ghichu = this.Controls.Find("txt_ghichu", true).FirstOrDefault() as TextBox;

            this.Load += themtailieu_thanhly_Load;
        }

        private void themtailieu_thanhly_Load(object sender, EventArgs e)
        {
            if (txt_ngay != null)
            {
                txt_ngay.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
                txt_ngay.ReadOnly = true;
            }

        }


        private void btn_huy_Click_1(object sender, EventArgs e)
        {
            MessageBox.Show("Hủy thêm tài liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }

        private void btn_luu_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txt_maphieu?.Text))
            {
                MessageBox.Show("Mã phiếu không được để trống.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string maPhieu = txt_maphieu.Text.Trim();
            string ghiChu = txt_ghichu?.Text.Trim() ?? "";
            string trangThai = "Da thanh ly";

            string sqlInsert = @"
                INSERT INTO THANHLYTAILIEU (MAHOADON, MANV, NGAYTHANHLY, TRANGTHAI, GHICHU)
                VALUES (:maPhieu, :maNV, SYSDATE, :trangThai, :ghiChu)";

            try
            {
                if (Database.Connect())
                {
                    OracleParameter[] parameters = new OracleParameter[]
                    {
                        new OracleParameter("maPhieu", maPhieu),
                        new OracleParameter("maNV", MANV_LAP),
                        new OracleParameter("trangThai", trangThai),
                        new OracleParameter("ghiChu", ghiChu)
                    };

                    int rowsAffected = Database.ExecuteNonQuery(sqlInsert, parameters);

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Lưu phiếu thanh lý thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.DialogResult = DialogResult.OK; // Báo Form cha tải lại
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Không thể thêm phiếu thanh lý. Kiểm tra lại Mã phiếu hoặc Mã NV.", "Lỗi DB");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi CSDL khi lưu phiếu thanh lý: " + ex.Message, "Lỗi SQL");
            }
            finally
            {
                Database.Close();
            }
        }
    }
}