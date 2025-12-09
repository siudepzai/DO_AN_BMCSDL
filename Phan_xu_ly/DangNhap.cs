using Oracle.ManagedDataAccess.Client;
using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO_AN_BMCSDL.Phan_xu_ly
{
    internal class DangNhap
    {
        public static bool Login(string username, string password, string selectedRole, out string actualRole)
        {
            actualRole = string.Empty;
            string tableName = string.Empty;
            string validRoles = string.Empty;
            if (selectedRole == "Độc giả")
            {
                tableName = "DOCGIA";
                validRoles = "'Lecturer', 'Doc gia', 'Students', 'ExternalReaders'";
            }
            else if (selectedRole == "Thủ thư")
            {
                tableName = "NHANVIEN";
                validRoles = "'Nhan vien'";
            }
            else
            {
                return false;
            }

            try
            {
                using (OracleConnection con = Data.CreateOpenConnection())
                {
                    string sql = $"SELECT VAITRO FROM {tableName} WHERE TRIM(TAIKHOAN) = :p_user AND MATKHAU = :p_pass AND VAITRO IN ({validRoles})";

                    using (OracleCommand cmd = new OracleCommand(sql, con))
                    {
                        cmd.Parameters.Add(":p_user", OracleDbType.Varchar2).Value = username;
                        cmd.Parameters.Add(":p_pass", OracleDbType.Varchar2).Value = password;
                        object result = cmd.ExecuteScalar();

                        if (result != null)
                        {
                            actualRole = result.ToString();
                            return true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi truy vấn database trong quá trình đăng nhập. Chi tiết: " + ex.Message, ex);
            }
            return false;
        }
    }
}
