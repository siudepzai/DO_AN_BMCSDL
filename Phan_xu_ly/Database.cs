using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _40_caesarOracle
{
    internal class Database
    {
        private static string _connectionString;
        private static OracleConnection _connection;

        // Hàm build connection string
        public static void Set_Database(string host, string port, string serviceName, string user, string pass)

        {
            host = "localhost";
            port = "1521";
            serviceName = "ORCL";
            user = "C##DO_AN";
            pass = "12345";
            if (string.IsNullOrWhiteSpace(host) || string.IsNullOrWhiteSpace(port) ||
                string.IsNullOrWhiteSpace(serviceName) || string.IsNullOrWhiteSpace(user))
            {
                throw new ArgumentException("Thông tin kết nối không hợp lệ!");
            }

            // Nếu user là SYS thì phải có DBA Privilege
            if (user.Trim().ToLower() == "sys")
            {
                _connectionString = $"User Id={user};Password={pass};Data Source={host}:{port}/{serviceName};DBA Privilege=SYSDBA;";
            }
            else
            {
                _connectionString = $"User Id={user};Password={pass};Data Source={host}:{port}/{serviceName};";
            }

            _connection = new OracleConnection(_connectionString);
        }

        // Kết nối DB
        public static bool Connect()
        {
            try
            {
                if (_connection == null)
                    return false;

                if (_connection.State != ConnectionState.Open)
                    _connection.Open();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi kết nối Oracle: " + ex.Message);
                return false;
            }
        }

        // Đóng kết nối
        public static void Close()
        {
            if (_connection != null && _connection.State != ConnectionState.Closed)
                _connection.Close();
        }

        // Lấy connection để dùng chỗ khác
        public static OracleConnection Get_Connection()
        {
            return _connection;
        }

        // Thực thi SELECT → DataTable
        public static DataTable ExecuteQuery(string sql, params OracleParameter[] parameters)
        {
            DataTable dt = new DataTable();
            using (OracleCommand cmd = new OracleCommand(sql, _connection))
            {
                if (parameters != null && parameters.Length > 0)
                    cmd.Parameters.AddRange(parameters);

                using (OracleDataAdapter da = new OracleDataAdapter(cmd))
                {
                    da.Fill(dt);
                }
            }
            return dt;
        }
        public static int ExecuteNonQuery(string sql, params OracleParameter[] parameters)
        {
            // Kiểm tra kết nối
            if (_connection == null || _connection.State != ConnectionState.Open)
            {
                // Thay bằng logic mở kết nối nếu cần, hoặc ném lỗi nếu muốn bắt buộc phải mở trước
                // Ví dụ: Connect(); 
                throw new InvalidOperationException("Kết nối Oracle chưa được mở. Vui lòng gọi Database.Connect() trước.");
            }

            int rowsAffected = 0;

            // Sử dụng using để đảm bảo đối tượng Command được giải phóng
            using (OracleCommand command = new OracleCommand(sql, _connection))
            {
                // Thêm các tham số vào command nếu có
                if (parameters != null)
                {
                    command.Parameters.AddRange(parameters);
                }

                try
                {
                    // Thực thi lệnh NonQuery
                    rowsAffected = command.ExecuteNonQuery();
                }
                catch (OracleException ex)
                {
                    // Ném lại lỗi để lớp gọi có thể xử lý (hoặc ghi log)
                    throw new Exception("Lỗi thực thi lệnh SQL NonQuery: " + ex.Message, ex);
                }
            }

            return rowsAffected;
        }
    

public static string ExecuteFunction(string funcName, params OracleParameter[] parameters)
        {
            using (OracleCommand cmd = new OracleCommand(funcName, _connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                // Thêm tham số return
                OracleParameter returnVal = new OracleParameter("returnVal", OracleDbType.Varchar2, 4000);
                returnVal.Direction = ParameterDirection.ReturnValue;
                cmd.Parameters.Add(returnVal);

                if (parameters != null && parameters.Length > 0)
                    cmd.Parameters.AddRange(parameters);

                cmd.ExecuteNonQuery();

                return returnVal.Value?.ToString();
            }
        }

    }
}