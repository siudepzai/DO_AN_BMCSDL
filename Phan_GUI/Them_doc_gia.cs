using _40_caesarOracle;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
           
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btn_luu_Click(object sender, EventArgs e)
        {
           
            string maTV = txt_madocgia.Text.Trim();
            string tenTV = txt_tendocgia.Text.Trim();
            string vaiTro = txt_chucvu.Text.Trim();
            string ngaySinhString = txt_ngaysinh.Text.Trim();
            string gioiTinh = txt_gioitinh.Text.Trim();
            string sdt = txt_sdt.Text.Trim();
            string email = txt_email.Text.Trim();
            string diaChi = txtdiachi.Text.Trim();
            DateTime ngaySinhDate;

           
            if (!DateTime.TryParseExact(ngaySinhString, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out ngaySinhDate))
            {
                MessageBox.Show("Ngày sinh không hợp lệ. Vui lòng nhập theo định dạng DD/MM/YYYY.", "Lỗi định dạng", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string sql = @"INSERT INTO DOCGIA (MATHANHVIEN, TENTV, VAITRO, NGSINH, GIOITINH, SODIENTHOAI_ENC, EMAIL_ENC, DIACHI, TAIKHOAN, MATKHAU)
                           VALUES (:maTV, :tenTV, :vaiTro, :ngSinh, :gt, 
                                   C##DO_AN.ENCRYPT_DES(:sdt), 
                                   C##DO_AN.ENCRYPT_DES(:email), 
                                   :dc, :tk, :mk)";

            try
            {
                if (Database.Connect())
                {
                    using (OracleCommand cmd = new OracleCommand(sql, Database.Get_Connection()))
                    {
                       
                        cmd.Parameters.Add(new OracleParameter("maTV", maTV));
                        cmd.Parameters.Add(new OracleParameter("tenTV", tenTV));
                        cmd.Parameters.Add(new OracleParameter("vaiTro", vaiTro));
                        cmd.Parameters.Add(new OracleParameter("ngSinh", OracleDbType.Date) { Value = ngaySinhDate });
                        cmd.Parameters.Add(new OracleParameter("gt", gioiTinh));

                        
                        cmd.Parameters.Add(new OracleParameter("sdt", sdt));
                        cmd.Parameters.Add(new OracleParameter("email", email));

                        cmd.Parameters.Add(new OracleParameter("dc", diaChi));
                        cmd.Parameters.Add(new OracleParameter("tk", maTV));
                        cmd.Parameters.Add(new OracleParameter("mk", "123456"));

                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Thêm độc giả thành công!", "Thành công");

                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                }
            }
            catch (Exception ex) 
            {
                MessageBox.Show("Lỗi CSDL khi thêm độc giả: " + ex.Message, "Lỗi SQL");
            }
            finally
            {
                Database.Close();
            }
        }

        private void Them_doc_gia_Load(object sender, EventArgs e)
        {

        }
    }
}
