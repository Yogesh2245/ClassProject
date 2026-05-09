namespace LMSDesk
{
    partial class LiveCourses
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
            this.btnRefresh = new System.Windows.Forms.Button();
            this.cmbCertificate = new System.Windows.Forms.ComboBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.cmbMarklist = new System.Windows.Forms.ComboBox();
            this.cmbStatus = new System.Windows.Forms.ComboBox();
            this.txtCourse = new System.Windows.Forms.TextBox();
            this.txtstudent = new System.Windows.Forms.TextBox();
            this.dgvLiveCourses = new System.Windows.Forms.DataGridView();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLiveCourses)).BeginInit();
            this.panel3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnRefresh
            // 
            this.btnRefresh.BackColor = System.Drawing.Color.LightCyan;
            this.btnRefresh.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRefresh.ForeColor = System.Drawing.Color.DarkRed;
            this.btnRefresh.Location = new System.Drawing.Point(807, 5);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(78, 29);
            this.btnRefresh.TabIndex = 5;
            this.btnRefresh.Text = "🔄 Refresh";
            this.btnRefresh.UseVisualStyleBackColor = false;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // cmbCertificate
            // 
            this.cmbCertificate.BackColor = System.Drawing.Color.White;
            this.cmbCertificate.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cmbCertificate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCertificate.FormattingEnabled = true;
            this.cmbCertificate.Items.AddRange(new object[] {
            "All",
            "Generated",
            "Not Generated"});
            this.cmbCertificate.Location = new System.Drawing.Point(3, 3);
            this.cmbCertificate.Name = "cmbCertificate";
            this.cmbCertificate.Size = new System.Drawing.Size(134, 22);
            this.cmbCertificate.TabIndex = 6;
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.Color.LightGray;
            this.btnSearch.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSearch.ForeColor = System.Drawing.Color.Navy;
            this.btnSearch.Location = new System.Drawing.Point(807, 44);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(78, 28);
            this.btnSearch.TabIndex = 4;
            this.btnSearch.Text = "🔍 Search";
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // cmbMarklist
            // 
            this.cmbMarklist.BackColor = System.Drawing.Color.White;
            this.cmbMarklist.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cmbMarklist.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMarklist.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbMarklist.FormattingEnabled = true;
            this.cmbMarklist.Items.AddRange(new object[] {
            "All",
            "Generated",
            "Not Generated"});
            this.cmbMarklist.Location = new System.Drawing.Point(617, 45);
            this.cmbMarklist.Name = "cmbMarklist";
            this.cmbMarklist.Size = new System.Drawing.Size(173, 27);
            this.cmbMarklist.TabIndex = 3;
            // 
            // cmbStatus
            // 
            this.cmbStatus.BackColor = System.Drawing.Color.White;
            this.cmbStatus.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cmbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStatus.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbStatus.FormattingEnabled = true;
            this.cmbStatus.Items.AddRange(new object[] {
            "Active",
            "Completed"});
            this.cmbStatus.Location = new System.Drawing.Point(423, 45);
            this.cmbStatus.Name = "cmbStatus";
            this.cmbStatus.Size = new System.Drawing.Size(172, 27);
            this.cmbStatus.TabIndex = 2;
            // 
            // txtCourse
            // 
            this.txtCourse.BackColor = System.Drawing.Color.White;
            this.txtCourse.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCourse.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCourse.ForeColor = System.Drawing.SystemColors.GrayText;
            this.txtCourse.Location = new System.Drawing.Point(224, 45);
            this.txtCourse.Name = "txtCourse";
            this.txtCourse.Size = new System.Drawing.Size(181, 27);
            this.txtCourse.TabIndex = 1;
            this.txtCourse.Tag = "";
            this.txtCourse.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtCourse.Enter += new System.EventHandler(this.txtCourse_Enter);
            this.txtCourse.Leave += new System.EventHandler(this.txtCourse_Leave);
            // 
            // txtstudent
            // 
            this.txtstudent.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtstudent.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtstudent.ForeColor = System.Drawing.SystemColors.GrayText;
            this.txtstudent.Location = new System.Drawing.Point(10, 45);
            this.txtstudent.Name = "txtstudent";
            this.txtstudent.Size = new System.Drawing.Size(196, 27);
            this.txtstudent.TabIndex = 0;
            this.txtstudent.Tag = "";
            this.txtstudent.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtstudent.TextChanged += new System.EventHandler(this.txtstudent_TextChanged);
            this.txtstudent.Enter += new System.EventHandler(this.txtstudent_Enter);
            this.txtstudent.Leave += new System.EventHandler(this.txtstudent_Leave);
            // 
            // dgvLiveCourses
            // 
            this.dgvLiveCourses.AllowUserToAddRows = false;
            this.dgvLiveCourses.BackgroundColor = System.Drawing.Color.White;
            this.dgvLiveCourses.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLiveCourses.GridColor = System.Drawing.Color.WhiteSmoke;
            this.dgvLiveCourses.Location = new System.Drawing.Point(12, 140);
            this.dgvLiveCourses.Name = "dgvLiveCourses";
            this.dgvLiveCourses.ReadOnly = true;
            this.dgvLiveCourses.RowHeadersVisible = false;
            this.dgvLiveCourses.Size = new System.Drawing.Size(971, 312);
            this.dgvLiveCourses.TabIndex = 3;
            this.dgvLiveCourses.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvLiveCourses_CellContentClick);
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.White;
            this.panel3.Controls.Add(this.label6);
            this.panel3.Controls.Add(this.label4);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Controls.Add(this.btnRefresh);
            this.panel3.Controls.Add(this.btnSearch);
            this.panel3.Controls.Add(this.txtstudent);
            this.panel3.Controls.Add(this.txtCourse);
            this.panel3.Controls.Add(this.cmbMarklist);
            this.panel3.Controls.Add(this.cmbStatus);
            this.panel3.Location = new System.Drawing.Point(13, 44);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(970, 90);
            this.panel3.TabIndex = 8;
            this.panel3.Paint += new System.Windows.Forms.PaintEventHandler(this.panel3_Paint);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(617, 16);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(106, 19);
            this.label6.TabIndex = 12;
            this.label6.Text = "Marklist status";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(423, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(49, 19);
            this.label4.TabIndex = 10;
            this.label4.Text = "Status";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(224, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(143, 19);
            this.label3.TabIndex = 9;
            this.label3.Text = "Search Course Name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(10, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(147, 19);
            this.label2.TabIndex = 8;
            this.label2.Text = "Search Student Name";
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.flowLayoutPanel1);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.dgvLiveCourses);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Location = new System.Drawing.Point(33, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1002, 475);
            this.panel1.TabIndex = 9;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.cmbCertificate);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(1015, 52);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(10, 10);
            this.flowLayoutPanel1.TabIndex = 10;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label1.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(0, 1);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1002, 36);
            this.label1.TabIndex = 9;
            this.label1.Text = "Live Course Enrollment";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LiveCourses
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1169, 585);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "LiveCourses";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "LiveCourses";
            this.Load += new System.EventHandler(this.LiveCourses_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvLiveCourses)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.TextBox txtstudent;
        private System.Windows.Forms.TextBox txtCourse;
        private System.Windows.Forms.ComboBox cmbStatus;
        private System.Windows.Forms.ComboBox cmbMarklist;
        private System.Windows.Forms.ComboBox cmbCertificate;
        private System.Windows.Forms.DataGridView dgvLiveCourses;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
    }
}