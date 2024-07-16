using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacketStructure
{
    public class ChiTietHoadon
    {
        private string sMaHD;
        private string sMaMon;
        private int iSoLuong;

        public ChiTietHoadon()
        {
            this.sMaHD = "";
            this.sMaMon = "";
            this.iSoLuong = 0;
        }

        public ChiTietHoadon(string sMaHD, string sMaMon, int iSoluong)
        {
            this.sMaHD = sMaHD;
            this.sMaMon = sMaMon;
            this.iSoLuong = iSoluong;
        }

        public string MaHD
        {
            get { return this.sMaHD; }
            set { this.sMaHD = value; }
        }

        public string MaMon
        {
            get { return this.sMaMon; }
            set { this.sMaMon = value; }
        }

        public int SoLuong
        {
            get { return this.iSoLuong; }
            set { this.iSoLuong = value; }
        }
    }
}