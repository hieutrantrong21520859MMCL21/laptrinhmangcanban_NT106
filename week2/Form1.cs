using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Runtime.ExceptionServices;

namespace BaiTapBuoi2
{
    public partial class frmForm : Form
    {
        public frmForm()
        {
            InitializeComponent();
        }

        string str_conn = @"Data Source=DESKTOP-U7NVEFD\SQLEXPRESS;Initial Catalog = StudentManagement; Integrated Security = True";
        SqlConnection conn = null;

        public void load_data()
        {
            string strSelect = "select * from Student";
            SqlCommand sql_cmd = new SqlCommand(strSelect, conn);
            SqlDataReader dataReader = sql_cmd.ExecuteReader();
            DataTable dataTable = new DataTable();
            dataTable.Load(dataReader);
            dgvData.DataSource = dataTable;
        }

        public void reset()
        {
            txtID.Text = "";
            txtName.Text = "";
            txtAddress.Text = "";
            txtID_find.Text = "";
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            try
            {
                if (conn == null)
                {
                    conn = new SqlConnection(str_conn);
                }
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                    MessageBox.Show("Kết nối thành công!");
                    load_data();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            if ( conn != null && conn.State == ConnectionState.Open)
            {
                conn.Close();
                MessageBox.Show("Ngắt kết nối thành công!");
                dgvData.DataSource = null;
                reset();
            }
            else
            {
                MessageBox.Show("Bạn chưa kết nối tới server! Yêu cầu kết nối lại!");
            }
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            try
            {
                if (conn.State == ConnectionState.Open)
                {
                    string sqlQuery = "select * from Student where id = @id";
                    SqlCommand sql_cmd = new SqlCommand(sqlQuery, conn);
                    sql_cmd.Parameters.AddWithValue("id", txtID_find.Text.ToUpper());
                    SqlDataReader dataReader = sql_cmd.ExecuteReader();
                    DataTable dataTable = new DataTable();
                    dataTable.Load(dataReader);
                    dgvData.DataSource = dataTable;
                    MessageBox.Show("Bạn đã truy vấn thành công!");
                    reset();
                }
                else
                {
                    MessageBox.Show("Bạn chưa kết nối tới server! Yêu cầu kết nối lại!");
                }
            }
            catch (Exception ex)
            {
                DialogResult result = MessageBox.Show(ex.Message);
                if (result == DialogResult.OK)
                {
                    Close();
                }
            }
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            try
            {
                if (conn.State == ConnectionState.Open)
                {
                    string sqlInsert = "insert into Student values (@id, @name, @birth, @address)";
                    SqlCommand sql_cmd = new SqlCommand(sqlInsert, conn);
                    sql_cmd.Parameters.AddWithValue("id", txtID.Text.ToUpper());
                    sql_cmd.Parameters.AddWithValue("name", txtName.Text);
                    sql_cmd.Parameters.AddWithValue("birth", dtpBirth.Text);
                    sql_cmd.Parameters.AddWithValue("address", txtAddress.Text);
                    sql_cmd.ExecuteNonQuery();
                    load_data();
                    MessageBox.Show("Bạn đã thêm thành công!");
                    reset();
                }
                else
                {
                    MessageBox.Show("Bạn chưa kết nối tới server! Yêu cầu kết nối lại!");
                }
            }
            catch (Exception ex)
            {
                DialogResult result = MessageBox.Show(ex.Message);
                if (result == DialogResult.OK)
                {
                    Close();
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (conn.State == ConnectionState.Open)
                {
                    string sqlDelete = "delete from Student where id = @id";
                    SqlCommand sql_cmd = new SqlCommand(sqlDelete, conn);
                    sql_cmd.Parameters.AddWithValue("id", txtID_find.Text.ToUpper());
                    sql_cmd.ExecuteNonQuery();
                    load_data();
                    reset();
                    MessageBox.Show("Bạn đã xóa thành công!");
                }
                else
                {
                    MessageBox.Show("Bạn chưa kết nối tới server! Yêu cầu kết nối lại!");
                }
            }
            catch (Exception ex)
            {
                DialogResult result = MessageBox.Show(ex.Message);
                if (result == DialogResult.OK)
                {
                    Close();
                }
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (conn.State == ConnectionState.Open)
                {
                    string strUpdate = "update Student set name = @name, birth = @birth, address = @address where id = @id";
                    SqlCommand sql_cmd = new SqlCommand(strUpdate, conn);
                    sql_cmd.Parameters.AddWithValue("id", txtID_find.Text.ToUpper());
                    sql_cmd.Parameters.AddWithValue("name", txtName.Text);
                    sql_cmd.Parameters.AddWithValue("birth",dtpBirth.Text);
                    sql_cmd.Parameters.AddWithValue("address", txtAddress.Text);
                    sql_cmd.ExecuteNonQuery();
                    load_data();
                    reset();
                    MessageBox.Show("Bạn đã sửa thành công!");
                }
                else
                {
                    MessageBox.Show("Bạn chưa kết nối tới server! Yêu cầu kết nối lại!");
                }
            }
            catch (Exception ex)
            {

                DialogResult result = MessageBox.Show(ex.Message);
                if (result == DialogResult.OK)
                {
                    Close();
                }
            }
        }
    }
}