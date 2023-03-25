namespace BaiTapBuoi2
{
    partial class frmForm
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
            this.btnConnect = new System.Windows.Forms.Button();
            this.btnDisconnect = new System.Windows.Forms.Button();
            this.btnInsert = new System.Windows.Forms.Button();
            this.btnQuery = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.grbDataset = new System.Windows.Forms.GroupBox();
            this.dgvData = new System.Windows.Forms.DataGridView();
            this.grbInformation = new System.Windows.Forms.GroupBox();
            this.txtID_find = new System.Windows.Forms.TextBox();
            this.lblID_find = new System.Windows.Forms.Label();
            this.dtpBirth = new System.Windows.Forms.DateTimePicker();
            this.txtAddress = new System.Windows.Forms.TextBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.txtID = new System.Windows.Forms.TextBox();
            this.lblAddress = new System.Windows.Forms.Label();
            this.lblBirth = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.lblID = new System.Windows.Forms.Label();
            this.grbConnection = new System.Windows.Forms.GroupBox();
            this.grbModules = new System.Windows.Forms.GroupBox();
            this.grbDataset.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).BeginInit();
            this.grbInformation.SuspendLayout();
            this.grbConnection.SuspendLayout();
            this.grbModules.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(38, 37);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(135, 31);
            this.btnConnect.TabIndex = 0;
            this.btnConnect.Text = "Kết nối";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // btnDisconnect
            // 
            this.btnDisconnect.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDisconnect.Location = new System.Drawing.Point(38, 90);
            this.btnDisconnect.Name = "btnDisconnect";
            this.btnDisconnect.Size = new System.Drawing.Size(135, 31);
            this.btnDisconnect.TabIndex = 1;
            this.btnDisconnect.Text = "Ngắt kết nối";
            this.btnDisconnect.UseVisualStyleBackColor = true;
            this.btnDisconnect.Click += new System.EventHandler(this.btnDisconnect_Click);
            // 
            // btnInsert
            // 
            this.btnInsert.Location = new System.Drawing.Point(38, 111);
            this.btnInsert.Name = "btnInsert";
            this.btnInsert.Size = new System.Drawing.Size(135, 31);
            this.btnInsert.TabIndex = 2;
            this.btnInsert.Text = "Thêm";
            this.btnInsert.UseVisualStyleBackColor = true;
            this.btnInsert.Click += new System.EventHandler(this.btnInsert_Click);
            // 
            // btnQuery
            // 
            this.btnQuery.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnQuery.Location = new System.Drawing.Point(38, 42);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(135, 31);
            this.btnQuery.TabIndex = 3;
            this.btnQuery.Text = "Truy vấn";
            this.btnQuery.UseVisualStyleBackColor = true;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(38, 176);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(135, 31);
            this.btnDelete.TabIndex = 4;
            this.btnDelete.Text = "Xóa";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(38, 243);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(135, 31);
            this.btnUpdate.TabIndex = 5;
            this.btnUpdate.Text = "Sửa";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // grbDataset
            // 
            this.grbDataset.Controls.Add(this.dgvData);
            this.grbDataset.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grbDataset.Location = new System.Drawing.Point(3, 12);
            this.grbDataset.Name = "grbDataset";
            this.grbDataset.Size = new System.Drawing.Size(594, 242);
            this.grbDataset.TabIndex = 6;
            this.grbDataset.TabStop = false;
            this.grbDataset.Text = "Thông tin sinh viên";
            // 
            // dgvData
            // 
            this.dgvData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvData.Location = new System.Drawing.Point(3, 23);
            this.dgvData.Name = "dgvData";
            this.dgvData.RowHeadersWidth = 51;
            this.dgvData.RowTemplate.Height = 24;
            this.dgvData.Size = new System.Drawing.Size(588, 216);
            this.dgvData.TabIndex = 0;
            // 
            // grbInformation
            // 
            this.grbInformation.Controls.Add(this.txtID_find);
            this.grbInformation.Controls.Add(this.lblID_find);
            this.grbInformation.Controls.Add(this.dtpBirth);
            this.grbInformation.Controls.Add(this.txtAddress);
            this.grbInformation.Controls.Add(this.txtName);
            this.grbInformation.Controls.Add(this.txtID);
            this.grbInformation.Controls.Add(this.lblAddress);
            this.grbInformation.Controls.Add(this.lblBirth);
            this.grbInformation.Controls.Add(this.lblName);
            this.grbInformation.Controls.Add(this.lblID);
            this.grbInformation.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grbInformation.Location = new System.Drawing.Point(3, 260);
            this.grbInformation.Name = "grbInformation";
            this.grbInformation.Size = new System.Drawing.Size(594, 191);
            this.grbInformation.TabIndex = 7;
            this.grbInformation.TabStop = false;
            this.grbInformation.Text = "Điền thông tin";
            // 
            // txtID_find
            // 
            this.txtID_find.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtID_find.Location = new System.Drawing.Point(293, 154);
            this.txtID_find.Name = "txtID_find";
            this.txtID_find.Size = new System.Drawing.Size(111, 24);
            this.txtID_find.TabIndex = 9;
            // 
            // lblID_find
            // 
            this.lblID_find.AutoSize = true;
            this.lblID_find.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblID_find.Location = new System.Drawing.Point(66, 160);
            this.lblID_find.Name = "lblID_find";
            this.lblID_find.Size = new System.Drawing.Size(194, 18);
            this.lblID_find.TabIndex = 8;
            this.lblID_find.Text = "Nhập id cần tìm/xóa/sửa:";
            // 
            // dtpBirth
            // 
            this.dtpBirth.CustomFormat = "d/M/yyyy";
            this.dtpBirth.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpBirth.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpBirth.Location = new System.Drawing.Point(388, 37);
            this.dtpBirth.Name = "dtpBirth";
            this.dtpBirth.Size = new System.Drawing.Size(200, 27);
            this.dtpBirth.TabIndex = 7;
            // 
            // txtAddress
            // 
            this.txtAddress.Location = new System.Drawing.Point(388, 106);
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Size = new System.Drawing.Size(200, 27);
            this.txtAddress.TabIndex = 6;
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(69, 106);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(200, 27);
            this.txtName.TabIndex = 5;
            // 
            // txtID
            // 
            this.txtID.Location = new System.Drawing.Point(69, 34);
            this.txtID.Name = "txtID";
            this.txtID.Size = new System.Drawing.Size(200, 27);
            this.txtID.TabIndex = 4;
            // 
            // lblAddress
            // 
            this.lblAddress.AutoSize = true;
            this.lblAddress.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAddress.Location = new System.Drawing.Point(300, 115);
            this.lblAddress.Name = "lblAddress";
            this.lblAddress.Size = new System.Drawing.Size(73, 18);
            this.lblAddress.TabIndex = 3;
            this.lblAddress.Text = "address:";
            // 
            // lblBirth
            // 
            this.lblBirth.AutoSize = true;
            this.lblBirth.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBirth.Location = new System.Drawing.Point(327, 43);
            this.lblBirth.Name = "lblBirth";
            this.lblBirth.Size = new System.Drawing.Size(46, 18);
            this.lblBirth.TabIndex = 2;
            this.lblBirth.Text = "birth:";
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblName.Location = new System.Drawing.Point(0, 115);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(54, 18);
            this.lblName.TabIndex = 1;
            this.lblName.Text = "name:";
            // 
            // lblID
            // 
            this.lblID.AutoSize = true;
            this.lblID.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblID.Location = new System.Drawing.Point(28, 43);
            this.lblID.Name = "lblID";
            this.lblID.Size = new System.Drawing.Size(26, 18);
            this.lblID.TabIndex = 0;
            this.lblID.Text = "id:";
            // 
            // grbConnection
            // 
            this.grbConnection.Controls.Add(this.btnDisconnect);
            this.grbConnection.Controls.Add(this.btnConnect);
            this.grbConnection.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grbConnection.Location = new System.Drawing.Point(600, 12);
            this.grbConnection.Name = "grbConnection";
            this.grbConnection.Size = new System.Drawing.Size(197, 146);
            this.grbConnection.TabIndex = 8;
            this.grbConnection.TabStop = false;
            this.grbConnection.Text = "Kết nối/Ngắt kết nối";
            // 
            // grbModules
            // 
            this.grbModules.Controls.Add(this.btnQuery);
            this.grbModules.Controls.Add(this.btnInsert);
            this.grbModules.Controls.Add(this.btnUpdate);
            this.grbModules.Controls.Add(this.btnDelete);
            this.grbModules.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grbModules.Location = new System.Drawing.Point(600, 164);
            this.grbModules.Name = "grbModules";
            this.grbModules.Size = new System.Drawing.Size(197, 287);
            this.grbModules.TabIndex = 9;
            this.grbModules.TabStop = false;
            this.grbModules.Text = "Chức năng";
            // 
            // frmForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.grbModules);
            this.Controls.Add(this.grbConnection);
            this.Controls.Add(this.grbInformation);
            this.Controls.Add(this.grbDataset);
            this.Name = "frmForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Quản lý sinh viên";
            this.grbDataset.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).EndInit();
            this.grbInformation.ResumeLayout(false);
            this.grbInformation.PerformLayout();
            this.grbConnection.ResumeLayout(false);
            this.grbModules.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Button btnDisconnect;
        private System.Windows.Forms.Button btnInsert;
        private System.Windows.Forms.Button btnQuery;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.GroupBox grbDataset;
        private System.Windows.Forms.DataGridView dgvData;
        private System.Windows.Forms.GroupBox grbInformation;
        private System.Windows.Forms.DateTimePicker dtpBirth;
        private System.Windows.Forms.TextBox txtAddress;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.TextBox txtID;
        private System.Windows.Forms.Label lblAddress;
        private System.Windows.Forms.Label lblBirth;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblID;
        private System.Windows.Forms.GroupBox grbConnection;
        private System.Windows.Forms.GroupBox grbModules;
        private System.Windows.Forms.TextBox txtID_find;
        private System.Windows.Forms.Label lblID_find;
    }
}

