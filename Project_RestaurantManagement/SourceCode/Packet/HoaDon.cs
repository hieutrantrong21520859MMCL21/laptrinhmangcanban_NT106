using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacketStructure
{
    public class HoaDon
    {
        private string sMaHD;
        private string sMaNV;
        private string sMaKH;
        private string sMaBan;
        private string sNgHD;
        private int iTriGia;

        public HoaDon()
        {
            this.sMaHD = "";
            this.sMaNV = "";
            this.sMaKH = "";
            this.sMaBan = "";
            this.sNgHD = "";
            this.iTriGia = 0;
        }

        public HoaDon(string sMaHD, string sMaNV, string sMaKH, string sMaBan, string sNgHD, int iTriGia)
        {
            this.sMaHD = sMaHD;
            this.sMaNV = sMaNV;
            this.sMaKH = sMaKH;
            this.sMaBan = sMaBan;
            this.sNgHD = sNgHD;
            this.iTriGia = iTriGia;
        }

        public string MaHD
        {
            get { return this.sMaHD; }
            set { this.sMaHD = value; }
        }

        public string MaNV
        {
            get { return this.sMaNV; }
            set { this.sMaNV = value; }
        }

        public string MaKH
        {
            get { return this.sMaKH; }
            set { this.sMaKH = value; }
        }

        public string MaBan
        {
            get { return this.sMaBan; }
            set { this.sMaBan = value; }
        }

        public string NgayHD
        {
            get { return this.sNgHD; }
            set { this.sNgHD = value; }
        }

        public int TriGia
        {
            get { return this.iTriGia; }
            set { this.iTriGia = value; }
        }
    }
}