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

namespace UDPClient
{
    public partial class Client : Form
    {
        public Client()
        {
            InitializeComponent();
        }

        private Socket udpClient;
        private bool connecting = true;
        private Thread thdUDPClient;
        delegate void ClearCachedReceivedData(string data, IPAddress ipaddr);

        private void btnConnect_Click(object sender, EventArgs e)
        {
            udpClient = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            udpClient.Bind(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 11001));
            thdUDPClient = new Thread(new ThreadStart(ClientThread));
            thdUDPClient.Start();
            this.btnConnect.Enabled = false;
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            IPEndPoint remote = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 11000);
            EndPoint serverEndpoint = (EndPoint)remote;
            byte[] message = Encoding.UTF8.GetBytes(txtMessage.Text);
            try
            {
                udpClient.SendTo(message, serverEndpoint);
                lstChatBox.Items.Add($"Client: {Encoding.UTF8.GetString(message)}");
                txtMessage.Text = string.Empty;
            }
            catch
            {
                MessageBox.Show("Sending failed!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                lstChatBox.Items.Add($"Client: {Encoding.UTF8.GetString(message)}");
            }
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
                string message = $"Server: {data}";
                lstChatBox.Items.Add(message);
            }
        }

        private void ClientThread()
        {
            IPEndPoint remote = new IPEndPoint(IPAddress.Any, 0);
            EndPoint serverEndpoint = (EndPoint)remote;
            while (connecting)
            {
                try
                {
                    byte[] data = new byte[1024];
                    int length = udpClient.ReceiveFrom(data, ref serverEndpoint);
                    string message = Encoding.UTF8.GetString(data, 0, length);
                    ReceiveData(message, remote.Address);
                }
                catch
                {
                    MessageBox.Show("Connection failed!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    thdUDPClient.Abort();
                }
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            connecting = false;
            this.btnConnect.Enabled = true;
            udpClient.Close();
        }
    }
}