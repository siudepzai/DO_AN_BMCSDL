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

            // 1. Xác định bảng và vai trò hợp lệ để truy vấn
            if (selectedRole == "Độc giả")
            {
                tableName = "DOCGIA";
                // Vai trò hợp lệ trong bảng DOCGIA (theo file Do_an.txt):
                validRoles = "'Lecturer', 'Doc gia', 'Students', 'ExternalReaders'";
            }
            else if (selectedRole == "Thủ thư")
            {
                tableName = "NHANVIEN";
                // Vai trò hợp lệ trong bảng NHANVIEN (theo file Do_an.txt):
                validRoles = "'Nhan vien'";
            }
            else
            {
                // Bỏ qua vai trò Admin hoặc vai trò khác
                return false;
            }

            try
            {
                // 2. Tạo và mở kết nối
                using (OracleConnection con = Data.CreateOpenConnection())
                {
                    // Câu lệnh SQL để kiểm tra tài khoản, mật khẩu và vai trò
                    // Chú ý: Cần mã hóa mật khẩu trong thực tế!
                    string sql = $"SELECT VAITRO FROM {tableName} WHERE TRIM(TAIKHOAN) = :p_user AND MATKHAU = :p_pass AND VAITRO IN ({validRoles})";

                    using (OracleCommand cmd = new OracleCommand(sql, con))
                    {
                        // Thiết lập tham số an toàn (tránh SQL Injection)
                        cmd.Parameters.Add(":p_user", OracleDbType.Varchar2).Value = username;
                        cmd.Parameters.Add(":p_pass", OracleDbType.Varchar2).Value = password;

                        // ExecuteScalar trả về giá trị đầu tiên của hàng đầu tiên (chính là VAITRO)
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
                // Ném lỗi ngoại lệ để Form có thể bắt và hiển thị lỗi kết nối
                throw new Exception("Lỗi truy vấn database trong quá trình đăng nhập. Chi tiết: " + ex.Message, ex);
            }
            return false;
        }
    }
}
