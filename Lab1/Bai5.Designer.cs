namespace ThucHanhTuan1
{
    partial class frmForm5
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
            this.lblA = new System.Windows.Forms.Label();
            this.lblB = new System.Windows.Forms.Label();
            this.txtA = new System.Windows.Forms.TextBox();
            this.txtB = new System.Windows.Forms.TextBox();
            this.btnCalculate = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.grbResult = new System.Windows.Forms.GroupBox();
            this.lblFacto_B = new System.Windows.Forms.Label();
            this.lblSum_Exponent = new System.Windows.Forms.Label();
            this.lblSum_B = new System.Windows.Forms.Label();
            this.lblSum_A = new System.Windows.Forms.Label();
            this.lblFacto_A = new System.Windows.Forms.Label();
            this.grbResult.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblA
            // 
            this.lblA.AutoSize = true;
            this.lblA.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblA.Location = new System.Drawing.Point(12, 58);
            this.lblA.Name = "lblA";
            this.lblA.Size = new System.Drawing.Size(64, 20);
            this.lblA.TabIndex = 0;
            this.lblA.Text = "Nhập A";
            // 
            // lblB
            // 
            this.lblB.AutoSize = true;
            this.lblB.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblB.Location = new System.Drawing.Point(476, 58);
            this.lblB.Name = "lblB";
            this.lblB.Size = new System.Drawing.Size(65, 20);
            this.lblB.TabIndex = 2;
            this.lblB.Text = "Nhập B";
            // 
            // txtA
            // 
            this.txtA.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtA.Location = new System.Drawing.Point(121, 51);
            this.txtA.Name = "txtA";
            this.txtA.Size = new System.Drawing.Size(284, 27);
            this.txtA.TabIndex = 1;
            // 
            // txtB
            // 
            this.txtB.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtB.Location = new System.Drawing.Point(612, 51);
            this.txtB.Name = "txtB";
            this.txtB.Size = new System.Drawing.Size(284, 27);
            this.txtB.TabIndex = 3;
            // 
            // btnCalculate
            // 
            this.btnCalculate.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCalculate.Location = new System.Drawing.Point(16, 150);
            this.btnCalculate.Name = "btnCalculate";
            this.btnCalculate.Size = new System.Drawing.Size(389, 35);
            this.btnCalculate.TabIndex = 4;
            this.btnCalculate.Text = "Tính các giá trị";
            this.btnCalculate.UseVisualStyleBackColor = true;
            this.btnCalculate.Click += new System.EventHandler(this.btnCalculate_Click);
            // 
            // btnReset
            // 
            this.btnReset.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReset.Location = new System.Drawing.Point(547, 150);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(103, 35);
            this.btnReset.TabIndex = 5;
            this.btnReset.Text = "Xóa";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // btnExit
            // 
            this.btnExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.Location = new System.Drawing.Point(792, 150);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(103, 35);
            this.btnExit.TabIndex = 6;
            this.btnExit.Text = "Thoát";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // grbResult
            // 
            this.grbResult.Controls.Add(this.lblFacto_B);
            this.grbResult.Controls.Add(this.lblSum_Exponent);
            this.grbResult.Controls.Add(this.lblSum_B);
            this.grbResult.Controls.Add(this.lblSum_A);
            this.grbResult.Controls.Add(this.lblFacto_A);
            this.grbResult.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grbResult.Location = new System.Drawing.Point(16, 225);
            this.grbResult.Name = "grbResult";
            this.grbResult.Size = new System.Drawing.Size(889, 205);
            this.grbResult.TabIndex = 7;
            this.grbResult.TabStop = false;
            this.grbResult.Text = "Kết quả";
            // 
            // lblFacto_B
            // 
            this.lblFacto_B.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblFacto_B.Location = new System.Drawing.Point(465, 50);
            this.lblFacto_B.Name = "lblFacto_B";
            this.lblFacto_B.Size = new System.Drawing.Size(418, 23);
            this.lblFacto_B.TabIndex = 4;
            this.lblFacto_B.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblSum_Exponent
            // 
            this.lblSum_Exponent.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblSum_Exponent.Location = new System.Drawing.Point(111, 157);
            this.lblSum_Exponent.Name = "lblSum_Exponent";
            this.lblSum_Exponent.Size = new System.Drawing.Size(666, 23);
            this.lblSum_Exponent.TabIndex = 3;
            this.lblSum_Exponent.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblSum_B
            // 
            this.lblSum_B.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblSum_B.Location = new System.Drawing.Point(465, 99);
            this.lblSum_B.Name = "lblSum_B";
            this.lblSum_B.Size = new System.Drawing.Size(418, 23);
            this.lblSum_B.TabIndex = 2;
            this.lblSum_B.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblSum_A
            // 
            this.lblSum_A.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblSum_A.Location = new System.Drawing.Point(7, 99);
            this.lblSum_A.Name = "lblSum_A";
            this.lblSum_A.Size = new System.Drawing.Size(418, 23);
            this.lblSum_A.TabIndex = 1;
            this.lblSum_A.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblFacto_A
            // 
            this.lblFacto_A.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblFacto_A.Location = new System.Drawing.Point(7, 50);
            this.lblFacto_A.Name = "lblFacto_A";
            this.lblFacto_A.Size = new System.Drawing.Size(418, 23);
            this.lblFacto_A.TabIndex = 0;
            this.lblFacto_A.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // frmForm5
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnExit;
            this.ClientSize = new System.Drawing.Size(917, 444);
            this.Controls.Add(this.grbResult);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.btnCalculate);
            this.Controls.Add(this.txtB);
            this.Controls.Add(this.txtA);
            this.Controls.Add(this.lblB);
            this.Controls.Add(this.lblA);
            this.Name = "frmForm5";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Lab1-Bai5";
            this.grbResult.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblA;
        private System.Windows.Forms.Label lblB;
        private System.Windows.Forms.TextBox txtA;
        private System.Windows.Forms.TextBox txtB;
        private System.Windows.Forms.Button btnCalculate;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.GroupBox grbResult;
        private System.Windows.Forms.Label lblFacto_B;
        private System.Windows.Forms.Label lblSum_Exponent;
        private System.Windows.Forms.Label lblSum_B;
        private System.Windows.Forms.Label lblSum_A;
        private System.Windows.Forms.Label lblFacto_A;
    }
}