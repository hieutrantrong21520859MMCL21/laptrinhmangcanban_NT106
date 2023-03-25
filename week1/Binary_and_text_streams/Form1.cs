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
using System.Security.Cryptography.X509Certificates;

namespace Binary_and_text_streams_chuong2
{
    public partial class frmForm : Form
    {
        public frmForm()
        {
            InitializeComponent();
        }

        private void btnRead_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.ShowDialog();
            FileStream fs = new FileStream(ofd.FileName, FileMode.OpenOrCreate);
            StreamReader sr = new StreamReader(fs);
            int lineCount = 0;
            while (sr.ReadLine() != null)
            {
                lineCount++;
            }
            fs.Close();
            MessageBox.Show("There are " + lineCount + " lines in " + ofd.FileName);
        }

        private void btnWrite_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.ShowDialog();
            FileStream fs = new FileStream(sfd.FileName, FileMode.Create);
            BinaryWriter bw = new BinaryWriter(fs);
            int[] myArray = new int[1000];
            for(int i=0;i<1000;i++)
            {
                myArray[i] = i;
                bw.Write(myArray[i]);
            }
            bw.Close();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
