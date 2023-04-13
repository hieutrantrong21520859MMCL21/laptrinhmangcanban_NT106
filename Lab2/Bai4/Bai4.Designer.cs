namespace Bai4
{
    partial class frmBai4
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
            this.btnWrite = new System.Windows.Forms.Button();
            this.lblName = new System.Windows.Forms.Label();
            this.lblID = new System.Windows.Forms.Label();
            this.lblPhone = new System.Windows.Forms.Label();
            this.lblCourse1 = new System.Windows.Forms.Label();
            this.lblCourse2 = new System.Windows.Forms.Label();
            this.lblCourse3 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.txtID = new System.Windows.Forms.TextBox();
            this.txtPhone = new System.Windows.Forms.TextBox();
            this.txtCourse1 = new System.Windows.Forms.TextBox();
            this.txtCourse2 = new System.Windows.Forms.TextBox();
            this.txtCourse3 = new System.Windows.Forms.TextBox();
            this.txtResult = new System.Windows.Forms.TextBox();
            this.btnBack = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.lblPageNumber = new System.Windows.Forms.Label();
            this.btnRead = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnWrite
            // 
            this.btnWrite.Location = new System.Drawing.Point(98, 12);
            this.btnWrite.Name = "btnWrite";
            this.btnWrite.Size = new System.Drawing.Size(178, 34);
            this.btnWrite.TabIndex = 0;
            this.btnWrite.Text = "Write to a File input4.txt";
            this.btnWrite.UseVisualStyleBackColor = true;
            this.btnWrite.Click += new System.EventHandler(this.btnWrite_Click);
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(21, 83);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(44, 16);
            this.lblName.TabIndex = 2;
            this.lblName.Text = "Name";
            // 
            // lblID
            // 
            this.lblID.AutoSize = true;
            this.lblID.Location = new System.Drawing.Point(21, 137);
            this.lblID.Name = "lblID";
            this.lblID.Size = new System.Drawing.Size(78, 16);
            this.lblID.TabIndex = 3;
            this.lblID.Text = "Student\'s ID";
            // 
            // lblPhone
            // 
            this.lblPhone.AutoSize = true;
            this.lblPhone.Location = new System.Drawing.Point(21, 191);
            this.lblPhone.Name = "lblPhone";
            this.lblPhone.Size = new System.Drawing.Size(46, 16);
            this.lblPhone.TabIndex = 4;
            this.lblPhone.Text = "Phone";
            // 
            // lblCourse1
            // 
            this.lblCourse1.AutoSize = true;
            this.lblCourse1.Location = new System.Drawing.Point(21, 245);
            this.lblCourse1.Name = "lblCourse1";
            this.lblCourse1.Size = new System.Drawing.Size(60, 16);
            this.lblCourse1.TabIndex = 5;
            this.lblCourse1.Text = "Course 1";
            // 
            // lblCourse2
            // 
            this.lblCourse2.AutoSize = true;
            this.lblCourse2.Location = new System.Drawing.Point(21, 299);
            this.lblCourse2.Name = "lblCourse2";
            this.lblCourse2.Size = new System.Drawing.Size(60, 16);
            this.lblCourse2.TabIndex = 6;
            this.lblCourse2.Text = "Course 2";
            // 
            // lblCourse3
            // 
            this.lblCourse3.AutoSize = true;
            this.lblCourse3.Location = new System.Drawing.Point(21, 353);
            this.lblCourse3.Name = "lblCourse3";
            this.lblCourse3.Size = new System.Drawing.Size(60, 16);
            this.lblCourse3.TabIndex = 7;
            this.lblCourse3.Text = "Course 3";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(132, 83);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(216, 22);
            this.txtName.TabIndex = 8;
            // 
            // txtID
            // 
            this.txtID.Location = new System.Drawing.Point(132, 130);
            this.txtID.Name = "txtID";
            this.txtID.Size = new System.Drawing.Size(216, 22);
            this.txtID.TabIndex = 9;
            // 
            // txtPhone
            // 
            this.txtPhone.Location = new System.Drawing.Point(132, 184);
            this.txtPhone.Name = "txtPhone";
            this.txtPhone.Size = new System.Drawing.Size(216, 22);
            this.txtPhone.TabIndex = 10;
            // 
            // txtCourse1
            // 
            this.txtCourse1.Location = new System.Drawing.Point(132, 238);
            this.txtCourse1.Name = "txtCourse1";
            this.txtCourse1.Size = new System.Drawing.Size(216, 22);
            this.txtCourse1.TabIndex = 11;
            // 
            // txtCourse2
            // 
            this.txtCourse2.Location = new System.Drawing.Point(132, 292);
            this.txtCourse2.Name = "txtCourse2";
            this.txtCourse2.Size = new System.Drawing.Size(216, 22);
            this.txtCourse2.TabIndex = 12;
            // 
            // txtCourse3
            // 
            this.txtCourse3.Location = new System.Drawing.Point(132, 346);
            this.txtCourse3.Name = "txtCourse3";
            this.txtCourse3.Size = new System.Drawing.Size(216, 22);
            this.txtCourse3.TabIndex = 13;
            // 
            // txtResult
            // 
            this.txtResult.Location = new System.Drawing.Point(467, 65);
            this.txtResult.Multiline = true;
            this.txtResult.Name = "txtResult";
            this.txtResult.ReadOnly = true;
            this.txtResult.Size = new System.Drawing.Size(312, 398);
            this.txtResult.TabIndex = 14;
            // 
            // btnBack
            // 
            this.btnBack.Location = new System.Drawing.Point(467, 469);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(75, 23);
            this.btnBack.TabIndex = 15;
            this.btnBack.Text = "Back";
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // btnNext
            // 
            this.btnNext.Location = new System.Drawing.Point(704, 469);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(75, 23);
            this.btnNext.TabIndex = 16;
            this.btnNext.Text = "Next";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // lblPageNumber
            // 
            this.lblPageNumber.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblPageNumber.Location = new System.Drawing.Point(589, 472);
            this.lblPageNumber.Name = "lblPageNumber";
            this.lblPageNumber.Size = new System.Drawing.Size(74, 20);
            this.lblPageNumber.TabIndex = 17;
            this.lblPageNumber.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnRead
            // 
            this.btnRead.Location = new System.Drawing.Point(551, 12);
            this.btnRead.Name = "btnRead";
            this.btnRead.Size = new System.Drawing.Size(159, 34);
            this.btnRead.TabIndex = 18;
            this.btnRead.Text = "Read a File input4.txt";
            this.btnRead.UseVisualStyleBackColor = true;
            this.btnRead.Click += new System.EventHandler(this.btnRead_Click);
            // 
            // frmBai4
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 506);
            this.Controls.Add(this.btnRead);
            this.Controls.Add(this.lblPageNumber);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.txtResult);
            this.Controls.Add(this.txtCourse3);
            this.Controls.Add(this.txtCourse2);
            this.Controls.Add(this.txtCourse1);
            this.Controls.Add(this.txtPhone);
            this.Controls.Add(this.txtID);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.lblCourse3);
            this.Controls.Add(this.lblCourse2);
            this.Controls.Add(this.lblCourse1);
            this.Controls.Add(this.lblPhone);
            this.Controls.Add(this.lblID);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.btnWrite);
            this.Name = "frmBai4";
            this.Text = "Đọc và ghi File sử dụng BinaryFormatter";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnWrite;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblID;
        private System.Windows.Forms.Label lblPhone;
        private System.Windows.Forms.Label lblCourse1;
        private System.Windows.Forms.Label lblCourse2;
        private System.Windows.Forms.Label lblCourse3;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.TextBox txtID;
        private System.Windows.Forms.TextBox txtPhone;
        private System.Windows.Forms.TextBox txtCourse1;
        private System.Windows.Forms.TextBox txtCourse2;
        private System.Windows.Forms.TextBox txtCourse3;
        private System.Windows.Forms.TextBox txtResult;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Label lblPageNumber;
        private System.Windows.Forms.Button btnRead;
    }
}

