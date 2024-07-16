namespace Server
{
    partial class frmServer
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblDanhSach = new System.Windows.Forms.Label();
            this.lstDanhSach = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // lblDanhSach
            // 
            this.lblDanhSach.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDanhSach.Location = new System.Drawing.Point(12, 26);
            this.lblDanhSach.Name = "lblDanhSach";
            this.lblDanhSach.Size = new System.Drawing.Size(640, 39);
            this.lblDanhSach.TabIndex = 0;
            this.lblDanhSach.Text = "Danh sách nhân viên trong giờ làm";
            this.lblDanhSach.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lstDanhSach
            // 
            this.lstDanhSach.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstDanhSach.FormattingEnabled = true;
            this.lstDanhSach.ItemHeight = 25;
            this.lstDanhSach.Location = new System.Drawing.Point(12, 80);
            this.lstDanhSach.Name = "lstDanhSach";
            this.lstDanhSach.Size = new System.Drawing.Size(640, 354);
            this.lstDanhSach.TabIndex = 1;
            // 
            // frmServer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(664, 450);
            this.Controls.Add(this.lstDanhSach);
            this.Controls.Add(this.lblDanhSach);
            this.Name = "frmServer";
            this.Text = "Server";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmServer_FormClosing);
            this.Load += new System.EventHandler(this.frmServer_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblDanhSach;
        private System.Windows.Forms.ListBox lstDanhSach;
    }
}