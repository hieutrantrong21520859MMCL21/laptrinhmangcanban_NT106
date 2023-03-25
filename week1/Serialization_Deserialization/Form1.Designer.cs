namespace Serialization_Deserialization
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
            this.btnSoap_se = new System.Windows.Forms.Button();
            this.btnSoap_de = new System.Windows.Forms.Button();
            this.btnBinary_se = new System.Windows.Forms.Button();
            this.btnBinary_de = new System.Windows.Forms.Button();
            this.btnXML_se = new System.Windows.Forms.Button();
            this.btnXML_de = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnSoap_se
            // 
            this.btnSoap_se.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnSoap_se.Location = new System.Drawing.Point(52, 65);
            this.btnSoap_se.Name = "btnSoap_se";
            this.btnSoap_se.Size = new System.Drawing.Size(136, 23);
            this.btnSoap_se.TabIndex = 0;
            this.btnSoap_se.Text = "SOAP serialize";
            this.btnSoap_se.UseVisualStyleBackColor = false;
            this.btnSoap_se.Click += new System.EventHandler(this.btnSoap_se_Click);
            // 
            // btnSoap_de
            // 
            this.btnSoap_de.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnSoap_de.Location = new System.Drawing.Point(52, 151);
            this.btnSoap_de.Name = "btnSoap_de";
            this.btnSoap_de.Size = new System.Drawing.Size(136, 23);
            this.btnSoap_de.TabIndex = 1;
            this.btnSoap_de.Text = "SOAP deserialize";
            this.btnSoap_de.UseVisualStyleBackColor = false;
            this.btnSoap_de.Click += new System.EventHandler(this.btnSoap_de_Click);
            // 
            // btnBinary_se
            // 
            this.btnBinary_se.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnBinary_se.Location = new System.Drawing.Point(332, 65);
            this.btnBinary_se.Name = "btnBinary_se";
            this.btnBinary_se.Size = new System.Drawing.Size(136, 23);
            this.btnBinary_se.TabIndex = 2;
            this.btnBinary_se.Text = "Binary serialize";
            this.btnBinary_se.UseVisualStyleBackColor = false;
            this.btnBinary_se.Click += new System.EventHandler(this.btnBinary_se_Click);
            // 
            // btnBinary_de
            // 
            this.btnBinary_de.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnBinary_de.Location = new System.Drawing.Point(332, 151);
            this.btnBinary_de.Name = "btnBinary_de";
            this.btnBinary_de.Size = new System.Drawing.Size(136, 23);
            this.btnBinary_de.TabIndex = 3;
            this.btnBinary_de.Text = "Binary deserialize";
            this.btnBinary_de.UseVisualStyleBackColor = false;
            this.btnBinary_de.Click += new System.EventHandler(this.btnBinary_de_Click);
            // 
            // btnXML_se
            // 
            this.btnXML_se.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnXML_se.Location = new System.Drawing.Point(612, 65);
            this.btnXML_se.Name = "btnXML_se";
            this.btnXML_se.Size = new System.Drawing.Size(136, 23);
            this.btnXML_se.TabIndex = 4;
            this.btnXML_se.Text = "XML serialize";
            this.btnXML_se.UseVisualStyleBackColor = false;
            this.btnXML_se.Click += new System.EventHandler(this.btnXML_se_Click);
            // 
            // btnXML_de
            // 
            this.btnXML_de.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnXML_de.Location = new System.Drawing.Point(612, 151);
            this.btnXML_de.Name = "btnXML_de";
            this.btnXML_de.Size = new System.Drawing.Size(136, 23);
            this.btnXML_de.TabIndex = 5;
            this.btnXML_de.Text = "XML deserialize";
            this.btnXML_de.UseVisualStyleBackColor = false;
            this.btnXML_de.Click += new System.EventHandler(this.btnXML_de_Click);
            // 
            // frmForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.ClientSize = new System.Drawing.Size(800, 227);
            this.Controls.Add(this.btnXML_de);
            this.Controls.Add(this.btnXML_se);
            this.Controls.Add(this.btnBinary_de);
            this.Controls.Add(this.btnBinary_se);
            this.Controls.Add(this.btnSoap_de);
            this.Controls.Add(this.btnSoap_se);
            this.Name = "frmForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Serialization_Deserialization";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnSoap_se;
        private System.Windows.Forms.Button btnSoap_de;
        private System.Windows.Forms.Button btnBinary_se;
        private System.Windows.Forms.Button btnBinary_de;
        private System.Windows.Forms.Button btnXML_se;
        private System.Windows.Forms.Button btnXML_de;
    }
}

