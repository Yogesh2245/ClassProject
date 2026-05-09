namespace LMSDesk
{
    partial class marklistprint
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
            this.dgvmark = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbBranchList = new System.Windows.Forms.ComboBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.rdbPrinted = new System.Windows.Forms.RadioButton();
            this.rdbNotPrinted = new System.Windows.Forms.RadioButton();
            this.pnlLoader = new System.Windows.Forms.Panel();
            this.picLoader = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvmark)).BeginInit();
            this.panel2.SuspendLayout();
            this.pnlLoader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLoader)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvmark
            // 
            this.dgvmark.AllowUserToAddRows = false;
            this.dgvmark.AllowUserToDeleteRows = false;
            this.dgvmark.AllowUserToResizeColumns = false;
            this.dgvmark.AllowUserToResizeRows = false;
            this.dgvmark.BackgroundColor = System.Drawing.Color.White;
            this.dgvmark.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvmark.Location = new System.Drawing.Point(12, 104);
            this.dgvmark.Name = "dgvmark";
            this.dgvmark.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvmark.Size = new System.Drawing.Size(951, 432);
            this.dgvmark.TabIndex = 0;
            this.dgvmark.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvmark_CellClick);
            this.dgvmark.CurrentCellDirtyStateChanged += new System.EventHandler(this.dgvmark_CurrentCellDirtyStateChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(387, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(158, 23);
            this.label1.TabIndex = 1;
            this.label1.Text = "MARKLIST DETAILS";
            // 
            // cmbBranchList
            // 
            this.cmbBranchList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBranchList.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbBranchList.FormattingEnabled = true;
            this.cmbBranchList.Location = new System.Drawing.Point(16, 55);
            this.cmbBranchList.Name = "cmbBranchList";
            this.cmbBranchList.Size = new System.Drawing.Size(121, 27);
            this.cmbBranchList.TabIndex = 11;
            this.cmbBranchList.SelectedIndexChanged += new System.EventHandler(this.cmbBranchList_SelectedIndexChanged);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.rdbPrinted);
            this.panel2.Controls.Add(this.rdbNotPrinted);
            this.panel2.Location = new System.Drawing.Point(552, 50);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(200, 37);
            this.panel2.TabIndex = 10;
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
            // pnlLoader
            // 
            this.pnlLoader.Controls.Add(this.picLoader);
            this.pnlLoader.Location = new System.Drawing.Point(373, 165);
            this.pnlLoader.Name = "pnlLoader";
            this.pnlLoader.Size = new System.Drawing.Size(229, 218);
            this.pnlLoader.TabIndex = 12;
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
            // marklistprint
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(975, 548);
            this.Controls.Add(this.pnlLoader);
            this.Controls.Add(this.cmbBranchList);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgvmark);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "marklistprint";
            this.Text = "marklistprint";
            this.Load += new System.EventHandler(this.marklistprint_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvmark)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.pnlLoader.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picLoader)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvmark;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbBranchList;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.RadioButton rdbPrinted;
        private System.Windows.Forms.RadioButton rdbNotPrinted;
        private System.Windows.Forms.Panel pnlLoader;
        private System.Windows.Forms.PictureBox picLoader;
    }
}