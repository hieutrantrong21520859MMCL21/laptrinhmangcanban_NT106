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
    public partial class frmForm2 : Form
    {
        public frmForm2()
        {
            InitializeComponent();
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            if (txtInput1.Text.Length == 0 || txtInput2.Text.Length == 0 || txtInput3.Text.Length == 0)
            {
                MessageBox.Show("Vui lòng nhập giá trị!");
            }
            else
            {
                double input1, input2, input3;
                if (!double.TryParse(txtInput1.Text, out input1) || !double.TryParse(txtInput2.Text, out input2) || !double.TryParse(txtInput3.Text, out input3))
                {
                    MessageBox.Show("Vui lòng nhập số thực!");
                }
                else
                {
                    input1 = double.Parse(txtInput1.Text);
                    input2 = double.Parse(txtInput2.Text);
                    input3 = double.Parse(txtInput3.Text);
                    lblMax_val.Text = (input1 > input2) ? ((input1 > input3) ? txtInput1.Text : txtInput3.Text) : ((input2 > input3) ? txtInput2.Text : txtInput3.Text);
                    lblMin_val.Text = (input1 < input2) ? ((input1 < input3) ? txtInput1.Text : txtInput3.Text) : ((input2 < input3) ? txtInput2.Text : txtInput3.Text);
                }
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            txtInput1.Text = "";
            txtInput2.Text = "";
            txtInput3.Text = "";
            lblMax_val.Text = "";
            lblMin_val.Text = "";
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}