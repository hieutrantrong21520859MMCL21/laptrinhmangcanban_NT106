using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UDPServer
{
    public partial class Server : Form
    {
        public Server()
        {
            InitializeComponent();
        }

        private UdpClient udpServer;
        private bool listening = true;
        private Thread thdUDPServer;
        delegate void ClearCachedReceivedData(string data, IPAddress ipaddr);

        private void Server_Load(object sender, EventArgs e)
        {
            udpServer = new UdpClient(11000);
            thdUDPServer = new Thread(new ThreadStart(ServerThread));
            thdUDPServer.Start();
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            byte[] message = Encoding.UTF8.GetBytes(txtMessage.Text);
            udpServer.Send(message, message.Length, "127.0.0.1", 11001);
            lstChatBox.Items.Add($"Server: {Encoding.UTF8.GetString(message)}");
            txtMessage.Text = string.Empty;
        }

        private void ReceiveData(string data, IPAddress ipaddr)
        {
            if (lstChatBox.InvokeRequired)
            {
                var method = new ClearCachedReceivedData(ReceiveData);
                lstChatBox.Invoke(method, new object[] { data, ipaddr });
            }
            else
            {
                string message = $"Client: {data}";
                lstChatBox.Items.Add(message);
            }
        }

        private void ServerThread()
        {
            IPEndPoint clientEndpoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 11000);
            while (listening)
            {
                try
                {
                    byte[] data = new byte[1024];
                    data = udpServer.Receive(ref clientEndpoint);
                    string message = Encoding.UTF8.GetString(data);
                    ReceiveData(message, clientEndpoint.Address);
                }
                catch
                {
                    MessageBox.Show("Sending failed!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    thdUDPServer.Abort();
                }
            }
        }
    }
}