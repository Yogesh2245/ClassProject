using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LMSDesk
{


    public partial class GenMarkList : Form
    {
        int currentMarklistId = 0;
       // public int StudentId { get; set; }
        bool isSaving = false;

        //    public int CourseId { get; set; }
        public int StudentId { get; set; }

        string license = AppSession.LicenseName;

        public GenMarkList()
        {
            InitializeComponent();
            this.Shown += GenMarkList_Shown;

            //InitializeGrid();
        }


        string subjectJsonPath = Path.Combine(Application.StartupPath, "subjects.json");

        private void InitializeGrid()
        {
            DataTable markTable = new DataTable();

            markTable.Columns.Add("Subject", typeof(string));
            markTable.Columns.Add("Max", typeof(int));
            markTable.Columns.Add("Obt", typeof(int));
            markTable.Columns.Add("Grade", typeof(string));
            //markTable.Columns.Add("Type", typeof(string));
            markTable.Columns.Add("Remark", typeof(string));
            markTable.Columns.Add("Admin Name", typeof(string));
            markTable.Columns.Add("Remark Note", typeof(int));
            markTable.Columns.Add("Institute Name", typeof(int));
            //markTable.Columns.Add("Student Id", typeof(string));
            markTable.Columns.Add("Exam Center", typeof(string));
            //markTable.Columns.Add("Duration", typeof(string));
            markTable.Columns.Add("Examination Date", typeof(string));



            // dgvMarks.AutoGenerateColumns = true;
            //   dgvMarks.DataSource = markTable;
        }

        private async void GenMarkList_Load(object sender, EventArgs e)
        {
            SetupGrid();
            StyleGrid();
            await LoadInstitutes();

            LoadSubjectSuggestions();   // ✅ ADD THIS



            lblstudentfullname.Text = txtStName.Text;
            lblcoursename.Text = txtCsName.Text;
            lblmax.Text = txtMaxMarks.Text;
            lblobt.Text = txtMarksObt.Text;
            lblgrade.Text = txtGrade.Text;
            lblstudentid.Text = lblID.Text;
            lble.Text = dtpExamDate.Value.ToString("yyyy-MM-dd"); // full date
            lbly.Text = dtpExamDate.Value.Year.ToString();        // ONLY year

            lblec.Text = txtExamCenter.Text;
            lblcourseid.Text = lblCrAsId.Text; 
        }

        private void SetupGrid()
        {
            dgvMarks.AutoGenerateColumns = false;
            dgvMarks.Columns.Clear();

            dgvMarks.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Subject", DataPropertyName = "subject" });
            dgvMarks.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Max", DataPropertyName = "maximum_marks" });
            dgvMarks.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Obt", DataPropertyName = "marks_obtained" });
            dgvMarks.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Grade", DataPropertyName = "grade" });
            //  dgvMarks.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Type", DataPropertyName = "type_of_mark" });
            dgvMarks.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Remark", DataPropertyName = "remark" });

            dgvMarks.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Admin Name", DataPropertyName = "admin_name" });
            // dgvMarks.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Remark Note", DataPropertyName = "remark_note" });
            dgvMarks.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Institute Name", DataPropertyName = "institute_name" });
            //   dgvMarks.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Student Id", DataPropertyName = "student_id" });
            dgvMarks.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Exam Center", DataPropertyName = "exam center" });
            //  dgvMarks.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Duration", DataPropertyName = "duration" });
            dgvMarks.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Examination Date", DataPropertyName = "examination date" });


            dgvMarks.Columns[4].Visible = false;
            dgvMarks.Columns[5].Visible = false;
            dgvMarks.Columns[6].Visible = false;
            dgvMarks.Columns[7].Visible = false;
            dgvMarks.Columns[8].Visible = false;
        }
        private void StyleGrid()
        {
            dgvMarks.EnableHeadersVisualStyles = false;

            dgvMarks.ColumnHeadersDefaultCellStyle.BackColor = Color.LightGray; // Dark Blue
            dgvMarks.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            dgvMarks.ColumnHeadersDefaultCellStyle.Font = new Font("Calibri", 12, FontStyle.Regular);
            dgvMarks.ColumnHeadersHeight = 35;

            dgvMarks.DefaultCellStyle.Font = new Font("Calibri", 10);
            // dgvMarks.GridColor = Color.LightGray;
            dgvMarks.RowTemplate.Height = 35;

            // dgvMarks.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 247, 250);
        }
         private async void btnAddSubject_Click(object sender, EventArgs e)
        {
            /*
                        if (!ValidateInput())
                            return;

                        // ✅ JSON Save
                        SaveSubjectToJson(txtSubject.Text.Trim());

                        dgvMarks.Rows.Add(
                            txtSubject.Text.Trim(),
                            txtMaxMarks.Text,
                            txtMarksObt.Text,
                            txtGrade.Text.Trim(),
                            //   cmbTypeOfMark.SelectedItem.ToString(),
                            "Pass",
                            txtAdminName.Text.Trim(),
                             //  txtRemarkNote.Text.Trim(),
                             license,
                             //  txtStudentId.Text.Trim(),
                             txtExamCenter.Text.Trim(),
                              //   txtDuration.Text.Trim(),
                              dtpExamDate.Value.ToShortDateString()
                        );

                        ClearInputs();*/



            if (!ValidateInput())
                return;

            // सध्याचे सब्जेक्ट नाव स्टोअर करून ठेवा (चेक करण्यासाठी)
            string currentSubject = txtSubject.Text.Trim();

 
            
            
            
            // 


             
            SaveSubjectToJson(currentSubject);

            // DataGridView मध्ये रो ॲड करणे
            dgvMarks.Rows.Add(
                currentSubject,
                txtMaxMarks.Text,
                txtMarksObt.Text,
                txtGrade.Text.Trim(),
                "Pass",
                txtAdminName.Text.Trim(),
                license,
                txtExamCenter.Text.Trim(),
                dtpExamDate.Value.ToShortDateString()
            );

            // १. आधी इनपुट्स क्लिअर करा
            ClearInputs();

            // २. जर आता ॲड केलेला सब्जेक्ट "Theory" असेल, तर पुढच्या एन्ट्रीसाठी सेटिंग करा
            if (currentSubject.Equals("Theory", StringComparison.OrdinalIgnoreCase))
            {
                txtSubject.Text = "Job File";
                txtMaxMarks.Focus(); // डायरेक्ट Max Marks वर फोकस जाईल
            }
            else
            {
                txtSubject.Focus(); // इतर वेळी नेहमीप्रमाणे सब्जेक्टवर फोकस राहील
            }





        }
        private void ClearInputs()
        {
            txtSubject.Clear();
            txtMaxMarks.Clear();
            txtMarksObt.Clear();
            txtRemark.Clear();
            txtGrade.Clear();
            //  cmbTypeOfMark.SelectedIndex = -1;

            txtAdminName.Clear();
            //  txtRemarkNote.Clear();
            //  cmbInstituteName.SelectedIndex = -1;
            // txtStudentId.Clear();
            txtExamCenter.Clear();
            //   txtDuration.Clear();
            dtpExamDate.CustomFormat = " ";
        }


        private void txtMaxMarks_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtMarksObt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        private bool ValidateInput()
        {

            if (string.IsNullOrWhiteSpace(txtSubject.Text))
            {
                MessageBox.Show("Subject is required");
                txtSubject.Focus();
                return false;
            }

            if (!int.TryParse(txtMaxMarks.Text, out int maxMarks))
            {
                MessageBox.Show("Enter valid Max Marks");
                txtMaxMarks.Focus();
                return false;
            }


            if (!int.TryParse(txtMarksObt.Text, out int marksObt))
            {
                MessageBox.Show("Enter valid Marks Obtained");
                txtMarksObt.Focus();
                return false;
            }

            if (marksObt > maxMarks)
            {
                MessageBox.Show("Marks Obtained cannot be greater than Max Marks");
                txtMarksObt.Focus();
                return false;
            }

          

            return true;
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


                    txtExamCenter.Text = cmbInstituteName.GetItemText(
                        cmbInstituteName.SelectedItem);
                }
            }
        }
        private void GenMarkList_Shown(object sender, EventArgs e)
        {
            if (cmbInstituteName.Items.Count > 0)
            {
                cmbInstituteName.SelectedIndex = 0;
                txtExamCenter.Text = cmbInstituteName.Text;
            }
        }



        private void cmbInstituteName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbInstituteName.SelectedIndex != -1)
            {
                txtExamCenter.Text = cmbInstituteName.GetItemText(
            cmbInstituteName.SelectedItem);
            }
        }
        private static readonly HttpClient client = new HttpClient();




        public async Task<bool> UpdateMarklistStatusAsync(string courseId)
        {
            string url = AppSession.ApiFolder + "update_marklist_status.php?license=" +license;

            using (var client = new HttpClient())
            {
                try
                {

                    var data = new Dictionary<string, string> { { "id", courseId } };
                    var content = new FormUrlEncodedContent(data);

                    var response = await client.PostAsync(url, content);
                    var result = await response.Content.ReadAsStringAsync();


                    System.Diagnostics.Debug.WriteLine($"Response: {result}");

                    return response.IsSuccessStatusCode && result.Contains("\"status\":\"success\"");
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"C# Error: {ex.Message}");
                    return false;
                }
            }
        }


        private void panel4_Paint(object sender, PaintEventArgs e)
        {
            {

                int borderRadius = 30;
                Color borderColor = Color.FromArgb(180, 180, 180);
                Color fillColor = Color.White;
                float borderThickness = 2f;


                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;


                RectangleF rect = new RectangleF(borderThickness / 2, borderThickness / 2,
                                                 panel4.Width - borderThickness,
                                                 panel4.Height - borderThickness);

                using (System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath())
                {
                    // Define the curves
                    path.AddArc(rect.X, rect.Y, borderRadius, borderRadius, 180, 90);
                    path.AddArc(rect.Right - borderRadius, rect.Y, borderRadius, borderRadius, 270, 90);
                    path.AddArc(rect.Right - borderRadius, rect.Bottom - borderRadius, borderRadius, borderRadius, 0, 90);
                    path.AddArc(rect.X, rect.Bottom - borderRadius, borderRadius, borderRadius, 90, 90);
                    path.CloseFigure();


                    using (SolidBrush brush = new SolidBrush(fillColor))
                    {
                        e.Graphics.FillPath(brush, path);
                    }


                    using (Pen pen = new Pen(borderColor, borderThickness))
                    {
                        e.Graphics.DrawPath(pen, path);
                    }


                    panel4.Region = new Region(path);
                }
            }

        }

        // १. हा व्हेरिएबल क्लासच्या एकदम वर (Top) ठेवा
        //   bool isSaving = false;
        //public class Subject
        //{
        //  public string SubjectName { get; set; }
        //public int Max { get; set; }
        // public int Obt { get; set; }
        //public string Grade { get; set; }
        //  }//
        public async Task UpdateAssignedCourseStatus(
int id,
string marklist = null,
int? cert = null,
string current = null)
        {
            var url = AppSession.ApiFolder + "update_assigned_course_status.php?license="+license;

            var data = new Dictionary<string, object>();
            data["id"] = id;

            if (marklist != null)
                data["MarklistGen"] = marklist;

            if (cert != null)
                data["IsCertificationGen"] = cert;

            if (current != null)
                data["CurrentStatus"] = current;

            var json = Newtonsoft.Json.JsonConvert.SerializeObject(data);

            using (HttpClient client = new HttpClient())
            {
                var content = new StringContent(
                    json,
                    Encoding.UTF8,
                    "application/json");

                var response = await client.PostAsync(url, content);
                var result = await response.Content.ReadAsStringAsync();

               // MessageBox.Show(result);
            }
        }
        private async void btnMarklistGenerate_Click(object sender, EventArgs e)
        {
            lbly.Text = dtpExamDate.Value.Year.ToString();
            btnMarklistGenerate.Enabled = false;

            try
            {
                // *//* ===============================
                //    1️⃣ COLLECT SUBJECTS
                //  ================================ *//*
                var subjects = dgvMarks.Rows.Cast<DataGridViewRow>()
      .Where(r => !r.IsNewRow)
      .Select(r => new
      {
          subject = (r.Cells[0].Value?.ToString() ?? "")
                      .Substring(0, Math.Min(15,
                      (r.Cells[0].Value?.ToString() ?? "").Length)),

          maximum_marks = Convert.ToInt32(r.Cells[1].Value ?? 0),
          marks_obtained = Convert.ToInt32(r.Cells[2].Value ?? 0),
          grade = r.Cells[3].Value?.ToString() ?? "",

          // ✅ NEW PARAMETER
          remark = r.Cells[4].Value?.ToString() ?? ""
      }).ToList();

                if (subjects.Count == 0)
                {
                    MessageBox.Show("Please add at least one subject.");
                    return;
                }

                foreach (var s in subjects)
                {
                    if (string.IsNullOrWhiteSpace(s.subject))
                    {
                        MessageBox.Show("Subject name cannot be empty.");
                        return;
                    }

                    if (s.marks_obtained > s.maximum_marks)
                    {
                        MessageBox.Show($"Marks obtained cannot exceed max marks for {s.subject}");
                        return;
                    }
                }
                int totalMax = subjects.Sum(x => x.maximum_marks);
                int totalObt = subjects.Sum(x => x.marks_obtained);
                /* string finalGrade = txtGrade.Text.Trim();
                 if (string.IsNullOrWhiteSpace(finalGrade))
                 {
                     finalGrade = subjects.LastOrDefault()?.grade ?? "";
                 }*/

                // ✅ AUTO GRADE CALCULATION

                string finalGrade = CalculateFinalGrade(totalObt, totalMax);

                // UI मध्ये पण दाखवायचं असेल तर
               // txtGrade.Text = finalGrade;
               // lblgrade.Text = finalGrade;



                //
                //    *//* ===============================
                //     2️⃣ PREPARE COMMON PAYLOAD
                //  ================================ *//*
                var payload = new
                {
                    student_id = this.StudentId,
                    student_name = txtStName.Text.Trim(),
                    branch_name = license,
                    course_name = txtCsName.Text.Trim(),
                    duration = txtDuration.Text.Trim(),
                    remark_note = txtRemark.Text.Trim(),
                    examination_date = dtpExamDate.Value.ToString("yyyy-MM-dd"),
                    exam_center = txtExamCenter.Text.Trim(),
                    institute_name = cmbInstituteName.Text.Trim(),
                    admin_name = txtAdminName.Text.Trim(),
                    total_max_marks = totalMax,       // ✅ Include totals
                    total_obtained_marks = totalObt,  // ✅ Include totals
                    final_grade = finalGrade,
                    mother_name = lblmothername.Text.Trim(),
                    subjects = subjects,
                    Marklist_Code = "" + lblbranchname.Text + "-M-" + lblstudentid.Text + "-" + lblcourseid.Text,
                    course_id = int.Parse(lblcourseid.Text),
                    course_code = "--"
                };

                string json = JsonConvert.SerializeObject(payload, Formatting.Indented);

                using (HttpClient client = new HttpClient())
                {
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    //    *//* ===============================
                    ////      3️⃣ SAVE INTO OLD TABLES
                    //    MarklistDetails + MarkListItems
                    // ================================ *//*
                    var oldResponse = await client.PostAsync(
                        AppSession.ApiFolder + "save_marklist.php?license=" + license,
                        content
                    );

                    string oldResult = await oldResponse.Content.ReadAsStringAsync();
                    dynamic oldRes = JsonConvert.DeserializeObject(oldResult);

                    if (oldRes.status != true)
                    {
                        MessageBox.Show("OLD DB Error: " + oldRes.message);
                        return;
                    }

                    string marklistCode = oldRes.marklist_code.ToString();
                    lblID.Text = marklistCode;

                    //*//* ===============================
                    //   4️⃣ SAVE INTO NEW TABLES
                    //   MarklistDetails1 + MarkListItems1
                    //================================ *//*
                    var newContent = new StringContent(json, Encoding.UTF8, "application/json");

                    var newResponse = await client.PostAsync(
                        AppSession.ApiFolder + "insert_marklistdetailsdb1.php",
                        newContent
                    );

                    string newResult = await newResponse.Content.ReadAsStringAsync();
                    dynamic newRes = JsonConvert.DeserializeObject(newResult);

                    if (newRes.status != true)
                    {
                        MessageBox.Show("NEW DB Error: " + newRes.message);
                        return;
                    }

                    //* ===============================
                    //   5️⃣ SAVE REQUESTED CERTIFICATE
                    // ================================ *//*
                    await InsertRequestedCertificateAsync(marklistCode, totalMax, totalObt, finalGrade);
                    await UpdateAssignedCourseStatus(int.Parse( lblcourseid.Text), "Completed", 1, "Completed");

                    //* MessageBox.Show(
                    //"SUCCESSFULLY SAVED🎉\n\n" +

                    //"Marklist Code: " + marklistCode);

                    //*//*
                    // SUCCESS POPUP
                    SuccessPopup popup =
                        new SuccessPopup("Marklist Generated Successfully");
                    popup.ShowDialog();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("CError: " + ex.Message);
            }
            finally
            {
                btnMarklistGenerate.Enabled = true;
                this.DialogResult = DialogResult.OK;
            }


        }


        // Auto Calculate Gradr As per the Marks 
        /*        private string CalculateFinalGrade(int totalObt, int totalMax)
                {
                    if (totalMax == 0)
                        return "NA";

                    // Range based grading
                    if (totalObt >= 175 && totalObt <= 200)
                        return "A+";

                    if (totalObt >= 150 && totalObt <= 174)
                        return "A";

                    if (totalObt >= 125 && totalObt <= 149)
                        return "B+";

                    if (totalObt >= 100 && totalObt <= 124)
                        return "B";

                    if (totalObt >= 70 && totalObt <= 99)
                        return "C";

                    return "F";
                }*/

        private string CalculateFinalGrade(int totalObt, int totalMax)
        {
            if (totalMax == 0)
                return "NA";

            double percent =
                (double)totalObt / totalMax * 100;

            if (percent >= 87.5) return "A+";
            if (percent >= 75) return "A";
            if (percent >= 62.5) return "B+";
            if (percent >= 50) return "B";
            if (percent >= 35) return "C";

            return "F";
        }




        private void SaveSubjectToJson(string subjectName)
        {
            if (string.IsNullOrWhiteSpace(subjectName))
                return;

            SubjectStore store;

            if (File.Exists(subjectJsonPath))
            {
                string json = File.ReadAllText(subjectJsonPath);
                store = JsonConvert.DeserializeObject<SubjectStore>(json);
            }
            else
            {
                store = new SubjectStore();
            }

            // Duplicate avoid
            if (!store.Subjects
                .Any(s => s.Equals(subjectName,
                StringComparison.OrdinalIgnoreCase)))
            {
                store.Subjects.Add(subjectName);
            }

            string output =
                JsonConvert.SerializeObject(store, Formatting.Indented);

            File.WriteAllText(subjectJsonPath, output);
        }
        private void LoadSubjectSuggestions()
        {
            if (!File.Exists(subjectJsonPath))
                return;

            string json = File.ReadAllText(subjectJsonPath);

            SubjectStore store =
                JsonConvert.DeserializeObject<SubjectStore>(json);

            AutoCompleteStringCollection collection =
                new AutoCompleteStringCollection();

            collection.AddRange(store.Subjects.ToArray());

            txtSubject.AutoCompleteMode =
                AutoCompleteMode.SuggestAppend;

            txtSubject.AutoCompleteSource =
                AutoCompleteSource.CustomSource;

            txtSubject.AutoCompleteCustomSource =
                collection;
        }


        // ---------------------------------------------------
        // Insert Requested Certificate Async
        private async Task InsertRequestedCertificateAsync(string marklistCode, int totalMax, int totalObt, string userGrade)
        {
            try
            {
                string finalGrade = userGrade;
                using (HttpClient client = new HttpClient())
                {

                    var requestData = new
                    {
                        branch_id = 1,
                        branch_name = AppSession.LicenseName,
                        student_id = this.StudentId,
                        student_full_name = lblstudentfullname.Text,
                        mother_name = lblmothername.Text,
                        gender = lblgender.Text,
                        dob = lbldob.Text,
                        email = lblemail.Text,
                        address = lbladdress.Text,
                        student_photo = lblstudetntphoto.Text,

                        // ✅ NEW FIELD
                        institute_name = cmbInstituteName.Text.Trim(),

                        course_id = int.Parse(lblcourseid.Text),
                        course_name = lblcoursename.Text,
                        course_code = "" + lblbranchname.Text + "-C-" + lblstudentid.Text + "-" + lblcourseid.Text, 
                        course_start_date = dtpCourseStartDate.Value.ToString("yyyy-MM-dd"),
                        course_end_date = dtpCourseEndDate.Value.ToString("yyyy-MM-dd"),
                        teacher_name = lblteachername.Text,
                        marklist_code = "" + lblbranchname.Text + "-M-" + lblstudentid.Text + "-" + lblcourseid.Text,
                        max_mark = totalMax,
                        obt_mark = totalObt,
                        grade = finalGrade,
                        exam_center = txtExamCenter.Text,
                        examination_date = dtpExamDate.Value.ToString("yyyy-MM-dd"),
                        exam_year = int.Parse(lbly.Text)
                    };


                    string json = JsonConvert.SerializeObject(requestData);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    var response = await client.PostAsync(AppSession.ApiFolder + "insert_requested_certificate.php?license=" + license, content);
                    string dbResult = await response.Content.ReadAsStringAsync();

                    Console.WriteLine("Requested Certificate Response: " + dbResult);

                    if (!response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("Certificate DB Error: " + dbResult);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving Requested Certificate: " + ex.Message);
            }
        }

        private async void lblID_TextChanged(object sender, EventArgs e)
        {
            
            if (isSaving) return;

            string cleanId = System.Text.RegularExpressions.Regex.Replace(lblID.Text, @"[^\d]", "");
            if (int.TryParse(cleanId, out int stid))
            {
                await LoadStudentById(stid);
            }
        }
        private void lblCrAsId_Click(object sender, EventArgs e)
        {
            
        }
       
        private async Task LoadStudentById(int studentId)
        {
            using (HttpClient client = new HttpClient())
            {
                var requestData = new { student_id = studentId };
                var json = JsonConvert.SerializeObject(requestData);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync(AppSession.ApiFolder +"read_student.php?license=" + license, content);
                string responseJson = await response.Content.ReadAsStringAsync();
                dynamic result = JsonConvert.DeserializeObject(responseJson);

                if (result.status == true)
                {
                    lblstudentid.Text = result.data.student_id.ToString();
                    lblstudentfullname.Text = result.data.first_name + " " + result.data.last_name;
                    lblgender.Text = result.data.gender;
                    lbldob.Text = result.data.dob;
                    lblemail.Text = result.data.email;
                    lblmobile.Text = result.data.mobile;
                    lblcoursename.Text = result.data.course_name;
                    lblinstitute.Text = result.data.institute_name;
                    lblbranchname.Text = result.data.Branch_name;
                    lblmothername.Text = result.data.Mother_name;
                }
            }
        }

        private void dgvMarks_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void txtDuration_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow only digits & control keys
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtDuration_TextChanged(object sender, EventArgs e)
        {
            CalculateEndDate();

        }

        private void dtpCourseStartDate_ValueChanged(object sender, EventArgs e)
        {
            CalculateEndDate();
        }


        private void CalculateEndDate()
        {
            try
            {
                // Empty असेल तर skip
                if (string.IsNullOrWhiteSpace(txtDuration.Text))
                    return;

                // Parse duration
                if (!int.TryParse(txtDuration.Text, out int months))
                {
                    MessageBox.Show("Please enter valid duration in months.");
                    txtDuration.Clear();
                    return;
                }

                if (months <= 0)
                {
                    MessageBox.Show("Duration must be greater than 0.");
                    txtDuration.Clear();
                    return;
                }

                DateTime startDate = dtpCourseStartDate.Value;

                // Add months
                DateTime endDate = startDate.AddMonths(months);

                // Set End Date
                dtpCourseEndDate.Value = endDate;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

    }
}
public class SubjectStore
{
    public List<string> Subjects { get; set; } = new List<string>();
}