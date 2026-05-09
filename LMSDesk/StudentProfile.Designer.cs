namespace LMSDesk
{
    partial class StudentProfile
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
            this.cmbSelectStudent = new System.Windows.Forms.ComboBox();
            this.cmbCourseName = new System.Windows.Forms.ComboBox();
            this.dgvAssignedCourse = new System.Windows.Forms.DataGridView();
            this.btnAssignCourse = new System.Windows.Forms.Button();
            this.dtpdateofstart = new System.Windows.Forms.DateTimePicker();
            this.lblStId = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpdateofend = new System.Windows.Forms.DateTimePicker();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.pbStudentPhoto = new System.Windows.Forms.PictureBox();
            this.label8 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtAddress = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtMobile = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAssignedCourse)).BeginInit();
            this.panel1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbStudentPhoto)).BeginInit();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmbSelectStudent
            // 
            this.cmbSelectStudent.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSelectStudent.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbSelectStudent.FormattingEnabled = true;
            this.cmbSelectStudent.Location = new System.Drawing.Point(26, 26);
            this.cmbSelectStudent.Name = "cmbSelectStudent";
            this.cmbSelectStudent.Size = new System.Drawing.Size(288, 31);
            this.cmbSelectStudent.TabIndex = 0;
            this.cmbSelectStudent.SelectedIndexChanged += new System.EventHandler(this.cmbSelectStudent_SelectedIndexChanged);
            // 
            // cmbCourseName
            // 
            this.cmbCourseName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCourseName.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbCourseName.FormattingEnabled = true;
            this.cmbCourseName.Location = new System.Drawing.Point(20, 27);
            this.cmbCourseName.Name = "cmbCourseName";
            this.cmbCourseName.Size = new System.Drawing.Size(288, 27);
            this.cmbCourseName.TabIndex = 0;
            // 
            // dgvAssignedCourse
            // 
            this.dgvAssignedCourse.AllowUserToAddRows = false;
            this.dgvAssignedCourse.AllowUserToDeleteRows = false;
            this.dgvAssignedCourse.AllowUserToResizeRows = false;
            this.dgvAssignedCourse.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvAssignedCourse.BackgroundColor = System.Drawing.Color.White;
            this.dgvAssignedCourse.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAssignedCourse.Location = new System.Drawing.Point(11, 95);
            this.dgvAssignedCourse.MultiSelect = false;
            this.dgvAssignedCourse.Name = "dgvAssignedCourse";
            this.dgvAssignedCourse.RowHeadersVisible = false;
            this.dgvAssignedCourse.RowTemplate.Height = 32;
            this.dgvAssignedCourse.Size = new System.Drawing.Size(982, 161);
            this.dgvAssignedCourse.TabIndex = 4;
            this.dgvAssignedCourse.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvAssignedCourse_CellContentClick);
            // 
            // btnAssignCourse
            // 
            this.btnAssignCourse.BackColor = System.Drawing.Color.SteelBlue;
            this.btnAssignCourse.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAssignCourse.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAssignCourse.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAssignCourse.ForeColor = System.Drawing.Color.White;
            this.btnAssignCourse.Location = new System.Drawing.Point(896, 23);
            this.btnAssignCourse.Name = "btnAssignCourse";
            this.btnAssignCourse.Size = new System.Drawing.Size(68, 31);
            this.btnAssignCourse.TabIndex = 2;
            this.btnAssignCourse.Text = "Add";
            this.btnAssignCourse.UseVisualStyleBackColor = false;
            this.btnAssignCourse.Click += new System.EventHandler(this.btnAssignCourse_Click);
            // 
            // dtpdateofstart
            // 
            this.dtpdateofstart.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpdateofstart.Location = new System.Drawing.Point(425, 27);
            this.dtpdateofstart.Name = "dtpdateofstart";
            this.dtpdateofstart.Size = new System.Drawing.Size(169, 27);
            this.dtpdateofstart.TabIndex = 1;
            // 
            // lblStId
            // 
            this.lblStId.AutoSize = true;
            this.lblStId.Location = new System.Drawing.Point(40, 32);
            this.lblStId.Name = "lblStId";
            this.lblStId.Size = new System.Drawing.Size(13, 13);
            this.lblStId.TabIndex = 1;
            this.lblStId.Text = "0";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.btnAssignCourse);
            this.panel1.Controls.Add(this.dtpdateofstart);
            this.panel1.Controls.Add(this.cmbCourseName);
            this.panel1.Location = new System.Drawing.Point(11, 9);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(982, 64);
            this.panel1.TabIndex = 9;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(422, 6);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(109, 18);
            this.label3.TabIndex = 13;
            this.label3.Text = "Enrollment Date";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(17, 6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(132, 18);
            this.label2.TabIndex = 12;
            this.label2.Text = "Select Course Name";
            // 
            // dtpdateofend
            // 
            this.dtpdateofend.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpdateofend.Location = new System.Drawing.Point(3, 3);
            this.dtpdateofend.Name = "dtpdateofend";
            this.dtpdateofend.Size = new System.Drawing.Size(202, 31);
            this.dtpdateofend.TabIndex = 7;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.dtpdateofend);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(1099, 26);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(10, 10);
            this.flowLayoutPanel1.TabIndex = 10;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.Controls.Add(this.panel6);
            this.panel2.Controls.Add(this.panel5);
            this.panel2.Controls.Add(this.txtSearch);
            this.panel2.Controls.Add(this.label10);
            this.panel2.Controls.Add(this.panel4);
            this.panel2.Controls.Add(this.label9);
            this.panel2.Controls.Add(this.pbStudentPhoto);
            this.panel2.Controls.Add(this.label8);
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.flowLayoutPanel1);
            this.panel2.Location = new System.Drawing.Point(12, 5);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1038, 571);
            this.panel2.TabIndex = 0;
            this.panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.panel2_Paint);
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.lblStId);
            this.panel6.Location = new System.Drawing.Point(345, 63);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(10, 10);
            this.panel6.TabIndex = 1091;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.cmbSelectStudent);
            this.panel5.Location = new System.Drawing.Point(123, 202);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(10, 10);
            this.panel5.TabIndex = 1090;
            // 
            // txtSearch
            // 
            this.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSearch.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtSearch.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtSearch.Font = new System.Drawing.Font("Cambria", 16F, System.Drawing.FontStyle.Bold);
            this.txtSearch.ForeColor = System.Drawing.Color.Red;
            this.txtSearch.Location = new System.Drawing.Point(17, 63);
            this.txtSearch.MaxLength = 60;
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.ReadOnly = true;
            this.txtSearch.Size = new System.Drawing.Size(306, 33);
            this.txtSearch.TabIndex = 1088;
            this.txtSearch.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSearch_KeyPress);
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.label10.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.White;
            this.label10.Location = new System.Drawing.Point(0, 1);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(1035, 35);
            this.label10.TabIndex = 18;
            this.label10.Text = "Student Profile";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel4
            // 
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.panel1);
            this.panel4.Controls.Add(this.label4);
            this.panel4.Controls.Add(this.dgvAssignedCourse);
            this.panel4.Location = new System.Drawing.Point(17, 290);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1007, 261);
            this.panel4.TabIndex = 17;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.Linen;
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label4.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(11, 73);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(982, 22);
            this.label4.TabIndex = 13;
            this.label4.Text = "Course List";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(871, 42);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(154, 18);
            this.label9.TabIndex = 16;
            this.label9.Text = "Selected Student Photo";
            // 
            // pbStudentPhoto
            // 
            this.pbStudentPhoto.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbStudentPhoto.Location = new System.Drawing.Point(877, 63);
            this.pbStudentPhoto.Name = "pbStudentPhoto";
            this.pbStudentPhoto.Size = new System.Drawing.Size(117, 115);
            this.pbStudentPhoto.TabIndex = 15;
            this.pbStudentPhoto.TabStop = false;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(517, 42);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(191, 18);
            this.label8.TabIndex = 14;
            this.label8.Text = "Selected Student Information";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.txtEmail);
            this.panel3.Controls.Add(this.label5);
            this.panel3.Controls.Add(this.txtAddress);
            this.panel3.Controls.Add(this.label6);
            this.panel3.Controls.Add(this.txtMobile);
            this.panel3.Controls.Add(this.label7);
            this.panel3.Location = new System.Drawing.Point(520, 63);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(332, 115);
            this.panel3.TabIndex = 12;
            this.panel3.Paint += new System.Windows.Forms.PaintEventHandler(this.panel3_Paint);
            // 
            // txtEmail
            // 
            this.txtEmail.BackColor = System.Drawing.SystemColors.Window;
            this.txtEmail.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtEmail.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEmail.Location = new System.Drawing.Point(159, 47);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.ReadOnly = true;
            this.txtEmail.Size = new System.Drawing.Size(155, 26);
            this.txtEmail.TabIndex = 19;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(15, 17);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(104, 18);
            this.label5.TabIndex = 14;
            this.label5.Text = "Mobile number";
            // 
            // txtAddress
            // 
            this.txtAddress.BackColor = System.Drawing.SystemColors.Window;
            this.txtAddress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAddress.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAddress.Location = new System.Drawing.Point(159, 80);
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.ReadOnly = true;
            this.txtAddress.Size = new System.Drawing.Size(155, 26);
            this.txtAddress.TabIndex = 18;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(15, 82);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(58, 18);
            this.label6.TabIndex = 15;
            this.label6.Text = "Address";
            // 
            // txtMobile
            // 
            this.txtMobile.BackColor = System.Drawing.SystemColors.Window;
            this.txtMobile.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMobile.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMobile.Location = new System.Drawing.Point(159, 15);
            this.txtMobile.Name = "txtMobile";
            this.txtMobile.ReadOnly = true;
            this.txtMobile.Size = new System.Drawing.Size(155, 26);
            this.txtMobile.TabIndex = 17;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(15, 49);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(61, 18);
            this.label7.TabIndex = 16;
            this.label7.Text = "Email_id";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(14, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(138, 18);
            this.label1.TabIndex = 11;
            this.label1.Text = "Select Student Name";
            // 
            // StudentProfile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1088, 597);
            this.Controls.Add(this.panel2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "StudentProfile";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "StudentProfile";
            this.Load += new System.EventHandler(this.AssignedCourseForm_Load_1);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAssignedCourse)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbStudentPhoto)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbSelectStudent;
        private System.Windows.Forms.ComboBox cmbCourseName;
        private System.Windows.Forms.DataGridView dgvAssignedCourse;
        private System.Windows.Forms.Button btnAssignCourse;
        private System.Windows.Forms.DateTimePicker dtpdateofstart;
        private System.Windows.Forms.Label lblStId;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DateTimePicker dtpdateofend;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtMobile;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.TextBox txtAddress;
        private System.Windows.Forms.PictureBox pbStudentPhoto;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label10;
        public System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel6;
    }
}