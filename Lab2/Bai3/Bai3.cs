using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bai3
{
    public partial class Bai3 : Form
    {
        public Bai3()
        {
            InitializeComponent();
        }

        private int GetPriority(string op)
        {
            if (op == "^")
            {
                return 3;
            }
            if (op == "*" || op == "/" || op == "%")
            {
                return 2;
            }
            return 1;
        }

        private bool IsOperator(string ele)
        {
            return Regex.Match(ele, @"\^|\+|\-|\*|\/|\%").Success;
        }

        private bool IsOperand(string ele)
        {
            return Regex.Match(ele, @"^\d+$|^([a-z]|[A-Z])$").Success;
        }

        private string GetPostfixExpression(string infix_expression)
        {
            //Khoi tao mot list rong de bieu thi bieu thuc hau to
            List<string> postfix_expression = new List<string>();

            //Khoi tao stack rong de luu cac toan tu va dau ngoac
            Stack<string> operators_and_parentheses = new Stack<string>();

            //Bat dau giai thuat
            foreach (string ele in infix_expression.Split(' '))
            {
                if (IsOperand(ele))
                {
                    postfix_expression.Add(ele);
                    continue;
                }
                if (ele == "(")
                {
                    operators_and_parentheses.Push(ele);
                    continue;
                }
                if (ele == ")")
                {
                    string top = operators_and_parentheses.Pop();
                    while (top != "(")
                    {
                        postfix_expression.Add(top);
                        top = operators_and_parentheses.Pop();
                    }
                    continue;
                }
                if (IsOperand(ele))
                {
                    while (operators_and_parentheses.Count > 0 && GetPriority(operators_and_parentheses.Peek()) >= GetPriority(ele))
                    {
                        postfix_expression.Add(operators_and_parentheses.Pop());
                    }
                    operators_and_parentheses.Push(ele);
                    continue;
                }
            }
            while (operators_and_parentheses.Count > 0)
            {
                postfix_expression.Add(operators_and_parentheses.Pop());
            }
            return string.Join(" ", postfix_expression.ToArray());
        }

        private double GetNumericResult(string postfix_expression)
        {
            //Khoi tao stack rong de luu cac phan tu trong bieu thuc hau to
            Stack<double> s = new Stack<double>();

            //Bat dau giai thuat
            foreach (string ele in postfix_expression.Split(' '))
            {
                if (IsOperand(ele))
                {
                    double num = Convert.ToDouble(ele);
                    s.Push(num);
                }
                else
                {
                    double first_top = s.Pop();
                    double second_top = s.Pop();
                    double value = 0;
                    switch (ele)
                    {
                        case "^":
                            value = Math.Pow(second_top, first_top);
                            break;
                        case "*":
                            value = second_top * first_top;
                            break;
                        case "/":
                            value = (double)second_top / first_top;
                            break;
                        case "+":
                            value = second_top + first_top;
                            break;
                        case "-":
                            value = second_top - first_top;
                            break;
                    }
                    s.Push(value);
                }
            }
            return s.Peek();
        }

        private void btnWrite_Click(object sender, EventArgs e)
        {
            //Tao file input3.txt
            FileStream fs = new FileStream("input3.txt", FileMode.Append, FileAccess.Write);

            //Ghi du lieu da nhap vao file input3.txt (giua cac ky tu phai co khoang trang)
            StreamWriter sw = new StreamWriter(fs);
            sw.WriteLine(txtExpression.Text.Trim());
            sw.Close();
            txtExpression.Text = string.Empty;
            fs.Close();
        }

        private void btnRead_Click(object sender, EventArgs e)
        {
            if (lstResult.Items.Count > 0)
            {
                lstResult.Items.Clear();
            }
            FileStream fs = File.Open("input3.txt", FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(fs);
            while (!sr.EndOfStream)
            {
                string infix_expression = sr.ReadLine();
                string postfix_expression = GetPostfixExpression(infix_expression);
                double num = GetNumericResult(postfix_expression);
                lstResult.Items.Add($"{infix_expression} = {num}");

                //Ghi vao file output3.txt
                File.AppendAllLines("output3.txt", new string[] { $"{infix_expression} = {num}" });
            }
            fs.Close();
        }
    }
}