using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacketStructure
{
    public class ThucDon
    {
        private string sMaMon;
        private string sTenMon;
        private string sDonVi;
        private int iGia;

        public ThucDon()
        {
            this.sMaMon = "";
            this.sTenMon = "";
            this.sDonVi = "";
            this.iGia = 0;
        }

        public ThucDon(string sMaMon, string sTenMon, string sDonVi, int iGia)
        {
            this.sMaMon = sMaMon;
            this.sTenMon = sTenMon;
            this.sDonVi = sDonVi;
            this.iGia = iGia;
        }

        public string MaMon
        {
            get { return this.sMaMon; }
            set { this.sMaMon = value; }
        }

        public string TenMon
        {
            get { return this.sTenMon; }
            set { this.sTenMon = value; }
        }

        public string DonVi
        {
            get { return this.sDonVi; }
            set { this.sDonVi = value; }
        }

        public int Gia
        {
            get { return this.iGia; }
            set { this.iGia = value; }
        }
    }
}