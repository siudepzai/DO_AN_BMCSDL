using Oracle.ManagedDataAccess.Client;
using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO_AN_BMCSDL.Phan_xu_ly
{
    internal class DangKyTheMoi
    {
        // Hàm tạo ID tự động dựa trên thời gian và hash nhẹ, đảm bảo độ dài 13 ký tự cho MATHANHVIEN
        private static string GenerateUniqueId(int length)
        {
            long timestamp = DateTime.Now.Ticks;
            string baseId = timestamp.ToString();

            // Đảm bảo đủ độ dài 13 (ví dụ)
            if (baseId.Length > length)
            {
                return baseId.Substring(baseId.Length - length);
            }
            else if (baseId.Length < length)
            {
                return baseId.PadLeft(length, '0');
            }
            return baseId;
        }

        // Tạo mã số thẻ (10 ký tự)
        public static string GenerateMaSoThe()
        {
            return GenerateUniqueId(10);
        }

        // Tạo mã thành viên (13 ký tự)
        public static string GenerateMaThanhVien()
        {
            return GenerateUniqueId(13);
        }

        public static void DangKyDocGiaVaThe(
            string hoTen, DateTime ngaySinh, string gioiTinh, string ngheNghiep,
            string vaiTro, string diaChi, string khoaHoc, string email,
            string sdt, string ghiChu, string taiKhoan, string matKhau)
        {
            // Tên trường mặc định
            const string TENTRUONG_DEFAULT = "DH Cong Thuong TPHCM";

            // Tạo ID cho độc giả và thẻ
            string maThanhVien = GenerateMaThanhVien();
            string maSoThe = GenerateMaSoThe();

            // Thiết lập hạn sử dụng 5 năm sau ngày đăng ký
            DateTime hanSuDung = DateTime.Now.AddYears(5);

            // Bắt đầu Transaction
            using (OracleConnection con = Data.CreateOpenConnection())
            using (OracleTransaction transaction = con.BeginTransaction())
            {
                try
                {
                    // --- 1. INSERT VÀO BẢNG DOCGIA ---
                    string sqlDocGia = @"
                        INSERT INTO DOCGIA (
                            MATHANHVIEN, TENTV, NGSINH, GIOITINH, NGHENGHIEP, 
                            SODIENTHOAI, TAIKHOAN, MATKHAU, VAITRO, DIACHI, 
                            TENTRUONG, KHOAHOC, EMAIL, GHICHU
                        ) 
                        VALUES (
                            :p_mtv, :p_ten, :p_nsinh, :p_gtinh, :p_nnghiep, 
                            :p_sdt, :p_tkhoan, :p_mkhau, :p_vtro, :p_dchi, 
                            :p_ttrg, :p_khoc, :p_email, :p_gchu
                        )";

                    using (OracleCommand cmdDocGia = new OracleCommand(sqlDocGia, con))
                    {
                        cmdDocGia.Parameters.Add(":p_mtv", OracleDbType.Char, 13).Value = maThanhVien;
                        cmdDocGia.Parameters.Add(":p_ten", OracleDbType.Varchar2).Value = hoTen;
                        cmdDocGia.Parameters.Add(":p_nsinh", OracleDbType.Date).Value = ngaySinh;
                        cmdDocGia.Parameters.Add(":p_gtinh", OracleDbType.Varchar2).Value = gioiTinh;
                        cmdDocGia.Parameters.Add(":p_nnghiep", OracleDbType.Varchar2).Value = ngheNghiep;
                        cmdDocGia.Parameters.Add(":p_sdt", OracleDbType.Char, 13).Value = (object)sdt ?? DBNull.Value;
                        cmdDocGia.Parameters.Add(":p_tkhoan", OracleDbType.Char, 25).Value = taiKhoan;
                        cmdDocGia.Parameters.Add(":p_mkhau", OracleDbType.Char, 20).Value = matKhau;
                        cmdDocGia.Parameters.Add(":p_vtro", OracleDbType.Varchar2).Value = vaiTro;
                        cmdDocGia.Parameters.Add(":p_dchi", OracleDbType.Varchar2).Value = (object)diaChi ?? DBNull.Value;
                        cmdDocGia.Parameters.Add(":p_ttrg", OracleDbType.Varchar2).Value = TENTRUONG_DEFAULT;
                        cmdDocGia.Parameters.Add(":p_khoc", OracleDbType.Char, 30).Value = (object)khoaHoc ?? DBNull.Value;
                        cmdDocGia.Parameters.Add(":p_email", OracleDbType.Char, 30).Value = (object)email ?? DBNull.Value;
                        cmdDocGia.Parameters.Add(":p_gchu", OracleDbType.Varchar2).Value = (object)ghiChu ?? DBNull.Value;

                        cmdDocGia.ExecuteNonQuery();
                    }

                    // --- 2. INSERT VÀO BẢNG THEBANDOC ---
                    string sqlTheBandoc = @"
                        INSERT INTO THEBANDOC (MASOTHE, MATHANHVIEN, TINHTRANGTHE, HANSUDUNG) 
                        VALUES (:p_msthe, :p_mtv, :p_tthe, :p_hsd)";

                    using (OracleCommand cmdThe = new OracleCommand(sqlTheBandoc, con))
                    {
                        cmdThe.Parameters.Add(":p_msthe", OracleDbType.Char, 10).Value = maSoThe;
                        cmdThe.Parameters.Add(":p_mtv", OracleDbType.Char, 13).Value = maThanhVien;
                        cmdThe.Parameters.Add(":p_tthe", OracleDbType.Varchar2).Value = "Chưa cấp"; // Tình trạng ban đầu
                        cmdThe.Parameters.Add(":p_hsd", OracleDbType.Varchar2).Value = hanSuDung.ToString("yyyy-MM-dd"); // Lưu dưới dạng chuỗi 'yyyy-MM-dd' cho an toàn (theo schema trong file Do_an.txt)

                        cmdThe.ExecuteNonQuery();
                    }

                    // Commit transaction nếu cả hai lệnh INSERT đều thành công
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    // Rollback nếu có lỗi xảy ra
                    transaction.Rollback();
                    throw new Exception("Lỗi đăng ký tài khoản hoặc thẻ. Chi tiết: " + ex.Message, ex);
                }
            }
        }
    }
}
