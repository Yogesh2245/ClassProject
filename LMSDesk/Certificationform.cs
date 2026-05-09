using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace LMSDesk
{
    public partial class CertificationForm : Form
    {
        
        private readonly CertificateProcessor _processor = new CertificateProcessor();
        private string _studentPhotoPath = "";
        private Bitmap _currentCert = null;

        string license = AppSession.LicenseName;

        public CertificationForm()
        {
            InitializeComponent();
            this.Shown += CertificationForm_Shown;

        }
        string selectedImagePath = "";
        private async void CertificationForm_Load(object sender, EventArgs e)
        {

            await LoadInstitutes();
        }
        private  async void btnSubmit_Click_1(object sender, EventArgs e)
        {
            await SaveCertificationFormDetails();
        }
        private async Task SaveCertificationFormDetails()
        {
            string error = "";

            if (string.IsNullOrWhiteSpace(txtStudentName.Text))
                error = "Student Name is required.";
            else if (string.IsNullOrWhiteSpace(txtCsname.Text))
                error = "Course Name is required.";
            else if (string.IsNullOrWhiteSpace(txtCourseCode.Text))
                error = "Course Code is required.";
            else if (string.IsNullOrWhiteSpace(txtbranchname.Text))
                error = "Branch Name is required.";
            else if (cmbInstituteName.SelectedIndex == -1)
                error = "Please select Institute.";
            else if (cmbGrade.SelectedIndex == -1)
                error = "Please select Grade.";
            else if (dtpEnrollmentDate.Value.Date > DateTime.Now.Date)
                error = "Enrollment date cannot be in future.";
            else if (dtpCompletionDate.Value.Date > DateTime.Now.Date)
                error = "Completion date cannot be in future.";
            else if (dtpIssueDate.Value.Date > DateTime.Now.Date)
                error = "Issue date cannot be in future.";

            if (!string.IsNullOrEmpty(error))
            {
                MessageBox.Show(error, "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string yearValue = dtpYear.Value.Year + "-01-01";
            string certificateId = Guid.NewGuid().ToString();

            var certification = new
            {
                certificate_id = certificateId,
                student_name = txtStudentName.Text.Trim(),
                course_name = txtCsname.Text.Trim(),
                course_code = txtCourseCode.Text.Trim(),
                institute_name = cmbInstituteName.Text.Trim(),
                Branch_name = txtbranchname.Text.Trim(),
                enrollment_date = dtpEnrollmentDate.Value.ToString("yyyy-MM-dd"),
                completion_date = dtpCompletionDate.Value.ToString("yyyy-MM-dd"),
                issue_date = dtpIssueDate.Value.ToString("yyyy-MM-dd"),
                grade = cmbGrade.Text.Trim(),
                year = yearValue 
            };

            string json = JsonConvert.SerializeObject(certification);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response =
                        await client.PostAsync(AppSession.ApiFolder + "certification.php?license=" +license, content);

                    string result = await response.Content.ReadAsStringAsync();

                    // Show raw response if not JSON
                    if (string.IsNullOrWhiteSpace(result))
                    {
                        MessageBox.Show("API returned empty response");
                        return;
                    }

                    if (!result.TrimStart().StartsWith("{"))
                    {
                        MessageBox.Show("Server returned non-JSON:\n\n" + result);
                        return;
                    }

                    ApiResponse apiResponse = JsonConvert.DeserializeObject<ApiResponse>(result);

                    if (apiResponse.status)
                    {
                      
                        MessageBox.Show("Certificate saved successfully! ✅");
                        ClearForm();
                    }
                    else
                    {
                        MessageBox.Show(apiResponse.message ?? "Save failed");
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show("API Error: " + ex.Message);
                }
            }
        }
      

        private async Task LoadInstitutes()
        {
            using (HttpClient client = new HttpClient())
            {
                string response = await client.GetStringAsync(
                  AppSession.ApiFolder + "getInstitute.php?license=" +license);

                var apiResult =
                    JsonConvert.DeserializeObject<InstituteApiResponse>(response);
                cmbInstituteName.DataSource = apiResult.data;
                cmbInstituteName.DisplayMember = "institute_name";
                if (cmbInstituteName.Items.Count > 0)
                {
                    cmbInstituteName.SelectedIndex = 0;

                    // Force the text manually from the selected item
                    txtbranchname.Text = cmbInstituteName.GetItemText(
                        cmbInstituteName.SelectedItem);
                }
            }
        }
        private void CertificationForm_Shown(object sender, EventArgs e)
        {
            if (cmbInstituteName.Items.Count > 0)
            {
                cmbInstituteName.SelectedIndex = 0;
                txtbranchname.Text = cmbInstituteName.Text;
            }
        }
        private void cmbInstituteName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbInstituteName.SelectedIndex != -1)
            {
                txtbranchname.Text = cmbInstituteName.GetItemText(
            cmbInstituteName.SelectedItem);
            }
        }
        private void ClearForm()
        {
          //  txtCertificateD.Clear();
            txtStudentName.Clear();
          //  dtpDOB.Value = DateTime.Now;
         //   rdbMale.Checked = false;
        //    rdbFemale.Checked = false;
            txtCsname.Clear();
            txtCourseCode.Clear();
            cmbInstituteName.SelectedIndex = -1;
           // txtBranchName.Clear();
            dtpEnrollmentDate.Value = DateTime.Now;
            dtpCompletionDate.Value = DateTime.Now;
            dtpYear.Value = DateTime.Now;
            dtpIssueDate.Value = DateTime.Now;
            // txtDuration.Clear();
            cmbGrade.SelectedIndex = -1;
         //   cmbInstructorName.SelectedIndex = -1;
         //   txtRemarks.Clear();
        //    cbOnline.Checked = false;
        //    cbOffline.Checked = false;
         //   txtSignatoryDesignation.Clear();
        //    txtSignatoryName.Clear();
        }


        private void label8_Click(object sender, EventArgs e)
        {

        }

        private async void button1_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                // Create the object from your existing class
                var info = new CertificateInfo
                {
                    Name = txtStudentName.Text,
                    Center = cmbInstituteName.Text.Trim(),
                    SerialNumber = txtCourseCode.Text,
                    Course = txtCsname.Text.Trim(),
                    Grade = cmbGrade.Text.Trim(),
                    Year = dtpYear.Text,
                    ExamDate = dtpIssueDate.Value,
                    DateFrom = dtpEnrollmentDate.Value,
                    DateTo = dtpCompletionDate.Value,
                    PhotoPath = _studentPhotoPath
                };

                // REUSABLE: You can now call this from any variable!
                if (_currentCert != null) _currentCert.Dispose(); 
                _currentCert = _processor.CreateCertificate(info, false);


                // Display Preview
                if (pbPreview.Image != null) pbPreview.Image.Dispose();
                pbPreview.Image = (Bitmap)_currentCert.Clone();

                //  btnSave.Enabled = true;
              //  await UpdateCertificateStatusAsync(lblCrAsId.Text);
                MessageBox.Show("Certificate Generated Successfully!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
        public class ApiResponse
        {
            public bool status { get; set; }
            public string message { get; set; }
        }
        private void pbPreview_Click(object sender, EventArgs e)
        {

        }

        private void txtCertificateD_TextChanged(object sender, EventArgs e)
        {

        }

        private void rdbMale_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void rdbFemale_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void btnBrowsePhoto_Click(object sender, EventArgs e)
        {
            {
                openFileDialog1.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";
                openFileDialog1.Title = "Select Student Photo";
                openFileDialog1.Multiselect = false;

                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    selectedImagePath = openFileDialog1.FileName;
                    _studentPhotoPath = selectedImagePath;
                    picStudent.Image = Image.FromFile(selectedImagePath);
                    MessageBox.Show("Photo selected successfully");
                }
            }
        }

        private void txtStudentName_TextChanged(object sender, EventArgs e)
        {

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

        private void lblCrAsId_Click(object sender, EventArgs e)
        {

        }
    }
}

public class CertificationModel
{
    public string certificate_id { get; set; }
    public string student_name { get; set; }
    public string course_name { get; set; }
    public string course_code { get; set; }
    public string Branch_name { get; set; }
    public string institute_name { get; set; }

    public string enrollment_date { get; set; }   // yyyy-MM-dd
    public string completion_date { get; set; }   // yyyy-MM-dd
    public string issue_date { get; set; }         // yyyy-MM-dd

    public string year { get; set; }               // yyyy-01-01 (DATE)

    public string grade { get; set; }
}

   
