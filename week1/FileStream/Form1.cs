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
using System.Threading;

namespace FileStream_chuong2
{
    public partial class frmFileReader : Form
    {
        public frmFileReader()
        {
            InitializeComponent();
        }
        FileStream fs;
        byte[] fileContents;
        AsyncCallback callback;
        delegate void InfoMessageDel(string info);

        public void InfoMessage(string s)
        {
            if (tbResults.InvokeRequired)
            {
                var d = new InfoMessageDel(InfoMessage);
                tbResults.Invoke(d, new object[] { s });
            }
            else tbResults.Text = s;
        }

        private void btnReadAsync_Click(object sender, EventArgs e)
        {
            openFileDialog.ShowDialog();
            callback = new AsyncCallback(fs_StateChanged);
            fs = new FileStream(openFileDialog.FileName,
                              FileMode.Open,
                              FileAccess.Read,
                              FileShare.Read,
                              4096,
                              true);
            fileContents = new Byte[fs.Length];
            fs.BeginRead(fileContents,
                         0,
                         (int)fs.Length,
                         callback,
                         null);
        }

        private void fs_StateChanged(IAsyncResult asyncResult)
        {
            if (asyncResult.IsCompleted)
            {
                CheckForIllegalCrossThreadCalls = false;
                Label.CheckForIllegalCrossThreadCalls = false;
                string s = Encoding.UTF8.GetString(fileContents);
                InfoMessage(s);
                fs.Close();
            }
        }

        private void btnReadSync_Click(object sender, EventArgs e)
        {
            openFileDialog.ShowDialog();
            Thread thdSyncRead = new Thread(new ThreadStart(syncRead));
            thdSyncRead.Start();
        }

        public void syncRead()
        {
            FileStream fs;
            try
            {
                fs = new FileStream(openFileDialog.FileName, FileMode.OpenOrCreate);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            fs.Seek(0, SeekOrigin.Begin);
            byte[] fileContents = new byte[fs.Length];
            fs.Read(fileContents, 0, (int)fs.Length);
            CheckForIllegalCrossThreadCalls = false;
            Label.CheckForIllegalCrossThreadCalls = false;
            string s = Encoding.UTF8.GetString(fileContents);
            InfoMessage(s);
            fs.Close();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
