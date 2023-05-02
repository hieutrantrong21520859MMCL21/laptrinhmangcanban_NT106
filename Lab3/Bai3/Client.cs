using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bai3
{
    public partial class frmClient : Form
    {
        public frmClient()
        {
            InitializeComponent();
        }

        private TcpClient tcpClient;
        private IPEndPoint server_endpoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 10000);

        private void btnConnect_Click(object sender, EventArgs e)
        {
            tcpClient = new TcpClient();
            try
            {
                tcpClient.Connect(server_endpoint);
                this.btnConnect.Enabled = false;
                this.btnDisconnect.Enabled = true;
            }
            catch
            {
                MessageBox.Show("Connection failed!");
                tcpClient.Close();
            }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            try
            {
                if (tcpClient == null)
                {
                    MessageBox.Show("Client has connected yet!");
                    return;
                }
                NetworkStream net_stream = tcpClient.GetStream();
                byte[] data = Encoding.UTF8.GetBytes(txtMessage.Text);
                net_stream.Write(data, 0, data.Length);
                net_stream.Flush();
                txtMessage.Text = string.Empty;
            }
            catch
            {
                MessageBox.Show("Connection has closed!");
            }
        }

        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            tcpClient.Close();
            this.btnConnect.Enabled = true;
            this.btnDisconnect.Enabled = false;
        }
    }
}