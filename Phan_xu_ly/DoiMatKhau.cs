using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO_AN_BMCSDL.Phan_xu_ly
{
    internal class DoiMatKhau
    {
        public static string ChangePassword(string taiKhoan, string matKhauCu, string matKhauMoi)
        {
            string result = "ERROR";
            try
            {
                using (OracleConnection con = Data.CreateOpenConnection())
                {
                    using (OracleCommand cmd = new OracleCommand("SP_DOIMATKHAU", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // 1. Tham số đầu vào (Input Parameters)
                        cmd.Parameters.Add("p_TAIKHOAN", OracleDbType.Char, 25).Value = taiKhoan;
                        cmd.Parameters.Add("p_MATKHAU_CU", OracleDbType.Char, 20).Value = matKhauCu;
                        cmd.Parameters.Add("p_MATKHAU_MOI", OracleDbType.Char, 20).Value = matKhauMoi;

                        // 2. Tham số đầu ra (Output Parameter)
                        // Giả định kích thước chuỗi kết quả không quá 200 ký tự
                        OracleParameter outParam = new OracleParameter("p_KET_QUA", OracleDbType.Varchar2, 4000, ParameterDirection.Output);
                        cmd.Parameters.Add(outParam);

                        // 3. Thực thi SP
                        cmd.ExecuteNonQuery();

                        // 4. Lấy kết quả từ tham số đầu ra
                        result = outParam.Value.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                return "ERROR: " + ex.Message;
            }
            return result;
        }
    }
}
