using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LMSDesk
{
    public partial class LiveCourses : Form
    {
        private DashboardForm _dashboard;

        string license = AppSession.LicenseName;
     public LiveCourses(DashboardForm dashboard)
        {
            InitializeComponent();
            _dashboard = dashboard;
            this.Load += LiveCourses_Load;
        }
 
        private async void LiveCourses_Load(object sender, EventArgs e)
        {
         
            cmbStatus.SelectedIndex = 0; btnSearch.PerformClick();
            SetupLiveCoursesGrid();

            // ComboBox default values (KEEP PRESENT)
            cmbStatus.SelectedIndex = 0;
            cmbMarklist.SelectedIndex = 0;
            cmbCertificate.SelectedIndex = 0;
            dgvLiveCourses.DataSource = null;
            dgvLiveCourses.Rows.Clear();
            SetupGridTheme();
            panel1.BackColor = Color.FromArgb(32, 64, 128);
            label1.ForeColor = Color.White;
            label1.Font = new Font("Segoe UI", 14, FontStyle.Bold);

            StyleTextBox(txtstudent);
            StyleTextBox(txtCourse);

          //  StyleComboBox(cmbStatus);
           // StyleComboBox(cmbMarklist);

            StyleButton(btnSearch, Color.FromArgb(32, 64, 128));
            //StyleButton(btnRefresh, Color.FromArgb(64,64,64));
        }

        private void StyleTextBox(TextBox txt)
        {
            txt.BorderStyle = BorderStyle.FixedSingle;
            txt.Font = new Font("Segoe UI", 10);
            txt.BackColor = Color.White;
        }

        private void StyleComboBox(ComboBox cmb)
        {
            cmb.Font = new Font("Segoe UI", 10);
            cmb.FlatStyle = FlatStyle.Flat;
        }

        private void StyleButton(Button btn, Color bgColor)
        {
            btn.BackColor = bgColor;
            btn.ForeColor = Color.White;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            btn.Cursor = Cursors.Hand;
        }

        private void SetupGridTheme()
        {
            // 1. Enable custom styles
            dgvLiveCourses.BorderStyle = BorderStyle.None;
            dgvLiveCourses.BackgroundColor = Color.White;
            dgvLiveCourses.EnableHeadersVisualStyles = false;

            // 2. Define the Header Style
            DataGridViewCellStyle headerStyle = new DataGridViewCellStyle();
            headerStyle.BackColor = Color.FromArgb(70, 130, 180); // Professional Steel Blue
            headerStyle.ForeColor = Color.White;                // White text for contrast
            headerStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            headerStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            // 3. Apply to the grid
            dgvLiveCourses.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(32, 64, 128);
            dgvLiveCourses.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvLiveCourses.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgvLiveCourses.ColumnHeadersDefaultCellStyle = headerStyle;
            dgvLiveCourses.GridColor = Color.FromArgb(224, 224, 224);
            // 4. Extra Professional Touch: Selection Color
            //dgvLiveCourses.DefaultCellStyle.SelectionBackColor = Color.FromArgb(232, 240, 254);
            // dgvLiveCourses.DefaultCellStyle.SelectionForeColor = Color.Black;

            dgvLiveCourses.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            dgvLiveCourses.DefaultCellStyle.SelectionBackColor = Color.FromArgb(220, 230, 250);
            dgvLiveCourses.DefaultCellStyle.SelectionForeColor = Color.Black;

            // 5. Grid Line Color
            //  dgvLiveCourses.GridColor = Color.FromArgb(224, 224, 224);
            dgvLiveCourses.RowHeadersVisible = false;
            dgvLiveCourses.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 247, 250);
        }
        private void SetupLiveCoursesGrid()
        {
            dgvLiveCourses.Columns.Clear();
            dgvLiveCourses.AutoGenerateColumns = false;

            dgvLiveCourses.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "id",
                HeaderText = "ID",
                DataPropertyName = "id",
                Width = 60
            });

            dgvLiveCourses.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "student_name",
                HeaderText = "Student",
                DataPropertyName = "student_name",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });

            dgvLiveCourses.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "course_name",
                HeaderText = "Course",
                DataPropertyName = "course_name",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });

            dgvLiveCourses.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "teacher_name",
                HeaderText = "Teacher",
                DataPropertyName = "teacher_name",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });

            dgvLiveCourses.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "CurrentStatus",
                HeaderText = "Status",
                DataPropertyName = "CurrentStatus",
                Width = 120
            });

            dgvLiveCourses.Columns.Add(new DataGridViewButtonColumn
            {
                Name = "Marklist",
                HeaderText = "Marklist",
                Text = "Make MarkList",
                UseColumnTextForButtonValue = true,
                Width = 140

            }); 
        
        }

        private async Task LoadLiveCoursesWithFilter()
        {
            string student = txtstudent.Text == "Search Student" ? "" : txtstudent.Text.Trim();
            string course = txtCourse.Text == "Search Course" ? "" : txtCourse.Text.Trim();
            string status = cmbStatus.Text == "All" ? "" : cmbStatus.Text;
            string marklist = cmbMarklist.Text == "All" ? "" : cmbMarklist.Text;
            string cert = cmbCertificate.Text == "All" ? "" : cmbCertificate.Text;
            string license = AppSession.LicenseName;

   


            string url =
                  AppSession.ApiFolder +  $"get_live_courses.php?license={license}" +
                    $"&student={student}" +
                    $"&course={course}" +
                    $"&status={status}" +
                    $"&marklist={marklist}" +
                    $"&certificate={cert}";


            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetStringAsync(url);
                var json = JObject.Parse(response);
                var list = json["data"].ToObject<List<LiveCourse>>();

                dgvLiveCourses.DataSource = list;
            }
        }

        // =========================
        // SEARCH BUTTON (ONLY TRIGGER)
        // =========================
        private async void btnSearch_Click(object sender, EventArgs e)
        {
            btnSearch.Enabled = false;
            btnSearch.Text = "Searching...";

            await LoadLiveCoursesWithFilter();

            btnSearch.Text = "Search";
            btnSearch.Enabled = true;
        }

        // =========================
        // PLACEHOLDER LOGIC
        // =========================
        private void txtstudent_Enter(object sender, EventArgs e)
        {
            if (txtstudent.Text == "Search Student")
            {
                txtstudent.Text = "";
                txtstudent.ForeColor = Color.Black;
            }
        }

        private void txtstudent_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtstudent.Text))
            {
                txtstudent.Text = "Search Student";
                txtstudent.ForeColor = Color.Gray;
            }
        }

        private void txtCourse_Enter(object sender, EventArgs e)
        {
            if (txtCourse.Text == "Search Course")
            {
                txtCourse.Text = "";
                txtCourse.ForeColor = Color.Black;
            }
        }

        private void txtCourse_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCourse.Text))
            {
                txtCourse.Text = "Search Course";
                txtCourse.ForeColor = Color.Gray;
            }
        }

        // =========================
        // GRID BUTTON CLICK
        // =========================
        private async void dgvLiveCourses_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            LiveCourse rowData =
                dgvLiveCourses.Rows[e.RowIndex].DataBoundItem as LiveCourse;

            if (rowData == null) return;

            string columnName = dgvLiveCourses.Columns[e.ColumnIndex].Name;

            // ===== MARKLIST CLICK =====
            if (columnName == "Marklist")
            {
                GenMarkList genMarkList = new GenMarkList();
                genMarkList.StudentId = rowData.student_id;
                genMarkList.lblID.Text = rowData.student_id.ToString();
                genMarkList.txtStName.Text = rowData.student_name ?? "";
                genMarkList.txtCsName.Text = rowData.course_name ?? "";
                //genMarkList.txtSubject.Text = rowData.course_name ?? "";
                genMarkList.txtSubject.Text = "Theory";
                genMarkList.lblCrAsId.Text = dgvLiveCourses.CurrentRow.Cells[0].Value.ToString();
               // genMarkList.


                if (genMarkList.ShowDialog() == DialogResult.OK) 
                {

                    await LoadLiveCoursesWithFilter();

                }
            }
 
        }
  private void btnRefresh_Click(object sender, EventArgs e)
        {
            txtstudent.Text = "Search Student";
            txtstudent.ForeColor = Color.Gray;

            txtCourse.Text = "Search Course";
            txtCourse.ForeColor = Color.Gray;

            // Reset filters
            cmbStatus.SelectedIndex = 0;
            cmbMarklist.SelectedIndex = 0;
            cmbCertificate.SelectedIndex = 0;

            // Clear grid (make it empty)
            dgvLiveCourses.DataSource = null;
            dgvLiveCourses.Rows.Clear();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

            {
                // 1. Settings for a clean, professional look
                int borderRadius = 30;
                Color borderColor = Color.FromArgb(180, 180, 180); // Slightly darker for visibility
                Color fillColor = Color.White;
                float borderThickness = 2f;

                // 2. High-quality rendering
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                // 3. Shrink the rectangle slightly so the border is NOT cut off by the region
                RectangleF rect = new RectangleF(borderThickness / 2, borderThickness / 2,
                                                 panel1.Width - borderThickness,
                                                 panel1.Height - borderThickness);

                using (System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath())
                {
                    // Define the curves
                    path.AddArc(rect.X, rect.Y, borderRadius, borderRadius, 180, 90);
                    path.AddArc(rect.Right - borderRadius, rect.Y, borderRadius, borderRadius, 270, 90);
                    path.AddArc(rect.Right - borderRadius, rect.Bottom - borderRadius, borderRadius, borderRadius, 0, 90);
                    path.AddArc(rect.X, rect.Bottom - borderRadius, borderRadius, borderRadius, 90, 90);
                    path.CloseFigure();

                    // 4. Fill the background
                    using (SolidBrush brush = new SolidBrush(fillColor))
                    {
                        e.Graphics.FillPath(brush, path);
                    }

                    // 5. Draw the border
                    using (Pen pen = new Pen(borderColor, borderThickness))
                    {
                        e.Graphics.DrawPath(pen, path);
                    }

                    // 6. Set the region so child controls (like your blue header) 
                    // follow the panel's curves.
                    panel1.Region = new Region(path);
                }
            }
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {
            {
                // 1. Settings for a clean, professional look
                int borderRadius = 30;
                Color borderColor = Color.FromArgb(180, 180, 180); // Slightly darker for visibility
                Color fillColor = Color.White;
                float borderThickness = 2f;

                // 2. High-quality rendering
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                // 3. Shrink the rectangle slightly so the border is NOT cut off by the region
                RectangleF rect = new RectangleF(borderThickness / 2, borderThickness / 2,
                                                 panel3.Width - borderThickness,
                                                 panel3.Height - borderThickness);

                using (System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath())
                {
                    // Define the curves
                    path.AddArc(rect.X, rect.Y, borderRadius, borderRadius, 180, 90);
                    path.AddArc(rect.Right - borderRadius, rect.Y, borderRadius, borderRadius, 270, 90);
                    path.AddArc(rect.Right - borderRadius, rect.Bottom - borderRadius, borderRadius, borderRadius, 0, 90);
                    path.AddArc(rect.X, rect.Bottom - borderRadius, borderRadius, borderRadius, 90, 90);
                    path.CloseFigure();

                    // 4. Fill the background
                    using (SolidBrush brush = new SolidBrush(fillColor))
                    {
                        e.Graphics.FillPath(brush, path);
                    }

                    // 5. Draw the border
                    using (Pen pen = new Pen(borderColor, borderThickness))
                    {
                        e.Graphics.DrawPath(pen, path);
                    }

                    // 6. Set the region so child controls (like your blue header) 
                    // follow the panel's curves.
                    panel3.Region = new Region(path);
                }
            }
        }

        private void txtstudent_TextChanged(object sender, EventArgs e)
        {

        }
    }

    // =========================
    // MODEL
    // =========================
    public class LiveCourse
    {
        public int id { get; set; }
        public int student_id { get; set; }
        public int course_id { get; set; }
        public string student_name { get; set; }
        public string course_name { get; set; }
        public DateTime start_date { get; set; }
        public DateTime end_date { get; set; }
        public string teacher_name { get; set; }
        public int TotalFees { get; set; }
        public string PaymentRecurringType { get; set; }
        public string MarklistGen { get; set; }
    //    public string IsCertificationGen { get; set; }
        public string CurrentStatus { get; set; }
    }
}
