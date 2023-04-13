using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Bai1
{
    public partial class frmBai1 : Form
    {
        public frmBai1()
        {
            InitializeComponent();
        }

        private void btnRead_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.ShowDialog();
            FileStream fs = new FileStream(ofd.FileName, FileMode.Open, FileAccess.Read);//file input1.txt nam trong thu muc bin/Debug
            StreamReader sr = new StreamReader(fs);
            rtxtResult.Text = sr.ReadToEnd();
            fs.Close();
        }

        private void btnWrite_Click(object sender, EventArgs e)
        {
            FileStream fs = new FileStream("output1.txt", FileMode.Create, FileAccess.ReadWrite);
            StreamWriter sr = new StreamWriter(fs);
            sr.Write(rtxtResult.Text.ToUpper());
            sr.Flush();
            fs.Close();
        }
    }
}