using PacketStructure;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Server
{
    public partial class frmServer : Form
    {
        public frmServer()
        {
            InitializeComponent();
        }

        public static SqlConnection conn;
        private TcpListener tcpServer;
        private const int delay = 100;
        private delegate void StatusDelegate(string endpoint, string message);

        private void UpdateStatus(string endpoint, string message)
        {
            if (lstDanhSach.InvokeRequired)
            {
                var invoker = new StatusDelegate(UpdateStatus);
                lstDanhSach.Invoke(invoker, new object[] { endpoint, message });
            }
            else
            {
                lstDanhSach.Items.Add($"{endpoint}: {message}");
            }
        }

        private void Receive(object obj)
        {
            TcpClient client = obj as TcpClient;
            while (client.Connected)
            {
                NetworkStream net_stream = client.GetStream();
                byte[] data = new byte[1024];
                int byte_count = net_stream.Read(data, 0, data.Length);

                if (byte_count == 0)
                {
                    break;
                }

                Packet receivedData = new Packet(data);
                Packet sendData;
                byte[] response;

                switch (receivedData.User)
                {
                    case User.Both:
                        switch (receivedData.Option)
                        {
                            case Options.LogIn:
                                if (DatabaseAccess.AccountExists(receivedData))
                                {
                                    if (receivedData.TaiKhoan.TenDangNhap == "admin")
                                    {
                                        sendData = new Packet()
                                        {
                                            User = User.QuanLy,
                                            Header = Header.TaiKhoan,
                                            Option = Options.LogIn,
                                            Message = "Quản lý đăng nhập thành công!"
                                        };
                                    }
                                    else
                                    {
                                        sendData = new Packet()
                                        {
                                            User = User.NhanVien,
                                            Header = Header.TaiKhoan,
                                            Option = Options.LogIn,
                                            Message = $"Nhân viên đăng nhập thành công!"
                                        };
                                    }
                                    UpdateStatus(client.Client.RemoteEndPoint.ToString(), "Đăng nhập thành công");
                                }
                                else
                                {
                                    sendData = new Packet()
                                    {
                                        User = User.Both,
                                        Header = Header.TaiKhoan,
                                        Option = Options.LogIn,
                                        Message = "Tài khoản này không tồn tại! Vui lòng đăng nhập lại!",
                                    };
                                    UpdateStatus(client.Client.RemoteEndPoint.ToString(), "Đăng nhập thất bại");
                                }
                                response = sendData.GetDataStream();
                                net_stream.Write(response, 0, response.Length);
                                net_stream.Flush();
                                break;

                            case Options.SignUp:
                                if (!DatabaseAccess.AccountExists(receivedData))
                                {
                                    DatabaseAccess.InsertAccount(receivedData);
                                    sendData = new Packet()
                                    {
                                        User = User.Both,
                                        Header = Header.TaiKhoan,
                                        Option = Options.SignUp,
                                        Message = "Đăng ký thành công!"
                                    };
                                    UpdateStatus(client.Client.RemoteEndPoint.ToString(), "Đăng ký thành công");
                                }
                                else
                                {
                                    sendData = new Packet()
                                    {
                                        User = User.Both,
                                        Header = Header.TaiKhoan,
                                        Option = Options.SignUp,
                                        Message = "Tài khoản này đã tồn tại! Vui lòng tạo mới tài khoản!"
                                    };
                                    UpdateStatus(client.Client.RemoteEndPoint.ToString(), "Đăng ký thất bại");
                                }
                                response = sendData.GetDataStream();
                                net_stream.Write(response, 0, response.Length);
                                net_stream.Flush();
                                break;
                        }
                        break;

                    case User.QuanLy:
                        switch (receivedData.Header)
                        {
                            case Header.NhanVien:
                                switch (receivedData.Option)
                                {
                                    case Options.View:
                                        UpdateStatus(client.Client.RemoteEndPoint.ToString(), "Quản lý xem thông tin nhân viên");
                                        List<Packet> packets = DatabaseAccess.StaffInfo("select * from NhanVien", receivedData);
                                        foreach (Packet packet in packets)
                                        {
                                            response = packet.GetDataStream();
                                            net_stream.Write(response, 0, response.Length);
                                            net_stream.Flush();
                                            Thread.Sleep(delay);
                                        }
                                        break;

                                    case Options.Insert:
                                        UpdateStatus(client.Client.RemoteEndPoint.ToString(), "Quản lý thêm nhân viên");
                                        DatabaseAccess.InsertStaff(receivedData);
                                        packets = DatabaseAccess.StaffInfo("select * from NhanVien", receivedData);
                                        foreach (Packet packet in packets)
                                        {
                                            response = packet.GetDataStream();
                                            net_stream.Write(response, 0, response.Length);
                                            net_stream.Flush();
                                            Thread.Sleep(delay);
                                        }
                                        break;

                                    case Options.Delete:
                                        UpdateStatus(client.Client.RemoteEndPoint.ToString(), $"Quản lý xóa nhân viên {receivedData.NhanVien.MaNV}");
                                        DatabaseAccess.DeleteStaff(receivedData);
                                        packets = DatabaseAccess.StaffInfo("select * from NhanVien", receivedData);
                                        foreach (Packet packet in packets)
                                        {
                                            response = packet.GetDataStream();
                                            net_stream.Write(response, 0, response.Length);
                                            net_stream.Flush();
                                            Thread.Sleep(delay);
                                        }
                                        break;

                                    case Options.Update:
                                        UpdateStatus(client.Client.RemoteEndPoint.ToString(), $"Quản lý cập nhật thông tin nhân viên {receivedData.NhanVien.MaNV}");
                                        DatabaseAccess.UpdateStaff(receivedData);
                                        packets = DatabaseAccess.StaffInfo("select * from NhanVien", receivedData);
                                        foreach (Packet packet in packets)
                                        {
                                            response = packet.GetDataStream();
                                            net_stream.Write(response, 0, response.Length);
                                            net_stream.Flush();
                                            Thread.Sleep(delay);
                                        }
                                        break;

                                    case Options.Search:
                                        UpdateStatus(client.Client.RemoteEndPoint.ToString(), $"Quản lý tìm thông tin nhân viên {receivedData.NhanVien.MaNV}");
                                        packets = DatabaseAccess.StaffInfo("select * from NhanVien where MaNV = @manv", receivedData);
                                        foreach (Packet packet in packets)
                                        {
                                            response = packet.GetDataStream();
                                            net_stream.Write(response, 0, response.Length);
                                            net_stream.Flush();
                                            Thread.Sleep(delay);
                                        }
                                        break;
                                }
                                break;

                            case Header.ThucDon:
                                switch (receivedData.Option)
                                {
                                    case Options.View:
                                        UpdateStatus(client.Client.RemoteEndPoint.ToString(), $"Quản lý xem thông tin món ăn");
                                        List<Packet> packets = DatabaseAccess.FoodInfo("select * from ThucDon", receivedData);
                                        foreach (Packet packet in packets)
                                        {
                                            response = packet.GetDataStream();
                                            net_stream.Write(response, 0, response.Length);
                                            net_stream.Flush();
                                            Thread.Sleep(delay);
                                        }
                                        break;

                                    case Options.Insert:
                                        UpdateStatus(client.Client.RemoteEndPoint.ToString(), $"Quản lý thêm món ăn");
                                        DatabaseAccess.InsertFood(receivedData);
                                        packets = DatabaseAccess.FoodInfo("select * from ThucDon", receivedData);
                                        foreach (Packet packet in packets)
                                        {
                                            response = packet.GetDataStream();
                                            net_stream.Write(response, 0, response.Length);
                                            net_stream.Flush();
                                            Thread.Sleep(delay);
                                        }
                                        break;

                                    case Options.Update:
                                        UpdateStatus(client.Client.RemoteEndPoint.ToString(), $"Quản lý cập nhật thông tin món ăn {receivedData.ThucDon.MaMon}");
                                        DatabaseAccess.UpdateFood(receivedData);
                                        packets = DatabaseAccess.FoodInfo("select * from ThucDon", receivedData);
                                        foreach (Packet packet in packets)
                                        {
                                            response = packet.GetDataStream();
                                            net_stream.Write(response, 0, response.Length);
                                            net_stream.Flush();
                                            Thread.Sleep(delay);
                                        }
                                        break;

                                    case Options.Delete:
                                        UpdateStatus(client.Client.RemoteEndPoint.ToString(), $"Quản lý xóa món ăn {receivedData.ThucDon.MaMon}");
                                        DatabaseAccess.DeleteFood(receivedData);
                                        packets = DatabaseAccess.FoodInfo("select * from ThucDon", receivedData);
                                        foreach (Packet packet in packets)
                                        {
                                            response = packet.GetDataStream();
                                            net_stream.Write(response, 0, response.Length);
                                            net_stream.Flush();
                                            Thread.Sleep(delay);
                                        }
                                        break;

                                    case Options.Search:
                                        UpdateStatus(client.Client.RemoteEndPoint.ToString(), $"Quản lý tìm thông tin món ăn {receivedData.ThucDon.MaMon}");
                                        packets = DatabaseAccess.FoodInfo("select * from ThucDon where MaMon = @mamon", receivedData);
                                        foreach (Packet packet in packets)
                                        {
                                            response = packet.GetDataStream();
                                            net_stream.Write(response, 0, response.Length);
                                            net_stream.Flush();
                                            Thread.Sleep(delay);
                                        }
                                        break;
                                }
                                break;

                            case Header.Ban:
                                switch (receivedData.Option)
                                {
                                    case Options.View:
                                        UpdateStatus(client.Client.RemoteEndPoint.ToString(), $"Quản lý xem thông tin bàn ăn");
                                        List<Packet> packets = DatabaseAccess.TableInfo("select * from Ban", receivedData);
                                        foreach (Packet packet in packets)
                                        {
                                            response = packet.GetDataStream();
                                            net_stream.Write(response, 0, response.Length);
                                            net_stream.Flush();
                                            Thread.Sleep(delay);
                                        }
                                        break;

                                    case Options.Search:
                                        UpdateStatus(client.Client.RemoteEndPoint.ToString(), $"Quản lý tìm thông tin bàn ăn {receivedData.Ban.MaBan}");
                                        packets = DatabaseAccess.TableInfo("select * from Ban where MaBan = @maban", receivedData);
                                        foreach (Packet packet in packets)
                                        {
                                            response = packet.GetDataStream();
                                            net_stream.Write(response, 0, response.Length);
                                            net_stream.Flush();
                                            Thread.Sleep(delay);
                                        }
                                        break;
                                }
                                break;

                            case Header.HoaDon:
                                switch (receivedData.Option)
                                {
                                    case Options.View:
                                        UpdateStatus(client.Client.RemoteEndPoint.ToString(), $"Quản lý xem thông tin hóa đơn");
                                        List<Packet> packets = DatabaseAccess.BiilInfo("select * from HoaDon", receivedData);
                                        foreach (Packet packet in packets)
                                        {
                                            response = packet.GetDataStream();
                                            net_stream.Write(response, 0, response.Length);
                                            net_stream.Flush();
                                            Thread.Sleep(delay);
                                        }
                                        break;

                                    case Options.Search:
                                        UpdateStatus(client.Client.RemoteEndPoint.ToString(), $"Quản lý xem thông tin hóa đơn {receivedData.HoaDon.MaHD}");
                                        packets = DatabaseAccess.BiilInfo("select * from HoaDon where MaHD = @mahd", receivedData);
                                        foreach (Packet packet in packets)
                                        {
                                            response = packet.GetDataStream();
                                            net_stream.Write(response, 0, response.Length);
                                            net_stream.Flush();
                                            Thread.Sleep(delay);
                                        }
                                        break;
                                }
                                break;

                            case Header.KhachHang:
                                switch (receivedData.Option)
                                {
                                    case Options.View:
                                        UpdateStatus(client.Client.RemoteEndPoint.ToString(), $"Quản lý xem thông tin khách hàng");
                                        List<Packet> packets = DatabaseAccess.CustomersInfo("select * from KhachHang", receivedData);
                                        foreach (Packet packet in packets)
                                        {
                                            response = packet.GetDataStream();
                                            net_stream.Write(response, 0, response.Length);
                                            net_stream.Flush();
                                            Thread.Sleep(delay);
                                        }
                                        break;

                                    case Options.Insert:
                                        UpdateStatus(client.Client.RemoteEndPoint.ToString(), $"Quản lý thêm khách hàng");
                                        DatabaseAccess.InsertCustomer(receivedData);
                                        packets = DatabaseAccess.CustomersInfo("select * from KhachHang", receivedData);
                                        foreach (Packet packet in packets)
                                        {
                                            response = packet.GetDataStream();
                                            net_stream.Write(response, 0, response.Length);
                                            net_stream.Flush();
                                            Thread.Sleep(delay);
                                        }
                                        break;

                                    case Options.Delete:
                                        UpdateStatus(client.Client.RemoteEndPoint.ToString(), $"Quản lý xóa khách hàng {receivedData.KhachHang.MaKH}");
                                        DatabaseAccess.DeleteCustomer(receivedData);
                                        packets = DatabaseAccess.CustomersInfo("select * from KhachHang", receivedData);
                                        foreach (Packet packet in packets)
                                        {
                                            response = packet.GetDataStream();
                                            net_stream.Write(response, 0, response.Length);
                                            net_stream.Flush();
                                            Thread.Sleep(delay);
                                        }
                                        break;

                                    case Options.Update:
                                        UpdateStatus(client.Client.RemoteEndPoint.ToString(), $"Quản lý cập nhật thông tin khách hàng {receivedData.KhachHang.MaKH}");
                                        DatabaseAccess.UpdateCustomer(receivedData);
                                        packets = DatabaseAccess.CustomersInfo("select * from KhachHang", receivedData);
                                        foreach (Packet packet in packets)
                                        {
                                            response = packet.GetDataStream();
                                            net_stream.Write(response, 0, response.Length);
                                            net_stream.Flush();
                                            Thread.Sleep(delay);
                                        }
                                        break;

                                    case Options.Search:
                                        UpdateStatus(client.Client.RemoteEndPoint.ToString(), $"Quản lý tìm thông tin khách hàng {receivedData.KhachHang.MaKH}");
                                        packets = DatabaseAccess.CustomersInfo("select * from KhachHang where MaKH = @makh", receivedData);
                                        foreach (Packet packet in packets)
                                        {
                                            response = packet.GetDataStream();
                                            net_stream.Write(response, 0, response.Length);
                                            net_stream.Flush();
                                            Thread.Sleep(delay);
                                        }
                                        break;
                                }
                                break;

                            case Header.ChiTietHoaDon:
                                switch (receivedData.Option)
                                {
                                    case Options.View:
                                        UpdateStatus(client.Client.RemoteEndPoint.ToString(), $"Quản lý xem chi tiết hóa đơn");
                                        List<Packet> packets = DatabaseAccess.DetailInfo("select * from ChiTietHoaDon", receivedData);
                                        foreach (Packet packet in packets)
                                        {
                                            response = packet.GetDataStream();
                                            net_stream.Write(response, 0, response.Length);
                                            net_stream.Flush();
                                            Thread.Sleep(delay);
                                        }
                                        break;

                                    case Options.Search:
                                        UpdateStatus(client.Client.RemoteEndPoint.ToString(), $"Quản lý tìm thông tin chi tiết hóa đơn {receivedData.ChiTietHoaDon.MaHD}");
                                        packets = DatabaseAccess.DetailInfo("select * from ChiTietHoaDon where MaHD = @mahd", receivedData);
                                        foreach (Packet packet in packets)
                                        {
                                            response = packet.GetDataStream();
                                            net_stream.Write(response, 0, response.Length);
                                            net_stream.Flush();
                                            Thread.Sleep(delay);
                                        }
                                        break;
                                }
                                break;
                        }
                        break;

                    case User.NhanVien:
                        switch (receivedData.Header)
                        {
                            case Header.Ban:
                                switch (receivedData.Option)
                                {
                                    case Options.View:
                                        UpdateStatus(client.Client.RemoteEndPoint.ToString(), $"Nhân viên xem bàn ăn");
                                        List<Packet> packetList = DatabaseAccess.TableInfo("select * from Ban", receivedData);
                                        foreach (Packet packet in packetList)
                                        {
                                            response = packet.GetDataStream();
                                            net_stream.Write(response, 0, response.Length);
                                            net_stream.Flush();
                                            Thread.Sleep(delay);
                                        }
                                        break;

                                    case Options.Search:
                                        UpdateStatus(client.Client.RemoteEndPoint.ToString(), $"Nhân viên tìm thông tin bàn ăn {receivedData.Ban.MaBan}");
                                        if (DatabaseAccess.TableHasBeenBooked(receivedData))
                                        {
                                            sendData = new Packet()
                                            {
                                                User = User.NhanVien,
                                                Header = Header.Ban,
                                                Option = Options.Search,
                                                Message = "Tình trạng bàn: Có người"
                                            };
                                        }
                                        else
                                        {
                                            sendData = new Packet()
                                            {
                                                User = User.NhanVien,
                                                Header = Header.Ban,
                                                Option = Options.Search,
                                                Message = "Tình trạng bàn: Trống"
                                            };
                                        }
                                        response = sendData.GetDataStream();
                                        net_stream.Write(response, 0, response.Length);
                                        net_stream.Flush();
                                        break;

                                    case Options.Update:
                                        UpdateStatus(client.Client.RemoteEndPoint.ToString(), $"Nhân viên đặt bàn ăn {receivedData.Ban.MaBan}");
                                        DatabaseAccess.UpdateTable(receivedData);
                                        sendData = new Packet()
                                        {
                                            User = User.NhanVien,
                                            Header = Header.Ban,
                                            Option = Options.Update,
                                            Message = "Đặt bàn thành công!"
                                        };
                                        response = sendData.GetDataStream();
                                        net_stream.Write(response, 0, response.Length);
                                        net_stream.Flush();
                                        break;
                                }
                                break;

                            case Header.ThucDon:
                                UpdateStatus(client.Client.RemoteEndPoint.ToString(), $"Nhân viên xem các món ăn");
                                List<Packet> packets = DatabaseAccess.FoodInfo("select * from ThucDon", receivedData);
                                foreach (Packet packet in packets)
                                {
                                    response = packet.GetDataStream();
                                    net_stream.Write(response, 0, response.Length);
                                    net_stream.Flush();
                                    Thread.Sleep(delay);
                                }
                                break;

                            case Header.HoaDon:
                                UpdateStatus(client.Client.RemoteEndPoint.ToString(), $"Nhân viên thêm hóa đơn");
                                DatabaseAccess.InsertBill(receivedData);
                                sendData = new Packet()
                                {
                                    User = User.NhanVien,
                                    Header = Header.HoaDon,
                                    Option = Options.Insert,
                                    Message = "Thêm hóa đơn thành công!"
                                };
                                response = sendData.GetDataStream();
                                net_stream.Write(response, 0, response.Length);
                                net_stream.Flush();
                                break;

                            case Header.ChiTietHoaDon:
                                UpdateStatus(client.Client.RemoteEndPoint.ToString(), $"Nhân viên thêm chi tiết hóa đơn");
                                DatabaseAccess.InsertBillDetail(receivedData);
                                Thread.Sleep(delay * 10);
                                sendData = new Packet()
                                {
                                    User = User.NhanVien,
                                    Header = Header.ChiTietHoaDon,
                                    Option = Options.Insert,
                                    Message = "Thêm chi tiết hóa đơn thành công!"
                                };
                                response = sendData.GetDataStream();
                                net_stream.Write(response, 0, response.Length);
                                net_stream.Flush();
                                break;
                        }
                        break;
                }
            }
            UpdateStatus(client.Client.RemoteEndPoint.ToString(), "Đã rời phiên làm việc");
        }

        private void Listen()
        {
            tcpServer = new TcpListener(IPAddress.Any, 10000);
            tcpServer.Start();
            try
            {
                while (true)
                {
                    TcpClient client = tcpServer.AcceptTcpClient();
                    Thread receiveThread = new Thread(Receive)
                    {
                        IsBackground = true
                    };
                    receiveThread.Start(client);
                }
            }
            catch
            {
                tcpServer = new TcpListener(IPAddress.Any, 10000);
            }
        }

        private void frmServer_Load(object sender, EventArgs e)
        {
            conn = DatabaseAccess.GetConnection();
            conn.Open();

            Thread serverThread = new Thread(new ThreadStart(Listen));
            serverThread.Start();
        }

        private void frmServer_FormClosing(object sender, FormClosingEventArgs e)
        {
            conn.Close();
        }
    }
}