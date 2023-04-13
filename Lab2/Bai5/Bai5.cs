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
using System.IO.Compression;
using System.Data.Odbc;

namespace Bai5
{
    public partial class frmBai5 : Form
    {
        public frmBai5()
        {
            InitializeComponent();
        }

        private void btnCompress_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.ShowDialog();
            using (FileStream originalFileStream = new FileStream(ofd.FileName, FileMode.Open))
            {
                using (FileStream compressedFileStream = File.Create("output5.gz"))
                {
                    using (GZipStream compressionStream = new GZipStream(compressedFileStream, CompressionMode.Compress))
                    {
                        originalFileStream.CopyTo(compressionStream);
                        MessageBox.Show($"Compressed input5.txt from {originalFileStream.Length} to {compressedFileStream.Length} bytes");
                    }
                }
            }
        }

        private void btnDecompress_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.ShowDialog();
            using (FileStream originalFileStream = new FileStream(ofd.FileName, FileMode.Open))
            {
                using (FileStream decompressedFileStream = new FileStream("output5.txt", FileMode.OpenOrCreate))
                {
                    using (GZipStream decompressionStream = new GZipStream(originalFileStream, CompressionMode.Decompress))
                    {
                        decompressionStream.CopyTo(decompressedFileStream);
                        MessageBox.Show($"Length of the decompressed file is {decompressedFileStream.Length} bytes");
                    }
                }
            }
        }
    }
}