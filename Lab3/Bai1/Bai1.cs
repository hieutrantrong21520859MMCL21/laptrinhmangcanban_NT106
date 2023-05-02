using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bai1
{
    public partial class frmBai1 : Form
    {
        public frmBai1()
        {
            InitializeComponent();
        }

        private void btnServer_Click(object sender, EventArgs e)
        {
            frmServer form_server = new frmServer();
            form_server.Show();
        }

        private void btnClient_Click(object sender, EventArgs e)
        {
            frmClient form_client = new frmClient();
            form_client.Show();
        }
    }
}