namespace FileStream_chuong2
{
    partial class frmFileReader
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
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.btnReadAsync = new System.Windows.Forms.Button();
            this.btnReadSync = new System.Windows.Forms.Button();
            this.tbResults = new System.Windows.Forms.TextBox();
            this.btnExit = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog1";
            // 
            // btnReadAsync
            // 
            this.btnReadAsync.Location = new System.Drawing.Point(647, 1);
            this.btnReadAsync.Name = "btnReadAsync";
            this.btnReadAsync.Size = new System.Drawing.Size(141, 45);
            this.btnReadAsync.TabIndex = 0;
            this.btnReadAsync.Text = "ReadAsync";
            this.btnReadAsync.UseVisualStyleBackColor = true;
            this.btnReadAsync.Click += new System.EventHandler(this.btnReadAsync_Click);
            // 
            // btnReadSync
            // 
            this.btnReadSync.Location = new System.Drawing.Point(647, 93);
            this.btnReadSync.Name = "btnReadSync";
            this.btnReadSync.Size = new System.Drawing.Size(141, 45);
            this.btnReadSync.TabIndex = 1;
            this.btnReadSync.Text = "ReadSync";
            this.btnReadSync.UseVisualStyleBackColor = true;
            this.btnReadSync.Click += new System.EventHandler(this.btnReadSync_Click);
            // 
            // tbResults
            // 
            this.tbResults.Location = new System.Drawing.Point(1, 1);
            this.tbResults.Multiline = true;
            this.tbResults.Name = "tbResults";
            this.tbResults.Size = new System.Drawing.Size(640, 449);
            this.tbResults.TabIndex = 2;
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(647, 185);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(141, 45);
            this.btnExit.TabIndex = 3;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // frmFileReader
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnExit;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.tbResults);
            this.Controls.Add(this.btnReadSync);
            this.Controls.Add(this.btnReadAsync);
            this.Name = "frmFileReader";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "File Reader";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.Button btnReadAsync;
        private System.Windows.Forms.Button btnReadSync;
        private System.Windows.Forms.TextBox tbResults;
        private System.Windows.Forms.Button btnExit;
    }
}