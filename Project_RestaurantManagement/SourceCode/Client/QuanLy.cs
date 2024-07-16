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
using PacketStructure;
using System.Xml.Linq;
using System.Threading;
using System.Globalization;

namespace Client
{
    public partial class frmQuanLy : Form
    {
        public frmQuanLy()
        {
            InitializeComponent();
        }

        private List<NhanVien> staffList = new List<NhanVien>();
        private List<ThucDon> foodList = new List<ThucDon>();
        private List<Ban> tableList = new List<Ban>();
        private List<HoaDon> billList = new List<HoaDon>();
        private List<KhachHang> customerList = new List<KhachHang>();
        private List<ChiTietHoadon> detailList = new List<ChiTietHoadon>();
        private const int delay = 1000;

        public void Receive()
        {
            try
            {
                while (true)
                {
                    NetworkStream net_stream = frmDangNhap.tcpClient.GetStream();
                    byte[] data = new byte[1024];
                    int byte_count = net_stream.Read(data, 0, data.Length);

                    if (byte_count == 0)
                    {
                        break;
                    }

                    Packet receivedData = new Packet(data);

                    switch (receivedData.Header)
                    {
                        case Header.NhanVien:
                            NhanVien staff = new NhanVien()
                            {
                                MaNV = receivedData.NhanVien.MaNV,
                                HoTenNV = receivedData.NhanVien.HoTenNV,
                                NgayVL = receivedData.NhanVien.NgayVL,
                                Luong = receivedData.NhanVien.Luong
                            };
                            staffList.Add(staff);
                            break;

                        case Header.ThucDon:
                            ThucDon food = new ThucDon()
                            {
                                MaMon = receivedData.ThucDon.MaMon,
                                TenMon = receivedData.ThucDon.TenMon,
                                DonVi = receivedData.ThucDon.DonVi,
                                Gia = receivedData.ThucDon.Gia
                            };
                            foodList.Add(food);
                            break;

                        case Header.Ban:
                            Ban table = new Ban()
                            {
                                MaBan = receivedData.Ban.MaBan,
                                TinhTrangBan = receivedData.Ban.TinhTrangBan
                            };
                            tableList.Add(table);
                            break;

                        case Header.HoaDon:
                            HoaDon bill = new HoaDon()
                            {
                                MaHD = receivedData.HoaDon.MaHD,
                                MaNV = receivedData.HoaDon.MaNV,
                                MaKH = receivedData.HoaDon.MaKH,
                                MaBan = receivedData.HoaDon.MaBan,
                                NgayHD = receivedData.HoaDon.NgayHD,
                                TriGia = receivedData.HoaDon.TriGia
                            };
                            billList.Add(bill);
                            break;

                        case Header.KhachHang:
                            KhachHang customer = new KhachHang()
                            {
                                MaKH = receivedData.KhachHang.MaKH,
                                HoTenKH = receivedData.KhachHang.HoTenKH,
                                SoDienThoai = receivedData.KhachHang.SoDienThoai
                            };
                            customerList.Add(customer);
                            break;

                        case Header.ChiTietHoaDon:
                            ChiTietHoadon detail = new ChiTietHoadon()
                            {
                                MaHD = receivedData.ChiTietHoaDon.MaHD,
                                MaMon = receivedData.ChiTietHoaDon.MaMon,
                                SoLuong = receivedData.ChiTietHoaDon.SoLuong
                            };
                            detailList.Add(detail);
                            break;
                    }
                }
            }
            catch
            {
                MessageBox.Show("Không thể kết nối tới máy chủ!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void frmQuanLy_Load(object sender, EventArgs e)
        {
            Thread managerThread = new Thread(new ThreadStart(Receive));
            managerThread.Start();
        }

        // Hiển thị toàn bộ dữ liệu của bảng NhanVien
        private void btn_Show_NhanVien_Click(object sender, EventArgs e)
        {
            if (staffList.Count > 0)
            {
                staffList.Clear();
            }

            // Tạo gói tin yêu cầu xem thông tin nhân viên
            try
            {
                NetworkStream net_stream = frmDangNhap.tcpClient.GetStream();
                Packet sendData = new Packet()
                {
                    User = User.QuanLy,
                    Header = Header.NhanVien,
                    Option = Options.View
                };
                byte[] data = sendData.GetDataStream();
                net_stream.Write(data, 0, data.Length);
                net_stream.Flush();

                // Tải dữ liệu lên Data Grid View
                Thread.Sleep(delay);
                var binding = new BindingSource()
                {
                    DataSource = staffList
                };
                dgv_NhanVien.DataSource = binding;
            }
            catch
            {
                MessageBox.Show("Không thể kết nối tới máy chủ!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Thêm dữ liệu vào bảng NhanVien
        private void btn_Add_NhanVien_Click(object sender, EventArgs e)
        {
            if (txt_MaNV.Text == "" || txt_HoTen_NhanVien.Text == "" || txt_Luong.Text == "")
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin nhân viên!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (staffList.Count > 0)
            {
                staffList.Clear();
            }

            // Tạo gói tin yêu cầu thêm nhân viên vào cơ sở dữ liệu
            try
            {
                NetworkStream net_stream = frmDangNhap.tcpClient.GetStream();
                Packet sendData = new Packet()
                {
                    User = User.QuanLy,
                    Header = Header.NhanVien,
                    Option = Options.Insert,
                    NhanVien = new NhanVien(txt_MaNV.Text, txt_HoTen_NhanVien.Text, DateTime.ParseExact(dtp_NgVL.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("MM/dd/yyyy"), (int)Convert.ToDecimal(txt_Luong.Text))
                };
                byte[] data = sendData.GetDataStream();
                net_stream.Write(data, 0, data.Length);
                net_stream.Flush();

                // Tải dữ liệu lên Data Grid View
                Thread.Sleep(delay);
                var binding = new BindingSource()
                {
                    DataSource = staffList
                };
                dgv_NhanVien.DataSource = binding;
            }
            catch
            {
                MessageBox.Show("Không thể kết nối tới máy chủ!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Cập nhật dữ liệu vào bảng NhanVien theo mã nhân viên
        private void btn_Update_NhanVien_Click(object sender, EventArgs e)
        {
            if (txt_MaNV.Text == "" || txt_HoTen_NhanVien.Text == "" || txt_Luong.Text == "")
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin nhân viên!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (staffList.Count > 0)
            {
                staffList.Clear();
            }

            // Tạo gói tin yêu cầu cập nhật
            try
            {
                NetworkStream net_stream = frmDangNhap.tcpClient.GetStream();
                Packet sendData = new Packet()
                {
                    User = User.QuanLy,
                    Header = Header.NhanVien,
                    Option = Options.Update,
                    NhanVien = new NhanVien(txt_MaNV.Text, txt_HoTen_NhanVien.Text, DateTime.ParseExact(dtp_NgVL.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("MM/dd/yyyy"), (int)Convert.ToDecimal(txt_Luong.Text))
                };
                byte[] data = sendData.GetDataStream();
                net_stream.Write(data, 0, data.Length);
                net_stream.Flush();

                // Tải dữ liệu lên Data Grid View
                Thread.Sleep(delay);
                var binding = new BindingSource()
                {
                    DataSource = staffList
                };
                dgv_NhanVien.DataSource = binding;
            }
            catch
            {
                MessageBox.Show("Không thể kết nối tới máy chủ!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        // Xóa dữ liệu ở bảng NhanVien theo mã nhân viên
        private void btn_Delete_NhanVien_Click(object sender, EventArgs e)
        {
            if (txt_MaNV.Text == "")
            {
                MessageBox.Show("Vui lòng nhập mã nhân viên cần xóa!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (staffList.Count > 0)
            {
                staffList.Clear();
            }

            // Tạo gói tin yêu cầu xóa thông tin một nhân viên
            try
            {
                NetworkStream net_stream = frmDangNhap.tcpClient.GetStream();
                Packet sendData = new Packet()
                {
                    User = User.QuanLy,
                    Header = Header.NhanVien,
                    Option = Options.Delete,
                    NhanVien = new NhanVien(txt_MaNV.Text, "", "", 0)
                };
                byte[] data = sendData.GetDataStream();
                net_stream.Write(data, 0, data.Length);
                net_stream.Flush();

                // Tải dữ liệu lên Data Grid View
                Thread.Sleep(delay);
                var binding = new BindingSource()
                {
                    DataSource = staffList
                };
                dgv_NhanVien.DataSource = binding;
            }
            catch
            {
                MessageBox.Show("Không thể kết nối tới máy chủ!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Xóa các thông tin tạm trên các ô textbox ở tab Nhân viên
        private void btn_Clear_NhanVien_Click(object sender, EventArgs e)
        {
            txt_MaNV.Text = "";
            txt_HoTen_NhanVien.Text = "";
            dtp_NgVL.Text = DateTime.Now.ToString();
            txt_Luong.Text = "";
            txt_Find_NhanVien.Text = "";
        }

        // Tìm kiếm thông tin nhân viên theo mã nhân viên
        private void btn_Find_NhanVien_Click(object sender, EventArgs e)
        {
            if (txt_Find_NhanVien.Text == "")
            {
                MessageBox.Show("Vui lòng nhập mã nhân viên cần tìm!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (staffList.Count > 0)
            {
                staffList.Clear();
            }

            // Tạo gói tin yêu cầu tìm thông tin của một nhân viên
            try
            {
                NetworkStream net_stream = frmDangNhap.tcpClient.GetStream();
                Packet sendData = new Packet()
                {
                    User = User.QuanLy,
                    Header = Header.NhanVien,
                    Option = Options.Search,
                    NhanVien = new NhanVien(txt_Find_NhanVien.Text, "", "", 0)
                };
                byte[] data = sendData.GetDataStream();
                net_stream.Write(data, 0, data.Length);
                net_stream.Flush();

                // Tải dữ liệu lên Data Grid View
                Thread.Sleep(delay);
                var binding = new BindingSource()
                {
                    DataSource = staffList
                };
                dgv_NhanVien.DataSource = binding;
            }
            catch
            {
                MessageBox.Show("Không thể kết nối tới máy chủ!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Chọn 1 hàng trong bảng NhanVien (chọn toàn bộ thông tin của 1 nhân viên)
        private void dgv_NhanVien_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            txt_MaNV.Text = dgv_NhanVien.SelectedCells[0].Value.ToString();
            txt_HoTen_NhanVien.Text = dgv_NhanVien.SelectedCells[1].Value.ToString();

            string date = dgv_NhanVien.SelectedCells[2].Value.ToString();
            dtp_NgVL.Text = DateTime.ParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("MM/dd/yyyy");

            int iLuong = (int)dgv_NhanVien.SelectedCells[3].Value;
            txt_Luong.Text = iLuong.ToString("N0");
        }

        // Hiển thị toàn bộ dữ liệu của bảng ThucDon
        private void btn_Show_ThucDon_Click(object sender, EventArgs e)
        {
            if (foodList.Count > 0)
            {
                foodList.Clear();
            }

            // Tạo gói tin yêu cầu xem thông tin thực đơn
            try
            {
                NetworkStream net_stream = frmDangNhap.tcpClient.GetStream();
                Packet sendData = new Packet()
                {
                    User = User.QuanLy,
                    Header = Header.ThucDon,
                    Option = Options.View
                };
                byte[] data = sendData.GetDataStream();
                net_stream.Write(data, 0, data.Length);
                net_stream.Flush();

                // Tải dữ liệu lên Data Grid View
                Thread.Sleep(delay);
                var binding = new BindingSource()
                {
                    DataSource = foodList
                };
                dgv_ThucDon.DataSource = binding;
            }
            catch
            {
                MessageBox.Show("Không thể kết nối tới máy chủ!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Thêm dữ liệu vào bảng ThucDon
        private void btn_Add_ThucDon_Click(object sender, EventArgs e)
        {
            if (txt_MaMon.Text == "" || txt_TenMon.Text == "" || txt_DVT.Text == "" || txt_Gia.Text == "")
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin món ăn!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (foodList.Count > 0)
            {
                foodList.Clear();
            }

            // Tạo gói tin yêu cầu thêm thông tin thực đơn\
            try
            {
                NetworkStream net_stream = frmDangNhap.tcpClient.GetStream();
                Packet sendData = new Packet()
                {
                    User = User.QuanLy,
                    Header = Header.ThucDon,
                    Option = Options.Insert,
                    ThucDon = new ThucDon(txt_MaMon.Text, txt_TenMon.Text, txt_DVT.Text, (int)Convert.ToDecimal(txt_Gia.Text))
                };
                byte[] data = sendData.GetDataStream();
                net_stream.Write(data, 0, data.Length);
                net_stream.Flush();

                // Tải dữ liệu lên Data Grid View
                Thread.Sleep(delay);
                var binding = new BindingSource()
                {
                    DataSource = foodList
                };
                dgv_ThucDon.DataSource = binding;
            }
            catch
            {
                MessageBox.Show("Không thể kết nối tới máy chủ!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Cập nhật dữ liệu vào bảng ThucDon theo mã món
        private void btn_Update_ThucDon_Click(object sender, EventArgs e)
        {
            if (foodList.Count > 0)
            {
                foodList.Clear();
            }

            if (txt_MaMon.Text == "" || txt_TenMon.Text == "" || txt_DVT.Text == "" || txt_Gia.Text == "")
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin món ăn!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Tạo gói tin yêu cầu thay đổi thông tin thực đơn
            try
            {
                NetworkStream net_stream = frmDangNhap.tcpClient.GetStream();
                Packet sendData = new Packet()
                {
                    User = User.QuanLy,
                    Header = Header.ThucDon,
                    Option = Options.Update,
                    ThucDon = new ThucDon(txt_MaMon.Text, txt_TenMon.Text, txt_DVT.Text, (int)Convert.ToDecimal(txt_Gia.Text))
                };
                byte[] data = sendData.GetDataStream();
                net_stream.Write(data, 0, data.Length);
                net_stream.Flush();

                // Tải dữ liệu lên Data Grid View
                Thread.Sleep(delay);
                var binding = new BindingSource()
                {
                    DataSource = foodList
                };
                dgv_ThucDon.DataSource = binding;
            }
            catch
            {
                MessageBox.Show("Không thể kết nối tới máy chủ!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Xóa dữ liệu ở bảng NhanVien theo mã món
        private void btn_Delete_ThucDon_Click(object sender, EventArgs e)
        {
            if (txt_MaMon.Text == "")
            {
                MessageBox.Show("Vui lòng nhập mã món cần xóa!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (foodList.Count > 0)
            {
                foodList.Clear();
            }

            // Tạo gói tin yêu cầu xóa thông tin thực đơn
            try
            {
                NetworkStream net_stream = frmDangNhap.tcpClient.GetStream();
                Packet sendData = new Packet()
                {
                    User = User.QuanLy,
                    Header = Header.ThucDon,
                    Option = Options.Delete,
                    ThucDon = new ThucDon(txt_MaMon.Text, "", "", 0)
                };
                byte[] data = sendData.GetDataStream();
                net_stream.Write(data, 0, data.Length);
                net_stream.Flush();

                // Tải dữ liệu lên Data Grid View
                Thread.Sleep(delay);
                var binding = new BindingSource()
                {
                    DataSource = foodList
                };
                dgv_ThucDon.DataSource = binding;
            }
            catch
            {
                MessageBox.Show("Không thể kết nối tới máy chủ!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Xóa các thông tin tạm trên các ô textbox ở tab Thực đơn
        private void btn_Clear_ThucDon_Click(object sender, EventArgs e)
        {
            txt_MaMon.Text = "";
            txt_TenMon.Text = "";
            txt_DVT.Text = "";
            txt_Gia.Text = "";
            txt_Find_ThucDon.Text = "";
        }

        // Tìm kiếm thông tin thực đơn theo mã món
        private void btn_Find_ThucDon_Click(object sender, EventArgs e)
        {
            if (txt_Find_ThucDon.Text == "")
            {
                MessageBox.Show("Vui lòng nhập mã món cần tìm!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (foodList.Count > 0)
            {
                foodList.Clear();
            }

            // Tạo gói tin yêu cầu tìm thông tin của một thực đơn
            try
            {
                NetworkStream net_stream = frmDangNhap.tcpClient.GetStream();
                Packet sendData = new Packet()
                {
                    User = User.QuanLy,
                    Header = Header.ThucDon,
                    Option = Options.Search,
                    ThucDon = new ThucDon(txt_Find_ThucDon.Text, "", "", 0)
                };
                byte[] data = sendData.GetDataStream();
                net_stream.Write(data, 0, data.Length);
                net_stream.Flush();

                // Tải dữ liệu lên Data Grid View
                Thread.Sleep(delay);
                var binding = new BindingSource()
                {
                    DataSource = foodList
                };
                dgv_ThucDon.DataSource = binding;
            }
            catch
            {
                MessageBox.Show("Không thể kết nối tới máy chủ!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Chọn 1 hàng trong bảng ThucDon (chọn toàn bộ thông tin của 1 thực đơn)
        private void dgv_ThucDon_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            txt_MaMon.Text = dgv_ThucDon.SelectedCells[0].Value.ToString();
            txt_TenMon.Text = dgv_ThucDon.SelectedCells[1].Value.ToString();
            txt_DVT.Text = dgv_ThucDon.SelectedCells[2].Value.ToString();

            int iGia = (int)dgv_ThucDon.SelectedCells[3].Value;
            txt_Gia.Text = iGia.ToString("N0");
        }

        // Hiển thị toàn bộ dữ liệu của bảng Ban
        private void btn_Show_all_Ban_Click(object sender, EventArgs e)
        {
            if (tableList.Count > 0)
            {
                tableList.Clear();
            }

            // Tạo gói tin yêu cầu xem thông tin bàn ăn
            try
            {
                NetworkStream net_stream = frmDangNhap.tcpClient.GetStream();
                Packet sendData = new Packet()
                {
                    User = User.QuanLy,
                    Header = Header.Ban,
                    Option = Options.View,
                };
                byte[] data = sendData.GetDataStream();
                net_stream.Write(data, 0, data.Length);
                net_stream.Flush();

                // Tải dữ liệu lên Data Grid View
                Thread.Sleep(delay);
                var binding = new BindingSource()
                {
                    DataSource = tableList
                };
                dgv_Ban.DataSource = binding;
            }
            catch
            {
                MessageBox.Show("Không thể kết nối tới máy chủ!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Xóa các thông tin tạm trên các ô textbox ở tab Bàn ăn
        private void btn_Clear_Ban_Click(object sender, EventArgs e)
        {
            txt_MaBan.Text = "";
            txt_TinhTrangBan.Text = "";
            txt_Find_Ban.Text = "";
        }

        // Tìm kiếm thông tin bàn ăn theo mã bàn
        private void btn_Find_Ban_Click(object sender, EventArgs e)
        {
            if (txt_Find_Ban.Text == "")
            {
                MessageBox.Show("Vui lòng nhập mã bàn cần tìm!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (tableList.Count > 0)
            {
                tableList.Clear();
            }

            // Tạo gói tin yêu cầu tìm thông tin một bàn ăn
            try
            {
                NetworkStream net_stream = frmDangNhap.tcpClient.GetStream();
                Packet sendData = new Packet()
                {
                    User = User.QuanLy,
                    Header = Header.Ban,
                    Option = Options.Search,
                    Ban = new Ban(txt_Find_Ban.Text, "")
                };
                byte[] data = sendData.GetDataStream();
                net_stream.Write(data, 0, data.Length);
                net_stream.Flush();

                // Tải dữ liệu lên Data Grid View
                Thread.Sleep(delay);
                var binding = new BindingSource()
                {
                    DataSource = tableList
                };
                dgv_Ban.DataSource = binding;
            }
            catch
            {
                MessageBox.Show("Không thể kết nối tới máy chủ!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Chọn 1 hàng trong bảng Ban (chọn toàn bộ thông tin của 1 bàn)
        private void dgv_Ban_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            txt_MaBan.Text = dgv_Ban.SelectedCells[0].Value.ToString();
            txt_TinhTrangBan.Text = dgv_Ban.SelectedCells[1].Value.ToString();
        }

        // Hiển thị toàn bộ dữ liệu của bảng HoaDon
        private void btn_Show_HoaDon_Click(object sender, EventArgs e)
        {
            if (billList.Count > 0)
            {
                billList.Clear();
            }

            // Tạo gói tin yêu cầu xem thông tin các hóa đơn
            try
            {
                NetworkStream net_stream = frmDangNhap.tcpClient.GetStream();
                Packet sendData = new Packet()
                {
                    User = User.QuanLy,
                    Header = Header.HoaDon,
                    Option = Options.View
                };
                byte[] data = sendData.GetDataStream();
                net_stream.Write(data, 0, data.Length);
                net_stream.Flush();

                // Tải dữ liệu lên Data Grid View
                Thread.Sleep(delay);
                var binding = new BindingSource()
                {
                    DataSource = billList
                };
                dgv_HoaDon.DataSource = binding;
            }
            catch
            {
                MessageBox.Show("Không thể kết nối tới máy chủ!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Xóa các thông tin tạm trên các ô textbox ở tab Hóa đơn
        private void btn_Clear_HoaDon_Click(object sender, EventArgs e)
        {
            txt_MaHD.Text = "";
            txt_MaNV_HoaDon.Text = "";
            txt_MaKH_HoaDon.Text = "";
            txt_MaBan_HoaDon.Text = "";
            txt_NgHD.Text = "";
            txt_TriGia.Text = "";
            txt_Find_HoaDon.Text = "";
        }

        // Tìm kiếm thông tin hóa đơn theo mã hóa đơn
        private void btn_Find_HoaDon_Click(object sender, EventArgs e)
        {
            if (txt_Find_HoaDon.Text == "")
            {
                MessageBox.Show("Vui lòng nhập mã hóa đơn cần tìm!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (billList.Count > 0)
            {
                billList.Clear();
            }

            // Tạo gói tin yêu cầu tìm thông tin của một hóa đơn
            try
            {
                NetworkStream net_stream = frmDangNhap.tcpClient.GetStream();
                Packet sendData = new Packet()
                {
                    User = User.QuanLy,
                    Header = Header.HoaDon,
                    Option = Options.Search,
                    HoaDon = new HoaDon(txt_Find_HoaDon.Text, "", "", "", "", 0)
                };
                byte[] data = sendData.GetDataStream();
                net_stream.Write(data, 0, data.Length);
                net_stream.Flush();

                // Tải dữ liệu lên Data Grid View
                Thread.Sleep(delay);
                var binding = new BindingSource()
                {
                    DataSource = billList
                };
                dgv_HoaDon.DataSource = binding;
            }
            catch
            {
                MessageBox.Show("Không thể kết nối tới máy chủ!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Chọn 1 hàng trong bảng HoaDon (chọn toàn bộ thông tin của 1 hóa đơn)
        private void dgv_HoaDon_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            txt_MaHD.Text = dgv_HoaDon.SelectedCells[0].Value.ToString();
            txt_MaNV_HoaDon.Text = dgv_HoaDon.SelectedCells[1].Value.ToString();
            txt_MaKH_HoaDon.Text = dgv_HoaDon.SelectedCells[2].Value.ToString();
            txt_MaBan_HoaDon.Text = dgv_HoaDon.SelectedCells[3].Value.ToString();
            txt_NgHD.Text = dgv_HoaDon.SelectedCells[4].Value.ToString();

            int iTriGia = (int)dgv_HoaDon.SelectedCells[5].Value;
            txt_TriGia.Text = iTriGia.ToString("N0");
        }

        // Hiển thị toàn bộ dữ liệu của bảng KhachHang
        private void btn_Show_KhachHang_Click(object sender, EventArgs e)
        {
            if (customerList.Count > 0)
            {
                customerList.Clear();
            }

            // Tạo gói tin yêu cầu xem thông tin các khách hàng
            try
            {
                NetworkStream net_stream = frmDangNhap.tcpClient.GetStream();
                Packet sendData = new Packet()
                {
                    User = User.QuanLy,
                    Header = Header.KhachHang,
                    Option = Options.View,
                };
                byte[] data = sendData.GetDataStream();
                net_stream.Write(data, 0, data.Length);
                net_stream.Flush();

                // Tải dữ liệu lên Data Grid View
                Thread.Sleep(delay);
                var binding = new BindingSource()
                {
                    DataSource = customerList
                };
                dgv_KhachHang.DataSource = binding;
            }
            catch
            {
                MessageBox.Show("Không thể kết nối tới máy chủ!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Thêm thông tin khách hàng
        private void btn_Insert_KhachHang_Click(object sender, EventArgs e)
        {
            if (txt_MaKH.Text == "" || txt_HoTen_KhachHang.Text == "" || txt_SDT.Text == "")
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin khách hàng!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (txt_SDT.TextLength > 10)
            {
                MessageBox.Show("Số điện thoại chỉ có 10 chữ số!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (customerList.Count > 0)
            {
                customerList.Clear();
            }

            // Tạo gói tin yêu cầu thêm thông tin các khách hàng
            try
            {
                NetworkStream net_stream = frmDangNhap.tcpClient.GetStream();
                Packet sendData = new Packet()
                {
                    User = User.QuanLy,
                    Header = Header.KhachHang,
                    Option = Options.Insert,
                    KhachHang = new KhachHang(txt_MaKH.Text, txt_HoTen_KhachHang.Text.Trim(), txt_SDT.Text)
                };
                byte[] data = sendData.GetDataStream();
                net_stream.Write(data, 0, data.Length);
                net_stream.Flush();

                // Tải dữ liệu lên Data Grid View
                Thread.Sleep(delay);
                var binding = new BindingSource()
                {
                    DataSource = customerList
                };
                dgv_KhachHang.DataSource = binding;
            }
            catch
            {
                MessageBox.Show("Không thể kết nối tới máy chủ!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Cập nhật thông tin của khách hàng
        private void btn_Update_KhachHang_Click(object sender, EventArgs e)
        {
            if (txt_MaKH.Text == "" || txt_HoTen_KhachHang.Text == "" || txt_SDT.Text == "")
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin khách hàng!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (txt_SDT.TextLength > 10)
            {
                MessageBox.Show("Số điện thoại chỉ có 10 chữ số!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (customerList.Count > 0)
            {
                customerList.Clear();
            }

            // Tạo gói tin yêu cầu cập nhật thông tin các khách hàng
            try
            {
                NetworkStream net_stream = frmDangNhap.tcpClient.GetStream();
                Packet sendData = new Packet()
                {
                    User = User.QuanLy,
                    Header = Header.KhachHang,
                    Option = Options.Update,
                    KhachHang = new KhachHang(txt_MaKH.Text, txt_HoTen_KhachHang.Text.Trim(), txt_SDT.Text)
                };
                byte[] data = sendData.GetDataStream();
                net_stream.Write(data, 0, data.Length);
                net_stream.Flush();

                // Tải dữ liệu lên Data Grid View
                Thread.Sleep(delay);
                var binding = new BindingSource()
                {
                    DataSource = customerList
                };
                dgv_KhachHang.DataSource = binding;
            }
            catch
            {
                MessageBox.Show("Không thể kết nối tới máy chủ!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Xóa thông tin khách hàng
        private void btn_Delete_KhachHang_Click(object sender, EventArgs e)
        {
            if (txt_MaKH.Text == "")
            {
                MessageBox.Show("Yêu cầu nhập mã khách hàng cần xóa!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (customerList.Count > 0)
            {
                customerList.Clear();
            }

            // Tạo gói tin yêu cầu xóa thông tin các khách hàng
            try
            {
                NetworkStream net_stream = frmDangNhap.tcpClient.GetStream();
                Packet sendData = new Packet()
                {
                    User = User.QuanLy,
                    Header = Header.KhachHang,
                    Option = Options.Delete,
                    KhachHang = new KhachHang(txt_MaKH.Text, "", "")
                };
                byte[] data = sendData.GetDataStream();
                net_stream.Write(data, 0, data.Length);
                net_stream.Flush();

                // Tải dữ liệu lên Data Grid View
                Thread.Sleep(delay);
                var binding = new BindingSource()
                {
                    DataSource = customerList
                };
                dgv_KhachHang.DataSource = binding;
            }
            catch
            {
                MessageBox.Show("Không thể kết nối tới máy chủ!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Xóa các thông tin tạm trên các ô textbox ở tab Khách hàng
        private void btn_Clear_KhachHang_Click(object sender, EventArgs e)
        {
            txt_MaKH.Text = "";
            txt_HoTen_KhachHang.Text = "";
            txt_SDT.Text = "";
            txt_Find_KhachHang.Text = "";
        }

        // Tìm kiếm thông tin khách hàng theo mã khách hàng
        private void btn_Find_KhachHang_Click(object sender, EventArgs e)
        {
            if (txt_Find_KhachHang.Text == "")
            {
                MessageBox.Show("Vui lòng nhập mã khách hàng cần tìm!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (customerList.Count > 0)
            {
                customerList.Clear();
            }

            // Tạo gói tin yêu cầu tìm thông tin của một khách hàng
            try
            {
                NetworkStream net_stream = frmDangNhap.tcpClient.GetStream();
                Packet sendData = new Packet()
                {
                    User = User.QuanLy,
                    Header = Header.KhachHang,
                    Option = Options.Search,
                    KhachHang = new KhachHang(txt_Find_KhachHang.Text, "", "")
                };
                byte[] data = sendData.GetDataStream();
                net_stream.Write(data, 0, data.Length);
                net_stream.Flush();

                // Tải dữ liệu lên Data Grid View
                Thread.Sleep(delay);
                var binding = new BindingSource()
                {
                    DataSource = customerList
                };
                dgv_KhachHang.DataSource = binding;
            }
            catch
            {
                MessageBox.Show("Không thể kết nối tới máy chủ!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Chọn 1 hàng trong bảng KhachHang (chọn toàn bộ thông tin của 1 khách hàng)
        private void dgv_KhachHang_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            txt_MaKH.Text = dgv_KhachHang.SelectedCells[0].Value.ToString();
            txt_HoTen_KhachHang.Text = dgv_KhachHang.SelectedCells[1].Value.ToString();
            txt_SDT.Text = dgv_KhachHang.SelectedCells[2].Value.ToString();
        }

        // Hiển thị toàn bộ dữ liệu của bảng ChiTietHoaDon
        private void btn_Show_CTHD_Click(object sender, EventArgs e)
        {
            if (detailList.Count > 0)
            {
                detailList.Clear();
            }

            // Tạo gói tin yêu cầu xem thông tin các chi tiết hóa đơn
            try
            {
                NetworkStream net_stream = frmDangNhap.tcpClient.GetStream();
                Packet sendData = new Packet()
                {
                    User = User.QuanLy,
                    Header = Header.ChiTietHoaDon,
                    Option = Options.View
                };
                byte[] data = sendData.GetDataStream();
                net_stream.Write(data, 0, data.Length);
                net_stream.Flush();

                // Tải dữ liệu lên Data Grid View
                Thread.Sleep(delay);
                var binding = new BindingSource()
                {
                    DataSource = detailList
                };
                dgv_CTHD.DataSource = binding;
            }
            catch
            {
                MessageBox.Show("Không thể kết nối tới máy chủ!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Xóa các thông tin tạm trên các ô textbox ở tab Chi tiết hóa đơn
        private void btn_Clear_CTHD_Click(object sender, EventArgs e)
        {
            txt_MaHD_CTHD.Text = "";
            txt_MaMon_CTHD.Text = "";
            txt_SL.Text = "";
            txt_Find_CTHD.Text = "";
        }

        // Chọn 1 hàng trong bảng ChiTietHoaDon (chọn toàn bộ thông tin của 1 chi tiết hóa đơn)
        private void btn_Find_CTHD_Click(object sender, EventArgs e)
        {
            if (txt_Find_CTHD.Text == "")
            {
                MessageBox.Show("Vui lòng nhập mã các chi tiết hóa đơn cần tìm!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (detailList.Count > 0)
            {
                detailList.Clear();
            }

            // Tạo gói tin yêu cầu tìm thông tin của một chi tiết hóa đơn
            try
            {
                NetworkStream net_stream = frmDangNhap.tcpClient.GetStream();
                Packet sendData = new Packet()
                {
                    User = User.QuanLy,
                    Header = Header.ChiTietHoaDon,
                    Option = Options.Search,
                    ChiTietHoaDon = new ChiTietHoadon(txt_Find_CTHD.Text, "", 0)
                };
                byte[] data = sendData.GetDataStream();
                net_stream.Write(data, 0, data.Length);
                net_stream.Flush();

                // Tải dữ liệu lên Data Grid View
                Thread.Sleep(delay);
                var binding = new BindingSource()
                {
                    DataSource = detailList
                };
                dgv_CTHD.DataSource = binding;
            }
            catch
            {
                MessageBox.Show("Không thể kết nối tới máy chủ!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Chọn 1 hàng trong bảng ChiTietHoaDon (chọn toàn bộ thông tin của 1 chi tiết hóa đơn)
        private void dgv_CTHD_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            txt_MaHD_CTHD.Text = dgv_CTHD.SelectedCells[0].Value.ToString();
            txt_MaMon_CTHD.Text = dgv_CTHD.SelectedCells[1].Value.ToString();
            txt_SL.Text = dgv_CTHD.SelectedCells[2].Value.ToString();
        }

        private void frmQuanLy_FormClosing(object sender, FormClosingEventArgs e)
        {
            frmDangNhap.tcpClient.Close();
        }
    }
}