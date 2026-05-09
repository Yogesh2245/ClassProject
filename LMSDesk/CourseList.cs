using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LMSDesk
{
    public partial class CourseList : Form
    {
        private List<Course> CourseRecords = new List<Course>();
        string license = AppSession.LicenseName;

        public CourseList()
        {
            InitializeComponent();
          
            this.Load += CourseList_Load;
        }
        private async void CourseList_Load(object sender, EventArgs e)
        {
            await LoadCourses();
        }
        private void SetGridHeaders()
        {
            dataGridReview2.ReadOnly = true;
            dataGridReview2.AllowUserToAddRows = false;
            dataGridReview2.BorderStyle = BorderStyle.None;
            dataGridReview2.BackgroundColor = Color.White;

            // 2. SHOW FULL HEADERS (No "...")
            // This allows columns to grow wider than the screen
            dataGridReview2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridReview2.ScrollBars = ScrollBars.Both;
            dataGridReview2.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(32, 64, 128);
            dataGridReview2.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridReview2.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);

            dataGridReview2.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            dataGridReview2.DefaultCellStyle.SelectionBackColor = Color.FromArgb(220, 230, 250);
            dataGridReview2.DefaultCellStyle.SelectionForeColor = Color.Black;

            dataGridReview2.RowHeadersVisible = false;
            dataGridReview2.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 247, 250);

            // 3. HEADER STYLING
           // dataGridReview2.EnableHeadersVisualStyles = false;
          //  DataGridViewCellStyle headerStyle = new DataGridViewCellStyle();
           // headerStyle.BackColor = System.Drawing.Color.FromArgb(45, 45, 45); // Dark Gray
         //   headerStyle.ForeColor = System.Drawing.Color.White;
          //  headerStyle.Font = new System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Bold);
          //  headerStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            //dataGridReview2.ColumnHeadersDefaultCellStyle = headerStyle;
          //  dataGridReview2.ColumnHeadersHeight = 45;
            dataGridReview2.Columns["course_id"].HeaderText = "Course ID";
            dataGridReview2.Columns["course_name"].HeaderText = "Course Name";
            dataGridReview2.Columns["course_code"].HeaderText = "Course Code";
            dataGridReview2.Columns["instructor_name"].HeaderText = "Instructor";
            dataGridReview2.Columns["course_duration"].HeaderText = "Duration";
            dataGridReview2.Columns["start_date"].HeaderText = "Start Date";
            dataGridReview2.Columns["end_date"].HeaderText = "End Date";
            dataGridReview2.Columns["course_category"].HeaderText = "Category";
            dataGridReview2.Columns["course_level"].HeaderText = "Level";
            dataGridReview2.Columns["course_fee"].HeaderText = "Fee";
            dataGridReview2.Columns["course_mode"].HeaderText = "Mode";
            dataGridReview2.Columns["course_description"].HeaderText = "Description";
           // dataGridReview2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

      private void Rename(string columnName, string headerText)
        {
            if (dataGridReview2.Columns.Contains(columnName))
            {
                dataGridReview2.Columns[columnName].HeaderText = headerText;
            }
        }
        private async Task LoadCourses()
        {
            try
            {
                string apiUrl = AppSession.ApiFolder + "getCourse.php?license=" +license;

                using (HttpClient client = new HttpClient())
                {
                    client.Timeout = TimeSpan.FromSeconds(30);

                    HttpResponseMessage response = await client.GetAsync(apiUrl);
                    response.EnsureSuccessStatusCode();

                    string json = await response.Content.ReadAsStringAsync();

                    CourseApiResponse apiResponse =
                        JsonConvert.DeserializeObject<CourseApiResponse>(json);

                    if (apiResponse != null && apiResponse.status)
                    {
                        // Save to global list
                        CourseRecords = apiResponse.data;

                        // Bind to Grid
                        dataGridReview2.AutoGenerateColumns = true;
                        dataGridReview2.DataSource = null; // Clear existing
                        dataGridReview2.DataSource = CourseRecords;

                        // Apply header formatting
                        SetGridHeaders();
                        dataGridReview2.ClearSelection();
                    }
                    else
                    {
                        MessageBox.Show("No student data found.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error loading courses:\n" + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }
        private void CourseList_Load_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void CourseList_Load_1(object sender, EventArgs e)
        {

        }

        private void txtcourse_TextChanged(object sender, EventArgs e)
        {
            string keyword = txtcourse.Text.Trim().ToLower();

            if (string.IsNullOrEmpty(keyword))
            {
                dataGridReview2.DataSource = null;
                dataGridReview2.DataSource = CourseRecords;
                SetGridHeaders();
                return;
            }

            var filtered = CourseRecords.FindAll(s =>
                (!string.IsNullOrEmpty(s.course_name) && s.course_name.ToLower().Contains(keyword)) 
               
            );

            dataGridReview2.DataSource = null;
            dataGridReview2.DataSource = filtered;
            SetGridHeaders();
        }
    }

        // 🔹 Rename column headers

    

    // 🔹 API response model
    public class CourseApiResponse
    {
        public bool status { get; set; }
        public List<Course> data { get; set; }
    }

    // 🔹 Course model
    public class Course
    {
        public int course_id { get; set; }
        public string course_name { get; set; }
        public string course_code { get; set; }
        public string instructor_name { get; set; }
        public string course_duration { get; set; }
        public string start_date { get; set; }
        public string end_date { get; set; }
        public string course_category { get; set; }
        public string course_level { get; set; }
        public decimal course_fee { get; set; }
        public string course_mode { get; set; }
        public string course_description { get; set; }
    }

}

