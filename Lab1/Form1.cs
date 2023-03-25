using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ThucHanhTuan1
{
    public partial class frmForm : Form
    {
        public frmForm()
        {
            InitializeComponent();
        }

        private void btnBai1_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmForm1 form1 = new frmForm1();
            form1.ShowDialog();
            this.Show();
        }

        private void btnBai2_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmForm2 form2 = new frmForm2();
            form2.ShowDialog();
            this.Show();
        }

        private void btnBai3_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmForm3 form3 = new frmForm3();
            form3.ShowDialog();
            this.Show();
        }

        private void btnBai4_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmForm4 form4 = new frmForm4();
            form4.ShowDialog();
            this.Show();
        }

        private void btnBai5_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmForm5 form5 = new frmForm5();
            form5.ShowDialog();
            this.Show();
        }
    }
}