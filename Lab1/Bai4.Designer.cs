namespace ThucHanhTuan1
{
    partial class frmForm4
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
            this.lblTopic = new System.Windows.Forms.Label();
            this.lblInput = new System.Windows.Forms.Label();
            this.lblResult = new System.Windows.Forms.Label();
            this.lblRate = new System.Windows.Forms.Label();
            this.btnConvert = new System.Windows.Forms.Button();
            this.txtInput = new System.Windows.Forms.TextBox();
            this.cboCur_unit = new System.Windows.Forms.ComboBox();
            this.lblOutput = new System.Windows.Forms.Label();
            this.lblRate_val = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblTopic
            // 
            this.lblTopic.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTopic.Location = new System.Drawing.Point(12, 9);
            this.lblTopic.Name = "lblTopic";
            this.lblTopic.Size = new System.Drawing.Size(208, 74);
            this.lblTopic.TabIndex = 0;
            this.lblTopic.Text = "Chuyển đổi tiền tệ";
            this.lblTopic.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblInput
            // 
            this.lblInput.AutoSize = true;
            this.lblInput.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInput.Location = new System.Drawing.Point(12, 119);
            this.lblInput.Name = "lblInput";
            this.lblInput.Size = new System.Drawing.Size(120, 20);
            this.lblInput.TabIndex = 1;
            this.lblInput.Text = "Số tiền cần đổi";
            // 
            // lblResult
            // 
            this.lblResult.AutoSize = true;
            this.lblResult.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblResult.Location = new System.Drawing.Point(12, 260);
            this.lblResult.Name = "lblResult";
            this.lblResult.Size = new System.Drawing.Size(111, 20);
            this.lblResult.TabIndex = 5;
            this.lblResult.Text = "Số tiền đã đổi";
            // 
            // lblRate
            // 
            this.lblRate.AutoSize = true;
            this.lblRate.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRate.Location = new System.Drawing.Point(12, 329);
            this.lblRate.Name = "lblRate";
            this.lblRate.Size = new System.Drawing.Size(112, 20);
            this.lblRate.TabIndex = 7;
            this.lblRate.Text = "Tỷ giá quy đổi";
            // 
            // btnConvert
            // 
            this.btnConvert.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConvert.Location = new System.Drawing.Point(16, 177);
            this.btnConvert.Name = "btnConvert";
            this.btnConvert.Size = new System.Drawing.Size(538, 34);
            this.btnConvert.TabIndex = 4;
            this.btnConvert.Text = "Chuyển đổi";
            this.btnConvert.UseVisualStyleBackColor = true;
            this.btnConvert.Click += new System.EventHandler(this.btnConvert_Click);
            // 
            // txtInput
            // 
            this.txtInput.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtInput.Location = new System.Drawing.Point(189, 112);
            this.txtInput.Name = "txtInput";
            this.txtInput.Size = new System.Drawing.Size(154, 27);
            this.txtInput.TabIndex = 2;
            // 
            // cboCur_unit
            // 
            this.cboCur_unit.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboCur_unit.FormattingEnabled = true;
            this.cboCur_unit.Items.AddRange(new object[] {
            "USD",
            "EUR",
            "GBP",
            "SGD",
            "JPY"});
            this.cboCur_unit.Location = new System.Drawing.Point(400, 111);
            this.cboCur_unit.Name = "cboCur_unit";
            this.cboCur_unit.Size = new System.Drawing.Size(154, 28);
            this.cboCur_unit.TabIndex = 3;
            // 
            // lblOutput
            // 
            this.lblOutput.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblOutput.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOutput.Location = new System.Drawing.Point(189, 257);
            this.lblOutput.Name = "lblOutput";
            this.lblOutput.Size = new System.Drawing.Size(365, 23);
            this.lblOutput.TabIndex = 6;
            this.lblOutput.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblRate_val
            // 
            this.lblRate_val.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblRate_val.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRate_val.Location = new System.Drawing.Point(189, 326);
            this.lblRate_val.Name = "lblRate_val";
            this.lblRate_val.Size = new System.Drawing.Size(365, 23);
            this.lblRate_val.TabIndex = 8;
            this.lblRate_val.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // frmForm4
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(644, 391);
            this.Controls.Add(this.lblRate_val);
            this.Controls.Add(this.lblOutput);
            this.Controls.Add(this.cboCur_unit);
            this.Controls.Add(this.txtInput);
            this.Controls.Add(this.btnConvert);
            this.Controls.Add(this.lblRate);
            this.Controls.Add(this.lblResult);
            this.Controls.Add(this.lblInput);
            this.Controls.Add(this.lblTopic);
            this.Name = "frmForm4";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Lab1-Bai4";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTopic;
        private System.Windows.Forms.Label lblInput;
        private System.Windows.Forms.Label lblResult;
        private System.Windows.Forms.Label lblRate;
        private System.Windows.Forms.Button btnConvert;
        private System.Windows.Forms.TextBox txtInput;
        private System.Windows.Forms.ComboBox cboCur_unit;
        private System.Windows.Forms.Label lblOutput;
        private System.Windows.Forms.Label lblRate_val;
    }
}