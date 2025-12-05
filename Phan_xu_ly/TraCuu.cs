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
        // Hàm chung để gọi Stored Procedure và trả về DataTable
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

                        // Giả định Stored Procedure có tham số OUT là REF CURSOR
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

        // 1. Tra cứu toàn bộ Phòng học (SP_TRACUU_PHONG)
        public static DataTable GetTatCaPhong()
        {
            return ExecuteTraCuuSP("SP_TRACUU_PHONG");
        }

        // 2. Tra cứu toàn bộ Tài liệu (SP_TRACUU_TAILIEU)
        public static DataTable GetTatCaTaiLieu()
        {
            return ExecuteTraCuuSP("SP_TRACUU_TAILIEU");
        }

        // 3. Xử lý tìm kiếm (Lọc dữ liệu)
        // Lọc DataTable hiện có bằng từ khóa tìm kiếm (case-insensitive)
        public static DataTable FilterData(DataTable sourceTable, string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
            {
                return sourceTable;
            }

            // Tạo DataView để lọc dữ liệu
            DataView dv = sourceTable.DefaultView;

            // Tạo chuỗi Filter cho tất cả các cột
            string filterExpression = string.Empty;
            string trimmedKeyword = keyword.Trim().Replace("'", "''"); // Xử lý trích dẫn

            foreach (DataColumn column in sourceTable.Columns)
            {
                // Thêm điều kiện OR cho mỗi cột, sử dụng LIKE (chứa chuỗi)
                if (column.DataType == typeof(string) || column.DataType == typeof(DateTime) || column.DataType == typeof(float) || column.DataType == typeof(int))
                {
                    // Chuyển đổi tất cả các cột sang chuỗi để tìm kiếm, 
                    // đảm bảo tìm kiếm linh hoạt trên cả chuỗi và số
                    if (filterExpression.Length > 0)
                        filterExpression += " OR ";

                    // Sử dụng hàm CONVERT để tìm kiếm tất cả các loại dữ liệu.
                    // Lưu ý: Chuỗi lọc DataTable trong C# không hỗ trợ hàm UPPER/LOWER 
                    // nên việc tìm kiếm mặc định là case-sensitive. Để đơn giản, ta tìm kiếm
                    // trên giá trị cột đã được chuyển thành chuỗi.
                    filterExpression += $"Convert([{column.ColumnName}], 'System.String') LIKE '%{trimmedKeyword}%'";
                }
            }

            dv.RowFilter = filterExpression;

            return dv.ToTable();
        }
    }
}
