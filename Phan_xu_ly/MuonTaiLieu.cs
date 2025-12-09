using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DO_AN_BMCSDL.Phan_xu_ly
{
    internal class MuonTaiLieu
    {
        private static string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["OracleConn"].ConnectionString;
        }

        public static string DangKyMuon(string taiKhoan, string maTaiLieu, int soLuongMuon)
        {
            string ketQua = "ERROR";
            using (OracleConnection conn = new OracleConnection(GetConnectionString()))
            {
                try
                {
                    conn.Open();
                    using (OracleCommand cmd = new OracleCommand("SP_DANGKY_MUON_TAILIEU", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("p_TAIKHOAN", OracleDbType.Varchar2).Value = taiKhoan;
                        cmd.Parameters.Add("p_MATAILIEU", OracleDbType.Varchar2).Value = maTaiLieu;
                        cmd.Parameters.Add("p_SOLUONGMUON", OracleDbType.Decimal).Value = soLuongMuon;

                        cmd.Parameters.Add("p_KET_QUA", OracleDbType.Varchar2, 100).Direction = ParameterDirection.Output;

                        cmd.ExecuteNonQuery();

                        ketQua = cmd.Parameters["p_KET_QUA"].Value.ToString();
                    }
                }
                catch (Exception ex)
                {
                    ketQua = "ERROR: " + ex.Message;
                }
            }
            return ketQua;
        }
        public static DataTable GetTaiLieuDaMuon(string taiKhoan)
        {
            DataTable dt = new DataTable();
            using (OracleConnection conn = new OracleConnection(GetConnectionString()))
            {
                try
                {
                    conn.Open();
                    using (OracleCommand cmd = new OracleCommand("SP_GET_MUON_PENDING", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("p_TAIKHOAN", OracleDbType.Varchar2).Value = taiKhoan;

                        cmd.Parameters.Add("p_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                        using (OracleDataAdapter da = new OracleDataAdapter(cmd))
                        {
                            da.Fill(dt);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi tải danh sách tài liệu đang mượn: " + ex.Message, "Lỗi Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    dt = null;
                }
            }
            return dt;
        }
    }
}
