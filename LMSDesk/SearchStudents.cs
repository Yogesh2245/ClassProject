using LMSDesk;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data; 
using System.Drawing; 
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
namespace LMSDesk
{
    public partial class SearchStudents : Form
    {
        private readonly string localFile =
            Path.Combine(Application.StartupPath, "students.json");

        private DataTable studentsTable;
        private BindingSource bs;

        public StudentProfile.StudentDropdown SelectedStudent
        { get; private set; }


        public SearchStudents()
        {
            InitializeComponent();
            LoadStudentsFromJson();
            ConfigureGrid();
            StyleGrid();

        }


        // ================= LOAD JSON =================
        // ================= LOAD JSON =================
        private void LoadStudentsFromJson()
        {
            if (!File.Exists(localFile))
            {
                MessageBox.Show("students.json not found!");
                return;
            }

            string json = File.ReadAllText(localFile);

            var parsed =
                JsonConvert.DeserializeObject<StudentProfile.ApiResponse>(json);

            studentsTable = new DataTable();

            // 🔹 Columns exactly as API
            studentsTable.Columns.Add("student_id");
            studentsTable.Columns.Add("full_name");
            studentsTable.Columns.Add("mobile");
            studentsTable.Columns.Add("email");
            studentsTable.Columns.Add("address");
            studentsTable.Columns.Add("student_photo");
            studentsTable.Columns.Add("course_name");


            foreach (var s in parsed.data)
            {
                studentsTable.Rows.Add(
                    s.student_id,
                    s.full_name,
                    s.mobile,
                    s.email,
                    s.address,
                    s.student_photo,
                    s.course_name
                );
            }

            bs = new BindingSource();
            bs.DataSource = studentsTable;
            dgvStudents.DataSource = bs;
        }


        // ================= COLUMN SHOW / HIDE =================
        private void ConfigureGrid()
        {
            // Hide all first
            foreach (DataGridViewColumn col in dgvStudents.Columns)
                col.Visible = false;

            // Show only required columns
            dgvStudents.Columns["student_id"].Visible = true;
            dgvStudents.Columns["full_name"].Visible = true;
            dgvStudents.Columns["mobile"].Visible = true;

            // Headers
            dgvStudents.Columns["student_id"].HeaderText = "ID";
            dgvStudents.Columns["full_name"].HeaderText = "Student Name";
            dgvStudents.Columns["mobile"].HeaderText = "Mobile";



            dgvStudents.Columns["course_name"].Visible = true;
            dgvStudents.Columns["course_name"].HeaderText = "Course";


          
            // Sizing
            dgvStudents.Columns["student_id"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvStudents.Columns["full_name"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvStudents.Columns["mobile"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvStudents.Columns["course_name"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
        }

        // ================= GRID STYLE =================
        private void StyleGrid()
        {
            dgvStudents.EnableHeadersVisualStyles = false;
            dgvStudents.AllowUserToAddRows = false;
            dgvStudents.AllowUserToDeleteRows = false;
            dgvStudents.AllowUserToResizeRows = false;
            dgvStudents.MultiSelect = false;
            dgvStudents.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvStudents.ReadOnly = true;
            dgvStudents.RowHeadersVisible = false;

            dgvStudents.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(70, 130, 180);
            dgvStudents.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvStudents.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            dgvStudents.ColumnHeadersHeight = 45;

            dgvStudents.DefaultCellStyle.Font = new Font("Segoe UI", 11F);
            dgvStudents.DefaultCellStyle.SelectionBackColor = Color.FromArgb(0, 128, 64);
            dgvStudents.DefaultCellStyle.SelectionForeColor = Color.White;

            dgvStudents.RowTemplate.Height = 40;
            dgvStudents.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
        }






        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            SetSelectedRow();
        }
       
       
      

        private void SelectClient_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    SetSelectedRow();
                }
                if (e.KeyCode == Keys.Escape)
                {
                    this.DialogResult = DialogResult.Cancel;
                }
                if (e.KeyCode == Keys.Down)
                {
                    int rowCount = dgvStudents.Rows.Count;
                    int row = dgvStudents.SelectedRows[0].Index;
                    if (row < dgvStudents.Rows.Count - 1)
                    {

                        dgvStudents.Rows[row + 1].Selected = true;
                        //  //= dataGridView1[1, row + 1];

                    }
                    else
                    {
                        dgvStudents.CurrentCell = dgvStudents[1, row];
                    }
                }
                if (e.KeyCode == Keys.Up)
                {
                    int rowCount = dgvStudents.Rows.Count;
                    int row = dgvStudents.SelectedRows[0].Index;
                    if (row != 0)
                    {

                        dgvStudents.Rows[row - 1].Selected = true;
                        //  //= dataGridView1[1, row + 1];
                    }
                    else
                    {
                        dgvStudents.CurrentCell = dgvStudents[1, row];
                    }
                }
            }
            catch (Exception) { }
        }
     
 
      

        private void dataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                // Custom cell painting for modern look
                e.Paint(e.CellBounds, DataGridViewPaintParts.All & ~DataGridViewPaintParts.Border);

                // Draw custom border
                using (Pen pen = new Pen(Color.FromArgb(230, 230, 230), 1))
                {
                    e.Graphics.DrawLine(pen, e.CellBounds.Left, e.CellBounds.Bottom - 1,
                                       e.CellBounds.Right, e.CellBounds.Bottom - 1);
                }

                e.Handled = true;
            }
        }
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            //txtSearch.Focus();
             //SetSelectedRow();
        }
 
 
        // code strt here to datagridview design strat

        // Add this method to your SelectClient class
        private void StyleDataGridView()
        {
            // Basic DataGridView properties
            dgvStudents.EnableHeadersVisualStyles = false;
            dgvStudents.AllowUserToAddRows = false;
            dgvStudents.AllowUserToDeleteRows = false;
            dgvStudents.AllowUserToResizeRows = false;
            dgvStudents.MultiSelect = false;
            dgvStudents.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvStudents.ReadOnly = true;

            // Row styling
            dgvStudents.RowTemplate.Height = 45; // Bigger rows
            dgvStudents.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;

            // Font styling
            dgvStudents.Font = new Font("Segoe UI", 11F, FontStyle.Regular);
            dgvStudents.DefaultCellStyle.Font = new Font("Segoe UI", 11F, FontStyle.Regular);

            // Colors and appearance
            dgvStudents.BackgroundColor = Color.FromArgb(250, 250, 250);
            dgvStudents.GridColor = Color.FromArgb(230, 230, 230);
            dgvStudents.BorderStyle = BorderStyle.None;

            // Header styling
            dgvStudents.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(70, 130, 180);
            dgvStudents.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvStudents.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            dgvStudents.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvStudents.ColumnHeadersHeight = 50;
            dgvStudents.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;

            // Cell styling
            dgvStudents.DefaultCellStyle.BackColor = Color.White;
            dgvStudents.DefaultCellStyle.ForeColor = Color.FromArgb(64, 64, 64);
            dgvStudents.DefaultCellStyle.SelectionBackColor = Color.FromArgb(70, 130, 180);
            dgvStudents.DefaultCellStyle.SelectionForeColor = Color.White;
            dgvStudents.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvStudents.DefaultCellStyle.Padding = new Padding(10, 5, 10, 5);

            // Alternating row colors for better readability
            dgvStudents.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 248, 248);
            dgvStudents.AlternatingRowsDefaultCellStyle.ForeColor = Color.FromArgb(64, 64, 64);

            // Remove cell borders for cleaner look
            dgvStudents.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvStudents.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgvStudents.RowHeadersVisible = false;

            // Hover effect
            dgvStudents.DefaultCellStyle.SelectionBackColor = Color.FromArgb(100, 150, 200);
        }
 
      
        
        private void dataGridView1_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            // Add subtle shadow effect to rows
            if (e.RowIndex >= 0)
            {
                Rectangle rowBounds = new Rectangle(0, e.RowBounds.Top, dgvStudents.Width, e.RowBounds.Height);
                using (SolidBrush brush = new SolidBrush(Color.FromArgb(10, 0, 0, 0)))
                {
                    e.Graphics.FillRectangle(brush, new Rectangle(rowBounds.X + 2, rowBounds.Bottom - 1, rowBounds.Width - 4, 2));
                }
            }
        }

        // Method to apply modern column styling
        private void SetModernColumnStyling()
        {
            if (dgvStudents.Columns.Count > 0)
            {
                // Client Name column - make it prominent
                if (dgvStudents.Columns.Count > 1)
                {
                    dgvStudents.Columns[1].DefaultCellStyle.Font = new Font("Segoe UI", 12F, FontStyle.Bold);

                    dgvStudents.Columns[1].DefaultCellStyle.ForeColor = Color.FromArgb(40, 40, 40);
                }

                // Contact number column - different color
                if (dgvStudents.Columns.Count > 3)
                {
                    dgvStudents.Columns[3].DefaultCellStyle.ForeColor = Color.FromArgb(70, 130, 180);
                    dgvStudents.Columns[3].DefaultCellStyle.Font = new Font("Segoe UI", 11F, FontStyle.Regular);
                }

                // Address column - lighter text
                if (dgvStudents.Columns.Count > 2)
                {
                    dgvStudents.Columns[2].DefaultCellStyle.ForeColor = Color.FromArgb(100, 100, 100);
                }
            }
        }


        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string search =
                        txtSearch.Text.Trim().Replace("'", "''");

            if (string.IsNullOrEmpty(search))
                bs.RemoveFilter();
            else
                bs.Filter =
                 $"full_name LIKE '%{search}%' OR mobile LIKE '%{search}%'";
        }
  

        private void SetSelectedRow()
        {
            /*   if (dgvStudents.CurrentRow == null) return;

               SelectedStudent =
                 new AssignedCourseForm.StudentDropdown
                 {
                     student_id =
                       dgvStudents.CurrentRow.Cells["student_id"].Value.ToString(),

                     first_name = "",
                     last_name = "",
                     mobile =
                       dgvStudents.CurrentRow.Cells["mobile"].Value.ToString(),

                     email =
                       dgvStudents.CurrentRow.Cells["email"].Value.ToString(),

                     address =
                       dgvStudents.CurrentRow.Cells["address"].Value.ToString(),

                     student_photo =
                       dgvStudents.CurrentRow.Cells["student_photo"].Value.ToString()
                 };

               SelectedStudent.first_name =
                 dgvStudents.CurrentRow.Cells["full_name"]
                 .Value.ToString();

               this.DialogResult = DialogResult.OK;
               this.Close();*/


            if (dgvStudents.SelectedRows.Count == 0)
                return;

            DataGridViewRow row =
                dgvStudents.SelectedRows[0];

            SelectedStudent =
                new StudentProfile.StudentDropdown
                {
                    student_id = row.Cells["student_id"].Value.ToString(),
                    mobile = row.Cells["mobile"].Value.ToString(),
                    email = row.Cells["email"].Value.ToString(),
                    address = row.Cells["address"].Value.ToString(),
                    student_photo = row.Cells["student_photo"].Value.ToString(),
                    first_name = row.Cells["full_name"].Value.ToString(),
                    last_name = "",

                    // ✅ NEW
                    course_name = row.Cells["course_name"].Value.ToString()
                };

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                // ================= ENTER =================
                if (e.KeyCode == Keys.Enter)
                {
                    SetSelectedRow();
                    e.Handled = true;
                }

                // ================= ESC =================
                if (e.KeyCode == Keys.Escape)
                {
                    this.DialogResult = DialogResult.Cancel;
                }

                // ================= DOWN =================
                if (e.KeyCode == Keys.Down)
                {
                    if (dgvStudents.Rows.Count == 0) return;

                    int row = 0;

                    if (dgvStudents.SelectedRows.Count > 0)
                        row = dgvStudents.SelectedRows[0].Index;

                    if (row < dgvStudents.Rows.Count - 1)
                        row++;

                    dgvStudents.ClearSelection();
                    dgvStudents.Rows[row].Selected = true;
                    dgvStudents.CurrentCell =
                        dgvStudents.Rows[row].Cells[0];
                }

                // ================= UP =================
                if (e.KeyCode == Keys.Up)
                {
                    if (dgvStudents.Rows.Count == 0) return;

                    int row = 0;

                    if (dgvStudents.SelectedRows.Count > 0)
                        row = dgvStudents.SelectedRows[0].Index;

                    if (row > 0)
                        row--;

                    dgvStudents.ClearSelection();
                    dgvStudents.Rows[row].Selected = true;
                    dgvStudents.CurrentCell =
                        dgvStudents.Rows[row].Cells[0];
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Key Navigation Error: " + ex.Message);
            }
        }
    }
}

 