using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static LMSDesk.marklistprint;

namespace LMSDesk
{
    public partial class FinalMarklistPrint : Form
    {
        // Storage for images
        private Image candidatePhoto = null;
        private Image instituteLogo = null;

        //public string MarklistId { get; set; }

        public string SelectedMarklistId { get; set; }

        private Image printImage;


        string license = AppSession.LicenseName;

        public FinalMarklistPrint()
        {
            InitializeComponent();

            this.Load += FinalMarklistPrint_Load;

            // Wire up events
            btnPreview.Click += BtnPreview_Click;
            btnSave.Click += BtnSave_Click;
            btnUploadPhoto.Click += BtnUploadPhoto_Click;
            btnUploadLogo.Click += BtnUploadLogo_Click;
            btnAddSubject.Click += BtnAddSubject_Click;
            dgvSubjects.CellContentClick += DgvSubjects_CellContentClick;
            // btnSearchDB.Click += BtnSearchDB_Click;
        }

        private void BtnAddSubject_Click(object sender, EventArgs e)
        {
            // Validate basic inputs
            if (string.IsNullOrWhiteSpace(txtSubName.Text))
            {
                MessageBox.Show("Please enter a subject name.");
                return;
            }

            // Add row to DataGridView
            dgvSubjects.Rows.Add(
                txtSubName.Text,
                txtSubMax.Text,
                txtSubObt.Text,
                txtSubRem.Text
            );

            // Clear entry boxes for next subject
            txtSubName.Clear();
            txtSubMax.Clear();
            txtSubObt.Clear();
            txtSubRem.Clear();
            txtSubName.Focus();
            AutoCalculateGrade();
        }

        private void BtnUploadLogo_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog { Filter = "Image Files|*.jpg;*.png;*.jpeg" })
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    instituteLogo = Image.FromFile(ofd.FileName);
                    MessageBox.Show("Institute Logo uploaded successfully!");
                }
            }
        }
        private void BtnUploadPhoto_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog { Filter = "Image Files|*.jpg;*.png;*.jpeg" })
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    candidatePhoto = Image.FromFile(ofd.FileName);
                    MessageBox.Show("Photo uploaded successfully!");
                }
            }
        }
        private void BtnPreview_Click(object sender, EventArgs e)
        {
            try
            {
                // Collect subjects from DataGridView
                List<SubjectItem> subjectList = new List<SubjectItem>();
                foreach (DataGridViewRow row in dgvSubjects.Rows)
                {
                    if (row.Cells[0].Value != null)
                    {
                        subjectList.Add(new SubjectItem
                        {
                            SubjectName = row.Cells[0].Value.ToString(),
                            MaxMarks = row.Cells[1].Value?.ToString() ?? "0",
                            MarksObtained = row.Cells[2].Value?.ToString() ?? "0",
                            //Remarks = row.Cells[3].Value?.ToString() ?? ""
                            Remarks = row.Cells[3].Value == null ? "" : row.Cells[3].Value.ToString()

                        });
                    }
                }

                MarkSheetData data = new MarkSheetData
                {
                    CandidateName = txtName.Text,
                    GuardianName = txtGuardian.Text,
                    DateOfBirth = dtpDOB.Value.ToString("dd/MM/yyyy"),
                    Duration = txtDuration.Text,
                    Examination = dtpExam.Value.ToString("MMM-yyyy"),
                    ProfileSrNo = txtSerial.Text,
                    CourseName = txtCourse.Text,
                    InstituteName = txtInstitute.Text,
                    Subjects = subjectList, // Dynamic list
                    Grade = txtGrade.Text,
                    //Photo = candidatePhoto ?? pcbphoto.Image, // 🔥 fallback
                    Photo = chkPrintPhoto.Checked ? (candidatePhoto ?? pcbphoto.Image): null,
                    InsPhoto = instituteLogo,
                    SessionYear = string.IsNullOrWhiteSpace(txtSession.Text) ? "" : txtSession.Text,
                    course_id = int.Parse(lblCourseId.Text),
                    student_id = lblStudentId.Text,
                    Branch_name = lblBranchCOde.Text,

                    // FLAG
                    PrintPhoto = chkPrintPhoto.Checked
                };

                // Render via Engine
               // picMarkList.Image = MarkListEngine.Render(data);


                try
                {
                   // picMarkList.Image = MarkListEngine.Render(data);


                    Bitmap page = MarkListEngine.Render(data);

                    // 🔥 Add overlay AFTER render
                    page = PageOverlayHelper.AddTopCornerCodes(
                                page,
                               data.SessionYear,
                               data.ProfileSrNo
                           );

                    // Show preview
                    picMarkList.Image = page;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        ex.ToString(),   // FULL stack trace
                        "Preview Error"
                    );
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error generating preview: " + ex.Message);
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (picMarkList.Image == null)
            {
                MessageBox.Show("Please generate a preview first.");
                return;
            }

            using (SaveFileDialog sfd = new SaveFileDialog { Filter = "PNG Image|*.png", FileName = "MarkSheet.png" })
            {
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    picMarkList.Image.Save(sfd.FileName, System.Drawing.Imaging.ImageFormat.Png);
                    MessageBox.Show("Saved Successfully!");
                    ClearForm();
                }
            }
        }
        private void DgvSubjects_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Check if the clicked cell is the Button Column (index 4) and not the header
            if (e.ColumnIndex == dgvSubjects.Columns["btnDeleteCol"].Index && e.RowIndex >= 0)
            {
                dgvSubjects.Rows.RemoveAt(e.RowIndex);
                AutoCalculateGrade();   // 🔥 ADD

            }
        }

        private void ClearForm()
        {
            txtName.Clear();
            txtGuardian.Clear();
            txtDuration.Clear();
            txtSerial.Clear();
            txtCourse.Clear();
            txtInstitute.Clear();
            txtGrade.Clear();
            txtSession.Clear();

            if (dgvSubjects.DataSource != null)
            {
                dgvSubjects.DataSource = null;   // unbind first
            }

            dgvSubjects.Rows.Clear();            // now safe

            dtpDOB.Value = DateTime.Now;
            dtpExam.Value = DateTime.Now;

            candidatePhoto = null;
            instituteLogo = null;

            if (picMarkList.Image != null)
            {
                picMarkList.Image.Dispose();
                picMarkList.Image = null;
            }

            txtName.Focus();
        }

        private async void FinalMarklistPrint_Load(object sender, EventArgs e)
        {

            dgvSubjects.AutoGenerateColumns = false;   // 🔥 MOST IMPORTANT

            if (!string.IsNullOrWhiteSpace(SelectedMarklistId))
            {
                await GetMarkListData(SelectedMarklistId);
            }
            AutoCalculateGrade();   // 🔥 ADD
        }

        //public static string GetPassingYear(DateTime date)
        //{
        //    int startYear = date.Month >= 4 ? date.Year : date.Year - 1;
        //    int endYearShort = (startYear + 1) % 100;

        //    return $"{startYear}-{endYearShort:D2}";
        //}

        public static string GetPassingYear(DateTime date)
        {
            int startYear = date.Year;
            int endYearShort = (startYear + 1) % 100;

            return $"{startYear}-{endYearShort:D2}";
        }


        private async Task GetMarkListData(string marklistId)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(marklistId))
                    return;

                //string apiUrl = AppSession.ApiFolder + $"get_marklist_by_id.php?Marklist_id={marklistId}";

                string apiUrl = AppSession.ApiFolder +"get_marklist_by_id.php?Marklist_id=" + marklistId;

                using (HttpClient client = new HttpClient())
                {
                    client.Timeout = TimeSpan.FromSeconds(60);

                    var response = await client.GetAsync(apiUrl);
                    response.EnsureSuccessStatusCode();

                    string json = await response.Content.ReadAsStringAsync();

                    var result = JsonConvert.DeserializeObject<MarkListResponse>(json);

                    if (result == null || !result.success)
                    {
                        MessageBox.Show("Marklist not found");
                        return;
                    }

                    // ---------- TEXTBOX ----------
                    txtName.Text = result.details.student_name ?? "";
                    txtCourse.Text = result.details.course_name ?? "";
                    txtInstitute.Text = result.details.institute_name ?? "";
                    // txtGuardian.Text = result.details.Mother_name ?? "";

                    // आईचे पूर्ण नाव आधी व्हेरिएबलमध्ये घ्या
                    string fullMotherName = result.details.Mother_name ?? "";

                    if (!string.IsNullOrWhiteSpace(fullMotherName))
                    {
                        // १. पूर्ण नावाचे तुकडे करा (Space जिथे आहे तिथे)
                        // २. त्यातील पहिला तुकडा (Index 0) निवडा
                        string firstMotherName = fullMotherName.Trim().Split(' ')[0];

                        // टेक्स्टबॉक्समध्ये फक्त पहिले नाव दाखवा
                        txtGuardian.Text = firstMotherName;
                    }
                    else
                    {
                        txtGuardian.Text = "";
                    }

                    txtSerial.Text =   result.details.Marklist_Code;

                    // ही ओळ जोडा: सर्व्हरवरून आलेली ड्युरेशन टेक्स्टबॉक्समध्ये दाखवा
                    //txtDuration.Text = result.details.duration ?? "";

                    // सर्व्हरवरून आलेली व्हॅल्यू (उदा. 06) आणि त्यापुढे " Months" जोडण्यासाठी
                    if (!string.IsNullOrEmpty(result.details.duration))
                    {
                        txtDuration.Text = result.details.duration + " Months";
                    }
                    else
                    {
                        txtDuration.Text = "";
                    }



                    if (!string.IsNullOrWhiteSpace(result.details.examination_date))
                    {
                        dtpExam.Value =
                            Convert.ToDateTime(result.details.examination_date);

                        txtSession.Text = GetPassingYear(dtpExam.Value);
                    }



                    // 🔥 AUTO DURATION CALCULATION
                   /* if (!string.IsNullOrWhiteSpace(result.details.examination_date))
                    {
                        DateTime examDate = Convert.ToDateTime(result.details.examination_date);

                        // Example: assume course end = exam date
                        DateTime startDate = examDate.AddMonths(-3); // fallback if start not available
                        DateTime endDate = examDate;

                        txtDuration.Text = CalculateDuration(startDate, endDate);
                    }*/

                    // ---------- PHOTO ----------
                    try
                    {
                        string photoFileName =
                            result.details.Branch_name + "_" +
                            result.details.student_id + ".jpg";
                        lblStudentId.Text = result.details.student_id;
                        lblBranchCOde.Text = result.details.Branch_name;
                       // txtGrade.Text = result.items.

                        string url =
                        AppSession.BaseUrl + "student_photos/" + photoFileName;

                        LoadImageFromUrl(url);
                    }
                    catch
                    {
                        pcbphoto.Image = null;
                    }

                    // ---------- GRID ----------
                    dgvSubjects.DataSource = null;
                    dgvSubjects.Rows.Clear();

                    foreach (var item in result.items)
                    {
                        dgvSubjects.Rows.Add(
                            item.subject ?? "",
                            item.maximum_marks.ToString(),
                            item.marks_obtained.ToString(),
                            item.remark ?? ""
                        );

                       // txtGrade.Text = item.grade;
                    }

                    // 🔥 AUTO GRADE
                    AutoCalculateGrade();

                    // 🔥🔥🔥 AUTO PREVIEW HERE 🔥🔥🔥
                    BtnPreview_Click(null, null);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error loading data\n" + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
         
     
        public async Task<string> InsertMarklistAsync(
          string marklistNo,
          string studentFullName,
          string courseName,
          string branchName,
          string courseCode,
          string examinationDate,
          string examYear,
          int totalMarks,
          int obtainedMarks,
          string result)
        {
            try
            {
                string apiUrl = AppSession.ApiFolder + "insert_marklist_afterprint.php";

                var marklistData = new
                {
                    marklist_no = marklistNo,
                    student_full_name = studentFullName,
                    course_name = courseName,
                    branch_name = branchName,
                    course_code = courseCode,
                    examination_date = examinationDate.ToString(),
                    exam_year = examYear,
                    total_marks = totalMarks,
                    obtained_marks = obtainedMarks,
                    result = result
                };


             


                string json =
                    JsonConvert.SerializeObject(marklistData);

                using (HttpClient client = new HttpClient())
                {
                    var content = new StringContent(
                        json,
                        Encoding.UTF8,
                        "application/json"
                    );

                    HttpResponseMessage response =
                        await client.PostAsync(apiUrl, content);

                    string results =
                        await response.Content.ReadAsStringAsync();

                    return results;
                }
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }


        private void LoadImageFromUrl(string url)
        {

            /* try
             {
                 using (WebClient wc = new WebClient())
                 {
                     byte[] bytes = wc.DownloadData(url);

                     using (MemoryStream ms = new MemoryStream(bytes))
                     {
                         pcbphoto.Image = Image.FromStream(ms);
                     }
                 }
             }
             catch
             {
                 pcbphoto.Image = null;   // No photo fallback
             }*/


            try
            {
                using (WebClient wc = new WebClient())
                {
                    byte[] bytes = wc.DownloadData(url);

                    using (MemoryStream ms = new MemoryStream(bytes))
                    {
                        Image img = Image.FromStream(ms);

                        pcbphoto.Image = img;        // Screen display
                        candidatePhoto = new Bitmap(img); // 🔥 Engine + Print
                    }
                }
            }
            catch
            {
                pcbphoto.Image = null;
                candidatePhoto = null;
            }
        }


        private string CalculateFinalGrade(int totalObt, int totalMax)
        {
            if (totalMax == 0)
                return "NA";

            double percent = (double)totalObt / totalMax * 100;

            if (percent >= 87.5) return "A+";
            if (percent >= 75) return "A";
            if (percent >= 62.5) return "B+";
            if (percent >= 50) return "B";
            if (percent >= 35) return "C";

            return "F";
        }

        private void AutoCalculateGrade()
        {
            int totalMax = 0;
            int totalObt = 0;

            foreach (DataGridViewRow row in dgvSubjects.Rows)
            {
                if (row.IsNewRow) continue;

                int max = 0;
                int obt = 0;

                int.TryParse(row.Cells[1].Value?.ToString(), out max);
                int.TryParse(row.Cells[2].Value?.ToString(), out obt);

                totalMax += max;
                totalObt += obt;
            }

            txtGrade.Text = CalculateFinalGrade(totalObt, totalMax);
        }




        private async void marklisid_TextChanged(object sender, EventArgs e)
        {
            // await GetMarkListData(marklisid.Text);
        }

        private async void btnprint_Click(object sender, EventArgs e)
        {
            try
            {
                if (picMarkList.Image == null)
                {
                    MessageBox.Show("Please generate preview first.");
                    return;
                }

                // Store image for printing
                printImage = new Bitmap(picMarkList.Image);
                
                // Printer dialog
                using (PrintDialog pd = new PrintDialog())
                {
                    pd.AllowSomePages = false;
                    pd.AllowSelection = false;
                    pd.UseEXDialog = true;

                    if (pd.ShowDialog() == DialogResult.OK)
                    {
                        PrintDocument printDoc = new PrintDocument();
                        printDoc.PrinterSettings = pd.PrinterSettings;

                        // IMPORTANT → same page size as image
                        printDoc.DefaultPageSettings.Landscape = false;
                        printDoc.PrintPage += PrintDoc_PrintPage;

                        printDoc.Print();

                        // 🔥 UPDATE STATUS
                        await UpdatePrintStatus(SelectedMarklistId);
                       await InsertMarklistAsync(txtSerial.Text, txtName.Text, txtCourse.Text, txtInstitute.Text, lblCourseId.Text, dtpExam.Value.ToString("yyyy-MM-dd"), dtpExam.Value.Year.ToString(), 0, 0, "0");
                        // 🔄 Parent Grid Refresh
                        this.Close();
                        //update 
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Print Error: " + ex.Message);
            }
        }

        private async Task UpdatePrintStatus(string marklistId)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var obj = new
                    {
                        marklist_id = marklistId
                    };

                    string json =
                        JsonConvert.SerializeObject(obj);

                    var content = new StringContent(
                        json,
                        Encoding.UTF8,
                        "application/json"
                    );

                    var response = await client.PostAsync(
                        AppSession.ApiFolder +
                        "update_marklist_print_status.php?license=" + license,
                        content
                    );

                    string result =
                        await response.Content.ReadAsStringAsync();

                    Console.WriteLine(result);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Status update error: " + ex.Message);
            }
        }


        private void PrintDoc_PrintPage(object sender, PrintPageEventArgs e)
        {
            try
            {
                if (printImage == null) return;

                // 🔥 REMOVE PRINTER HARD MARGINS
                e.Graphics.TranslateTransform(
                    -e.PageSettings.HardMarginX,
                    -e.PageSettings.HardMarginY
                );

                // Use FULL PAGE instead of margin
                Rectangle m = e.PageBounds;

                float imgWidth = printImage.Width;
                float imgHeight = printImage.Height;

                float ratio = Math.Min(
                    (float)m.Width / imgWidth,
                    (float)m.Height / imgHeight
                );

                int newWidth = (int)(imgWidth * ratio);
                int newHeight = (int)(imgHeight * ratio);

                int posX = (m.Width - newWidth) / 2;
                int posY = (m.Height - newHeight) / 2;

                e.Graphics.DrawImage(printImage, posX, posY, newWidth, newHeight);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Print Page Error: " + ex.Message);
            }
        }



        //private void PrintDoc_PrintPage(object sender, PrintPageEventArgs e)
        //{
        //    try
        //    {
        //        if (printImage == null) return;

        //        // Page printable area
        //        Rectangle m = e.MarginBounds;

        //        // Image original size
        //        float imgWidth = printImage.Width;
        //        float imgHeight = printImage.Height;

        //        // Scale ratio maintain
        //        float ratio = Math.Min(
        //            (float)m.Width / imgWidth,
        //            (float)m.Height / imgHeight
        //        );

        //        int newWidth = (int)(imgWidth * ratio);
        //        int newHeight = (int)(imgHeight * ratio);

        //        int posX = m.Left + (m.Width - newWidth) / 2;
        //        int posY = m.Top + (m.Height - newHeight) / 2;

        //        e.Graphics.DrawImage(printImage, posX, posY, newWidth, newHeight);
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Print Page Error: " + ex.Message);
        //    }
        //}

        private void chkPrintPhoto_CheckedChanged(object sender, EventArgs e)
        {
            BtnPreview_Click(null, null);

        }
                private string CalculateDuration(DateTime start, DateTime end)
        {
            int months = ((end.Year - start.Year) * 12) + end.Month - start.Month;

            if (months <= 0)
                months = 1;

            return months + " Months";
        }
    } 

    public class MarkListResponse
    {
        public bool success { get; set; }
        public MarkListDetails details { get; set; }
        public List<MarkListItem> items { get; set; }
    }

    public class MarkListItem
    {
        public string subject { get; set; }
        public int maximum_marks { get; set; }
        public int marks_obtained { get; set; }
        public string remark { get; set; }

        public string grade { get; set; }
        
    }

    public class MarkListDetails
    {
        public string Marklist_id { get; set; }
        public string student_name { get; set; }
        public string course_name { get; set; }
        public string Mother_name { get; set; }
        public string Branch_name { get; set; }
        public string institute_name { get; set; }
        public string examination_date { get; set; }
        public string student_id { get; set; }
        public string Marklist_Code { get; set; }
        // ही नवीन प्रॉपर्टी जोडा
        public string duration { get; set; }

    }
    public class MarklistApiResponse
    {
        public bool status { get; set; }
        public string message { get; set; }
        public MarklistData marklist { get; set; }
    }
    public class MarklistData
    {
        public string Marklist_id { get; set; }
        public string Marklist_Code { get; set; }
        public string student_name { get; set; }
        public string course_name { get; set; }
        public string Mother_name { get; set; }
        public string duration { get; set; }
        public string examination_date { get; set; }
        public string exam_center { get; set; }
        public string institute_name { get; set; }
        public string Branch_name { get; set; }
        public string admin_name { get; set; }
        public string remark_note { get; set; }
        public List<MarklistItem> items { get; set; }
    }
    public class MarklistItem
    {
        public string subject { get; set; }
        public string maximum_marks { get; set; }
        public string marks_obtained { get; set; }
        public string grade { get; set; }
        public string remark { get; set; }
    }
}