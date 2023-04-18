using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bai6
{
    public partial class Bai6 : Form
    {
        public Bai6()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // lay danh sach cac o dia
            string[] drives = Directory.GetLogicalDrives();

            TreeNode root;

            // duyet tung phan tu (ten o dia), them vao TreeView
            foreach (string drv in drives)
            {
                root = new TreeNode(drv);

                // them root vao TreeView
                treeFolder.Nodes.Add(root);
                root.Tag = drv;

                // them node con vao root
                root.Nodes.Add("");
            }
        }

        private void treeFolder_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            TreeNode node = e.Node;
            string path = (string)node.Tag;
            FileAttributes attr = File.GetAttributes(path);
            if (attr.HasFlag(FileAttributes.Directory) || Directory.GetLogicalDrives().Contains(path))
            {
                TreeViewCancelEventArgs new_e = new TreeViewCancelEventArgs(node, false, TreeViewAction.ByMouse);
                treeFolder_BeforeExpand(sender, new_e);
            }
            else
            {
                if (path.ToLower().EndsWith(".jpg") || path.ToLower().EndsWith(".png") || path.ToLower().EndsWith(".jpeg"))
                {
                    picResult.Visible = true;
                    picResult.SizeMode = PictureBoxSizeMode.StretchImage;
                    picResult.Image = Image.FromFile(path);
                    txtView.Visible = false;
                }
                else // doc file text
                {
                    txtView.Visible = true;
                    FileStream fs = File.Open(path, FileMode.Open);
                    StreamReader sr = new StreamReader(fs);
                    txtView.Text = sr.ReadToEnd();
                    fs.Close();
                    picResult.Visible = false;
                }
            }
        }

        private void treeFolder_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            // xac dinh node da chon
            TreeNode node = e.Node;

            // xoa node rong
            node.Nodes.Clear();

            // lay danh sach cac thu muc con
            string[] sub_folders = Directory.GetDirectories(node.FullPath);

            // lay danh sach cac file
            string[] files = Directory.GetFiles(node.FullPath);

            try
            {
                // duyet qua danh sach cac thu muc con, kiem tra neu con cac thu muc con khac
                foreach (string dir in sub_folders)
                {
                    DirectoryInfo di = new DirectoryInfo(dir);
                    TreeNode child = node.Nodes.Add(di.Name);
                    child.Tag = di.FullName;
                    child.Nodes.Add("");
                }

                // duyet qua danh sach cac file trong thu muc
                foreach (string file in files)
                {
                    FileInfo fi = new FileInfo(file);
                    TreeNode child = node.Nodes.Add(fi.Name);
                    child.Tag = fi.FullName;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}