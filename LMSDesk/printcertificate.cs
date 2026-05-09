using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LMSDesk
{
    public partial class printcertificate : Form
    {

        private DashboardForm _dashboard;

        string license = AppSession.LicenseName;


        public printcertificate(DashboardForm dashboard)
        {

            InitializeComponent();
            _dashboard = dashboard;
            this.Load += printcertificate_Load;


        }


        // This runs the moment the form opens
        private void printcertificate_Load(object sender, EventArgs e)
        {
            // ComboBox default values (KEEP PRESENT)
            cmbStatus.SelectedIndex = 0;
            cmbMarklist.SelectedIndex = 1;
            cmbCertificate.SelectedIndex = 0;
            dgvP1.DataSource = null;
            dgvP1.Rows.Clear();
            SetupGridTheme();
            SetupLiveCoursesGrid();
            DataGridViewCheckBoxColumn checkColumn = new DataGridViewCheckBoxColumn();
            checkColumn.Name = "Select";
            checkColumn.HeaderText = ""; // Usually blank looks better for the first column
            checkColumn.Width = 40;

            // 2. IMPORTANT: Insert at index 0 to put it BEFORE the ID column
            dgvP1.Columns.Insert(0, checkColumn);

            // 3. Set the default value for new rows
            checkColumn.DefaultCellStyle.NullValue = true;

            // 4. Check all current rows
            CheckAllRows();
        }

        private void CheckAllRows()
        {
            foreach (DataGridViewRow row in dgvP1.Rows)
            {
                if (!row.IsNewRow)
                {
                    // Direct reference to the name "Select" we gave the column
                    row.Cells["Select"].Value = true;
                }
            }
        }



        private void SetupGridTheme()
        {
            // 1. Enable custom styles
            dgvP1.EnableHeadersVisualStyles = false;

            // 2. Define the Header Style
            DataGridViewCellStyle headerStyle = new DataGridViewCellStyle();
            headerStyle.BackColor = Color.FromArgb(70, 130, 180); // Professional Steel Blue
            headerStyle.ForeColor = Color.White;                // White text for contrast
            headerStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            headerStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            // 3. Apply to the grid
            dgvP1.ColumnHeadersDefaultCellStyle = headerStyle;

            // 4. Extra Professional Touch: Selection Color
            dgvP1.DefaultCellStyle.SelectionBackColor = Color.FromArgb(232, 240, 254);
            dgvP1.DefaultCellStyle.SelectionForeColor = Color.Black;

            // 5. Grid Line Color
            dgvP1.GridColor = Color.FromArgb(224, 224, 224);
        }
        private void SetupLiveCoursesGrid()
        {
            dgvP1.Columns.Clear();
            dgvP1.AutoGenerateColumns = false;

            dgvP1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "id",
                HeaderText = "ID",
                DataPropertyName = "id",
                Width = 60
            });

            dgvP1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "student_name",
                HeaderText = "Student",
                DataPropertyName = "student_name",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });

            dgvP1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "course_name",
                HeaderText = "Course",
                DataPropertyName = "course_name",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });

            dgvP1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "teacher_name",
                HeaderText = "Teacher",
                DataPropertyName = "teacher_name",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });

            dgvP1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "CurrentStatus",
                HeaderText = "Status",
                DataPropertyName = "CurrentStatus",
                Width = 120
            });
            dgvP1.Columns.Add(new DataGridViewButtonColumn
            {
                Name = "Certificate",
                HeaderText = "Certificate",
                Text = "Generate",
                UseColumnTextForButtonValue = true
            });
        }
        private async Task LoadprintcertificateWithFilter()
        {
            string student = txtstudent.Text == "Search Student" ? "" : txtstudent.Text.Trim();
            string course = txtCourse.Text == "Search Course" ? "" : txtCourse.Text.Trim();
            string status = cmbStatus.Text == "All" ? "" : cmbStatus.Text;
            string cert = cmbCertificate.Text == "All" ? "" : cmbCertificate.Text;

            string url =
            AppSession.ApiFolder + $"get_live_courses.php" +
                $"?license={Uri.EscapeDataString(license)}" +
                $"&student={Uri.EscapeDataString(student)}" +
                $"&course={Uri.EscapeDataString(course)}" +
                $"&status={Uri.EscapeDataString(status)}" +
                $"&certificate={Uri.EscapeDataString(cert)}";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(url);

                    if (!response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("Server Error: " + response.StatusCode);
                        return;
                    }

                    string json = await response.Content.ReadAsStringAsync();

                    var apiResult = JObject.Parse(json);
                    var list = apiResult["data"].ToObject<List<LiveCourse>>();

                    dgvP1.DataSource = list;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("API Error: " + ex.Message);
                }
            }
        }


        private async void btnSearch_Click_1(object sender, EventArgs e)
        {
            {
                btnSearch.Enabled = false;
                btnSearch.Text = "Searching...";

                await LoadprintcertificateWithFilter();

                btnSearch.Text = "Search";
                btnSearch.Enabled = true;
            }
        }
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
        // private void dgvLiveCourses_CellContentClick(object sender, DataGridViewCellEventArgs e)


        private void btnRefresh_Click_1(object sender, EventArgs e)
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
            dgvP1.DataSource = null;
            dgvP1.Rows.Clear();
        }
     


        private void label1_Click(object sender, EventArgs e)
        {

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

        private void panel1_ClientSizeChanged(object sender, EventArgs e)
        {

        }

        private void dgvP1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // 1. Ignore header clicks
            if (e.RowIndex < 0) return;

            // 2. Use the 'LiveCourse' class to access data (Consistency with your LiveCourses form)
            LiveCourse rowData = dgvP1.Rows[e.RowIndex].DataBoundItem as LiveCourse;

            if (rowData == null) return;

            string columnName = dgvP1.Columns[e.ColumnIndex].Name;

            // 3. Check if the "Certificate" button was clicked
            if (columnName == "Certificate")
            {
                CertificationForm certification = new CertificationForm();

                // Pass the data using rowData properties
                certification.txtStudentName.Text = rowData.student_name ?? "";
                certification.txtCsname.Text = rowData.course_name ?? "";

                // IMPORTANT: Only call ShowDialog() ONCE
                certification.ShowDialog();
            }
        }
        private void cmbStatus_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtstudent_TextChanged(object sender, EventArgs e)
        {

        }

        private void cmbMarklist_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void btnprint_Click(object sender, EventArgs e)
        {

        }
    } }
public class printcertificate
{
    public string id { get; set; } // Change to string if PHP sends it as a string
    public string student_id { get; set; }
    public string student_name { get; set; }
    public string course_name { get; set; }
    public string teacher_name { get; set; }
    public string CurrentStatus { get; set; }
   // public string MarklistGen { get; set; }      // Matches IF() in PHP
   public string IsCertificationGen { get; set; } // Matches IF() in PHP
}