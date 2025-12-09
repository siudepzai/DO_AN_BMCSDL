using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DO_AN_BMCSDL.Phan_xu_ly
{
    public class XL_PhieuMuon
    {
        public static DataTable GetPhieuMuonByTaiKhoan(string taiKhoan)
        {
            DataTable dt = new DataTable();

            using (OracleConnection conn = Data.CreateOpenConnection())
            {
                using (OracleCommand cmd = new OracleCommand("GET_MUONSACH_BY_TAIKHOAN_PROC", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("p_TAIKHOAN", OracleDbType.Varchar2).Value = taiKhoan.Trim();
                    cmd.Parameters.Add("p_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                    using (OracleDataAdapter da = new OracleDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }

            return dt;
        }

        public static string TraTaiLieu(string maPhieu, string maTaiLieu)
        {
            string ketQua;

            using (OracleConnection conn = Data.CreateOpenConnection())
            {
                using (OracleCommand cmd = new OracleCommand("SP_TRA_TAILIEU", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("p_MAPHIEUMUON", OracleDbType.Varchar2).Value = maPhieu;
                    cmd.Parameters.Add("p_MATAILIEU", OracleDbType.Varchar2).Value = maTaiLieu;

                    OracleParameter result = cmd.Parameters.Add("p_KETQUA", OracleDbType.Varchar2, 100);
                    result.Direction = ParameterDirection.Output;

                    cmd.ExecuteNonQuery();

                    ketQua = result.Value.ToString();
                }
            }

            return ketQua;
        }


    }
}
