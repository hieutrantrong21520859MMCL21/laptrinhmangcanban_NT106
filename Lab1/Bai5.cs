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
    public partial class frmForm5 : Form
    {
        public frmForm5()
        {
            InitializeComponent();
        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            if (txtA.Text.Length == 0 || txtB.Text.Length == 0)
            {
                MessageBox.Show("Vui lòng nhập giá trị!");
            }
            else
            {
                int A, B;
                if (!int.TryParse(txtA.Text, out A) || !int.TryParse(txtB.Text, out B))
                {
                    MessageBox.Show("Vui lòng nhập số nguyên!");
                }
                else
                {
                    A = int.Parse(txtA.Text);
                    B = int.Parse(txtB.Text);

                    if (A <= 0 || B<=0)
                    {
                        MessageBox.Show("Vui lòng nhập giá trị dương!");
                    }
                    else
                    {
                        int Facto_A = 1, Facto_B = 1, Sum_A = 0, Sum_B = 0, Sum_Exponent = 0;

                        for (int i = 1; i <= A; i++)
                        {
                            Facto_A *= i;
                            Sum_A += i;
                        }

                        for (int i = 1; i <= B; i++)
                        {
                            Facto_B *= i;
                            Sum_B += i;
                        }

                        for (int i = 1; i <= B; i++)
                        {
                            Sum_Exponent += (int)Math.Pow(A, i);
                        }

                        lblFacto_A.Text = "A! = " + Facto_A.ToString("N0");
                        lblFacto_B.Text = "B! = " + Facto_B.ToString("N0");
                        lblSum_A.Text = "S1 = 1 + 2 + 3 + 4 + ... + A = " + Sum_A.ToString("N0");
                        lblSum_B.Text = "S2 = 1 + 2 + 3 + 4 + ... + B = " + Sum_B.ToString("N0");
                        lblSum_Exponent.Text = "S3 = A^1 + A^2 + A^3 + A^4 + ... + A^B = " + Sum_Exponent.ToString("N0");
                    }
                }
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            txtA.Text = "";
            txtB.Text = "";
            lblFacto_A.Text = "";
            lblFacto_B.Text = "";
            lblSum_A.Text = "";
            lblSum_B.Text = "";
            lblSum_Exponent.Text = "";
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
