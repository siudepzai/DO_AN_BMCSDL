using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO_AN_BMCSDL.Phan_xu_ly
{
    internal class LichSuMuon
    {
        public static DataTable GetLichSuMuonTaiLieu(string username)
        {
            DataTable dt = new DataTable();
            try
            {
                using (OracleConnection con = Data.CreateOpenConnection())
                {
                    using (OracleCommand cmd = new OracleCommand("SP_LICHSUMUON_TAILIEU", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("p_username", OracleDbType.Varchar2).Value = username;

                        cmd.Parameters.Add("p_result", OracleDbType.RefCursor, ParameterDirection.Output);

                        using (OracleDataAdapter da = new OracleDataAdapter(cmd))
                        {
                            da.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi truy vấn lịch sử mượn tài liệu. Chi tiết: " + ex.Message, ex);
            }
            return dt;
        }
        public static DataTable GetLichSuMuonPhong(string username)
        {
            DataTable dt = new DataTable();
            try
            {
                using (OracleConnection con = Data.CreateOpenConnection())
                {
                    using (OracleCommand cmd = new OracleCommand("SP_LICHSUMUON_PHONG", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("p_username", OracleDbType.Varchar2).Value = username;

                        cmd.Parameters.Add("p_result", OracleDbType.RefCursor, ParameterDirection.Output);

                        using (OracleDataAdapter da = new OracleDataAdapter(cmd))
                        {
                            da.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi truy vấn lịch sử mượn phòng. Chi tiết: " + ex.Message, ex);
            }
            return dt;
        }
    }
}
