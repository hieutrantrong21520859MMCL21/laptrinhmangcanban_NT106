using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bai1
{
    public partial class frmServer : Form
    {
        public frmServer()
        {
            InitializeComponent();
        }

        private UdpClient udpServer;
        private delegate void InfoMessageDelegate(string message);

        private void InfoMessage(string message)
        {
            if (lstChatBox.InvokeRequired)
            {
                var method = new InfoMessageDelegate(InfoMessage);
                lstChatBox.Invoke(method, new object[] { message });
            }
            else
            {
                if (message.Contains('\n'))
                {
                    message = message.Replace('\n', ' ');
                }
                lstChatBox.Items.Add($"Client: {message}");
            }
        }

        private void serverThread()
        {
            while (true)
            {
                IPEndPoint remote_endpoint = new IPEndPoint(IPAddress.Any, 0);
                byte[] data = udpServer.Receive(ref remote_endpoint);
                string message = Encoding.UTF8.GetString(data);
                InfoMessage(message);
            }
        }

        private void btnListen_Click(object sender, EventArgs e)
        {
            try
            {
                udpServer = new UdpClient(int.Parse(txtPort.Text));
                Thread thdServer = new Thread(new ThreadStart(serverThread));
                thdServer.Start();
                this.btnListen.Enabled = false;
                txtPort.ReadOnly = true;
            }
            catch
            {
                MessageBox.Show("Vui long nhap gia tri port cua server!");
            }
        }
    }
}