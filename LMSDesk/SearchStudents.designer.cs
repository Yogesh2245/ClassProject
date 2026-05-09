namespace LMSDesk
{
    partial class SearchStudents
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.dgvStudents = new System.Windows.Forms.DataGridView();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblCount = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.radioButton_ascending = new System.Windows.Forms.RadioButton();
            this.radioButton_descending = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.radioButton_ByNumber = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStudents)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtSearch
            // 
            this.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSearch.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtSearch.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtSearch.Font = new System.Drawing.Font("Cambria", 16F, System.Drawing.FontStyle.Bold);
            this.txtSearch.ForeColor = System.Drawing.Color.Red;
            this.txtSearch.Location = new System.Drawing.Point(158, 12);
            this.txtSearch.MaxLength = 60;
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(465, 33);
            this.txtSearch.TabIndex = 0;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            this.txtSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSearch_KeyDown);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.BackColor = System.Drawing.Color.Transparent;
            this.label12.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.Teal;
            this.label12.Location = new System.Drawing.Point(12, 19);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(123, 19);
            this.label12.TabIndex = 1085;
            this.label12.Text = "Search Job/Bill:";
            // 
            // dgvStudents
            // 
            this.dgvStudents.AllowUserToAddRows = false;
            this.dgvStudents.AllowUserToResizeColumns = false;
            this.dgvStudents.AllowUserToResizeRows = false;
            this.dgvStudents.BackgroundColor = System.Drawing.Color.Ivory;
            this.dgvStudents.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvStudents.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(37)))), ((int)(((byte)(38)))));
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.75F);
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvStudents.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvStudents.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvStudents.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dgvStudents.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvStudents.EnableHeadersVisualStyles = false;
            this.dgvStudents.GridColor = System.Drawing.Color.Black;
            this.dgvStudents.Location = new System.Drawing.Point(0, 0);
            this.dgvStudents.MultiSelect = false;
            this.dgvStudents.Name = "dgvStudents";
            this.dgvStudents.ReadOnly = true;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvStudents.RowHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvStudents.RowHeadersVisible = false;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Cambria", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvStudents.RowsDefaultCellStyle = dataGridViewCellStyle6;
            this.dgvStudents.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.dgvStudents.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.White;
            this.dgvStudents.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Cambria", 13F);
            this.dgvStudents.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.Black;
            this.dgvStudents.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.SeaGreen;
            this.dgvStudents.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.White;
            this.dgvStudents.RowTemplate.Height = 26;
            this.dgvStudents.RowTemplate.ReadOnly = true;
            this.dgvStudents.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvStudents.Size = new System.Drawing.Size(635, 676);
            this.dgvStudents.TabIndex = 1088;
            this.dgvStudents.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            this.dgvStudents.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dataGridView1_CellPainting);
            this.dgvStudents.RowPrePaint += new System.Windows.Forms.DataGridViewRowPrePaintEventHandler(this.dataGridView1_RowPrePaint);
            this.dgvStudents.SelectionChanged += new System.EventHandler(this.dataGridView1_SelectionChanged);
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Transparent;
            this.panel3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel3.Location = new System.Drawing.Point(312, 747);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(42, 32);
            this.panel3.TabIndex = 1092;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblCount);
            this.panel1.Controls.Add(this.label12);
            this.panel1.Controls.Add(this.txtSearch);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(635, 73);
            this.panel1.TabIndex = 1099;
            // 
            // lblCount
            // 
            this.lblCount.AutoSize = true;
            this.lblCount.Location = new System.Drawing.Point(-21, 52);
            this.lblCount.Name = "lblCount";
            this.lblCount.Size = new System.Drawing.Size(13, 13);
            this.lblCount.TabIndex = 1088;
            this.lblCount.Text = "0";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.dgvStudents);
            this.panel2.Controls.Add(this.radioButton_ascending);
            this.panel2.Controls.Add(this.radioButton_descending);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.radioButton_ByNumber);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 73);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(635, 676);
            this.panel2.TabIndex = 1100;
            // 
            // radioButton_ascending
            // 
            this.radioButton_ascending.AutoSize = true;
            this.radioButton_ascending.BackColor = System.Drawing.Color.Transparent;
            this.radioButton_ascending.Checked = true;
            this.radioButton_ascending.Font = new System.Drawing.Font("Cambria", 11F);
            this.radioButton_ascending.ForeColor = System.Drawing.Color.Black;
            this.radioButton_ascending.Location = new System.Drawing.Point(87, 42);
            this.radioButton_ascending.Name = "radioButton_ascending";
            this.radioButton_ascending.Size = new System.Drawing.Size(62, 21);
            this.radioButton_ascending.TabIndex = 1089;
            this.radioButton_ascending.TabStop = true;
            this.radioButton_ascending.Text = "A to Z";
            this.radioButton_ascending.UseVisualStyleBackColor = false;
            // 
            // radioButton_descending
            // 
            this.radioButton_descending.AutoSize = true;
            this.radioButton_descending.BackColor = System.Drawing.Color.Transparent;
            this.radioButton_descending.Font = new System.Drawing.Font("Cambria", 11F);
            this.radioButton_descending.ForeColor = System.Drawing.Color.Black;
            this.radioButton_descending.Location = new System.Drawing.Point(161, 42);
            this.radioButton_descending.Name = "radioButton_descending";
            this.radioButton_descending.Size = new System.Drawing.Size(62, 21);
            this.radioButton_descending.TabIndex = 1090;
            this.radioButton_descending.TabStop = true;
            this.radioButton_descending.Text = "Z to A";
            this.radioButton_descending.UseVisualStyleBackColor = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Cambria", 11F);
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(9, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 17);
            this.label1.TabIndex = 1094;
            this.label1.Text = "Short by:";
            // 
            // radioButton_ByNumber
            // 
            this.radioButton_ByNumber.AutoSize = true;
            this.radioButton_ByNumber.BackColor = System.Drawing.Color.Transparent;
            this.radioButton_ByNumber.Font = new System.Drawing.Font("Cambria", 11F);
            this.radioButton_ByNumber.ForeColor = System.Drawing.Color.Black;
            this.radioButton_ByNumber.Location = new System.Drawing.Point(235, 42);
            this.radioButton_ByNumber.Name = "radioButton_ByNumber";
            this.radioButton_ByNumber.Size = new System.Drawing.Size(122, 21);
            this.radioButton_ByNumber.TabIndex = 1091;
            this.radioButton_ByNumber.TabStop = true;
            this.radioButton_ByNumber.Text = "Client Numbers";
            this.radioButton_ByNumber.UseVisualStyleBackColor = false;
            // 
            // SearchStudents
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Beige;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(635, 749);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SearchStudents";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SelectClient_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.dgvStudents)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        public System.Windows.Forms.TextBox txtSearch;
        internal System.Windows.Forms.Label label12;
        private System.Windows.Forms.DataGridView dgvStudents;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.RadioButton radioButton_ascending;
        private System.Windows.Forms.RadioButton radioButton_descending;
        internal System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton radioButton_ByNumber;
        private System.Windows.Forms.Label lblCount;
    }
}