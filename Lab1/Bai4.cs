using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;

namespace ThucHanhTuan1
{
    public partial class frmForm4 : Form
    {
        public frmForm4()
        {
            InitializeComponent();
        }

        private void btnConvert_Click(object sender, EventArgs e)
        {
            if (txtInput.Text.Length == 0)
            {
                MessageBox.Show("Vui lòng nhập giá trị!");
            }
            else
            {
                double num;
                if (!double.TryParse(txtInput.Text, out num))
                {
                    MessageBox.Show("Vui lòng nhập giá trị thực!");
                }
                else
                {
                    num = double.Parse(txtInput.Text);
                    switch (cboCur_unit.Text)
                    {
                        case "USD":
                            lblOutput.Text = (num * 22772).ToString("N", new CultureInfo("en-US"));
                            lblRate_val.Text = "1 USD = 22,772 VND";
                            break;
                        case "EUR":
                            lblOutput.Text = (num * 28132).ToString("N", new CultureInfo("en-US"));
                            lblRate_val.Text = "1 EUR = 28,132 VND";
                            break;
                        case "GBP":
                            lblOutput.Text = (num * 31538).ToString("N", new CultureInfo("en-US"));
                            lblRate_val.Text = " 1 GBP = 31,538 VND";
                            break;
                        case "SGD":
                            lblOutput.Text = (num * 17286).ToString("N", new CultureInfo("en-US"));
                            lblRate_val.Text = "1 SGD = 17,286 VND";
                            break;
                        case "JPY":
                            lblOutput.Text = (num * 214).ToString("N", new CultureInfo("en-US"));
                            lblRate_val.Text = "1 JPY = 214 VND";
                            break;
                    }
                }
            }
        }
    }
}
