using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Bai4
{
    public partial class frmBai4 : Form
    {

        public frmBai4()
        {
            InitializeComponent();
        }

        private List<Student> students = new List<Student>();
        private int index = 0;

        private void Serialise(string file_name, Student student)
        {
            FileStream fs = new FileStream(file_name, FileMode.Append, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs);
            MemoryStream mem_stream = new MemoryStream();
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(mem_stream, student);
            string serialized_obj = Convert.ToBase64String(mem_stream.ToArray());
            sw.WriteLine(serialized_obj);
            sw.Flush();
            fs.Close();
        }

        private void Deserialise(StreamReader sr)
        {
            string serialized_obj = sr.ReadLine();
            byte[] mem_data = Convert.FromBase64String(serialized_obj);
            MemoryStream mem_stream = new MemoryStream(mem_data);
            BinaryFormatter bf = new BinaryFormatter();
            Student student = (Student)bf.Deserialize(mem_stream);
            students.Add(student);
        }

        private void LoadData(List<Student> students)
        {
            if (index % 2 == 0)
            {
                txtResult.Clear();
            }

            txtResult.Text += students[index].name + "\r\n"
                              + students[index].id + "\r\n"
                              + students[index].phone + "\r\n"
                              + students[index].score1 + "\r\n"
                              + students[index].score2 + "\r\n"
                              + students[index].score3 + "\r\n"
                              + students[index].avg + "\r\n\r\n";

            txtName.Text = students[index].name;
            txtID.Text = students[index].id;
            txtPhone.Text = students[index].phone;
            txtCourse1.Text = students[index].score1;
            txtCourse2.Text = students[index].score2;
            txtCourse3.Text = students[index].score3;

            lblPageNumber.Text = (index + 1) + "";
        }

        private void Reset()
        {
            txtName.Clear();
            txtID.Clear();
            txtPhone.Clear();
            txtCourse1.Clear();
            txtCourse2.Clear();
            txtCourse3.Clear();
        }

        private void btnWrite_Click(object sender, EventArgs e)
        {
            if (txtName.TextLength == 0 || txtID.TextLength == 0 || txtPhone.TextLength == 0 || txtCourse1.TextLength == 0 || txtCourse2.TextLength == 0 || txtCourse3.TextLength == 0)
            {
                MessageBox.Show("Vui long dien day du thong tin cua sinh vien!");
            }
            else
            {
                Student student = new Student();

                //Them ten sinh vien vao mang
                student.name = txtName.Text.Trim();

                //Kiem tra rang buoc MSSV
                if (txtID.Text.Trim().Length != 8)
                {
                    MessageBox.Show("MSSV khong nhieu hon 8 ki tu! Vui long nhap lai!");
                }
                else
                {
                    int num;
                    if (!int.TryParse(txtID.Text.Trim(), out num))
                    {
                        MessageBox.Show("Vui long nhap 8 chu so!");
                        return;
                    }
                    student.id = txtID.Text.Trim();
                }

                //Kiem tra rang buoc so dien thoai
                if (txtPhone.Text.Trim().Length != 10)
                {
                    MessageBox.Show("So dien thoai khong nhieu hon 10 chu so! Vui long nhap lai");
                }
                else
                {
                    string phone = txtPhone.Text.Trim();
                    if (phone[0] != '0')
                    {
                        MessageBox.Show("So dien thoai phai bat dau bang chu so 0!");
                        return;
                    }
                    student.phone = phone;
                }

                //Kiem tra rang buoc ve diem
                float score1, score2, score3;
                if (!float.TryParse(txtCourse1.Text, out score1) || !float.TryParse(txtCourse2.Text, out score2) || !float.TryParse(txtCourse3.Text, out score3))
                {
                    MessageBox.Show("Vui long nhap gia tri thuc!");
                }
                else
                {
                    score1 = float.Parse(txtCourse1.Text);
                    score2 = float.Parse(txtCourse2.Text);
                    score3 = float.Parse(txtCourse3.Text);
                    if ((score1 < 0 || score1 > 10) || (score2 < 0 || score2 > 10) || (score3 < 0 || score3 > 10))
                    {
                        MessageBox.Show("Diem chi co gia tri tu 0 den 10! Vui long nhap lai!");
                        return;
                    }
                    student.score1 = score1.ToString();
                    student.score2 = score2.ToString();
                    student.score3 = score3.ToString();

                    //Them diem trung binh vao thong tin sinh vien
                    float avg = (score1 + score2 + score3) / 3;
                    student.avg = avg.ToString();
                }

                //Ghi thong tin sinh vien vao cac stream, chuyen
                //cac stream nay sang chuoi kieu Base64 de luu tru
                //va cuoi cung ghi cac chuoi nay vao file input4.txt
                string file_name = "input4.txt";
                Serialise(file_name, student);

                //Reset du lieu trong cac textbox
                Reset();
            }
        }

        private void btnRead_Click(object sender, EventArgs e)
        {
            FileStream fs = File.Open("input4.txt", FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(fs);
            int i = 0;
            while (!sr.EndOfStream)
            {
                Deserialise(sr);

                //Ghi vao file output4.txt;
                FileStream output = new FileStream("output4.txt", FileMode.Append, FileAccess.Write);
                StreamWriter sw = new StreamWriter(output);
                sw.WriteLine(students[i].name + '\n' +
                             students[i].id + '\n' +
                             students[i].phone + '\n' +
                             students[i].score1 + '\n' +
                             students[i].score2 + '\n' +
                             students[i].score3 + '\n');
                sw.Flush();
                i++;
                output.Close();
            }
            LoadData(students);
            this.btnRead.Enabled = false;
            fs.Close();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            Reset();
            index++;
            if (index + 1 > students.Count)
            {
                MessageBox.Show($"Danh sach sinh vien chi co toi da {students.Count} trang!");
            }
            else
            {
                LoadData(students);
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Reset();
            index--;
            if (index < 0)
            {
                MessageBox.Show("Danh sach sinh vien co it nhat 1 trang!");
            }
            else
            {
                LoadData(students);
            }
        }
    }
}