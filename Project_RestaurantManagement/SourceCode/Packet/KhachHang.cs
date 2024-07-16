using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacketStructure
{
    public class KhachHang
    {
        private string sMaKH;
        private string sHoTenKH;
        private string sSoDienThoai;

        public KhachHang()
        {
            this.sMaKH = "";
            this.sHoTenKH = "";
            this.sSoDienThoai = "";
        }

        public KhachHang(string sMaKH, string sHoTenKH, string sSoDienThoai)
        {
            this.sMaKH = sMaKH;
            this.sHoTenKH = sHoTenKH;
            this.sSoDienThoai = sSoDienThoai;
        }

        public string MaKH
        {
            get { return this.sMaKH; }
            set { this.sMaKH = value; }
        }

        public string HoTenKH
        {
            get { return this.sHoTenKH; }
            set { this.sHoTenKH = value; }
        }

        public string SoDienThoai
        {
            get { return this.sSoDienThoai; }
            set { this.sSoDienThoai = value; }
        }
    }
}