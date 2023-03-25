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
    public partial class frmForm1 : Form
    {
        public frmForm1()
        {
            InitializeComponent();
        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            if (txtInput1.Text.Length == 0 || txtInput2.Text.Length == 0)
            {
                MessageBox.Show("Vui lòng nhập giá trị!");
            }
            else
            {
                int num1, num2;
                if (!int.TryParse(txtInput1.Text.Trim(), out num1) || !int.TryParse(txtInput2.Text.Trim(), out num2))
                {
                    MessageBox.Show("Vui lòng nhập số nguyên");
                }
                else
                {
                    num1 = int.Parse(txtInput1.Text.Trim());
                    num2 = int.Parse(txtInput2.Text.Trim());
                    int sum = num1 + num2;
                    lblOutput.Text = sum + "";
                }
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            txtInput1.Text = "";
            txtInput2.Text = "";
            lblOutput.Text = "";
        }
    }
}