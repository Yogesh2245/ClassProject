namespace LMSDesk
{
    partial class requested
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
            this.dgvrequested = new System.Windows.Forms.DataGridView();
            this.l = new System.Windows.Forms.Label();
            this.btnprint = new System.Windows.Forms.Button();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.chkPrintPhoto = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.rdbNotPrinted = new System.Windows.Forms.RadioButton();
            this.rdbPrinted = new System.Windows.Forms.RadioButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.cmbBranchList = new System.Windows.Forms.ComboBox();
            this.pnlLoader = new System.Windows.Forms.Panel();
            this.picLoader = new System.Windows.Forms.PictureBox();
            this.chkUseDefaultTemplate = new System.Windows.Forms.CheckBox();
            this.panel3 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.dgvrequested)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.pnlLoader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLoader)).BeginInit();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvrequested
            // 
            this.dgvrequested.BackgroundColor = System.Drawing.Color.White;
            this.dgvrequested.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvrequested.EnableHeadersVisualStyles = false;
            this.dgvrequested.Location = new System.Drawing.Point(12, 44);
            this.dgvrequested.Name = "dgvrequested";
            this.dgvrequested.RowHeadersVisible = false;
            this.dgvrequested.Size = new System.Drawing.Size(1098, 493);
            this.dgvrequested.TabIndex = 0;
            this.dgvrequested.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvrequested_CellContentClick);
            this.dgvrequested.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvrequested_CellValueChanged);
            // 
            // l
            // 
            this.l.AutoSize = true;
            this.l.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.l.Location = new System.Drawing.Point(254, 9);
            this.l.Name = "l";
            this.l.Size = new System.Drawing.Size(244, 23);
            this.l.TabIndex = 1;
            this.l.Text = "REQUIESTED CERTIFICATE LIST";
            // 
            // btnprint
            // 
            this.btnprint.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.btnprint.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnprint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnprint.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnprint.Location = new System.Drawing.Point(9, 38);
            this.btnprint.Name = "btnprint";
            this.btnprint.Size = new System.Drawing.Size(62, 29);
            this.btnprint.TabIndex = 2;
            this.btnprint.Text = "Print";
            this.btnprint.UseVisualStyleBackColor = false;
            this.btnprint.Click += new System.EventHandler(this.btnprint_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.checkBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.checkBox1.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBox1.Location = new System.Drawing.Point(5, 9);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(66, 23);
            this.checkBox1.TabIndex = 3;
            this.checkBox1.Text = "Select";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // chkPrintPhoto
            // 
            this.chkPrintPhoto.AutoSize = true;
            this.chkPrintPhoto.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkPrintPhoto.Location = new System.Drawing.Point(3, 5);
            this.chkPrintPhoto.Name = "chkPrintPhoto";
            this.chkPrintPhoto.Size = new System.Drawing.Size(183, 27);
            this.chkPrintPhoto.TabIndex = 4;
            this.chkPrintPhoto.Text = "PRINT WITH PHOTO";
            this.chkPrintPhoto.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.checkBox1);
            this.panel1.Controls.Add(this.btnprint);
            this.panel1.Location = new System.Drawing.Point(12, 9);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(10, 10);
            this.panel1.TabIndex = 5;
            // 
            // rdbNotPrinted
            // 
            this.rdbNotPrinted.AutoSize = true;
            this.rdbNotPrinted.Checked = true;
            this.rdbNotPrinted.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdbNotPrinted.Location = new System.Drawing.Point(97, 7);
            this.rdbNotPrinted.Name = "rdbNotPrinted";
            this.rdbNotPrinted.Size = new System.Drawing.Size(100, 23);
            this.rdbNotPrinted.TabIndex = 7;
            this.rdbNotPrinted.TabStop = true;
            this.rdbNotPrinted.Text = "Not Printed";
            this.rdbNotPrinted.UseVisualStyleBackColor = true;
            this.rdbNotPrinted.CheckedChanged += new System.EventHandler(this.rdbNotPrinted_CheckedChanged);
            // 
            // rdbPrinted
            // 
            this.rdbPrinted.AutoSize = true;
            this.rdbPrinted.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdbPrinted.Location = new System.Drawing.Point(7, 7);
            this.rdbPrinted.Name = "rdbPrinted";
            this.rdbPrinted.Size = new System.Drawing.Size(73, 23);
            this.rdbPrinted.TabIndex = 6;
            this.rdbPrinted.Text = "Printed";
            this.rdbPrinted.UseVisualStyleBackColor = true;
            this.rdbPrinted.CheckedChanged += new System.EventHandler(this.rdbPrinted_CheckedChanged);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.rdbPrinted);
            this.panel2.Controls.Add(this.rdbNotPrinted);
            this.panel2.Location = new System.Drawing.Point(504, 4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(200, 37);
            this.panel2.TabIndex = 8;
            // 
            // cmbBranchList
            // 
            this.cmbBranchList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBranchList.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbBranchList.FormattingEnabled = true;
            this.cmbBranchList.Location = new System.Drawing.Point(37, 9);
            this.cmbBranchList.Name = "cmbBranchList";
            this.cmbBranchList.Size = new System.Drawing.Size(211, 27);
            this.cmbBranchList.TabIndex = 9;
            this.cmbBranchList.SelectedIndexChanged += new System.EventHandler(this.cmbBranchList_SelectedIndexChanged);
            // 
            // pnlLoader
            // 
            this.pnlLoader.Controls.Add(this.picLoader);
            this.pnlLoader.Location = new System.Drawing.Point(442, 140);
            this.pnlLoader.Name = "pnlLoader";
            this.pnlLoader.Size = new System.Drawing.Size(229, 218);
            this.pnlLoader.TabIndex = 11;
            // 
            // picLoader
            // 
            this.picLoader.Image = global::LMSDesk.Properties.Resources.search;
            this.picLoader.Location = new System.Drawing.Point(14, 14);
            this.picLoader.Name = "picLoader";
            this.picLoader.Size = new System.Drawing.Size(201, 189);
            this.picLoader.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picLoader.TabIndex = 10;
            this.picLoader.TabStop = false;
            // 
            // chkUseDefaultTemplate
            // 
            this.chkUseDefaultTemplate.AutoSize = true;
            this.chkUseDefaultTemplate.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkUseDefaultTemplate.Location = new System.Drawing.Point(192, 5);
            this.chkUseDefaultTemplate.Name = "chkUseDefaultTemplate";
            this.chkUseDefaultTemplate.Size = new System.Drawing.Size(208, 27);
            this.chkUseDefaultTemplate.TabIndex = 12;
            this.chkUseDefaultTemplate.Text = "PRINT WITH TEMPLATE";
            this.chkUseDefaultTemplate.UseVisualStyleBackColor = true;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.chkPrintPhoto);
            this.panel3.Controls.Add(this.chkUseDefaultTemplate);
            this.panel3.Location = new System.Drawing.Point(709, 1);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(401, 35);
            this.panel3.TabIndex = 13;
            // 
            // requested
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1121, 552);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.pnlLoader);
            this.Controls.Add(this.cmbBranchList);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.l);
            this.Controls.Add(this.dgvrequested);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "requested";
            this.Text = "requested";
            this.Load += new System.EventHandler(this.requested_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvrequested)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.pnlLoader.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picLoader)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvrequested;
        private System.Windows.Forms.Label l;
        private System.Windows.Forms.Button btnprint;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.CheckBox chkPrintPhoto;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton rdbNotPrinted;
        private System.Windows.Forms.RadioButton rdbPrinted;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ComboBox cmbBranchList;
        private System.Windows.Forms.PictureBox picLoader;
        private System.Windows.Forms.Panel pnlLoader;
        private System.Windows.Forms.CheckBox chkUseDefaultTemplate;
        private System.Windows.Forms.Panel panel3;
    }
}