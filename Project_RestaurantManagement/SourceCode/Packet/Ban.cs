using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacketStructure
{
    public class Ban
    {
        private string sMaBan;
        private string sTinhTrangBan;

        public Ban()
        {
            this.sMaBan = "";
            this.sTinhTrangBan = "";
        }

        public Ban(string sMaBan, string sTinhTrangBan)
        {
            this.sMaBan = sMaBan;
            this.sTinhTrangBan = sTinhTrangBan;
        }

        public string MaBan
        {
            get { return this.sMaBan; }
            set { this.sMaBan = value; }
        }

        public string TinhTrangBan
        {
            get { return this.sTinhTrangBan; }
            set { this.sTinhTrangBan = value; }
        }
    }
}