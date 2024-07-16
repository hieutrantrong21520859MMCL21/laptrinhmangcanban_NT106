using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacketStructure
{
    public class TaiKhoan
    {
        private string sMaTK;
        private string sTenDangNhap;
        private string sHoTen;
        private string sMatKhau;
        private string sVaiTro;

        public TaiKhoan()
        {
            this.sMaTK = "";
            this.sTenDangNhap = "";
            this.sHoTen = "";
            this.sMatKhau = "";
            this.sVaiTro = "";
        }

        public TaiKhoan(string sMaTK, string sTenDangNhap, string sHoTen, string sMatKhau, string sVaiTro)
        {
            this.sMaTK = sMaTK;
            this.sTenDangNhap = sTenDangNhap;
            this.sHoTen = sHoTen;
            this.sMatKhau = sMatKhau;
            this.sVaiTro = sVaiTro;
        }

        public string MaTK
        {
            get { return this.sMaTK; }
            set { this.sMaTK = value; }
        }

        public string TenDangNhap
        {
            get { return this.sTenDangNhap; }
            set { this.sTenDangNhap = value; }
        }

        public string HoTen
        {
            get { return this.sHoTen; }
            set { this.sHoTen = value; }
        }

        public string MatKhau
        {
            get { return this.sMatKhau; }
            set { this.sMatKhau = value; }
        }

        public string VaiTro
        {
            get { return this.sVaiTro; }
            set { this.sVaiTro = value; }
        }
    }
}