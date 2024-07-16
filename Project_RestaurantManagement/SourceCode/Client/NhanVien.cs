using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Threading;
using System.Net.Sockets;
using System.Windows.Forms;
using PacketStructure;

namespace Client
{
    public partial class frmNhanVien : Form
    {
        public frmNhanVien()
        {
            InitializeComponent();
        }

        // Đối tượng Table lưu trữ thông tin mã bàn, tổng giá trị bữa ăn và hóa đơn bàn ăn đó
        public class Table
        {
            public string MaBan { get; set; }
            public int TongTien { get; set; }
            public ListView HoaDon { get; set; }
        }

        // Đánh dấu bàn ăn
        private int id_table_selected = -1;

        // Khởi tạo danh sách bàn ăn
        List<Table> tableList = new List<Table>();

        // Khởi tạo danh sách món ăn
        List<ThucDon> foodList = new List<ThucDon>();

        private const int delay = 100;

        private void Reset()
        {
            tableList[id_table_selected].HoaDon.Items.Clear();
            lv_Bill.Items.Clear();
            txt_MANV.Text = string.Empty;
            txt_MAKH.Text = string.Empty;
            txt_MABAN.Text = string.Empty;
            txt_MAHD.Text = string.Empty;
            txt_quantity.Text = string.Empty;
            cb_Name_dish.Text = "Tên món";
        }

        private void Receive()
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
                        case Header.Ban:
                            switch (receivedData.Option)
                            {
                                case Options.View:
                                    Table table = new Table()
                                    {
                                        MaBan = receivedData.Ban.MaBan,
                                        TongTien = 0,
                                        HoaDon = new ListView()
                                    };
                                    tableList.Add(table);
                                    break;

                                case Options.Search:
                                    this.Invoke(new Action(() =>
                                    {
                                        lb_table_status.Text = receivedData.Message;
                                        btn_add_dish.Enabled = true;
                                        btn_remove_dish.Enabled = true;
                                        btn_order_table.Enabled = true;

                                        if (receivedData.Message == "Tình trạng bàn: Trống")
                                        {
                                            btn_order_table.Text = "Đặt bàn";
                                            btn_pay.Enabled = false;
                                        }
                                        else
                                        {
                                            btn_order_table.Text = "Hủy bàn";
                                            btn_pay.Enabled = true;
                                        }
                                    }));
                                    break;

                                case Options.Update:
                                    MessageBox.Show(receivedData.Message);
                                    break;
                            }
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
                            this.Invoke(new Action(() =>
                            {
                                cb_Name_dish.Items.Add(receivedData.ThucDon.TenMon);
                            }));
                            break;

                        case Header.HoaDon:
                            MessageBox.Show(receivedData.Message);
                            break;

                        case Header.ChiTietHoaDon:
                            MessageBox.Show(receivedData.Message);
                            break;
                    }
                }
            }
            catch
            {
                MessageBox.Show("Không thể kết nối tới máy chủ!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Tự động hiện mã bàn lên giao diện
        private void Auto_fill_MaBan()
        {
            txt_MABAN.Text = tableList[id_table_selected].MaBan;
        }

        // Hiển thị hóa đơn của bàn ăn
        private void Load_bill()
        {
            lv_Bill.Items.Clear();
            foreach (ListViewItem item in tableList[id_table_selected].HoaDon.Items)
            {
                lv_Bill.Items.Add((ListViewItem)item.Clone());
            }
        }

        // Lưu hóa đơn của một bàn
        private void Save_bill()
        {
            tableList[id_table_selected].HoaDon.Items.Clear();
            foreach (ListViewItem item in lv_Bill.Items)
            {
                tableList[id_table_selected].HoaDon.Items.Add((ListViewItem)item.Clone());
            }
        }

        // Tính tổng tiền của một hóa đơn
        private void Cal_total(int total)
        {
            tableList[id_table_selected].TongTien += total;
            lb_total_payment.Text = "Tổng tiền: " + tableList[id_table_selected].TongTien.ToString("N0") + " VNĐ";
        }

        private void frmNhanVien_Load(object sender, EventArgs e)
        {
            // Khóa các chức năng.
            btn_add_dish.Enabled = false;
            btn_remove_dish.Enabled = false;
            btn_order_table.Enabled = false;
            btn_pay.Enabled = false;

            // Khởi động tiến trình nghe bên Client
            Thread staffThread = new Thread(new ThreadStart(Receive));
            staffThread.Start();

            // Gửi gói tin yêu cầu lấy thông tin các món ăn
            try
            {
                NetworkStream net_stream = frmDangNhap.tcpClient.GetStream();
                Packet sendData = new Packet()
                {
                    User = User.NhanVien,
                    Header = Header.ThucDon,
                    Option = Options.View
                };
                byte[] data = sendData.GetDataStream();
                net_stream.Write(data, 0, data.Length);
                net_stream.Flush();
                Thread.Sleep(delay);

                // Gửi gói tin yêu cầu lấy thông tin mã bàn
                sendData = new Packet()
                {
                    User = User.NhanVien,
                    Header = Header.Ban,
                    Option = Options.View
                };
                data = sendData.GetDataStream();
                net_stream.Write(data, 0, data.Length);
                net_stream.Flush();
            }
            catch
            {
                MessageBox.Show("Không thể kết nối tới máy chủ!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_table1_Click(object sender, EventArgs e)
        {
            id_table_selected = 0;
            Auto_fill_MaBan();
            Load_bill();

            // Gửi gói tin yêu cầu đặt bàn ăn 1
            try
            {
                NetworkStream net_stream = frmDangNhap.tcpClient.GetStream();
                Packet sendData = new Packet()
                {
                    User = User.NhanVien,
                    Header = Header.Ban,
                    Option = Options.Search,
                    Ban = new Ban(tableList[id_table_selected].MaBan, "")
                };
                byte[] data = sendData.GetDataStream();
                net_stream.Write(data, 0, data.Length);
                net_stream.Flush();

                // Hiển thị tổng tiền của hóa đơn
                lb_total_payment.Text = "Tổng tiền: " + tableList[id_table_selected].TongTien.ToString("N0") + " VNĐ";
            }
            catch
            {
                MessageBox.Show("Không thể kết nối tới máy chủ!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_table2_Click(object sender, EventArgs e)
        {
            id_table_selected = 1;
            Auto_fill_MaBan();
            Load_bill();

            // Gửi gói tin yêu cầu đặt bàn ăn 2
            try
            {
                NetworkStream net_stream = frmDangNhap.tcpClient.GetStream();
                Packet sendData = new Packet()
                {
                    User = User.NhanVien,
                    Header = Header.Ban,
                    Option = Options.Search,
                    Ban = new Ban(tableList[id_table_selected].MaBan, "")
                };
                byte[] data = sendData.GetDataStream();
                net_stream.Write(data, 0, data.Length);
                net_stream.Flush();

                // Hiển thị tổng tiền của hóa đơn
                lb_total_payment.Text = "Tổng tiền: " + tableList[id_table_selected].TongTien.ToString("N0") + " VNĐ";
            }
            catch
            {
                MessageBox.Show("Không thể kết nối tới máy chủ!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_table3_Click(object sender, EventArgs e)
        {
            id_table_selected = 2;
            Auto_fill_MaBan();
            Load_bill();

            // Gửi gói tin yêu cầu đặt bàn ăn 3
            try
            {
                NetworkStream net_stream = frmDangNhap.tcpClient.GetStream();
                Packet sendData = new Packet()
                {
                    User = User.NhanVien,
                    Header = Header.Ban,
                    Option = Options.Search,
                    Ban = new Ban(tableList[id_table_selected].MaBan, "")
                };
                byte[] data = sendData.GetDataStream();
                net_stream.Write(data, 0, data.Length);
                net_stream.Flush();

                // Hiển thị tổng tiền của hóa đơn
                lb_total_payment.Text = "Tổng tiền: " + tableList[id_table_selected].TongTien.ToString("N0") + " VNĐ";
            }
            catch
            {
                MessageBox.Show("Không thể kết nối tới máy chủ!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_table4_Click(object sender, EventArgs e)
        {
            id_table_selected = 3;
            Auto_fill_MaBan();
            Load_bill();

            // Gửi gói tin yêu cầu đặt bàn ăn 4
            try
            {
                NetworkStream net_stream = frmDangNhap.tcpClient.GetStream();
                Packet sendData = new Packet()
                {
                    User = User.NhanVien,
                    Header = Header.Ban,
                    Option = Options.Search,
                    Ban = new Ban(tableList[id_table_selected].MaBan, "")
                };
                byte[] data = sendData.GetDataStream();
                net_stream.Write(data, 0, data.Length);
                net_stream.Flush();

                // Hiển thị tổng tiền của hóa đơn
                lb_total_payment.Text = "Tổng tiền: " + tableList[id_table_selected].TongTien.ToString("N0") + " VNĐ";
            }
            catch
            {
                MessageBox.Show("Không thể kết nối tới máy chủ!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_table5_Click(object sender, EventArgs e)
        {
            id_table_selected = 4;
            Auto_fill_MaBan();
            Load_bill();

            // Gửi gói tin yêu cầu đặt bàn ăn 5
            try
            {
                NetworkStream net_stream = frmDangNhap.tcpClient.GetStream();
                Packet sendData = new Packet()
                {
                    User = User.NhanVien,
                    Header = Header.Ban,
                    Option = Options.Search,
                    Ban = new Ban(tableList[id_table_selected].MaBan, "")
                };
                byte[] data = sendData.GetDataStream();
                net_stream.Write(data, 0, data.Length);
                net_stream.Flush();

                // Hiển thị tổng tiền của hóa đơn
                lb_total_payment.Text = "Tổng tiền: " + tableList[id_table_selected].TongTien.ToString("N0") + " VNĐ";
            }
            catch
            {
                MessageBox.Show("Không thể kết nối tới máy chủ!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_table6_Click(object sender, EventArgs e)
        {
            id_table_selected = 5;
            Auto_fill_MaBan();
            Load_bill();

            // Gửi gói tin yêu cầu đặt bàn ăn 6
            try
            {
                NetworkStream net_stream = frmDangNhap.tcpClient.GetStream();
                Packet sendData = new Packet()
                {
                    User = User.NhanVien,
                    Header = Header.Ban,
                    Option = Options.Search,
                    Ban = new Ban(tableList[id_table_selected].MaBan, "")
                };
                byte[] data = sendData.GetDataStream();
                net_stream.Write(data, 0, data.Length);
                net_stream.Flush();

                // Hiển thị tổng tiền của hóa đơn
                lb_total_payment.Text = "Tổng tiền: " + tableList[id_table_selected].TongTien.ToString("N0") + " VNĐ";
            }
            catch
            {
                MessageBox.Show("Không thể kết nối tới máy chủ!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Cập nhật trình trạng của 1 bàn ăn
        private void btn_order_table_Click(object sender, EventArgs e)
        {
            string status;

            if (btn_order_table.Text != "Hủy bàn")
            {
                lb_table_status.Text = "Tình trạng bàn: Có người";
                status = "Có người";
                btn_order_table.Text = "Hủy bàn";
                btn_pay.Enabled = true;
            }
            else
            {
                lb_table_status.Text = "Tình trạng bàn: Trống";
                status = "Trống";
                btn_order_table.Text = "Đặt bàn";
                btn_pay.Enabled = false;
            }

            // Tạo gói tin cập nhật trạng thái bàn ăn
            try
            {
                NetworkStream net_stream = frmDangNhap.tcpClient.GetStream();
                Packet sendData = new Packet()
                {
                    User = User.NhanVien,
                    Header = Header.Ban,
                    Option = Options.Update,
                    Ban = new Ban(tableList[id_table_selected].MaBan, status)
                };
                byte[] data = sendData.GetDataStream();
                net_stream.Write(data, 0, data.Length);
                net_stream.Flush();
            }
            catch
            {
                MessageBox.Show("Không thể kết nối tới máy chủ!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_add_dish_Click(object sender, EventArgs e)
        {
            //Kiểm tra nhập đủ thông tin chi tiết hóa đơn
            if (txt_quantity.Text == "" || cb_Name_dish.Text == "" || cb_Name_dish.Text == "Tên món")
            {
                MessageBox.Show("Vui lòng nhập tên món ăn và số lượng món ăn!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            //Cập nhật bảng chi tiết hóa đơn với tên món ăn, giá 1 phần ăn, số lượng phần ăn và giá của số phần ăn đó
            ListViewItem bill_detail = new ListViewItem();
            int price = 0;

            foreach (ThucDon food in foodList)
            {
                if (cb_Name_dish.Text == food.TenMon)
                {
                    price = food.Gia;
                    break;
                }
            }

            int total = price * Convert.ToInt32(txt_quantity.Text);
            bill_detail.Text = cb_Name_dish.Text;
            lv_Bill.Items.Add(bill_detail);
            bill_detail.SubItems.Add(txt_quantity.Text);
            bill_detail.SubItems.Add(price.ToString());
            bill_detail.SubItems.Add(total.ToString());
            Cal_total(total);
            Save_bill();
        }

        private void btn_remove_dish_Click(object sender, EventArgs e)
        {
            if (lv_Bill.Items.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn món trước!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            ListViewItem bill_detail = lv_Bill.SelectedItems[0];
            int price = Convert.ToInt32(bill_detail.SubItems[3].Text);
            lv_Bill.Items.Remove(bill_detail);
            Cal_total(-price);
            Save_bill();
        }

        private void btn_pay_Click(object sender, EventArgs e)
        {
            //Kiểm tra nhập đủ thông tin hóa đơn.
            if (txt_MAHD.Text == "" || txt_MANV.Text == "" || txt_MAKH.Text == "")
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin hóa đơn.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                NetworkStream net_stream = frmDangNhap.tcpClient.GetStream();
                byte[] data;

                // Tạo gói tin yêu cầu thêm hóa đơn vào cơ sở dữ liệu
                Packet sendData = new Packet()
                {
                    User = User.NhanVien,
                    Header = Header.HoaDon,
                    Option = Options.Insert,
                    HoaDon = new HoaDon("", txt_MANV.Text, txt_MAKH.Text, txt_MABAN.Text, DateTime.Now.ToString("MM/dd/yyyy"), tableList[id_table_selected].TongTien)
                };

                data = sendData.GetDataStream();
                net_stream.Write(data, 0, data.Length);
                net_stream.Flush();
                Thread.Sleep(delay);

                // Thêm chi tiết hóa đơn
                foreach (ListViewItem item in lv_Bill.Items)
                {
                    string sMaMon = "";
                    foreach (ThucDon food in foodList)
                    {
                        if (item.SubItems[0].Text == food.TenMon)
                        {
                            sMaMon = food.MaMon;
                            break;
                        }
                    }
                    sendData = new Packet()
                    {
                        User = User.NhanVien,
                        Header = Header.ChiTietHoaDon,
                        Option = Options.Insert,
                        ChiTietHoaDon = new ChiTietHoadon("", sMaMon, int.Parse(item.SubItems[1].Text))
                    };
                    data = sendData.GetDataStream();
                    net_stream.Write(data, 0, data.Length);
                    net_stream.Flush();
                    Thread.Sleep(delay);
                }

                // Cập nhật tình trạng bàn về trạng thái trống
                lb_table_status.Text = "Tình trạng bàn: Trống";
                sendData = new Packet()
                {
                    User = User.NhanVien,
                    Header = Header.Ban,
                    Option = Options.Update,
                    Ban = new Ban(tableList[id_table_selected].MaBan, "Trống")
                };
                data = sendData.GetDataStream();
                net_stream.Write(data, 0, data.Length);
                net_stream.Flush();
                Thread.Sleep(delay);

                // Xóa các thông tin hóa đơn để tiện cho lần nhập tiếp theo và mở lại các button chọn bàn
                Reset();
            }
            catch
            {
                MessageBox.Show("Không thể kết nối tới máy chủ!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmNhanVien_FormClosing(object sender, FormClosingEventArgs e)
        {
            frmDangNhap.tcpClient.Close();
        }
    }
}