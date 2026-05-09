using LMSDesk;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LMSDesk
{
    public partial class StudentProfile : Form
    {
        private DashboardForm _dashboard;

        // 🔐 FLAG TO PREVENT AUTO SELECTION BUG
        private List<StudentDropdown> studentRecords = new List<StudentDropdown>();
        private bool _isStudentComboLoaded = false;

        string license = AppSession.LicenseName;


        private readonly string studentLocalFile = Path.Combine(Application.StartupPath, "students.json");

        public StudentProfile(DashboardForm dashboard)
        {
            //sdfsdf 
            InitializeComponent();
            
            
            _dashboard = dashboard;
            this.Load += AssignedCourseForm_Load_1;
        }

        // ================= FORM LOAD =================
        private async void AssignedCourseForm_Load_1(object sender, EventArgs e)
        {
            _isStudentComboLoaded = false;
            //await LoadStudentsIntoCombo();


                  if (File.Exists(studentLocalFile) )
                    {
                        await FetchStudentsAndStoreJson();
                    }


            await LoadCoursesIntoCombo();

            PrepareEmptyGrid();
    
            lblStId.Text = "";
            pbStudentPhoto.Image = null; 

            _isStudentComboLoaded = true;
        }

        // ================= PREPARE EMPTY GRID WITH HEADERS =================
        private void PrepareEmptyGrid()
        {
            dgvAssignedCourse.DataSource = null;
            dgvAssignedCourse.Rows.Clear();
            dgvAssignedCourse.Columns.Clear();

            dgvAssignedCourse.AllowUserToAddRows = false;
            dgvAssignedCourse.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // 🔹 CREATE HEADER COLUMNS MANUALLY
            dgvAssignedCourse.Columns.Add("id", "ID"); // लपवलेला ID कॉलम
            dgvAssignedCourse.Columns["id"].Visible = false;

            dgvAssignedCourse.Columns.Add("course_name", "Course");
            dgvAssignedCourse.Columns.Add("teacher_name", "Teacher");
            dgvAssignedCourse.Columns.Add("start_date", "Start Date");
           // dgvAssignedCourse.Columns["btnDelete"].DefaultCellStyle.ForeColor = Color.Red;

            //AddButtonColumn("btnExam", "Exam");
            //AddButtonColumn("btnMarklist", "Marklist");
            //AddButtonColumn("btnCertificate", "Certificate");
            //AddButtonColumn("btnStatus", "Status");  
            AddButtonColumn("btnDelete", "Delete"); // डिलीट बटण
       

            // BUTTON HEADERS ONLY
            //AddActionButtons();
        }
        private async Task LoadStudentsIntoCombo()
        {
            try
            {
                

                using (HttpClient client = new HttpClient())
                {
                   
                    string json = await client.GetStringAsync(AppSession.ApiFolder + "getStudent.php?license=" +license);

                   
                    ApiResponse response = JsonConvert.DeserializeObject<ApiResponse>(json);

                    if (response != null && response.status)
                    {
                      
                        this.studentRecords = response.data;

                       
                        cmbSelectStudent.DataSource = null; 
                        cmbSelectStudent.DataSource = this.studentRecords;

                        
                        cmbSelectStudent.DisplayMember = "full_name"; 
                        cmbSelectStudent.ValueMember = "student_id";

                        cmbSelectStudent.SelectedIndex = -1;
                       
                    }
                }
            }
            catch (Exception ex)
            {
                // MessageBox.Show("डेटा लोड करताना त्रुटी: " + ex.Message);
                ToastControl.ShowToast(this,"Error loading student data: " + ex.Message,ToastControl.ToastType.Error,4);
            }
           
             
            
        }
            private async void cmbSelectStudent_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!_isStudentComboLoaded) return;

           
            if (cmbSelectStudent.SelectedIndex == -1 || cmbSelectStudent.SelectedItem == null)
            {
                ClearStudentDetails();
                return;
            }
            if (cmbSelectStudent.SelectedItem is StudentDropdown selected)
            {
                // 3. 📝 AUTO-FILL TEXTBOXES (These boxes are on your Course Form)
                txtMobile.Text = selected.mobile;
                txtEmail.Text = selected.email;
                txtAddress.Text = selected.address;
                lblStId.Text = selected.student_id.ToString();
                if (!string.IsNullOrEmpty(selected.student_photo))
                {
                  
                    string imageUrl = AppSession.BaseUrl +"student_photos/" + selected.student_photo;

                    pbStudentPhoto.ImageLocation = imageUrl;
                    pbStudentPhoto.SizeMode = PictureBoxSizeMode.Zoom;
                }
                else
                {
                    pbStudentPhoto.Image = null; 
                }
               // await LoadAssignedCoursesByStudent(selected.student_id.ToString());
                // 4. 📥 Load the Course List for this specific student
                 await LoadAssignedCoursesByStudent(selected.student_id);
            }
            else
            {
                // 5. 🧹 Clear everything if no student is selected
                ClearStudentDetails();
            }
        }
        private void ClearStudentDetails()
        {
            txtMobile.Clear();
            txtEmail.Clear();
            txtAddress.Clear();
            lblStId.Text = "";
            pbStudentPhoto.Image = null;
            PrepareEmptyGrid();
        }
        private void SetupGridTheme()
        {
            // 1. Enable custom styles
            dgvAssignedCourse.EnableHeadersVisualStyles = false;

            // 2. Define the Header Style
            DataGridViewCellStyle headerStyle = new DataGridViewCellStyle();
            headerStyle.BackColor = Color.FromArgb(70, 130, 180); // Professional Steel Blue
            headerStyle.ForeColor = Color.White;                // White text for contrast
            headerStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            headerStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            // 3. Apply to the grid
            dgvAssignedCourse.ColumnHeadersDefaultCellStyle = headerStyle;

            // 4. Extra Professional Touch: Selection Color
            dgvAssignedCourse.DefaultCellStyle.SelectionBackColor = Color.FromArgb(232, 240, 254);
            dgvAssignedCourse.DefaultCellStyle.SelectionForeColor = Color.Black;

            // 5. Grid Line Color
            dgvAssignedCourse.GridColor = Color.FromArgb(224, 224, 224);
        }
        // ================= LOAD STUDENTS =================
     

        // ================= LOAD COURSES =================
        private async Task LoadCoursesIntoCombo()
        {
            using (HttpClient client = new HttpClient())
            {
                string json = await client.GetStringAsync(
                    AppSession.ApiFolder + "getCoursesForDropdown.php?license=" + license
                );

                CourseDropdownResponse response =
                    JsonConvert.DeserializeObject<CourseDropdownResponse>(json);

                cmbCourseName.DataSource = response.data;
                cmbCourseName.DisplayMember = "course_name";
                cmbCourseName.ValueMember = "id";
                cmbCourseName.SelectedIndex = -1;
            }
        }

        // ================= STUDENT SELECTION =================

 


        // ================= LOAD ASSIGNED COURSES BY STUDENT =================
        private async Task LoadAssignedCoursesByStudent(string studentId)
        {
            // 🔹 Strong validation
            if (string.IsNullOrWhiteSpace(studentId))
            {
                dgvAssignedCourse.Rows.Clear();
                return;
            }

            if (!int.TryParse(studentId, out int stId))
            {
                dgvAssignedCourse.Rows.Clear();
                return;
            }

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    client.DefaultRequestHeaders.Add("User-Agent", "C# Application");

                    string url =
                        AppSession.ApiFolder + $"get_assigned_courses.php?license={license}&student_id={stId}";

                    string json = await client.GetStringAsync(url);

                    AssignedCourseResponse response =
                        JsonConvert.DeserializeObject<AssignedCourseResponse>(json);

                    dgvAssignedCourse.Rows.Clear();

                    if (response == null ||
                        response.status == false ||
                        response.data == null ||
                        response.data.Count == 0)
                        return;

                    foreach (var item in response.data)
                    {
                        int rowIndex = dgvAssignedCourse.Rows.Add();

                        dgvAssignedCourse.Rows[rowIndex].Cells["id"].Value = item.id;

                        dgvAssignedCourse.Rows[rowIndex].Cells["course_name"].Value = item.course_name;
                        dgvAssignedCourse.Rows[rowIndex].Cells["teacher_name"].Value = item.teacher_name;
                        dgvAssignedCourse.Rows[rowIndex].Cells["start_date"].Value = item.start_date;
                    }
                }
                catch (Exception ex)
                {
                    //MessageBox.Show("Course Load Error: " + ex.Message);
                    ToastControl.ShowToast(this,"Failed to load courses: " + ex.Message,ToastControl.ToastType.Error,4);
                }
            }

            SetupGridTheme();
        }


        // ================= ADD ACTION BUTTONS =================

        private void AddButtonColumn(string name, string text)
        {
            if (dgvAssignedCourse.Columns.Contains(name)) return;

            DataGridViewButtonColumn btn = new DataGridViewButtonColumn
            {
                Name = name,
                HeaderText = text,
                Text = text,
                UseColumnTextForButtonValue = true,
                FlatStyle = FlatStyle.Popup
            };

            dgvAssignedCourse.Columns.Add(btn);
        } 

        private async Task FetchStudentsAndStoreJson()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string url =
                      AppSession.ApiFolder + "getStudent.php?license=" + license;

                    string json = await client.GetStringAsync(url);

                    File.WriteAllText(studentLocalFile, json);
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Student JSON Fetch Error: " + ex.Message);

                ToastControl.ShowToast(this,"Student sync failed: " + ex.Message,ToastControl.ToastType.Error,4);
            }
        } 
        // ================= ASSIGN COURSE =================
        private async void btnAssignCourse_Click(object sender, EventArgs e)
        {
            
            if (string.IsNullOrEmpty(lblStId.Text) || cmbCourseName.SelectedValue == null)
            {
               // MessageBox.Show("Please Select Student and Course First!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                ToastControl.ShowToast(this,"Please select student and course first.",ToastControl.ToastType.Warning,3);
                return;
            }

           
            var postData = new
            {
                student_id = lblStId.Text,
                student_name = txtSearch.Text,
                course_id = cmbCourseName.SelectedValue,
                course_name = cmbCourseName.Text,
                start_date = dtpdateofstart.Value.ToString("yyyy-MM-dd")
            };

            using (HttpClient client = new HttpClient())
            {
                try
                {
                
                    client.DefaultRequestHeaders.Add("User-Agent", "C# Application");

                   
                    string json = JsonConvert.SerializeObject(postData);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                
                    var response = await client.PostAsync(
                        AppSession.ApiFolder + "saveAssignedCourse.php?license=" + license,
                        content
                    );

                    if (response.IsSuccessStatusCode)
                    {
                        string result = await response.Content.ReadAsStringAsync();

                        
                        var apiResponse = JsonConvert.DeserializeObject<ApiResponse>(result);

                        if (apiResponse != null && apiResponse.status)
                        {
                            //MessageBox.Show("Course details filled Successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            ToastControl.ShowToast(this,"Course assigned successfully.",ToastControl.ToastType.Success,3);

                            await LoadAssignedCoursesByStudent(lblStId.Text);

                            cmbCourseName.SelectedIndex = -1;
                        }
                        else
                        {
                           // MessageBox.Show("Getting issue: " + (apiResponse?.message ?? "Unknown server error"));
                            ToastControl.ShowToast(this,"Server issue: " + (apiResponse?.message ?? "Unknown error"),ToastControl.ToastType.Error,4);
                        }
                    }
                    else
                    {
                        //MessageBox.Show("Server Error: " + response.StatusCode);
                        ToastControl.ShowToast(this,"Server error: " + response.StatusCode,ToastControl.ToastType.Error,4);
                    }
                }
                catch (Exception ex)
                {
                    //MessageBox.Show("Error: " + ex.Message, "Exception Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    ToastControl.ShowToast(this,"Error: " + ex.Message,ToastControl.ToastType.Error,4);
                }
            } 

        }
        public class ApiResponse
        {
            public bool status { get; set; }
            public string message { get; set; }
            public List<StudentDropdown> data { get; set; }
        }

        // ================= MODELS =================
        public class StudentDropdown
        {
            public int id { get; set; }
            public string student_id { get; set; }
            public string first_name { get; set; }
            public string last_name { get; set; }
            public string mobile { get; set; }
            public string email { get; set; }
            public string address { get; set; }
            public string student_photo { get; set; }

            // ✅ ADD THIS
            public string course_name { get; set; }

            public string full_name => first_name + " " + last_name;
        }

        public class StudentDropdownResponse
        {
            public bool status { get; set; }
            public List<StudentDropdown> data { get; set; }
        }

       
        public class CourseDropdownResponse
        {
            public bool status { get; set; }
            public List<CourseDropdown> data { get; set; }
        }

        public class CourseDropdown
        {
            public int id { get; set; }
            public string course_name { get; set; }
        }

        public class AssignedCourseResponse
        {
            public bool status { get; set; }
            public List<AssignedCourse> data { get; set; }
        }

        public class AssignedCourse
        {
            public int id { get; set; } // हे ॲड करा
            public string course_name { get; set; }
            public string teacher_name { get; set; }
            public string start_date { get; set; }

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
                RectangleF rect = new RectangleF(borderThickness / 2, borderThickness / 2,panel2.Width - borderThickness,panel2.Height - borderThickness);

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
        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                // 👉 New instance every time
                SearchStudents s = new SearchStudents();

                // 👉 First key pass
                s.txtSearch.Text = e.KeyChar.ToString();

                s.txtSearch.SelectionStart = s.txtSearch.Text.Length;
                s.txtSearch.SelectionLength = 0;

                if (s.ShowDialog() == DialogResult.OK)
                {
                    var st = s.SelectedStudent;

                    if (st != null)
                    {
                        txtSearch.Text = st.full_name;
                        txtMobile.Text = st.mobile;
                        txtEmail.Text = st.email;
                        txtAddress.Text = st.address;
                        lblStId.Text = st.student_id;

                        if (!string.IsNullOrEmpty(st.student_photo))
                        {
                            string imageUrl =
                                AppSession.BaseUrl + "student_photos/" +
                                st.student_photo;

                            pbStudentPhoto.ImageLocation = imageUrl;
                            pbStudentPhoto.SizeMode = PictureBoxSizeMode.Zoom;
                        }
                        else
                        {
                            pbStudentPhoto.Image = null;
                        }


                        // 🔥 AUTO SELECT COURSE
                        if (!string.IsNullOrEmpty(st.course_name))
                        {
                            for (int i = 0; i < cmbCourseName.Items.Count; i++)
                            {
                                var item = (CourseDropdown)cmbCourseName.Items[i];

                                if (item.course_name == st.course_name)
                                {
                                    cmbCourseName.SelectedIndex = i;
                                    break;
                                }
                            }
                        }


                        _ = LoadAssignedCoursesByStudent(st.student_id);
                    }
                }

                e.Handled = true;
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Search Error: " + ex.Message);

                ToastControl.ShowToast(this,"Search failed: " + ex.Message,ToastControl.ToastType.Error,4);
            }
        }

        private async void dgvAssignedCourse_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            // If Delete button is clicked
            if (dgvAssignedCourse.Columns[e.ColumnIndex].Name == "btnDelete")
            {
                var courseId = dgvAssignedCourse.Rows[e.RowIndex].Cells["id"].Value.ToString();
                var courseName = dgvAssignedCourse.Rows[e.RowIndex].Cells["course_name"].Value.ToString();

                var confirm = MessageBox.Show($"Do you want to delete the course '{courseName}'?","Confirmation",MessageBoxButtons.YesNo,MessageBoxIcon.Question);

                if (confirm == DialogResult.Yes)
                {
                    await DeleteAssignedCourse(courseId);
                }
            }
        }

        private async Task DeleteAssignedCourse(string id)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string url = AppSession.ApiFolder + $"delete_assigned_course.php?license={license}&id={id}";
                    string json = await client.GetStringAsync(url);
                    dynamic response = JsonConvert.DeserializeObject(json);

                    if (response.status == true)
                    {
                        // MessageBox.Show(response.message.ToString(), "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ToastControl.ShowToast(this,response.message.ToString(),ToastControl.ToastType.Success,3);
                        await LoadAssignedCoursesByStudent(lblStId.Text); // ग्रिड रिफ्रेश करा
                    }
                    else
                    {
                       // MessageBox.Show(response.message.ToString(), "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        ToastControl.ShowToast(this,response.message.ToString(),ToastControl.ToastType.Warning,3);
                    }
                }
                catch (Exception ex)
                {
                    // MessageBox.Show("Delete Error: " + ex.Message);
                    ToastControl.ShowToast(this,"Delete failed: " + ex.Message, ToastControl.ToastType.Error,4);
                }
            }
        }
    }
}
