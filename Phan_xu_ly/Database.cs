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
        public static void Close()
        {
            if (_connection != null && _connection.State != ConnectionState.Closed)
                _connection.Close();
        }

        public static OracleConnection Get_Connection()
        {
            return _connection;
        }

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
            if (_connection == null || _connection.State != ConnectionState.Open)
            {
                throw new InvalidOperationException("Kết nối Oracle chưa được mở. Vui lòng gọi Database.Connect() trước.");
            }

            int rowsAffected = 0;

            using (OracleCommand command = new OracleCommand(sql, _connection))
            {
                if (parameters != null)
                {
                    command.Parameters.AddRange(parameters);
                }

                try
                {
                    rowsAffected = command.ExecuteNonQuery();
                }
                catch (OracleException ex)
                {
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