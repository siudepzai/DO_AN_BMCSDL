using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO_AN_BMCSDL.Phan_xu_ly
{
    internal class TraCuu
    {
        private static DataTable ExecuteTraCuuSP(string spName)
        {
            DataTable dt = new DataTable();
            try
            {
                using (OracleConnection con = Data.CreateOpenConnection())
                {
                    using (OracleCommand cmd = new OracleCommand(spName, con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

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
                throw new Exception($"Lỗi truy vấn tra cứu ({spName}). Chi tiết: " + ex.Message, ex);
            }
            return dt;
        }

        public static DataTable GetTatCaPhong()
        {
            return ExecuteTraCuuSP("SP_TRACUU_PHONG");
        }

        public static DataTable GetTatCaTaiLieu()
        {
            return ExecuteTraCuuSP("SP_TRACUU_TAILIEU");
        }

        public static DataTable FilterData(DataTable sourceTable, string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
            {
                return sourceTable;
            }

            DataView dv = sourceTable.DefaultView;

            string filterExpression = string.Empty;
            string trimmedKeyword = keyword.Trim().Replace("'", "''");

            foreach (DataColumn column in sourceTable.Columns)
            {
                if (column.DataType == typeof(string) || column.DataType == typeof(DateTime) || column.DataType == typeof(float) || column.DataType == typeof(int))
                {
                    if (filterExpression.Length > 0)
                        filterExpression += " OR ";

                    filterExpression += $"Convert([{column.ColumnName}], 'System.String') LIKE '%{trimmedKeyword}%'";
                }
            }

            dv.RowFilter = filterExpression;

            return dv.ToTable();
        }
    }
}
