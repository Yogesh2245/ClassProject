using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;

namespace LMSDesk
{
    public partial class StudentLIst : Form
    {
        private List<Student> studentRecords = new List<Student>();

        string license = AppSession.LicenseName;

 
        public StudentLIst()
        {
            InitializeComponent();

        }



        private async void StudentLIst_Load(object sender, EventArgs e)
        {
            await LoadStudents();
        }

        private void SetGridHeaders()
        {
            try
            {
                // 1. DATA PROTECTION (Read-Only)
                dataGridView1.ReadOnly = true;
                dataGridView1.AllowUserToAddRows = false;
                dataGridView1.BorderStyle = BorderStyle.None;
                dataGridView1.BackgroundColor = Color.White;

                // 2. SHOW FULL HEADERS (No "...")
                // This allows columns to grow wider than the screen
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                dataGridView1.ScrollBars = ScrollBars.Both;
                dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(32, 64, 128);
                dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
                dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);

                dataGridView1.DefaultCellStyle.Font = new Font("Segoe UI", 10);
                dataGridView1.DefaultCellStyle.SelectionBackColor = Color.FromArgb(220, 230, 250);
                dataGridView1.DefaultCellStyle.SelectionForeColor = Color.Black;

                dataGridView1.RowHeadersVisible = false;
                dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 247, 250);

                // 3. HEADER STYLING
                // dataGridView1.EnableHeadersVisualStyles = false;
                //  DataGridViewCellStyle headerStyle = new DataGridViewCellStyle();
                // headerStyle.BackColor = System.Drawing.Color.FromArgb(45, 45, 45); // Dark Gray
                // headerStyle.ForeColor = System.Drawing.Color.White;
                // headerStyle.Font = new System.Drawing.Font("calibri", 14, System.Drawing.FontStyle.Bold);
                // headerStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

                //   dataGridView1.ColumnHeadersDefaultCellStyle = headerStyle;
                // dataGridView1.ColumnHeadersHeight = 45;

                dataGridView1.Columns["student_id"].HeaderText = "Student ID";
                dataGridView1.Columns["first_name"].HeaderText = "Student Name";

                dataGridView1.Columns["last_name"].HeaderText = "Last Name";
                dataGridView1.Columns["last_name"].Visible = false;

                dataGridView1.Columns["full_name"].HeaderText = "Full Name";
                dataGridView1.Columns["full_name"].Visible = false;

                dataGridView1.Columns["dob"].HeaderText = "Date of Birth";
                dataGridView1.Columns["gender"].HeaderText = "Gender";
                dataGridView1.Columns["email"].HeaderText = "Email";
                dataGridView1.Columns["mobile"].HeaderText = "Mobile";
                dataGridView1.Columns["address"].HeaderText = "Address";
                dataGridView1.Columns["city"].HeaderText = "City";
                dataGridView1.Columns["postal_code"].HeaderText = "Postal Code";
                dataGridView1.Columns["nationality"].HeaderText = "Nationality";
                dataGridView1.Columns["state"].HeaderText = "State";
                dataGridView1.Columns["Mother_name"].HeaderText = "Mother Name";
                dataGridView1.Columns["parent_contact"].HeaderText = "Parent Contact";
                dataGridView1.Columns["qualification"].HeaderText = "Qualification";
                dataGridView1.Columns["enrollment_date"].HeaderText = "Enrollment Date";
                dataGridView1.Columns["additional_notes"].HeaderText = "Notes";
                dataGridView1.Columns["reg_date"].HeaderText = "Registered On";
                dataGridView1.Columns["institute_name"].HeaderText = "Institute";
                dataGridView1.Columns["course_name"].HeaderText = "Course";
                dataGridView1.Columns["Branch_name"].HeaderText = "Branch";

                 

                if (!dataGridView1.Columns.Contains("btnEdit"))
                {
                    DataGridViewButtonColumn btnEdit = new DataGridViewButtonColumn();
                    btnEdit.HeaderText = "Action";
                    btnEdit.Name = "btnEdit";
                    btnEdit.Text = "Edit";
                    btnEdit.UseColumnTextForButtonValue = true;
                   // dataGridView1.Columns.Add(btnEdit);

                    dataGridView1.Columns.Add(btnEdit);
                    dataGridView1.Columns["btnEdit"].DisplayIndex = 0;
                    dataGridView1.Columns["btnEdit"].Frozen = true;
                }


            }
            catch (Exception ex) { }
          //  dataGridView1.RowTemplate.Height = 30; // smaller height since no images
           // dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }
    
        private async Task LoadStudents()
        {
            try
            {
                string apiUrl = AppSession.ApiFolder + "getStudent.php?license=" +license;

                using (HttpClient client = new HttpClient())
                {
                    string json = await client.GetStringAsync(apiUrl);

                    ApiResponse apiResponse = JsonConvert.DeserializeObject<ApiResponse>(json);

                    /* if (apiResponse != null && apiResponse.status)
                     {
                         // Save to global list
                         studentRecords = apiResponse.data;

                         // Bind to Grid
                         dataGridView1.AutoGenerateColumns = true;
                         dataGridView1.DataSource = null; // Clear existing
                         dataGridView1.DataSource = studentRecords;

                         // Apply header formatting
                         SetGridHeaders();
                     }*/

                    if (apiResponse != null && apiResponse.status)
                    {
                        studentRecords = apiResponse.data;

                        dataGridView1.DataSource = null;
                        dataGridView1.AutoGenerateColumns = true; // हे आधीच सेट करा
                        dataGridView1.DataSource = studentRecords;

                        // **महत्त्वाचे**: कॉलम्स पूर्ण तयार होईपर्यंत थांबण्यासाठी हे वापरा
                        if (dataGridView1.Columns.Count > 0)
                        {
                            SetGridHeaders();
                        }
                    }
                    else
                    {
                        MessageBox.Show("No student data found.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading students:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        
        /*private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
            {
                if (e.RowIndex >= 0)
                {
                    // १. निवडलेल्या ओळीतून student_id मिळवा
                    int selectedId = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["student_id"].Value);

                    // २. GenMarkList चा नवीन ऑब्जेक्ट तयार करा
                    GenMarkList frm = new GenMarkList();

                    // ३. आयडी पास करा (हाच तो Student ID Reference आहे)
                    frm.StudentId = selectedId;

                    // ४. फॉर्म दाखवा
                    frm.Show();


            }}*/

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private async  void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            /*if (e.RowIndex < 0) return;

            if (dataGridView1.Columns[e.ColumnIndex].Name == "btnEdit")
            {
                var selectedStudent = studentRecords[e.RowIndex];

                AddStudent frm = new AddStudent();
                frm.FillDataForEdit(selectedStudent);

                // ✅ 'coursematser' चा फॉर्म शोधा जो आधीच उघडा आहे
                coursematser master = (coursematser)Application.OpenForms["coursematser"];

                if (master != null)
                {
                    // master फॉर्मवरील 'public' केलेली मेथड कॉल करा
                    master.OpenFormInpanel1_Paint(frm);
                }
                else
                {
                    // जर मास्टर फॉर्म सापडला नाही तर साध्या विंडोमध्ये उघडा
                    frm.ShowDialog();
                    _ = LoadStudents();
                }
            }*/

            if (e.RowIndex < 0) return;

            if (dataGridView1.Columns[e.ColumnIndex].Name == "btnEdit")
            {
                var selectedStudent = studentRecords[e.RowIndex];
                AddStudent frm = new AddStudent();

                // आता ही एरर येणार नाही कारण आपण मेथड 'Task' केली आहे
                await frm.FillDataForEdit(selectedStudent);

                coursematser master = (coursematser)Application.OpenForms["coursematser"];
                if (master != null)
                {
                    master.OpenFormInpanel1_Paint(frm);
                }
                else
                {
                    frm.ShowDialog();
                }
            }
        }

        // 'private' ऐवजी 'public' करा
    

        private void txtstserach_TextChanged(object sender, EventArgs e)
        {
            string keyword = txtstserach.Text.Trim().ToLower();

            if (string.IsNullOrEmpty(keyword))
            {
                dataGridView1.DataSource = null;
                dataGridView1.DataSource = studentRecords;
                SetGridHeaders();
                return;
            }

            var filtered = studentRecords.FindAll(s =>
                (!string.IsNullOrEmpty(s.first_name) && s.first_name.ToLower().Contains(keyword)) ||
                (!string.IsNullOrEmpty(s.last_name) && s.last_name.ToLower().Contains(keyword)) ||
                ($"{s.first_name} {s.last_name}".ToLower().Contains(keyword))
            );

            dataGridView1.DataSource = null;
            dataGridView1.DataSource = filtered;
            SetGridHeaders();
        }


    }
    public class ApiResponse
    {
        public bool status { get; set; }
        public List<Student> data { get; set; }
    }

    public class Student
    {

        public int student_id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string full_name => $"{first_name} {last_name}";
        public string dob { get; set; }
        public string gender { get; set; }
        public string email { get; set; }
        public string mobile { get; set; }
        public string address { get; set; }
        public string city { get; set; }
        public string postal_code { get; set; }
        public string nationality { get; set; }
        public string state { get; set; }
        public string Mother_name { get; set; }
        public string parent_contact { get; set; }
        public string qualification { get; set; }
        public string enrollment_date { get; set; }
        public string additional_notes { get; set; }
        public string reg_date { get; set; }
        public string institute_name { get; set; }
        public string course_name { get; set; }
        public string Branch_name { get; set; }

        public string AadharCardNo { get; set; }
        public string PanCardNo { get; set; }

        public string student_photo { get; set; }
    }
}

