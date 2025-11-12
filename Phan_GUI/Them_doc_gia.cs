using _40_caesarOracle;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;

namespace DO_AN_BMCSDL.Phan_GUI
{
    public partial class Them_doc_gia : Form
    {
        public Them_doc_gia()
        {
            InitializeComponent();
        }

        private void lab_themdocgia_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btn_huy_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Hủy thêm độc giả!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            // Đặt DialogResult là Cancel (không làm gì)
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btn_luu_Click(object sender, EventArgs e)
        {
            // 1. Lấy dữ liệu từ Form
            string maTV = txt_madocgia.Text.Trim();
            string tenTV = txt_tendocgia.Text.Trim();
            string vaiTro = txt_chucvu.Text.Trim();
            string ngaySinhString = txt_ngaysinh.Text.Trim();
            string gioiTinh = txt_gioitinh.Text.Trim(); // Giả định tên TextBox cho GT là txt_gt
            string sdt = txt_sdt.Text.Trim();
            string email = txt_email.Text.Trim();
            string diaChi = txtdiachi.Text.Trim();

            // Khai báo biến DateTime
            DateTime ngaySinhDate;

            // 2. Kiểm tra dữ liệu bắt buộc
            if (string.IsNullOrWhiteSpace(maTV) || string.IsNullOrWhiteSpace(tenTV))
            {
                MessageBox.Show("Mã và Tên độc giả không được để trống.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 3. Kiểm tra và chuyển đổi Ngày sinh (Khắc phục lỗi ORA-01861)
            // Phải nhập đúng DD/MM/YYYY
            if (!DateTime.TryParseExact(ngaySinhString, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out ngaySinhDate))
            {
                MessageBox.Show("Ngày sinh không hợp lệ. Vui lòng nhập theo định dạng DD/MM/YYYY.", "Lỗi định dạng", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 4. Thực hiện INSERT vào Oracle (Sử dụng 10 cột chính)
            string sql = @"INSERT INTO DOCGIA (MATHANHVIEN, TENTV, VAITRO, NGSINH, GIOITINH, SODIENTHOAI, EMAIL, DIACHI, TAIKHOAN, MATKHAU)
                   VALUES (:maTV, :tenTV, :vaiTro, :ngSinh, :gt, :sdt, :email, :dc, :tk, :mk)";

            try
            {
                if (Database.Connect())
                {
                    using (OracleCommand cmd = new OracleCommand(sql, Database.Get_Connection()))
                    {
                        // Thêm các tham số chuỗi
                        cmd.Parameters.Add(new OracleParameter("maTV", maTV));
                        cmd.Parameters.Add(new OracleParameter("tenTV", tenTV));
                        cmd.Parameters.Add(new OracleParameter("vaiTro", vaiTro));

                        // Tham số Ngày sinh (Sử dụng OracleDbType.Date để tránh lỗi format)
                        cmd.Parameters.Add(new OracleParameter("ngSinh", OracleDbType.Date) { Value = ngaySinhDate });

                        // Thêm các tham số chuỗi còn lại
                        cmd.Parameters.Add(new OracleParameter("gt", gioiTinh));
                        cmd.Parameters.Add(new OracleParameter("sdt", sdt));
                        cmd.Parameters.Add(new OracleParameter("email", email));
                        cmd.Parameters.Add(new OracleParameter("dc", diaChi));

                        // Tạo TK/MK mặc định
                        cmd.Parameters.Add(new OracleParameter("tk", maTV));
                        cmd.Parameters.Add(new OracleParameter("mk", "123456"));

                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Thêm độc giả thành công!", "Thành công");

                        // 5. Thiết lập DialogResult để Form cha biết kết quả
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                }
            }
            catch (OracleException ex)
            {
                // ORA-12899, ORA-00001 (trùng khóa chính), v.v.
                MessageBox.Show("Lỗi CSDL khi thêm độc giả: " + ex.Message, "Lỗi SQL");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi hệ thống: " + ex.Message, "Lỗi");
            }
            finally
            {
                Database.Close();
            }
        }
    }
}
