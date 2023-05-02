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

namespace Bai1
{
    public partial class frmClient : Form
    {
        public frmClient()
        {
            InitializeComponent();
        }

        private UdpClient udpClient;

        private void Client_Load(object sender, EventArgs e)
        {
            udpClient = new UdpClient();
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            if (txtServerIP.TextLength == 0 || txtServerPort.TextLength == 0)
            {
                MessageBox.Show("Vui long nhap day du thong tin cua server!");
            }
            else
            {
                IPAddress server_ip;
                if (!IPAddress.TryParse(txtServerIP.Text, out server_ip))
                {
                    MessageBox.Show("Vui long nhap lai dia chi IP cua server!");
                    txtServerIP.Text = string.Empty;
                    return;
                }
                server_ip = IPAddress.Parse(txtServerIP.Text);

                int server_port;
                if (!int.TryParse(txtServerPort.Text, out server_port))
                {
                    MessageBox.Show("Gia tri cua port phai la so nguyen!");
                    txtServerPort.Text = string.Empty;
                    return;
                }

                server_port = int.Parse(txtServerPort.Text);
                if (!(server_port >= 1024 && server_port <= 65535))
                {
                    MessageBox.Show("Vui long nhap lai gia tri port cua server!");
                    txtServerPort.Text = string.Empty;
                    return;
                }

                IPEndPoint server_endpoint = new IPEndPoint(server_ip, server_port);
                byte[] data = Encoding.UTF8.GetBytes(txtMessage.Text);
                udpClient.Send(data, data.Length, server_endpoint);
                txtMessage.Text = string.Empty;
            }
        }
    }
}