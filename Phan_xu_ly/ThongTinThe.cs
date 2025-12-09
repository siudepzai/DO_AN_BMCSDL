using Oracle.ManagedDataAccess.Client;
using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO_AN_BMCSDL.Phan_xu_ly
{
    internal class ThongTinThe
    {
        public class DocGiaInfo
        {
            public string MaThanhVien { get; set; }
            public string TenThanhVien { get; set; }
            public DateTime? NgaySinh { get; set; }
            public string GioiTinh { get; set; }
            public string NgheNghiep { get; set; }
            public string VaiTro { get; set; }
            public string DiaChi { get; set; }
            public string Sdt { get; set; }
            public string KhoaHoc { get; set; }
            public string Email { get; set; }
            public string TaiKhoan { get; set; }
            public string MaSoThe { get; set; }
            public string TinhTrangThe { get; set; }
            public DateTime? HanSuDung { get; set; }
        }
        public static DocGiaInfo GetDocGiaInfo(string username)
        {
            DocGiaInfo info = null;
            try
            {
                using (OracleConnection con = Data.CreateOpenConnection())
                {
                    string sql = @"
                        SELECT 
                            dg.MATHANHVIEN, dg.TENTV, dg.NGSINH, dg.GIOITINH, dg.NGHENGHIEP, dg.VAITRO, dg.KHOAHOC, 
                            dg.DIACHI, dg.SODIENTHOAI, dg.EMAIL, dg.TAIKHOAN, 
                            tbd.MASOTHE, tbd.TINHTRANGTHE, tbd.HANSUDUNG
                        FROM 
                            DOCGIA dg
                        LEFT JOIN 
                            THEBANDOC tbd ON TRIM(dg.MATHANHVIEN) = TRIM(tbd.MATHANHVIEN)
                        WHERE 
                            TRIM(dg.TAIKHOAN) = :p_user";

                    using (OracleCommand cmd = new OracleCommand(sql, con))
                    {
                        cmd.Parameters.Add(":p_user", OracleDbType.Varchar2).Value = username.Trim();

                        using (OracleDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                info = new DocGiaInfo
                                {
                                    MaThanhVien = reader["MATHANHVIEN"].ToString().Trim(),
                                    TenThanhVien = reader["TENTV"].ToString(),
                                    NgaySinh = reader["NGSINH"] is DBNull ? (DateTime?)null : Convert.ToDateTime(reader["NGSINH"]),
                                    GioiTinh = reader["GIOITINH"].ToString(),
                                    NgheNghiep = reader["NGHENGHIEP"].ToString(),
                                    VaiTro = reader["VAITRO"].ToString(),
                                    DiaChi = reader["DIACHI"].ToString(),
                                    KhoaHoc = reader["KHOAHOC"].ToString(),
                                    Sdt = reader["SODIENTHOAI"].ToString(),
                                    Email = reader["EMAIL"].ToString(),
                                    TaiKhoan = reader["TAIKHOAN"].ToString(),
                                    MaSoThe = reader["MASOTHE"] is DBNull ? "Chưa có thẻ" : reader["MASOTHE"].ToString(),
                                    TinhTrangThe = reader["TINHTRANGTHE"] is DBNull ? "Chưa cấp" : reader["TINHTRANGTHE"].ToString(),
                                    HanSuDung = reader["HANSUDUNG"] is DBNull ? (DateTime?)null : Convert.ToDateTime(reader["HANSUDUNG"])
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi truy vấn thông tin độc giả. Chi tiết: " + ex.Message, ex);
            }
            return info;
        }

    }
}
