using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;
using System.Threading;
using System.Net.Http;
using PacketStructure;
using System.Security.Cryptography;
using System.Linq.Expressions;
using System.Net.NetworkInformation;

namespace Client
{
    public partial class frmDangNhap : Form
    {
        public frmDangNhap()
        {
            InitializeComponent();
        }

        public static TcpClient tcpClient;
        private IPEndPoint serverEndpoint = new IPEndPoint(IPAddress.Loopback, 10000);

        private void Receive()
        {
            while (true)
            {
                NetworkStream net_stream = tcpClient.GetStream();
                byte[] data = new byte[1024];
                int byte_count = net_stream.Read(data, 0, data.Length);

                if (byte_count == 0)
                {
                    break;
                }

                // Nhận gói tin trả về từ server
                Packet receivedData = new Packet(data);
                
                switch(receivedData.User)
                {
                    case User.Both:
                        MessageBox.Show(receivedData.Message, "", MessageBoxButtons.OK);
                        break;

                    case User.QuanLy:
                        switch (receivedData.Header)
                        {
                            case Header.TaiKhoan:
                                MessageBox.Show(receivedData.Message);
                                frmQuanLy manager = new frmQuanLy();
                                this.Invoke(new Action(() =>
                                {
                                    this.Hide();
                                }));
                                manager.ShowDialog();
                                break;
                        }
                        break;

                    case User.NhanVien:
                        switch(receivedData.Header)
                        {
                            case Header.TaiKhoan:
                                MessageBox.Show(receivedData.Message);
                                frmNhanVien staff = new frmNhanVien();
                                this.Invoke(new Action(() =>
                                {
                                    this.Hide();
                                }));
                                staff.ShowDialog();
                                break;
                        }
                        break;
                }
            }
        }

        private bool InputIsLogical()
        {
            string sUsername = txtUsername.Text.Trim();
            string sPassword = txtPassword.Text.Trim();
            if (string.IsNullOrEmpty(sUsername) || string.IsNullOrEmpty(sPassword))
            {
                MessageBox.Show("Vui lòng điền đẩy đủ thông tin!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (sUsername.Length > 20)
            {
                MessageBox.Show("Tên đăng nhập không quá 20 ký tự!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (sPassword.Length > 20)
            {
                MessageBox.Show("Mật khẩu không quá 20 ký tự!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (sUsername.Where(x => x == ' ').Any())
            {
                MessageBox.Show("Tên đăng nhập không có khoảng trống!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (sPassword.Where(x => x == ' ').Any())
            {
                MessageBox.Show("Mật khẩu không có khoảng trống!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        private void frmDangNhap_Load(object sender, EventArgs e)
        {
            tcpClient = new TcpClient();
            try
            {
                tcpClient.Connect(serverEndpoint);
                Thread sign_inThread = new Thread(Receive)
                {
                    IsBackground = true
                };
                sign_inThread.Start();
            }
            catch
            {
                MessageBox.Show("Không thể kết nối tới cơ sở dữ liệu!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            if (InputIsLogical())
            {
                Packet sendData = new Packet()
                {
                    User = User.Both,
                    Header = Header.TaiKhoan,
                    Option = Options.LogIn,
                    TaiKhoan = new TaiKhoan("", txtUsername.Text.Trim(), "", txtPassword.Text.Trim(), ""),
                };
                byte[] data = sendData.GetDataStream();
                NetworkStream stream = tcpClient.GetStream();
                stream.Write(data, 0, data.Length);
                stream.Flush();
            }
        }

        private void linklbDangKy_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmDangKy sign_up = new frmDangKy();
            this.Hide();
            sign_up.ShowDialog();
            this.Show();
        }
    }
}