using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ThucHanhTuan1
{
    public partial class frmForm6 : Form
    {
        public frmForm6()
        {
            InitializeComponent();
        }

        private string[] scores;

        private void Check()
        {
            if (txtName.TextLength == 0 && txtScores.TextLength == 0)
            {
                MessageBox.Show("Vui lòng nhập tên và điểm số của sinh viên!");
                return;
            }
            if (txtName.TextLength == 0)
            {
                MessageBox.Show("Vui lòng nhập họ và tên sinh viên!");
                return;
            }
            if (txtScores.TextLength == 0)
            {
                MessageBox.Show("Vui lòng nhập điểm số của sinh viên!");
                return;
            }
            txtName.Text = txtName.Text.Trim();
            scores = txtScores.Text.Split(',');
            double score;
            foreach (string s in scores)
            {
                if (!double.TryParse(s, out score))
                {
                    MessageBox.Show("Vui lòng nhập đúng định dạng là số thực! Hãy nhập lại điểm số!");
                    return;
                }
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            Check();
            lstStudent.Items.Add($"----Sinh viên: {txtName.Text.Trim()}----");
            string text = "";
            int i = 1;
            foreach(string s in scores)
            {
                text += $"Môn {i++}: {s}" + '\t' + '\t';
            }
            lstStudent.Items.Add(text);
        }

        private void btnAverage_Click(object sender, EventArgs e)
        {
            Check();
            string name = $"----Sinh viên: {txtName.Text}----";
            if (lstStudent.Items.Count == 0)
            {
                lstStudent.Items.Add(name);
            }
            double sum = 0;
            foreach (string s in scores)
            {
                sum += double.Parse(s);
            }
            lstStudent.Items.Add($"Điểm trung bình: {sum / scores.Length}");
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            Check();
            string name = $"----Sinh viên:  {txtName.Text} ----";
            if (lstStudent.Items.Count == 0)
            {
                lstStudent.Items.Add(name);
            }
            double[] arr = new double[scores.Length];
            for(int i = 0; i < arr.Length; i++)
            {
                arr[i] = double.Parse(scores[i]);
            }
            List<int> max_index = new List<int>();
            List<int> min_index = new List<int>();
            for(int i = 0; i < arr.Length; i++)
            {
                if (arr[i] == arr.Max())
                {
                    max_index.Add(i+1);
                }
                if (arr[i] == arr.Min())
                {
                    min_index.Add(i+1);
                }
            }
            string max_res = "";
            string min_res = "";
            for(int i = 0; i < max_index.Count; i++)
            {
                max_res += max_index[i] + "" + ", ";
            }
            for (int i = 0; i < min_index.Count; i++)
            {
                min_res += min_index[i] + "" + ", ";
            }
            lstStudent.Items.Add($"Môn {max_res.Substring(0, max_res.Length - 2)} có điểm cao nhất"
                                 +'\t' + '\t'
                                 + $"Môn {min_res.Substring(0,min_res.Length - 2)} có điểm thấp nhất");
            
        }

        private void btnCount_Click(object sender, EventArgs e)
        {
            Check();
            string name = $"----Sinh viên:  {txtName.Text} ----";
            if (lstStudent.Items.Count == 0)
            {
                lstStudent.Items.Add(name);
            }
            int count_pass = 0;
            foreach(string s in scores)
            {
                if (double.Parse(s) >= 5)
                {
                    count_pass++;
                }
            }
            lstStudent.Items.Add($"Số môn đậu: {count_pass}" + '\t' + $"Số môn rớt: {scores.Length - count_pass}");
        }

        private void btnType_Click(object sender, EventArgs e)
        {
            Check();
            string name = $"----Sinh viên:  {txtName.Text} ----";
            if (lstStudent.Items.Count == 0)
            {
                lstStudent.Items.Add(name);
            }
            double sum = 0;
            foreach (string s in scores)
            {
                sum += double.Parse(s);
            }
            double avg = sum / scores.Length;
            string type = "";
            int count_gioi = 0, count_kha = 0, count_tb = 0, count_yeu = 0;
            foreach(string s in scores)
            {
                double score = double.Parse(s);
                if (score < 6.5) count_gioi++;
                if (score < 5) count_kha++;
                if (score < 3.5) count_tb++;
                if (score < 2) count_yeu++;
            }
            if (avg >= 8 && count_gioi == 0) type = "Giỏi";
            else if (avg >= 6.5 && count_kha == 0) type = "Khá";
            else if (avg >= 5 && count_tb == 0) type = "TB";
            else if (avg >= 3.5 && count_yeu == 0) type = "Yếu";
            else type = "kém";
            lstStudent.Items.Add($"Xếp loại: {type}");
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            txtName.Text = string.Empty;
            txtScores.Text = string.Empty;
        }
    }
}