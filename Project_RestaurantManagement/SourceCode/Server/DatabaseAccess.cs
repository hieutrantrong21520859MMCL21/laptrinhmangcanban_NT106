using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Data;
using System.Net;
using PacketStructure;
using System.Threading;
using System.Runtime.CompilerServices;

namespace Server
{
    public class DatabaseAccess
    {
        // Khoi tao chuoi ket noi
        private static string sql_conn = @"Data Source=DESKTOP-U7NVEFD\SQLEXPRESS;Initial Catalog=QuanLyNhaHang;Integrated Security=True";

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(sql_conn);
        }

        private static string EncryptedPassword(string password)
        {
            string encryptedPasswd = "";
            using (MD5CryptoServiceProvider encryption = new MD5CryptoServiceProvider())
            {
                byte[] key = encryption.ComputeHash(Encoding.ASCII.GetBytes("GROUP_4"));
                using (TripleDESCryptoServiceProvider method = new TripleDESCryptoServiceProvider() { Key = key, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 })
                {
                    ICryptoTransform cryptoTransform = method.CreateEncryptor();
                    byte[] data = Encoding.ASCII.GetBytes(password);
                    byte[] encryptedBytes = cryptoTransform.TransformFinalBlock(data, 0, data.Length);
                    encryptedPasswd = Convert.ToBase64String(encryptedBytes);
                }
            }
            return encryptedPasswd;
        }

        #region Các phương thức truy xuất quan hệ TaiKhoan
        public static bool AccountExists(Packet receivedData)
        {
            bool check = true;
            string command = "select * from TaiKhoan where TenDangNhap = @username and MatKhau = @passwd";
            SqlCommand sql_cmd = new SqlCommand(command, frmServer.conn);
            sql_cmd.Parameters.Add("username", SqlDbType.VarChar).Value = receivedData.TaiKhoan.TenDangNhap;
            sql_cmd.Parameters.Add("passwd", SqlDbType.VarChar).Value = EncryptedPassword(receivedData.TaiKhoan.MatKhau);
            SqlDataReader dataReader = sql_cmd.ExecuteReader();
            if (!dataReader.HasRows)
            {
                check = false;
            }
            dataReader.Close();
            return check;
        }

        private static int NumberOfAccounts()
        {
            int count = 0;
            string command = "select * from TaiKhoan";
            SqlCommand sql_cmd = new SqlCommand(command, frmServer.conn);
            SqlDataReader dataReader = sql_cmd.ExecuteReader();
            while (dataReader.Read())
            {
                count++;
            }
            dataReader.Close();
            return count;
        }

        public static void InsertAccount(Packet receivedData)
        {
            string command = "insert into TaiKhoan values (@matk, @tendangnhap, @hoten, @matkhau, @vaitro)";
            SqlCommand sql_cmd = new SqlCommand(command, frmServer.conn);
            int next_id = NumberOfAccounts() + 1;
            string sMaTK;

            if (next_id < 10)
            {
                sMaTK = $"TK0{next_id}";
            }
            else
            {
                sMaTK = $"TK{next_id}";
            }

            sql_cmd.Parameters.Add("matk", SqlDbType.VarChar).Value = sMaTK;
            sql_cmd.Parameters.Add("tendangnhap", SqlDbType.VarChar).Value = receivedData.TaiKhoan.TenDangNhap;
            sql_cmd.Parameters.Add("hoten", SqlDbType.NVarChar).Value = receivedData.TaiKhoan.HoTen;
            sql_cmd.Parameters.Add("matkhau", SqlDbType.VarChar).Value = EncryptedPassword(receivedData.TaiKhoan.MatKhau);
            sql_cmd.Parameters.Add("vaitro", SqlDbType.NVarChar).Value = receivedData.TaiKhoan.VaiTro;
            sql_cmd.ExecuteNonQuery();
        }
        #endregion

        #region Các phương thức truy xuất quan hệ KhachHang
        public static void InsertCustomer(Packet receivedData)
        {
            string command = "insert into KhachHang values (@makh, @hoten, @sdt)";
            SqlCommand sql_cmd = new SqlCommand(command, frmServer.conn);
            sql_cmd.Parameters.Add("makh", SqlDbType.VarChar).Value = receivedData.KhachHang.MaKH;
            sql_cmd.Parameters.Add("hoten", SqlDbType.NVarChar).Value = receivedData.KhachHang.HoTenKH;
            sql_cmd.Parameters.Add("sdt", SqlDbType.VarChar).Value = receivedData.KhachHang.SoDienThoai;
            sql_cmd.ExecuteNonQuery();
        }

        public static void DeleteCustomer(Packet receivedData)
        {
            string command = "delete from KhachHang where MaKH = @makh";
            SqlCommand sql_cmd = new SqlCommand(command, frmServer.conn);
            sql_cmd.Parameters.Add("makh", SqlDbType.VarChar).Value = receivedData.KhachHang.MaKH;
            sql_cmd.ExecuteNonQuery();
        }

        public static void UpdateCustomer(Packet receivedData)
        {
            string command = "update KhachHang set HoTen = @hoten, SoDienThoai = @sdt where MaKH = @makh";
            SqlCommand sql_cmd = new SqlCommand(command, frmServer.conn);
            sql_cmd.Parameters.Add("hoten", SqlDbType.NVarChar).Value = receivedData.KhachHang.HoTenKH;
            sql_cmd.Parameters.Add("sdt", SqlDbType.VarChar).Value = receivedData.KhachHang.SoDienThoai;
            sql_cmd.Parameters.Add("makh", SqlDbType.VarChar).Value = receivedData.KhachHang.MaKH;
            sql_cmd.ExecuteNonQuery();
        }

        public static List<Packet> CustomersInfo(string command, Packet receivedData)
        {
            List<Packet> customerList = new List<Packet>();
            SqlCommand sql_cmd = new SqlCommand(command, frmServer.conn);
            if (command.Contains("where"))
            {
                sql_cmd.Parameters.Add("makh", SqlDbType.VarChar).Value = receivedData.KhachHang.MaKH;
            }
            SqlDataReader dataReader = sql_cmd.ExecuteReader();
            while (dataReader.Read())
            {
                string sMaKH = dataReader.GetString(0);
                string sHoTen = dataReader.GetString(1);
                string sSDT = dataReader.GetString(2);
                Packet sendData = new Packet()
                {
                    User = User.QuanLy,
                    Header = Header.KhachHang,
                    Option = Options.View,
                    KhachHang = new KhachHang(sMaKH, sHoTen, sSDT)
                };
                customerList.Add(sendData);
            }
            dataReader.Close();
            return customerList;
        }
        #endregion

        #region Các phương thức truy xuất quan hệ NhanVien
        public static void InsertStaff(Packet receivedData)
        {
            string command = "insert into NhanVien values (@manv, @hoten, @ngayvl, @luong)";
            SqlCommand sql_cmd = new SqlCommand(command, frmServer.conn);
            sql_cmd.Parameters.Add("manv", SqlDbType.VarChar).Value = receivedData.NhanVien.MaNV;
            sql_cmd.Parameters.Add("hoten", SqlDbType.NVarChar).Value = receivedData.NhanVien.HoTenNV;
            sql_cmd.Parameters.Add("ngayvl", SqlDbType.SmallDateTime).Value = DateTime.Parse(receivedData.NhanVien.NgayVL);
            sql_cmd.Parameters.Add("luong", SqlDbType.Int).Value = receivedData.NhanVien.Luong;
            sql_cmd.ExecuteNonQuery();
        }

        public static void DeleteStaff(Packet receivedData)
        {
            string command = "delete from NhanVien where MaNV = @manv";
            SqlCommand sql_cmd = new SqlCommand(command, frmServer.conn);
            sql_cmd.Parameters.Add("manv", SqlDbType.VarChar).Value = receivedData.NhanVien.MaNV;
            sql_cmd.ExecuteNonQuery();
        }

        public static void UpdateStaff(Packet receivedData)
        {
            string command = "update NhanVien set HoTen = @hoten, NgayVL = @ngayvl, Luong = @luong where MaNV = @manv";
            SqlCommand sql_cmd = new SqlCommand(command, frmServer.conn);
            sql_cmd.Parameters.Add("hoten", SqlDbType.NVarChar).Value = receivedData.NhanVien.HoTenNV;
            sql_cmd.Parameters.Add("ngayvl", SqlDbType.SmallDateTime).Value = DateTime.Parse(receivedData.NhanVien.NgayVL);
            sql_cmd.Parameters.Add("luong", SqlDbType.Int).Value = receivedData.NhanVien.Luong;
            sql_cmd.Parameters.Add("manv", SqlDbType.VarChar).Value = receivedData.NhanVien.MaNV;
            sql_cmd.ExecuteNonQuery();
        }

        public static List<Packet> StaffInfo(string command, Packet receivedData)
        {
            List<Packet> staffList = new List<Packet>();
            SqlCommand sql_cmd = new SqlCommand(command, frmServer.conn);
            if (command.Contains("where"))
            {
                sql_cmd.Parameters.Add("manv", SqlDbType.VarChar).Value = receivedData.NhanVien.MaNV;
            }
            SqlDataReader dataReader = sql_cmd.ExecuteReader();
            while (dataReader.Read())
            {
                string sMaNV = dataReader.GetString(0);
                string sHoTenNV = dataReader.GetString(1);
                DateTime dateNgVL = dataReader.GetDateTime(2).Date;
                int iLuong = dataReader.GetInt32(3);
                Packet sendData = new Packet()
                {
                    User = User.QuanLy,
                    Header = Header.NhanVien,
                    Option = Options.View,
                    NhanVien = new NhanVien(sMaNV, sHoTenNV, dateNgVL.ToString("dd/MM/yyyy"), iLuong)
                };
                staffList.Add(sendData);
            }
            dataReader.Close();
            return staffList;
        }
        #endregion

        #region Các phương thức truy xuất quan hệ Ban
        public static void UpdateTable(Packet receivedData)
        {
            string command = "update Ban set TinhTrangBan = @tinhtrangban where MaBan = @maban";
            SqlCommand sql_cmd = new SqlCommand(command, frmServer.conn);
            sql_cmd.Parameters.Add("tinhtrangban", SqlDbType.NVarChar).Value = receivedData.Ban.TinhTrangBan;
            sql_cmd.Parameters.Add("maban", SqlDbType.VarChar).Value = receivedData.Ban.MaBan;
            sql_cmd.ExecuteNonQuery();
        }

        public static bool TableHasBeenBooked(Packet receivedData)
        {
            bool check = true;
            string command = "select * from Ban where MaBan = @maban and TinhTrangBan = N'Có người'";
            SqlCommand sql_cmd = new SqlCommand(command, frmServer.conn);
            sql_cmd.Parameters.Add("maban", SqlDbType.VarChar).Value = receivedData.Ban.MaBan;
            SqlDataReader dataReader = sql_cmd.ExecuteReader();
            if (!dataReader.HasRows)
            {
                check = false;
            }
            dataReader.Close();
            return check;
        }

        public static List<Packet> TableInfo(string command, Packet receivedData)
        {
            List<Packet> tableList = new List<Packet>();
            SqlCommand sql_cmd = new SqlCommand(command, frmServer.conn);
            if (command.Contains("where"))
            {
                sql_cmd.Parameters.Add("maban", SqlDbType.VarChar).Value = receivedData.Ban.MaBan;
            }
            SqlDataReader dataReader = sql_cmd.ExecuteReader();
            while (dataReader.Read())
            {
                string sMaBan = dataReader.GetString(0);
                string sTinhTrangBan = dataReader.GetString(1);
                Packet sendData = new Packet()
                {
                    User = User.QuanLy,
                    Header = Header.Ban,
                    Option = Options.View,
                    Ban = new Ban(sMaBan, sTinhTrangBan)
                };
                tableList.Add(sendData);
            }
            dataReader.Close();
            return tableList;
        }
        #endregion

        #region Các phương thức truy xuất quan hệ ThucDon
        public static void InsertFood(Packet receivedData)
        {
            string command = "insert into ThucDon values (@mamon, @tenmon, @donvi, @gia)";
            SqlCommand sql_cmd = new SqlCommand(command, frmServer.conn);
            sql_cmd.Parameters.Add("mamon", SqlDbType.VarChar).Value = receivedData.ThucDon.MaMon;
            sql_cmd.Parameters.Add("tenmon", SqlDbType.NVarChar).Value = receivedData.ThucDon.TenMon;
            sql_cmd.Parameters.Add("donvi", SqlDbType.NVarChar).Value = receivedData.ThucDon.DonVi;
            sql_cmd.Parameters.Add("gia", SqlDbType.Int).Value = receivedData.ThucDon.Gia;
            sql_cmd.ExecuteNonQuery();
        }

        public static void UpdateFood(Packet receivedData)
        {
            string command = "update ThucDon set TenMon = @tenmon, DonVi = @donvi, Gia = @gia where MaMon = @mamon";
            SqlCommand sql_cmd = new SqlCommand(command, frmServer.conn);
            sql_cmd.Parameters.Add("tenmon", SqlDbType.NVarChar).Value = receivedData.ThucDon.TenMon;
            sql_cmd.Parameters.Add("donvi", SqlDbType.NVarChar).Value = receivedData.ThucDon.DonVi;
            sql_cmd.Parameters.Add("gia", SqlDbType.Int).Value = receivedData.ThucDon.Gia;
            sql_cmd.Parameters.Add("mamon", SqlDbType.VarChar).Value = receivedData.ThucDon.MaMon;
            sql_cmd.ExecuteNonQuery();
        }

        public static void DeleteFood(Packet receivedData)
        {
            string command = "delete from ThucDon where MaMon = @mamon";
            SqlCommand sql_cmd = new SqlCommand(command, frmServer.conn);
            sql_cmd.Parameters.Add("mamon", SqlDbType.VarChar).Value = receivedData.ThucDon.MaMon;
            sql_cmd.ExecuteNonQuery();
        }

        public static List<Packet> FoodInfo(string command, Packet receivedData)
        {
            List<Packet> foodList = new List<Packet>();
            SqlCommand sql_cmd = new SqlCommand(command, frmServer.conn);
            if (command.Contains("where"))
            {
                sql_cmd.Parameters.Add("mamon", SqlDbType.VarChar).Value = receivedData.ThucDon.MaMon;
            }
            SqlDataReader dataReader = sql_cmd.ExecuteReader();
            while (dataReader.Read())
            {
                string sMaMon = dataReader.GetString(0);
                string sTenMon = dataReader.GetString(1);
                string SDonVi = dataReader.GetString(2);
                int iGia = dataReader.GetInt32(3);
                Packet sendData = new Packet()
                {
                    User = User.QuanLy,
                    Header = Header.ThucDon,
                    Option = Options.View,
                    ThucDon = new ThucDon(sMaMon, sTenMon, SDonVi, iGia)
                };
                foodList.Add(sendData);
            }
            dataReader.Close();
            return foodList;
        }
        #endregion

        #region Các phương thức truy xuất tới quan hệ HoaDon
        
        private static int NumberOfBills()
        {
            int count = 0;
            string command = "select * from HoaDon";
            SqlCommand sql_cmd = new SqlCommand(command, frmServer.conn);
            SqlDataReader dataReader = sql_cmd.ExecuteReader();
            while (dataReader.Read())
            {
                count++;
            }
            dataReader.Close();
            return count;
        }
        
        public static void InsertBill(Packet receivedData)
        {
            string command = "insert into HoaDon values (@mahd, @manv, @makh, @maban, @ngayhd, @trigia)";
            SqlCommand sql_cmd = new SqlCommand(command, frmServer.conn);
            int next_id = NumberOfBills() + 1;
            string sMaHD;

            if (next_id < 10)
            {
                sMaHD = $"HD0{next_id}";
            }
            else
            {
                sMaHD = $"HD{next_id}";
            }

            sql_cmd.Parameters.Add("mahd", SqlDbType.VarChar).Value = sMaHD;
            sql_cmd.Parameters.Add("manv", SqlDbType.VarChar).Value = receivedData.HoaDon.MaNV;
            sql_cmd.Parameters.Add("makh", SqlDbType.VarChar).Value = receivedData.HoaDon.MaKH;
            sql_cmd.Parameters.Add("maban", SqlDbType.VarChar).Value = receivedData.HoaDon.MaBan;
            sql_cmd.Parameters.Add("ngayhd", SqlDbType.SmallDateTime).Value = DateTime.Parse(receivedData.HoaDon.NgayHD);
            sql_cmd.Parameters.Add("trigia", SqlDbType.Int).Value = receivedData.HoaDon.TriGia;
            sql_cmd.ExecuteNonQuery();
        }

        public static List<Packet> BiilInfo(string command, Packet receivedData)
        {
            List<Packet> billList = new List<Packet>();
            SqlCommand sql_cmd = new SqlCommand(command, frmServer.conn);
            if (command.Contains("where"))
            {
                sql_cmd.Parameters.Add("mahd", SqlDbType.VarChar).Value = receivedData.HoaDon.MaHD;
            }
            SqlDataReader dataReader = sql_cmd.ExecuteReader();
            while (dataReader.Read())
            {
                string sMaHD = dataReader.GetString(0);
                string sMaNV = dataReader.GetString(1);
                string sMaKH = dataReader.GetString(2);
                string sMaBan = dataReader.GetString(3);
                DateTime dateNgHD = dataReader.GetDateTime(4).Date;
                int iTriGia = dataReader.GetInt32(5);
                Packet sendData = new Packet()
                {
                    User = User.QuanLy,
                    Header = Header.HoaDon,
                    Option = Options.View,
                    HoaDon = new HoaDon(sMaHD, sMaNV, sMaKH, sMaBan, dateNgHD.ToString("dd/MM/yyyy"), iTriGia)
                };
                billList.Add(sendData);
            }
            dataReader.Close();
            return billList;
        }
        #endregion

        #region Các phương thức truy xuất tới quan hệ ChiTietHoaDon
        public static void InsertBillDetail(Packet receivedData)
        {
            string command = "insert into ChiTietHoaDon values (@mahd, @mamon, @sl)";
            SqlCommand sql_cmd = new SqlCommand(command, frmServer.conn);
            int bill_id = NumberOfBills();
            string sMaHD;

            if (bill_id < 10)
            {
                sMaHD = $"HD0{bill_id}";
            }
            else
            {
                sMaHD = $"HD{bill_id}";
            }

            sql_cmd.Parameters.Add("mahd", SqlDbType.VarChar).Value = sMaHD;
            sql_cmd.Parameters.Add("mamon", SqlDbType.VarChar).Value = receivedData.ChiTietHoaDon.MaMon;
            sql_cmd.Parameters.Add("sl", SqlDbType.Int).Value = receivedData.ChiTietHoaDon.SoLuong;
            sql_cmd.ExecuteNonQuery();
        }

        public static List<Packet> DetailInfo(string command, Packet receivedData)
        {
            List<Packet> detailList = new List<Packet>();
            SqlCommand sql_cmd = new SqlCommand(command, frmServer.conn);
            if (command.Contains("where"))
            {
                sql_cmd.Parameters.Add("mahd", SqlDbType.VarChar).Value = receivedData.ChiTietHoaDon.MaHD;
            }
            SqlDataReader dataReader = sql_cmd.ExecuteReader();
            while (dataReader.Read())
            {
                string sMaHD = dataReader.GetString(0);
                string sMaMon = dataReader.GetString(1);
                int iSoLuong = dataReader.GetInt32(2);
                Packet sendData = new Packet()
                {
                    User = User.QuanLy,
                    Header = Header.ChiTietHoaDon,
                    Option = Options.View,
                    ChiTietHoaDon = new ChiTietHoadon(sMaHD, sMaMon, iSoLuong)
                };
                detailList.Add(sendData);
            }
            dataReader.Close();
            return detailList;
        }
        #endregion
    }
}