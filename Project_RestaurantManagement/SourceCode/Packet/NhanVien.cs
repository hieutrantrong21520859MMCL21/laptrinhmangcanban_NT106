using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacketStructure
{
    public class NhanVien
    {
        private string sMaNV;
        private string sHoTenNV;
        private string sNgVL;
        private int iLuong;

        public NhanVien()
        {
            this.sMaNV = "";
            this.sHoTenNV = "";
            this.sNgVL = "";
            this.iLuong = 0;
        }

        public NhanVien(string sMaNV, string sHoTenNV, string sNgVL, int iLuong)
        {
            this.sMaNV = sMaNV;
            this.sHoTenNV = sHoTenNV;
            this.sNgVL = sNgVL;
            this.iLuong = iLuong;
        }

        public string MaNV
        {
            get { return this.sMaNV; }
            set { this.sMaNV = value; }
        }

        public string HoTenNV
        {
            get { return this.sHoTenNV; }
            set { this.sHoTenNV = value; }
        }

        public string NgayVL
        {
            get { return this.sNgVL; }
            set { this.sNgVL = value; }
        }

        public int Luong
        {
            get { return this.iLuong; }
            set { this.iLuong = value; }
        }
    }
}