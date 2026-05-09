using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace LMSDesk
{
    public partial class CertificationList : Form
    {
        private List<Certificate> CertificationRecords = new List<Certificate>();
        string license = AppSession.LicenseName;

        public CertificationList()
        {
            InitializeComponent();
           
            this.Load += CertificationList_Load;
        }
        private async void CertificationList_Load(object sender, EventArgs e)
        {
            await LoadCertificates();
        }
     private async Task LoadCertificates()
        {
            try
            {
                string apiUrl = AppSession.ApiFolder +"getCertification.php?license=" +license;

                using (HttpClient client = new HttpClient())
                {
                    client.Timeout = TimeSpan.FromSeconds(30);

                    HttpResponseMessage response = await client.GetAsync(apiUrl);
                    response.EnsureSuccessStatusCode();

                    string json = await response.Content.ReadAsStringAsync();

                    CertificateApiResponse apiResponse =
                        JsonConvert.DeserializeObject<CertificateApiResponse>(json);

                    if (apiResponse != null && apiResponse.status)
                    {
                        // Save to global list
                        CertificationRecords = apiResponse.data;

                        // Bind to Grid
                        dgvCertificationList.AutoGenerateColumns = true;
                        dgvCertificationList.DataSource = null; // Clear existing
                        dgvCertificationList.DataSource = CertificationRecords;

                        // Apply header formatting
                        SetCertificateGridHeaders();
                        dgvCertificationList.ClearSelection();
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
                    "Error loading students:\n" + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }
        private void SetCertificateGridHeaders()
        {
            // 1. DATA PROTECTION (Read-Only)
            dgvCertificationList.ReadOnly = true;
            dgvCertificationList.AllowUserToAddRows = false;

            // 2. SHOW FULL HEADERS (No "...")
            // This allows columns to grow wider than the screen
            dgvCertificationList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvCertificationList.ScrollBars = ScrollBars.Both;

            // 3. HEADER STYLING
            dgvCertificationList.EnableHeadersVisualStyles = false;
            DataGridViewCellStyle headerStyle = new DataGridViewCellStyle();
            headerStyle.BackColor = System.Drawing.Color.FromArgb(45, 45, 45); // Dark Gray
            headerStyle.ForeColor = System.Drawing.Color.White;
            headerStyle.Font = new System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Bold);
            headerStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvCertificationList.ColumnHeadersDefaultCellStyle = headerStyle;
            dgvCertificationList.ColumnHeadersHeight = 45;
            dgvCertificationList.Columns["cr_id"].HeaderText = "ID";
            dgvCertificationList.Columns["certificate_id"].HeaderText = "Certificate ID";
            dgvCertificationList.Columns["student_name"].HeaderText = "Student Name";
            dgvCertificationList.Columns["dob"].HeaderText = "DOB";
            dgvCertificationList.Columns["gender"].HeaderText = "Gender";
            dgvCertificationList.Columns["course_name"].HeaderText = "Course";
            dgvCertificationList.Columns["course_code"].HeaderText = "Course Code";
            dgvCertificationList.Columns["institute_name"].HeaderText = "Institute";
            dgvCertificationList.Columns["enrollment_date"].HeaderText = "Enrollment Date";
            dgvCertificationList.Columns["completion_date"].HeaderText = "Completion Date";
            dgvCertificationList.Columns["duration"].HeaderText = "Duration";
            dgvCertificationList.Columns["grade"].HeaderText = "Grade";
            dgvCertificationList.Columns["instructor_name"].HeaderText = "Instructor";
            dgvCertificationList.Columns["signatory_name"].HeaderText = "Signatory";
            dgvCertificationList.Columns["signatory_designation"].HeaderText = "Designation";
            dgvCertificationList.Columns["remarks"].HeaderText = "Remarks";
            dgvCertificationList.Columns["issue_date"].HeaderText = "Issue Date";
            dgvCertificationList.Columns["training_mode"].HeaderText = "Training Mode";
            dgvCertificationList.Columns["created_at"].HeaderText = "Created At";

               //dgvCertificationList.AutoSizeColumnsMode =
              //  DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void txtCert_TextChanged(object sender, EventArgs e)
        {
            string keyword = txtCert.Text.Trim().ToLower();

            if (string.IsNullOrEmpty(keyword))
            {
                dgvCertificationList.DataSource = null;
                dgvCertificationList.DataSource = CertificationRecords;
                SetCertificateGridHeaders();
                return;
            }

            var filtered = CertificationRecords.FindAll(s =>
                (!string.IsNullOrEmpty(s.certificate_id) && s.certificate_id.ToLower().Contains(keyword))||
                (!string.IsNullOrEmpty(s.student_name) && s.student_name.ToLower().Contains(keyword))
            );

            dgvCertificationList.DataSource = null;
            dgvCertificationList.DataSource = filtered;
            SetCertificateGridHeaders();
        }
      }
    }

public class CertificateApiResponse
{
    public bool status { get; set; }
    public List<Certificate> data { get; set; }
}

public class Certificate
{
    public int cr_id { get; set; }
    public string certificate_id { get; set; }
    public string student_name { get; set; }
    public string dob { get; set; }
    public string gender { get; set; }
    public string course_name { get; set; }
    public string course_code { get; set; }
    public string institute_name { get; set; }
    public string enrollment_date { get; set; }
    public string completion_date { get; set; }
    public string duration { get; set; }
    public string grade { get; set; }
    public string instructor_name { get; set; }
    public string signatory_name { get; set; }
    public string signatory_designation { get; set; }
    public string remarks { get; set; }
    public string issue_date { get; set; }
    public string training_mode { get; set; }
    public string created_at { get; set; }
}





