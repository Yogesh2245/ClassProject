using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LMSDesk
{
    public partial class FacultyList : Form
    {
        private List<Faculty> FacultyRecords = new List<Faculty>();
        string license = AppSession.LicenseName;
        public FacultyList()
        {
            InitializeComponent();
            this.Load += Faculty_Load;
        }

        private async void Faculty_Load(object sender, EventArgs e)
        {
            await LoadFaculty();
        }

        private async Task LoadFaculty()
        {
            try
            {
                string apiUrl = AppSession.ApiFolder + "getFaculty.php?license=" +license;

                using (HttpClient client = new HttpClient())
                {
                    client.Timeout = TimeSpan.FromSeconds(30);

                    HttpResponseMessage response = await client.GetAsync(apiUrl);

                    if (!response.IsSuccessStatusCode)
                    {

                        MessageBox.Show($"API Error: {(int)response.StatusCode}");
                        return;
                    }

                    string json = await response.Content.ReadAsStringAsync();

                    FacultyApiResponse apiResponse =
                        JsonConvert.DeserializeObject<FacultyApiResponse>(json);

                    if (apiResponse != null && apiResponse.status)
                    {
                        dataGridView1.AutoGenerateColumns = true;
                        dataGridView1.DataSource = apiResponse.data;

                        SetFacultyGridHeaders();
                        dataGridView1.ClearSelection();
                    }
                    else
                    {
                        MessageBox.Show("No faculty found");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error loading faculty:\n" + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        // ✅ SAME STYLE AS STUDENT
        private void SetFacultyGridHeaders()

        {

            // 1. DATA PROTECTION (Read-Only)
            dataGridView1.ReadOnly = true;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.BorderStyle = BorderStyle.None;
            dataGridView1.BackgroundColor = Color.White;

            // 2. THE FIX: PREVENT COMPRESSED HEADERS
            // 'AllCells' ensures columns are wide enough for the full Header text.
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

            // Ensure scrollbars appear so you can scroll right to see all data
            dataGridView1.ScrollBars = ScrollBars.Both;
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(32, 64, 128);
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);

            dataGridView1.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            dataGridView1.DefaultCellStyle.SelectionBackColor = Color.FromArgb(220, 230, 250);
            dataGridView1.DefaultCellStyle.SelectionForeColor = Color.Black;

            dataGridView1.RowHeadersVisible = false;
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 247, 250);
            // 3. HEADER DESIGN & STYLING
            //dataGridView1.EnableHeadersVisualStyles = false;
            //DataGridViewCellStyle headerStyle = new DataGridViewCellStyle();
            //  headerStyle.BackColor = System.Drawing.Color.FromArgb(45, 45, 45); // Professional Dark Gray
            // headerStyle.ForeColor = System.Drawing.Color.White;
            // headerStyle.Font = new System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Bold);
            // headerStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            //  dataGridView1.ColumnHeadersDefaultCellStyle = headerStyle;
            // dataGridView1.ColumnHeadersHeight = 45;

            SetHeader("FacultyId", "Faculty ID");
            SetHeader("teacher_name", "Teacher Name");
            SetHeader("gender", "Gender");
            SetHeader("contact", "Contact Number");
            SetHeader("email", "Email Address");
            SetHeader("address", "Address");
            SetHeader("marital_status", "Marital Status");
            SetHeader("qualification", "Qualification");
            SetHeader("classes_can_teach", "Classes Can Teach");
            SetHeader("preferable_teaching_area", "Preferred Teaching Area");
            SetHeader("subjects_can_teach", "Subjects Can Teach");
            SetHeader("experience_years", "Experience (Years)");
            SetHeader("find_about_us", "Find About Us");
            SetHeader("date_of_joining", "Date of Joining");

            // dataGridView1.AutoSizeColumnsMode =
            // DataGridViewAutoSizeColumnsMode.Fill;
            // Hide faculty_id column

        }
        private void txtfacultysearch_TextChanged(object sender, EventArgs e)
        {
            string keyword = txtfacultysearch.Text.Trim().ToLower();

            if (string.IsNullOrEmpty(keyword))
            {
                dataGridView1.DataSource = null;
                dataGridView1.DataSource = FacultyRecords;
                SetFacultyGridHeaders();
                return;
            }

            var filtered = FacultyRecords.FindAll(s =>
                (!string.IsNullOrEmpty(s.teacher_name) && s.teacher_name.ToLower().Contains(keyword))  );

            dataGridView1.DataSource = null;
            dataGridView1.DataSource = filtered;
            SetFacultyGridHeaders();
        }


        // ✅ HELPER METHOD (LIKE STUDENT)
        private void SetHeader(string columnName, string headerText)
        {
            /*if (dataGridView1.Columns.Contains(columnName))
            {
                dataGridView1.Columns[columnName].HeaderText = headerText;
            }*/
        }
        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }


        // 🔹 API response model
        public class FacultyApiResponse
        {
            public bool status { get; set; }
            public List<Faculty> data { get; set; }
        }

        // 🔹 Faculty model (MATCHES API + DB)
        public class Faculty
        {
            public int FacultyId { get; set; }
            public string teacher_name { get; set; }
            public string gender { get; set; }
            public string contact { get; set; }
            public string email { get; set; }
            public string address { get; set; }
            public string marital_status { get; set; }
            public string qualification { get; set; }
            public string classes_can_teach { get; set; }
            public string preferable_teaching_area { get; set; }
            public string subjects_can_teach { get; set; }
            public int experience_years { get; set; }
            public string find_about_us { get; set; }
            public string date_of_joining { get; set; }
        }
    }
}