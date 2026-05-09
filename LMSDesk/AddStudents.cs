
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
namespace LMSDesk

{
    public partial class AddStudent : Form
    {

      public AddStudent()
        {
            InitializeComponent();
        
            this.Load += StudentAdd_Load;
            panel2.BackColor = Color.FromArgb(32, 64, 128);
            label7.ForeColor = Color.White;
            label7.Font = new Font("Segoe UI", 14, FontStyle.Bold);

            rdbMale.CheckedChanged += Gender_CheckedChanged;
            rdbFemale.CheckedChanged += Gender_CheckedChanged;

            // ===== GROUP BOXES =====
            StyleGroupBox(groupBox1);
            StyleGroupBox(groupBox2);
            StyleGroupBox(groupBox3);
            StyleGroupBox(groupBox4); 
            StyleGroupBox(groupBox5);

            // ===== INPUTS =====
            StyleAllInputs(this);

            // ===== LABELS (ALL BLACK EXCEPT HEADER) =====
            StyleAllLabelsBlack(this);

            // ===== BUTTONS =====
            StyleButton(btnSubmit, Color.FromArgb(32, 64, 128));
         //   StyleButton(btnBrowsePhoto, Color.FromArgb(108, 117, 125));

        }
     
        string selectedImagePath = "";
        public string currentStudentId = "";  

        string license = AppSession.LicenseName;
     private async void StudentAdd_Load(object sender, EventArgs e)
        {
            await LoadInstitutes();
            cmbState.SelectedIndex = 13;
            await LoadCourses();
           
        }
        private void StyleGroupBox(GroupBox grp)
        {
            grp.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            grp.ForeColor = Color.FromArgb(32, 64, 128);
            grp.BackColor = Color.White;
        }

        // ================= INPUT STYLING =================
        private void StyleAllInputs(Control parent)
        {
            foreach (Control c in parent.Controls)
            {
                if (c is TextBox txt)
                {
                    txt.Font = new Font("Segoe UI", 10);
                    txt.BorderStyle = BorderStyle.FixedSingle;
                }
                
                else if (c is RadioButton rdb)
                {
                    rdb.Font = new Font("Segoe UI", 9.5f);
                    rdb.ForeColor = Color.Black;
                    rdb.BackColor = Color.Transparent;
                    rdb.UseVisualStyleBackColor = false;
                }
                    
                else if (c is DateTimePicker dtp)
                {
                    dtp.Font = new Font("Segoe UI", 10);
                }
                else if (c is NumericUpDown num)
                {
                    num.Font = new Font("Segoe UI", 10);
                }

                if (c.HasChildren)
                    StyleAllInputs(c);
            }
        }

        // ================= LABEL COLOR FIX =================
        private void StyleAllLabelsBlack(Control parent)
        {
            foreach (Control c in parent.Controls)
            {
                if (c is Label lbl && lbl != label7)
                {
                    lbl.ForeColor = Color.Black;
                }

                if (c.HasChildren)
                    StyleAllLabelsBlack(c);
            }
        }

        // ================= BUTTON =================
        private void StyleButton(Button btn, Color bg)
        {
            btn.BackColor = bg;
            btn.ForeColor = Color.White;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            btn.Height = 36;
            btn.Width = 89;
            btn.Cursor = Cursors.Hand;
        }
    

        public bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email)) return false;
            // Standard email pattern
            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, pattern, RegexOptions.IgnoreCase);
        }  
        private async void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            { 
                if (!rdbMale.Checked && !rdbFemale.Checked)
                {
                    ToastControl.ShowToast(this,
                                             "Please select Gender (Male/Female).",
                                             ToastControl.ToastType.Warning,
                                             3);
                    return; // पुढचा कोड रन होणार नाही
                }


                using (HttpClient client = new HttpClient())
                using (MultipartFormDataContent form = new MultipartFormDataContent())
                {
                    // ================= TEXT DATA =================

                    form.Add(new StringContent(txtFirstName.Text.Trim().ToUpper()), "first_name");
                    form.Add(new StringContent(txtLastName.Text.Trim()), "last_name");
                    form.Add(new StringContent(dtpDOB.Value.ToString("yyyy-MM-dd")), "dob");
                    form.Add(new StringContent(rdbMale.Checked ? "Male" : "Female"), "gender");
                    form.Add(new StringContent(txtEmail.Text.Trim().ToLower()), "email");
                    form.Add(new StringContent(txtMobile.Text.Trim()), "mobile");
                    form.Add(new StringContent(txtAddress.Text.Trim()), "address");
                    form.Add(new StringContent(txtCity.Text.Trim()), "city");
                    form.Add(new StringContent(txtPostalCode.Text.Trim()), "postal_code");
                    form.Add(new StringContent(cmbNationality.Text.Trim()), "nationality");
                    form.Add(new StringContent(cmbState.Text.Trim()), "state");
                    form.Add(new StringContent(txtParentName.Text.Trim()), "parent_name");
                    form.Add(new StringContent(txtParentContact.Text.Trim()), "parent_contact");
                    form.Add(new StringContent(cmbQualification.Text.Trim()), "qualification");
                    form.Add(new StringContent(dtpEnrollmentDate.Value.ToString("yyyy-MM-dd")), "enrollment_date");
                    form.Add(new StringContent(txtAdditionalNotes.Text.Trim()), "additional_notes");
                    form.Add(new StringContent(AppSession.LicenseName), "institute_name");
                    form.Add(new StringContent(cmbCourseName.Text.Trim()), "course_name");

                    // नवीन फील्ड्स येथे ॲड करा:
                    form.Add(new StringContent(txtAadhardCardNumber.Text.Trim()), "AadharCardNo");
                    form.Add(new StringContent(txtPanCardNo.Text.Trim().ToUpper()), "PanCardNo");

                    // 🔥 Branch / License
                    form.Add(new StringContent(license), "Branch_name");
 
                    // 👈 जर अपडेट असेल तर student_id पाठवा आणि API बदला
                    string apiFile = "studentadd_v1.php";
                    if (!string.IsNullOrEmpty(currentStudentId))
                    {
                        form.Add(new StringContent(currentStudentId), "student_id");
                        apiFile = "student_update.php";
                    }

                    if (!string.IsNullOrEmpty(selectedImagePath))
                    {
                        byte[] imgBytes = File.ReadAllBytes(selectedImagePath);
                        form.Add(new ByteArrayContent(imgBytes), "photo", Path.GetFileName(selectedImagePath));
                    }

                    var res = await client.PostAsync(AppSession.ApiFolder + apiFile + "?license=" + license, form);
                    string result = await res.Content.ReadAsStringAsync();
                    dynamic json = JsonConvert.DeserializeObject(result);

                    if (json.status == true)
                    {
                        ToastControl.ShowToast(this, "Success: " + json.message, ToastControl.ToastType.Success, 3);
                        ClearForm();
                        if (!string.IsNullOrEmpty(currentStudentId)) this.Close(); // अपडेट असल्यास फॉर्म बंद करा
                    }
                    else
                    {
                        ToastControl.ShowToast(this, "Error: " + json.message, ToastControl.ToastType.Error, 4);
                    }
                }
            }
            catch (Exception ex)
            {
                ToastControl.ShowToast(this, "System Error: " + ex.Message, ToastControl.ToastType.Error, 4);
            }
        }


        // डेटा भरण्यासाठी नवीन मेथड


        // डेटा भरण्यासाठी नवीन मेथड
        public async Task FillDataForEdit(Student student)
        {

            // await Task.WhenAll(LoadInstitutes(), LoadCourses());

            await LoadInstitutes();
            await LoadCourses();

            currentStudentId = student.student_id.ToString();
            txtFirstName.Text = student.first_name;
            txtLastName.Text = student.last_name;


            /*
                        if (!string.IsNullOrEmpty(student.course_name))
                        {
                            cmbCourseName.Text = student.course_name;
                            // जर Text ने काम नाही केले तर खालील पर्याय वापरा:
                            // cmbCourseName.SelectedIndex = cmbCourseName.FindStringExact(student.course_name);
                        }

                        if (!string.IsNullOrEmpty(student.qualification))
                        {
                            cmbQualification.Text = student.qualification;
                        }

                        if (!string.IsNullOrEmpty(student.institute_name))
                        {
                            cmbInstituteName.Text = student.institute_name;
                        }*/

            if (!string.IsNullOrEmpty(student.course_name))
                cmbCourseName.Text = student.course_name;

            if (!string.IsNullOrEmpty(student.qualification))
                cmbQualification.Text = student.qualification;


            // तारखेसाठी सुरक्षित पद्धत
            if (!string.IsNullOrEmpty(student.dob))
                dtpDOB.Value = DateTime.Parse(student.dob);

            if (student.gender == "Male") rdbMale.Checked = true; else rdbFemale.Checked = true;

            txtEmail.Text = student.email;
            txtMobile.Text = student.mobile;
            txtAddress.Text = student.address;
            txtCity.Text = student.city;
            txtPostalCode.Text = student.postal_code;
            cmbNationality.Text = student.nationality;
            cmbState.Text = student.state;
            txtParentName.Text = student.Mother_name;
            txtParentContact.Text = student.parent_contact;
            cmbQualification.Text = student.qualification;

            if (!string.IsNullOrEmpty(student.enrollment_date))
                dtpEnrollmentDate.Value = DateTime.Parse(student.enrollment_date);

            txtAdditionalNotes.Text = student.additional_notes;
            txtAadhardCardNumber.Text = student.AadharCardNo;
            txtPanCardNo.Text = student.PanCardNo;

            // फोटो दाखवण्यासाठी
            if (!string.IsNullOrEmpty(student.student_photo))
            {
                string imageUrl = AppSession.BaseUrl + "student_photos/" + student.student_photo;
                picStudent.ImageLocation = imageUrl;
                picStudent.SizeMode = PictureBoxSizeMode.Zoom;
            }

            

            btnSubmit.Text = "Update Details";
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
                cmbInstituteName.ValueMember = "id";   // ✅ keep id
                cmbInstituteName.SelectedIndex = -1;

            }
        }
 
        private async Task LoadCourses()
       {
           using (HttpClient client = new HttpClient())
          {
                    string response = await client.GetStringAsync(
                   AppSession.ApiFolder + "get_course.php?license=" +license);

              var apiResult =
              JsonConvert.DeserializeObject<CourseApiResponse>(response);

        if (apiResult != null && apiResult.status)
        {
            cmbCourseName.DataSource = apiResult.data;
            cmbCourseName.DisplayMember = "course_name";
            cmbCourseName.ValueMember = "course_name";
            cmbCourseName.SelectedIndex = -1;
        }
    }
}
        public class InstituteModel
        {
            public int id { get; set; }
            public string institute_name { get; set; }
        }

        public class InstituteApiResponse
        {
            public bool status { get; set; }
            public List<InstituteModel> data { get; set; }
        }

        public class CourseModel
        {
            public string course_name { get; set; }
        }

        public class CourseApiResponse
        {
            public bool status { get; set; }
            public List<CourseModel> data { get; set; }
        }

        private void ClearForm()
        {
            txtFirstName.Clear();
            txtLastName.Clear();
            txtEmail.Clear();
            txtMobile.Clear();
            txtAddress.Clear();
            txtCity.Clear();
            txtPostalCode.Clear();
            txtParentName.Clear();
            txtParentContact.Clear();
            txtAdditionalNotes.Clear();
            rdbMale.Checked = false;
            rdbFemale.Checked = false;
            cmbInstituteName.SelectedIndex = -1;
            cmbNationality.Clear();
            cmbState.SelectedIndex = -1;
            cmbQualification.SelectedIndex = -1;
            cmbCourseName.SelectedIndex = -1;
            dtpDOB.Value = DateTime.Now;
            dtpEnrollmentDate.Value = DateTime.Now;
            txtAadhardCardNumber.Clear();
            txtPanCardNo.Clear();
            picStudent.Image = null;
          
        }


        public class ApiResponse
        {
            public bool status { get; set; }
            public string message { get; set; }
        } 
      
    private void panel2_Paint(object sender, PaintEventArgs e)
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
                                                 panel2.Width - borderThickness,
                                                 panel2.Height - borderThickness);

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
                    panel2.Region = new Region(path);
                }
            }

        } 

        private void picStudent_Click(object sender, EventArgs e)
        {
            {
                openFileDialog1.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";
                openFileDialog1.Title = "Select Student Photo";
                openFileDialog1.Multiselect = false;

                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    selectedImagePath = openFileDialog1.FileName;
                    picStudent.Image = Image.FromFile(selectedImagePath);
                    //  MessageBox.Show("Photo selected successfully");
                    ToastControl.ShowToast(this,
      "Photo selected successfully",
      ToastControl.ToastType.Success,
      2);
                }
            }
        }
 
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Parent?.Controls.Remove(this);
            this.Dispose();
        }

        private void rdbMale_CheckedChanged(object sender, EventArgs e)
        {

        }
        private void Gender_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rdb = sender as RadioButton;
            if (rdb == null || !rdb.Checked) return;

            string currentName = txtFirstName.Text.Trim();

            // Male साठी MR. आणि Female साठी MS. (जो दोन्हीसाठी चालतो)
            string prefix = rdbMale.Checked ? "MR. " : "MS. ";

            // जुने प्रीफिक्स काढून टाकणे (जर असतील तर)
            string[] oldPrefixes = { "MR. ", "MISS. ", "MRS. ", "MS. " };
            foreach (var p in oldPrefixes)
            {
                if (currentName.StartsWith(p))
                {
                    currentName = currentName.Replace(p, "");
                }
            }

            // नवीन प्रीफिक्स जोडणे
            txtFirstName.Text = prefix + currentName;
            txtFirstName.SelectionStart = txtFirstName.Text.Length;
        }
    }
    
}
public class StudentModel
{
    public string first_name { get; set; }
    public string last_name { get; set; }
    public string dob { get; set; }
    public string gender { get; set; }
    public string email { get; set; }
    public string mobile { get; set; }
    public string address { get; set; }
    public string city { get; set; }
    public string postal_code { get; set; }
    public string nationality { get; set; }
    public string state { get; set; }
    public string parent_name { get; set; }
    public string parent_contact { get; set; }
    public string qualification { get; set; }
    public string enrollment_date { get; set; }
    public string additional_notes { get; set; }
    public string student_photo { get; set; }
    public string institute_name { get; set; }
    public string course_name { get; set; }
    public string Branch_name { get; set; }

    public string AadharCardNo { get; set; } // नवीन
    public string PanCardNo { get; set; }    // नवीन
}


