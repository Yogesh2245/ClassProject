namespace LMSDesk
{
    partial class CourseList
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
            this.dataGridReview2 = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.txtcourse = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridReview2)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridReview2
            // 
            this.dataGridReview2.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridReview2.BackgroundColor = System.Drawing.Color.White;
            this.dataGridReview2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridReview2.Location = new System.Drawing.Point(12, 43);
            this.dataGridReview2.Name = "dataGridReview2";
            this.dataGridReview2.RowHeadersVisible = false;
            this.dataGridReview2.Size = new System.Drawing.Size(1067, 468);
            this.dataGridReview2.TabIndex = 0;
            this.dataGridReview2.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.CourseList_Load_CellContentClick);
            // 
            // label1
            // 
            this.label1.Cursor = System.Windows.Forms.Cursors.No;
            this.label1.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(448, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(136, 31);
            this.label1.TabIndex = 1;
            this.label1.Text = "COURSE LIST";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtcourse
            // 
            this.txtcourse.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtcourse.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtcourse.ForeColor = System.Drawing.Color.Black;
            this.txtcourse.Location = new System.Drawing.Point(12, 11);
            this.txtcourse.Name = "txtcourse";
            this.txtcourse.Size = new System.Drawing.Size(211, 27);
            this.txtcourse.TabIndex = 2;
            this.txtcourse.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtcourse.TextChanged += new System.EventHandler(this.txtcourse_TextChanged);
            // 
            // CourseList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1102, 523);
            this.Controls.Add(this.txtcourse);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataGridReview2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "CourseList";
            this.Text = "CourseList";
            this.Load += new System.EventHandler(this.CourseList_Load_1);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridReview2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridReview2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtcourse;
    }
}