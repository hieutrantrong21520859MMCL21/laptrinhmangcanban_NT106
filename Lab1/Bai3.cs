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
    public partial class frmForm3 : Form
    {
        public frmForm3()
        {
            InitializeComponent();
        }

        private void btnRead_Click(object sender, EventArgs e)
        {
            if (txtInput.Text.Length == 0)
            {
                MessageBox.Show("Vui lòng nhập giá trị từ 0 đến 9!");
            }
            else
            {
                int num;
                if (!int.TryParse(txtInput.Text, out num))
                {
                    MessageBox.Show("Vui lòng nhập số nguyên!");
                }
                else
                {
                    num = int.Parse(txtInput.Text);
                    if (num < 0 || num > 9)
                    {
                        MessageBox.Show("Số nguyên vừa nhập phải có giá trị từ 0 đến 9! Vui lòng nhập lại!");
                    }
                    switch (num)
                    {
                        case 0:
                            lblOutput.Text = "Không";
                            break;
                        case 1:
                            lblOutput.Text = "Một";
                            break;
                        case 2:
                            lblOutput.Text = "Hai";
                            break;
                        case 3:
                            lblOutput.Text = "Ba";
                            break;
                        case 4:
                            lblOutput.Text = "Bốn";
                            break;
                        case 5:
                            lblOutput.Text = "Năm";
                            break;
                        case 6:
                            lblOutput.Text = "Sáu";
                            break;
                        case 7:
                            lblOutput.Text = "Bảy";
                            break;
                        case 8:
                            lblOutput.Text = "Tám";
                            break;
                        case 9:
                            lblOutput.Text = "Chín";
                            break;
                        case 10:
                            lblOutput.Text = "Mười";
                            break;
                    }
                }
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            txtInput.Text = "";
            lblOutput.Text = "";
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}