using Microsoft.SqlServer.Server;
using PacketStructure;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacketStructure
{
    public enum User
    {
        NhanVien,
        QuanLy,
        Both,
        Null
    }

    public enum Header
    {
        TaiKhoan,
        KhachHang,
        NhanVien,
        Ban,
        ThucDon,
        HoaDon,
        ChiTietHoaDon,
        Null
    }

    public enum Options
    {
        LogIn,
        SignUp,
        Insert,
        Delete,
        Update,
        Search,
        View,
        Null
    }

    public class Packet
    {
        private Header hHeader;
        private User uUser;
        private Options oOption;
        private TaiKhoan accTaiKhoan;
        private KhachHang cusKhachHang;
        private NhanVien stafNhanVien;
        private Ban tableBan;
        private ThucDon foodThucDon;
        private HoaDon billHoaDon;
        private ChiTietHoadon detailChiTietHoaDon;
        private string sMessage;

        public User User
        {
            get { return this.uUser; }
            set { this.uUser = value; }
        }

        public Header Header
        {
            get { return this.hHeader; }
            set { this.hHeader = value; }
        }

        public Options Option
        {
            get { return this.oOption; }
            set { this.oOption = value; }
        }

        public TaiKhoan TaiKhoan
        {
            get { return this.accTaiKhoan; }
            set { this.accTaiKhoan = value; }
        }

        public KhachHang KhachHang
        {
            get { return this.cusKhachHang; }
            set { this.cusKhachHang = value; }
        }

        public NhanVien NhanVien
        {
            get { return this.stafNhanVien; }
            set { this.stafNhanVien = value; }
        }

        public Ban Ban
        {
            get { return this.tableBan; }
            set { this.tableBan = value; }
        }

        public ThucDon ThucDon
        {
            get { return this.foodThucDon; }
            set { this.foodThucDon = value; }
        }

        public HoaDon HoaDon
        {
            get { return this.billHoaDon; }
            set { this.billHoaDon = value; }
        }

        public ChiTietHoadon ChiTietHoaDon
        {
            get { return this.detailChiTietHoaDon; }
            set { this.detailChiTietHoaDon = value; }
        }

        public string Message
        {
            get { return this.sMessage; }
            set { this.sMessage = value; }
        }

        public Packet()
        {
            this.hHeader = Header.Null;
            this.uUser = User.Null;
            this.oOption = Options.Null;
            this.sMessage = "";
            this.accTaiKhoan = new TaiKhoan();
            this.cusKhachHang = new KhachHang();
            this.stafNhanVien = new NhanVien();
            this.tableBan = new Ban();
            this.foodThucDon = new ThucDon();
            this.billHoaDon = new HoaDon();
            this.detailChiTietHoaDon = new ChiTietHoadon();
        }

        public Packet(byte[] data)
        {
            this.accTaiKhoan = new TaiKhoan();
            this.cusKhachHang = new KhachHang();
            this.stafNhanVien = new NhanVien();
            this.tableBan = new Ban();
            this.foodThucDon = new ThucDon();
            this.billHoaDon = new HoaDon();
            this.detailChiTietHoaDon = new ChiTietHoadon();

            // Đọc User
            this.uUser = (User)BitConverter.ToInt32(data, 0);

            // Đọc Header
            this.hHeader = (Header)BitConverter.ToInt32(data, 4);

            // Đọc Option
            this.oOption = (Options)BitConverter.ToInt32(data, 8);

            // Đọc độ dài của mã tài khoản
            int iMaTK_length = BitConverter.ToInt32(data, 12);

            // Đọc độ dài của tên đăng nhập
            int iTenDangNhap_length = BitConverter.ToInt32(data, 16);

            // Đọc độ dài của họ tên
            int iHoTenTK_length = BitConverter.ToInt32(data, 20);

            // Đọc độ dài mật khẩu
            int iMatKhau_length = BitConverter.ToInt32(data, 24);

            // Đọc độ dài của vai trò
            int iVaiTro_length = BitConverter.ToInt32(data, 28);

            if (iMaTK_length > 0)
            {
                this.accTaiKhoan.MaTK = Encoding.UTF8.GetString(data, 32, iMaTK_length);
            }
            else
            {
                this.accTaiKhoan.MaTK = "";
            }

            if (iTenDangNhap_length > 0)
            {
                this.accTaiKhoan.TenDangNhap = Encoding.UTF8.GetString(data, 32 + iMaTK_length, iTenDangNhap_length);
            }
            else
            {
                this.accTaiKhoan.TenDangNhap = "";
            }

            if (iHoTenTK_length > 0)
            {
                this.accTaiKhoan.HoTen = Encoding.UTF8.GetString(data, 32 + iMaTK_length + iTenDangNhap_length, iHoTenTK_length);
            }
            else
            {
                this.accTaiKhoan.HoTen = "";
            }

            if (iMatKhau_length > 0)
            {
                this.accTaiKhoan.MatKhau = Encoding.UTF8.GetString(data, 32 + iMaTK_length + iTenDangNhap_length + iHoTenTK_length, iMatKhau_length);
            }
            else
            {
                this.accTaiKhoan.MatKhau = "";
            }

            if (iVaiTro_length > 0)
            {
                this.accTaiKhoan.VaiTro = Encoding.UTF8.GetString(data, 32 + iMaTK_length + iTenDangNhap_length + iHoTenTK_length + iMatKhau_length, iVaiTro_length);
            }
            else
            {
                this.accTaiKhoan.VaiTro = "";
            }

            // Đánh dấu vị trí hiện tại trong gói tin
            int current_index = 32 + iMaTK_length + iTenDangNhap_length + iHoTenTK_length + iMatKhau_length + iVaiTro_length;

            // Đọc độ dài của mã khách hàng
            int iMaKH_length = BitConverter.ToInt32(data, current_index);

            // Đọc độ dài của họ tên khách hàng
            int iHoTenKH_length = BitConverter.ToInt32(data, current_index + 4);

            // Đọc độ dài số điện thoại
            int iSoDienThoai_length = BitConverter.ToInt32(data, current_index + 8);

            if (iMaKH_length > 0)
            {
                this.cusKhachHang.MaKH = Encoding.UTF8.GetString(data, current_index + 12, iMaKH_length);
            }
            else
            {
                this.cusKhachHang.MaKH = "";
            }

            if (iHoTenKH_length > 0)
            {
                this.cusKhachHang.HoTenKH = Encoding.UTF8.GetString(data, current_index + 12 + iMaKH_length, iHoTenKH_length);
            }
            else
            {
                this.cusKhachHang.HoTenKH = "";
            }

            if (iSoDienThoai_length > 0)
            {
                this.cusKhachHang.SoDienThoai = Encoding.UTF8.GetString(data, current_index + 12 + iMaKH_length + iHoTenKH_length, iSoDienThoai_length);
            }
            else
            {
                this.cusKhachHang.SoDienThoai = "";
            }

            // Đánh dấu vị trí hiện tại trong gói tin
            current_index += 12 + iMaKH_length + iHoTenKH_length + iSoDienThoai_length;

            // Đọc độ dài của mã nhân viên
            int iMaNV_length = BitConverter.ToInt32(data, current_index);

            // Đọc độ dài của họ tên nhân viên
            int iHoTenNV_length = BitConverter.ToInt32(data, current_index + 4);

            // Đọc độ dài của ngày vào làm
            int iNgVL_length = BitConverter.ToInt32(data, current_index + 8);

            if (iMaNV_length > 0)
            {
                this.stafNhanVien.MaNV = Encoding.UTF8.GetString(data, current_index + 12, iMaNV_length);
            }
            else
            {
                this.stafNhanVien.MaNV = "";
            }

            if (iHoTenNV_length > 0)
            {
                this.stafNhanVien.HoTenNV = Encoding.UTF8.GetString(data, current_index + 12 + iMaNV_length, iHoTenNV_length);
            }
            else
            {
                this.stafNhanVien.HoTenNV = "";
            }

            if (iNgVL_length > 0)
            {
                this.stafNhanVien.NgayVL = Encoding.UTF8.GetString(data, current_index + 12 + iMaNV_length + iHoTenNV_length, iNgVL_length);
            }
            else
            {
                this.stafNhanVien.NgayVL = "";
            }

            //this.stafNhanVien.NgayVL = DateTime.Parse(Encoding.UTF8.GetString(data, current_index + 12 + iMaNV_length + iHoTenNV_length, iNgVL_length));

            this.stafNhanVien.Luong = BitConverter.ToInt32(data, current_index + 12 + iMaNV_length + iHoTenNV_length + iNgVL_length);

            // Đánh dấu vị trí hiện tại trong gói tin
            current_index += 12 + iMaNV_length + iHoTenNV_length + iNgVL_length + 4;

            // Đọc độ dài mã bàn
            int iMaBan_length = BitConverter.ToInt32(data, current_index);

            // Đọc độ dài tình trạng bàn
            int iTinhTrangBan_length = BitConverter.ToInt32(data, current_index + 4);

            if (iMaBan_length > 0)
            {
                this.tableBan.MaBan = Encoding.UTF8.GetString(data, current_index + 8, iMaBan_length);
            }
            else
            {
                this.tableBan.MaBan = "";
            }

            if (iTinhTrangBan_length > 0)
            {
                this.tableBan.TinhTrangBan = Encoding.UTF8.GetString(data, current_index + 8 + iMaBan_length, iTinhTrangBan_length);
            }

            // Đánh dấu tình trạng bàn
            current_index += 8 + iMaBan_length + iTinhTrangBan_length;

            // Đọc độ dài của mã món
            int iMaMon_length = BitConverter.ToInt32(data, current_index);

            // Đọc độ dài tên món
            int iTenMon_length = BitConverter.ToInt32(data, current_index + 4);

            // Đọc độ dài của đơn vị
            int iDonVi_length = BitConverter.ToInt32(data, current_index + 8);

            if (iMaMon_length > 0)
            {
                this.foodThucDon.MaMon = Encoding.UTF8.GetString(data, current_index + 12, iMaMon_length);
            }
            else
            {
                this.foodThucDon.MaMon = "";
            }

            if (iTenMon_length > 0)
            {
                this.foodThucDon.TenMon = Encoding.UTF8.GetString(data, current_index + 12 + iMaMon_length, iTenMon_length);
            }
            else
            {
                this.foodThucDon.TenMon = "";
            }

            if (iDonVi_length > 0)
            {
                this.foodThucDon.DonVi = Encoding.UTF8.GetString(data, current_index + 12 + iMaMon_length + iTenMon_length, iDonVi_length);
            }
            else
            {
                this.foodThucDon.DonVi = "";
            }

            this.foodThucDon.Gia = BitConverter.ToInt32(data, current_index + 12 + iMaMon_length + iTenMon_length + iDonVi_length);

            // Đánh dấu vị trí hiện tại trong gói tin
            current_index += 12 + iMaMon_length + iTenMon_length + iDonVi_length + 4;

            // Đọc độ dài mã hóa đơn
            int iMaHD_length = BitConverter.ToInt32(data, current_index);

            // Đọc độ dài mã nhân viên
            iMaNV_length = BitConverter.ToInt32(data, current_index + 4);

            // Đọc độ dài mã khách hàng
            iMaKH_length = BitConverter.ToInt32(data, current_index + 8);

            // Đọc độ dài mã bàn
            iMaBan_length = BitConverter.ToInt32(data, current_index + 12);

            // Đọc độ dài ngày hóa đơn
            int iNgHD_length = BitConverter.ToInt32(data, current_index + 16);

            if (iMaHD_length > 0)
            {
                this.billHoaDon.MaHD = Encoding.UTF8.GetString(data, current_index + 20, iMaHD_length);
            }
            else
            {
                this.billHoaDon.MaHD = "";
            }

            if (iMaNV_length > 0)
            {
                this.billHoaDon.MaNV = Encoding.UTF8.GetString(data, current_index + 20 + iMaHD_length, iMaNV_length);
            }
            else
            {
                this.billHoaDon.MaNV = "";
            }

            if (iMaKH_length > 0)
            {
                this.billHoaDon.MaKH = Encoding.UTF8.GetString(data, current_index + 20 + iMaHD_length + iMaNV_length, iMaKH_length);
            }
            else
            {
                this.billHoaDon.MaKH = "";
            }

            if (iMaBan_length > 0)
            {
                this.billHoaDon.MaBan = Encoding.UTF8.GetString(data, current_index + 20 + iMaHD_length + iMaNV_length + iMaKH_length, iMaBan_length);
            }
            else
            {
                this.billHoaDon.MaBan = "";
            }

            this.billHoaDon.NgayHD = Encoding.UTF8.GetString(data, current_index + 20 + iMaHD_length + iMaNV_length + iMaKH_length + iMaBan_length, iNgHD_length);
            this.billHoaDon.TriGia = BitConverter.ToInt32(data, current_index + 20 + iMaHD_length + iMaNV_length + iMaKH_length + iMaBan_length + iNgHD_length);

            // Đánh dấu vị trí hiện tại của gói tin
            current_index += 20 + iMaHD_length + iMaNV_length + iMaKH_length + iMaBan_length + iNgHD_length + 4;

            // Đọc độ dài của mã hóa đơn
            iMaHD_length = BitConverter.ToInt32(data, current_index);
            iMaMon_length = BitConverter.ToInt32(data, current_index + 4);

            if (iMaHD_length > 0)
            {
                this.detailChiTietHoaDon.MaHD = Encoding.UTF8.GetString(data, current_index + 8, iMaHD_length);
            }
            else
            {
                this.detailChiTietHoaDon.MaHD = "";
            }

            if (iMaMon_length > 0)
            {
                this.detailChiTietHoaDon.MaMon = Encoding.UTF8.GetString(data, current_index + 8 + iMaHD_length, iMaMon_length);
            }
            else
            {
                this.detailChiTietHoaDon.MaMon = "";
            }

            this.detailChiTietHoaDon.SoLuong = BitConverter.ToInt32(data, current_index + 8 + iMaHD_length + iMaMon_length);

            // Đánh dấu vị trí hiện tại trong gói tin
            current_index += 8 + iMaHD_length + iMaMon_length + 4;

            int iMessage_length = BitConverter.ToInt32(data, current_index);
            if (iMessage_length > 0)
            {
                this.sMessage = Encoding.UTF8.GetString(data, current_index + 4, iMessage_length);
            }
            else
            {
                this.sMessage = "";
            }
        }

        public byte[] GetDataStream()
        {
            List<byte> data = new List<byte>();

            // Thêm User
            data.AddRange(BitConverter.GetBytes((int)this.uUser));

            // Thêm Header
            data.AddRange(BitConverter.GetBytes((int)this.hHeader));

            // Thêm Option
            data.AddRange(BitConverter.GetBytes((int)this.oOption));

            // Thêm TaiKhoan
            data.AddRange(BitConverter.GetBytes(this.accTaiKhoan.MaTK.Length));
            data.AddRange(BitConverter.GetBytes(this.accTaiKhoan.TenDangNhap.Length));
            data.AddRange(BitConverter.GetBytes(Encoding.UTF8.GetByteCount(this.accTaiKhoan.HoTen)));
            data.AddRange(BitConverter.GetBytes(this.accTaiKhoan.MatKhau.Length));
            data.AddRange(BitConverter.GetBytes(Encoding.UTF8.GetByteCount(this.accTaiKhoan.VaiTro)));

            if (this.accTaiKhoan.MaTK != "")
            {
                data.AddRange(Encoding.UTF8.GetBytes(this.accTaiKhoan.MaTK));
            }

            if (this.accTaiKhoan.TenDangNhap != "")
            {
                data.AddRange(Encoding.UTF8.GetBytes(this.accTaiKhoan.TenDangNhap));
            }


            if (this.accTaiKhoan.HoTen != "")
            {
                data.AddRange(Encoding.UTF8.GetBytes(this.accTaiKhoan.HoTen));
            }

            if (this.accTaiKhoan.MatKhau != "")
            {
                data.AddRange(Encoding.UTF8.GetBytes(this.accTaiKhoan.MatKhau));
            }

            if (this.accTaiKhoan.VaiTro != "")
            {
                data.AddRange(Encoding.UTF8.GetBytes(this.accTaiKhoan.VaiTro));
            }

            // Thêm KhachHang
            data.AddRange(BitConverter.GetBytes(this.cusKhachHang.MaKH.Length));
            data.AddRange(BitConverter.GetBytes(Encoding.UTF8.GetByteCount(this.cusKhachHang.HoTenKH)));
            data.AddRange(BitConverter.GetBytes(this.cusKhachHang.SoDienThoai.Length));

            if (this.cusKhachHang.MaKH != "")
            {
                data.AddRange(Encoding.UTF8.GetBytes(this.cusKhachHang.MaKH));
            }

            if (this.cusKhachHang.HoTenKH != null)
            {
                data.AddRange(Encoding.UTF8.GetBytes(this.cusKhachHang.HoTenKH));
            }

            if (this.cusKhachHang.SoDienThoai != "")
            {
                data.AddRange(Encoding.UTF8.GetBytes(this.cusKhachHang.SoDienThoai));
            }

            // Thêm NhanVien
            data.AddRange(BitConverter.GetBytes(this.stafNhanVien.MaNV.Length));
            data.AddRange(BitConverter.GetBytes(Encoding.UTF8.GetByteCount(this.stafNhanVien.HoTenNV)));
            data.AddRange(BitConverter.GetBytes(this.stafNhanVien.NgayVL.Length));

            if (this.stafNhanVien.MaNV != "")
            {
                data.AddRange(Encoding.UTF8.GetBytes(this.stafNhanVien.MaNV));
            }

            if (this.stafNhanVien.HoTenNV != "")
            {
                data.AddRange(Encoding.UTF8.GetBytes(this.stafNhanVien.HoTenNV));
            }

            if (this.stafNhanVien.NgayVL != "")
            {
                data.AddRange(Encoding.UTF8.GetBytes(this.stafNhanVien.NgayVL));
            }

            data.AddRange(BitConverter.GetBytes(this.stafNhanVien.Luong));

            // Thêm Ban
            data.AddRange(BitConverter.GetBytes(this.tableBan.MaBan.Length));
            data.AddRange(BitConverter.GetBytes(Encoding.UTF8.GetByteCount(this.tableBan.TinhTrangBan)));

            if (this.tableBan.MaBan != "")
            {
                data.AddRange(Encoding.UTF8.GetBytes(this.tableBan.MaBan));
            }

            if (this.tableBan.TinhTrangBan != "")
            {
                data.AddRange(Encoding.UTF8.GetBytes(this.tableBan.TinhTrangBan));
            }

            // Thêm ThucDon
            data.AddRange(BitConverter.GetBytes(this.foodThucDon.MaMon.Length));
            data.AddRange(BitConverter.GetBytes(Encoding.UTF8.GetByteCount(this.foodThucDon.TenMon)));
            data.AddRange(BitConverter.GetBytes(Encoding.UTF8.GetByteCount(this.foodThucDon.DonVi)));

            if (this.foodThucDon.MaMon != "")
            {
                data.AddRange(Encoding.UTF8.GetBytes(this.foodThucDon.MaMon));
            }

            if (this.foodThucDon.TenMon != "")
            {
                data.AddRange(Encoding.UTF8.GetBytes(this.foodThucDon.TenMon));
            }

            if (this.foodThucDon.DonVi != "")
            {
                data.AddRange(Encoding.UTF8.GetBytes(this.foodThucDon.DonVi));
            }

            data.AddRange(BitConverter.GetBytes(this.foodThucDon.Gia));

            // Thêm HoaDon
            data.AddRange(BitConverter.GetBytes(this.billHoaDon.MaHD.Length));
            data.AddRange(BitConverter.GetBytes(this.billHoaDon.MaNV.Length));
            data.AddRange(BitConverter.GetBytes(this.billHoaDon.MaKH.Length));
            data.AddRange(BitConverter.GetBytes(this.billHoaDon.MaBan.Length));
            data.AddRange(BitConverter.GetBytes(this.billHoaDon.NgayHD.Length));

            if (this.billHoaDon.MaHD != "")
            {
                data.AddRange(Encoding.UTF8.GetBytes(this.billHoaDon.MaHD));
            }

            if (this.billHoaDon.MaNV != "")
            {
                data.AddRange(Encoding.UTF8.GetBytes(this.billHoaDon.MaNV));
            }

            if (this.billHoaDon.MaKH != "")
            {
                data.AddRange(Encoding.UTF8.GetBytes(this.billHoaDon.MaKH));
            }

            if (this.billHoaDon.MaBan != "")
            {
                data.AddRange(Encoding.UTF8.GetBytes(this.billHoaDon.MaBan));
            }

            data.AddRange(Encoding.UTF8.GetBytes(this.billHoaDon.NgayHD));
            data.AddRange(BitConverter.GetBytes(this.billHoaDon.TriGia));

            // Thêm ChiTietHoaDon
            data.AddRange(BitConverter.GetBytes(this.detailChiTietHoaDon.MaHD.Length));
            data.AddRange(BitConverter.GetBytes(this.detailChiTietHoaDon.MaMon.Length));

            if (this.detailChiTietHoaDon.MaHD != "")
            {
                data.AddRange(Encoding.UTF8.GetBytes(this.detailChiTietHoaDon.MaHD));
            }

            if (this.detailChiTietHoaDon.MaMon != "")
            {
                data.AddRange(Encoding.UTF8.GetBytes(this.detailChiTietHoaDon.MaMon));
            }

            data.AddRange(BitConverter.GetBytes(this.detailChiTietHoaDon.SoLuong));

            // Thêm Message
            data.AddRange(BitConverter.GetBytes(Encoding.UTF8.GetByteCount(this.sMessage)));
            if (this.sMessage != "")
            {
                data.AddRange(Encoding.UTF8.GetBytes(this.sMessage));
            }

            return data.ToArray();
        }
    }
}