using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bai2
{
    public partial class frmBai2 : Form
    {
        public frmBai2()
        {
            InitializeComponent();
        }

        private void btnRead_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.ShowDialog();

            //Doc file
            FileStream fs = new FileStream(ofd.FileName, FileMode.OpenOrCreate, FileAccess.Read);
            StreamReader sr = new StreamReader(fs);
            rtxtResult.Text = sr.ReadToEnd();

            //Lay ten file
            txtFileName.Text = ofd.SafeFileName.ToString();

            //Lay kich thuoc cua file
            txtSize.Text = $"{fs.Length} bytes";

            //Lay URL cua file
            txtURL.Text = fs.Name.ToString();

            //Hien thi so dong
            int line_count = 0;
            fs.Position = 0;//Dua con tro chuot ve lai vi tri dau tien cua file
            while (sr.ReadLine() != null)
            {
                line_count++;
            }
            txtLineCount.Text = line_count.ToString();

            //Hien thi so tu
            string tmp = rtxtResult.Text.Replace('\n', ' ');
            string[] words = tmp.Split(' ');
            txtWordCount.Text = words.Length.ToString();

            //Hien thi so ky tu
            txtCharacterCount.Text = rtxtResult.TextLength.ToString();

            fs.Close();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}