using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DO_AN_BMCSDL.Phan_xu_ly
{
    internal class MuonPhong
    {
        private static string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["OracleConn"].ConnectionString;
        }
        public static string DangKyDatPhong(string taiKhoan, string maPhong, int soGio)
        {
            string ketQua = "ERROR";
            using (OracleConnection conn = new OracleConnection(GetConnectionString()))
            {
                try
                {
                    conn.Open();
                    using (OracleCommand cmd = new OracleCommand("SP_DANGKY_DATPHONG", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("p_TAIKHOAN", OracleDbType.Varchar2).Value = taiKhoan;
                        cmd.Parameters.Add("p_MAPHONG", OracleDbType.Varchar2).Value = maPhong;
                        cmd.Parameters.Add("p_SOGIO", OracleDbType.Decimal).Value = soGio;

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
        public static DataTable GetPhongDangDat(string taiKhoan)
        {
            DataTable dt = new DataTable();
            using (OracleConnection conn = new OracleConnection(GetConnectionString()))
            {
                try
                {
                    conn.Open();
                    using (OracleCommand cmd = new OracleCommand("SP_GET_PHONG_DANG_DAT", conn))
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
                    MessageBox.Show("Lỗi khi tải danh sách phòng đang đặt: " + ex.Message, "Lỗi Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    dt = null;
                }
            }
            return dt;
        }
        public static string TraPhongSom(string taiKhoan, string maPhong)
        {
            string ketQua = "ERROR";
            using (OracleConnection conn = new OracleConnection(GetConnectionString()))
            {
                try
                {
                    conn.Open();
                    using (OracleCommand cmd = new OracleCommand("SP_TRA_PHONG_SOM", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("p_TAIKHOAN", OracleDbType.Varchar2).Value = taiKhoan;
                        cmd.Parameters.Add("p_MAPHONG", OracleDbType.Varchar2).Value = maPhong;
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
        public static string GiaHanDatPhong(string taiKhoan, string maPhong, int soGioGiaHan)
        {
            string ketQua = "ERROR";
            using (OracleConnection conn = new OracleConnection(GetConnectionString()))
            {
                try
                {
                    conn.Open();
                    using (OracleCommand cmd = new OracleCommand("SP_GIAHAN_DATPHONG", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("p_TAIKHOAN", OracleDbType.Varchar2).Value = taiKhoan;
                        cmd.Parameters.Add("p_MAPHONG", OracleDbType.Varchar2).Value = maPhong;
                        cmd.Parameters.Add("p_SOGIO_GIAHAN", OracleDbType.Decimal).Value = soGioGiaHan;
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
    }
}
