using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.Serialization.Formatters.Soap;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;

namespace Serialization_Deserialization
{
    public partial class frmForm : Form
    {
        public frmForm()
        {
            InitializeComponent();
        }

        public enum purchaseOrderStates
        {
            ISSUED,
            DELIVERED,
            INVOICED,
            PAID
        }

        [Serializable()]

        public class company
        {
            public string name;
            public string address;
            public string phone;
        }

        [Serializable()]

        public class lineItem
        {
            public string description;
            public int quantity;
            public double cost;
        }

        [Serializable()]

        public class purchaseOrder
        {
            private purchaseOrderStates _purchaseOrderStatus;
            private DateTime _issuanceDate;
            private DateTime _deliveryDate;
            private DateTime _invoiceDate;
            private DateTime _paymentDate;

            public company buyer;
            public company vendor;
            public string reference;
            public lineItem[] items;

            public purchaseOrder()
            {
                _purchaseOrderStatus=purchaseOrderStates.ISSUED;
                _issuanceDate = DateTime.Now;
            }

            public void recordDelivery()
            {
                if (_purchaseOrderStatus == purchaseOrderStates.ISSUED)
                {
                    _purchaseOrderStatus = purchaseOrderStates.DELIVERED;
                    _deliveryDate = DateTime.Now;
                }
            }

            public void recordInvoice()
            {
                if (_purchaseOrderStatus == purchaseOrderStates.DELIVERED)
                {
                    _purchaseOrderStatus = purchaseOrderStates.INVOICED;
                    _invoiceDate = DateTime.Now;
                }
            }

            public void recordPayment()
            {
                if (_purchaseOrderStatus == purchaseOrderStates.INVOICED)
                {
                    _purchaseOrderStatus = purchaseOrderStates.PAID;
                    _paymentDate = DateTime.Now;
                }
            }
        }

        private void btnSoap_se_Click(object sender, EventArgs e)
        {
            company Vendor = new company();
            company Buyer = new company();
            lineItem Goods = new lineItem();
            purchaseOrder po = new purchaseOrder();

            Vendor.name = "Acme Inc.";
            Buyer.name = "Wiley E. Coyote";
            Vendor.phone = "0825452467";
            Goods.description = "anti-Roadrunner cannon";
            Goods.quantity = 1;
            Goods.cost = 599.99;
            po.items = new lineItem[1];
            po.items[0] = Goods;
            po.buyer = Buyer;
            po.vendor = Vendor;

            SoapFormatter sf = new SoapFormatter();
            FileStream fs = File.Create("D:\\HOC TAP\\winform_c#\\Serialization_Deserialization\\po.xml");
            sf.Serialize(fs, po);
            fs.Close();
        }

        private void btnSoap_de_Click(object sender, EventArgs e)
        {
            SoapFormatter sf = new SoapFormatter();
            FileStream fs = File.OpenRead("D:\\HOC TAP\\winform_c#\\Serialization_Deserialization\\po.xml");
            purchaseOrder po = (purchaseOrder)sf.Deserialize(fs);
            fs.Close();
            MessageBox.Show("Customer is " + po.buyer.name
                            + "\nVendor is " + po.vendor.name
                            + ", phone is " + po.vendor.phone
                            + "\nItem is " + po.items[0].description
                            + " has quantity " + po.items[0].quantity.ToString()
                            + ", has cost " + po.items[0].cost.ToString());
        }

        private void btnBinary_se_Click(object sender, EventArgs e)
        {
            company Vendor = new company();
            company Buyer = new company();
            lineItem Goods = new lineItem();
            purchaseOrder po = new purchaseOrder();

            Vendor.name = "Acme Inc.";
            Buyer.name = "Wiley E. Coyote";
            Vendor.phone = "0825452467";
            Goods.description = "anti-Roadrunner cannon";
            Goods.quantity = 1;
            Goods.cost = 599.99;
            po.items = new lineItem[1];
            po.items[0] = Goods;
            po.buyer = Buyer;
            po.vendor = Vendor;

            BinaryFormatter bf = new BinaryFormatter();
            FileStream fs = new FileStream("po.txt", FileMode.Create);
            bf.Serialize(fs, po);
            fs.Close();
        }

        private void btnBinary_de_Click(object sender, EventArgs e)
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fs = new FileStream("po.txt", FileMode.Open);
            purchaseOrder po = (purchaseOrder)bf.Deserialize(fs);
            fs.Close();
            MessageBox.Show("Customer is " + po.buyer.name
                            + "\nVendor is " + po.vendor.name
                            + ", phone is " + po.vendor.phone
                            + "\nItem is " + po.items[0].description
                            + " has quantity " + po.items[0].quantity.ToString()
                            + ", has cost " + po.items[0].cost.ToString());
        }

        private void btnXML_se_Click(object sender, EventArgs e)
        {
            company Vendor = new company();
            company Buyer = new company();
            lineItem Goods = new lineItem();
            purchaseOrder po = new purchaseOrder();

            Vendor.name = "Acme Inc.";
            Buyer.name = "Wiley E. Coyote";
            Vendor.phone = "0825452467";
            Goods.description = "anti-Roadrunner cannon";
            Goods.quantity = 1;
            Goods.cost = 599.99;
            po.items = new lineItem[1];
            po.items[0] = Goods;
            po.buyer = Buyer;
            po.vendor = Vendor;

            XmlSerializer xs = new XmlSerializer(po.GetType());
            FileStream fs = File.Create("D:\\HOC TAP\\winform_c#\\Serialization_Deserialization\\po.xml");
            xs.Serialize(fs, po);
            fs.Close();
        }

        private void btnXML_de_Click(object sender, EventArgs e)
        {
            purchaseOrder po = new purchaseOrder();
            XmlSerializer xs = new XmlSerializer(po.GetType());
            FileStream fs = File.OpenRead("D:\\HOC TAP\\winform_c#\\Serialization_Deserialization\\po.xml");
            po = (purchaseOrder)xs.Deserialize(fs);
            fs.Close();
            MessageBox.Show("Customer is " + po.buyer.name);
        }
    }
}