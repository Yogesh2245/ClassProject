namespace LMSDesk
{
    partial class CertificationList
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
            this.dgvCertificationList = new System.Windows.Forms.DataGridView();
            this.lblCertificationList = new System.Windows.Forms.Label();
            this.txtCert = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCertificationList)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvCertificationList
            // 
            this.dgvCertificationList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvCertificationList.BackgroundColor = System.Drawing.Color.White;
            this.dgvCertificationList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCertificationList.Location = new System.Drawing.Point(25, 53);
            this.dgvCertificationList.Name = "dgvCertificationList";
            this.dgvCertificationList.RowHeadersVisible = false;
            this.dgvCertificationList.Size = new System.Drawing.Size(1073, 482);
            this.dgvCertificationList.TabIndex = 0;
            // 
            // lblCertificationList
            // 
            this.lblCertificationList.Cursor = System.Windows.Forms.Cursors.No;
            this.lblCertificationList.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCertificationList.Location = new System.Drawing.Point(449, 9);
            this.lblCertificationList.Name = "lblCertificationList";
            this.lblCertificationList.Size = new System.Drawing.Size(167, 32);
            this.lblCertificationList.TabIndex = 4;
            this.lblCertificationList.Text = "CERTIFICATE LIST";
            this.lblCertificationList.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtCert
            // 
            this.txtCert.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCert.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCert.Location = new System.Drawing.Point(25, 20);
            this.txtCert.Name = "txtCert";
            this.txtCert.Size = new System.Drawing.Size(211, 27);
            this.txtCert.TabIndex = 5;
            this.txtCert.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtCert.TextChanged += new System.EventHandler(this.txtCert_TextChanged);
            // 
            // CertificationList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1110, 547);
            this.Controls.Add(this.txtCert);
            this.Controls.Add(this.lblCertificationList);
            this.Controls.Add(this.dgvCertificationList);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "CertificationList";
            this.Text = "CertificationLIst";
            this.Load += new System.EventHandler(this.CertificationList_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCertificationList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvCertificationList;
        private System.Windows.Forms.Label lblCertificationList;
        private System.Windows.Forms.TextBox txtCert;
    }
}