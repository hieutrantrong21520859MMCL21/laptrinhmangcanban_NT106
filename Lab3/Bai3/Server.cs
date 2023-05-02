using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Threading;

namespace Bai3
{
    public partial class frmServer : Form
    {
        public frmServer()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
        }

        private TcpListener tcpServer;

        private void serverThread()
        {
            tcpServer = new TcpListener(IPAddress.Any, 10000);
            tcpServer.Start();
            while (true)
            {
                TcpClient tcpClient = tcpServer.AcceptTcpClient();
                lstChatBox.Items.Add("-----Connection accepted from client-----");
                while (tcpClient.Connected)
                {
                    NetworkStream net_stream = tcpClient.GetStream();
                    byte[] data = new byte[1024];
                    int byte_count = net_stream.Read(data, 0, data.Length);
                    if (byte_count == 0)
                    {
                        break;
                    }
                    string message = Encoding.UTF8.GetString(data, 0, byte_count);
                    if (message.Contains("\n"))
                    {
                        message = message.Replace("\n", " ");
                    }
                    lstChatBox.Items.Add($"From client: {message}");
                }
                lstChatBox.Items.Add("-----Client has left the chat room-----\n");
            }
        }

        private void btnListen_Click(object sender, EventArgs e)
        {
            lstChatBox.Items.Add("Server started!");
            Thread thdServer = new Thread(new ThreadStart(serverThread));
            thdServer.Start();
            this.btnListen.Enabled = false;
        }
    }
}