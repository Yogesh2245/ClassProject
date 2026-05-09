using Newtonsoft.Json;
using QRCoder;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.Drawing.Text;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

namespace LMSDesk
{
    public partial class requested : Form
    {
        public requested()
        {
            InitializeComponent(); 
            dgvrequested.CellMouseEnter += dgvrequested_CellMouseEnter;
            dgvrequested.CellMouseLeave += dgvrequested_CellMouseLeave; 
        } 

        private void InitLoader()
        {
            pnlLoader.Dock = DockStyle.Fill;
            pnlLoader.BackColor = Color.FromArgb(150, Color.White);
            pnlLoader.Visible = false;

            picLoader = new PictureBox();
            picLoader.Size = new System.Drawing.Size(120, 120);
            picLoader.SizeMode = PictureBoxSizeMode.Zoom;


            // Center
            picLoader.Left = (this.Width - picLoader.Width) / 2;
            picLoader.Top = (this.Height - picLoader.Height) / 2;



            pnlLoader.BringToFront();
        }
        private void ShowLoader()
        {
            pnlLoader.Visible = true;
            pnlLoader.BringToFront();
            System.Windows.Forms.Application.DoEvents(); // refresh UI immediately
        }

        private void HideLoader()
        {
            pnlLoader.Visible = false;
        }


        string license = AppSession.LicenseName;

        // ================= FORM LOAD =================
        private async void requested_Load(object sender, EventArgs e)
        {
            InitLoader();
            rdbNotPrinted.Checked = true;
            picLoader.Image = LMSDesk.Properties.Resources.search_for_employee_20260215060034;

            await LoadBranchList();          // 🔥 ADD THIS
            await LoadRequestedCertificates();
            SetupGrid();
          //  CheckAllRows();
        }
        private void SetupGrid()
        { 
            AddPrintButtonColumn();
 
        }

        private void AddPrintButtonColumn()
        {


            if (!dgvrequested.Columns.Contains("PrintBtn"))
            {

                DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
                btn.Name = "PrintBtn";
                btn.HeaderText = "Print";
                if (rdbNotPrinted.Checked)
                {
                    btn.Text = "🖨 Print";
                }
                else { btn.Text = "🖨 Re-Print"; }
                btn.UseColumnTextForButtonValue = true;
                btn.Width = 90; 
                dgvrequested.Columns.Insert(0, btn);
            }
        }


        private void dgvrequested_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0 &&
                dgvrequested.Columns[e.ColumnIndex].Name == "Select")
            {
                dgvrequested.Cursor = Cursors.Hand; // 👆 hand cursor
            }
            else
            {
                dgvrequested.Cursor = Cursors.Default;
            }
        }

        private void dgvrequested_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            dgvrequested.Cursor = Cursors.Default;
        }

        private void dgvrequested_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dgvrequested.IsCurrentCellDirty)
            {
                // This forces the checkbox to 'commit' the change as soon as you click it
                dgvrequested.CommitEdit(DataGridViewDataErrorContexts.Commit);
                // Add this inside your SetupGrid method
                dgvrequested.CurrentCellDirtyStateChanged += dgvrequested_CurrentCellDirtyStateChanged;
            }
        }
        private async Task LoadRequestedCertificates()
        {
            try
            {
                ShowLoader();   // 🔹 START LOADER

                dgvrequested.DataSource = null;

                var status = GetStatusFilter();

                string branch = cmbBranchList.SelectedItem?.ToString();

                if (branch == "All Branch")
                    branch = "";

                var obj = new
                {
                    branch_name = branch,
                    ReqStatus = status.reqStatus,
                    PrintStatus = status.printStatus
                };

                string json = JsonConvert.SerializeObject(obj);

                using (HttpClient client = new HttpClient())
                {
                    var content = new StringContent(
                        json,
                        Encoding.UTF8,
                        "application/json"
                    );

                    var response = await client.PostAsync(
                        AppSession.ApiFolder +
                        "read_requested_certificate.php?license=" + license,
                        content
                    );

                    string result = await response.Content.ReadAsStringAsync();

                    var api =
                        JsonConvert.DeserializeObject<
                        ApiResponse<RequestedCertificate>>(result);

                    if (api != null && api.status)
                    {
                        dgvrequested.DataSource = api.data;
                        SetupRequestedGridHeaders();
                    }
                }
            }
            catch (Exception ex)
            {
              //  MessageBox.Show(ex.Message);
            }
            finally
            {
                HideLoader();   // 🔹 STOP LOADER
            }
        }


        private async Task LoadBranchList()
        {
            try
            {
                ShowLoader();   // 🔹 START LOADER

                cmbBranchList.Items.Clear();
                cmbBranchList.Items.Add("All Branch");

                var status = GetStatusFilter();

                var obj = new
                {
                    branch_list = true,
                    ReqStatus = status.reqStatus,
                    PrintStatus = status.printStatus
                };

                string json = JsonConvert.SerializeObject(obj);

                using (HttpClient client = new HttpClient())
                {
                    var content = new StringContent(
                        json,
                        Encoding.UTF8,
                        "application/json"
                    );

                    var response = await client.PostAsync(
                        AppSession.ApiFolder + "read_requested_certificate.php",
                        content
                    );

                    string result = await response.Content.ReadAsStringAsync();

                    dynamic data = JsonConvert.DeserializeObject(result);

                    foreach (var row in data.data)
                    {
                        string branch = row.branch_name;

                        if (!string.IsNullOrWhiteSpace(branch))
                            cmbBranchList.Items.Add(branch);
                    }
                }

                cmbBranchList.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
               // MessageBox.Show(ex.Message);
            }
            finally
            {
                HideLoader();   // 🔹 STOP LOADER
            }
        }




        // ================= GRID DESIGN + HEADERS =================
        private void SetupRequestedGridHeaders()
        {
            dgvrequested.ReadOnly = false;
            dgvrequested.AllowUserToAddRows = false;
            dgvrequested.SelectionMode = DataGridViewSelectionMode.CellSelect;
            dgvrequested.MultiSelect = false;

            // ✅ UI FIX: prevent squeezed headers
            dgvrequested.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvrequested.ScrollBars = ScrollBars.Both;

            // ✅ CLEAN LOOK
            dgvrequested.BorderStyle = BorderStyle.None;
            dgvrequested.BackgroundColor = Color.White;
            dgvrequested.GridColor = Color.FromArgb(230, 230, 230);

            // ---------- HEADER STYLE ----------
            dgvrequested.EnableHeadersVisualStyles = false;
            dgvrequested.ColumnHeadersHeight = 45;

            dgvrequested.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(32, 64, 128);
            dgvrequested.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvrequested.ColumnHeadersDefaultCellStyle.Font =
                new Font("Segoe UI", 10, System.Drawing.FontStyle.Bold);
            dgvrequested.ColumnHeadersDefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleLeft;

            // ---------- ROW STYLE ----------
            dgvrequested.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            dgvrequested.DefaultCellStyle.SelectionBackColor =
                Color.FromArgb(220, 230, 250);
            dgvrequested.DefaultCellStyle.SelectionForeColor = Color.Black;

            dgvrequested.AlternatingRowsDefaultCellStyle.BackColor =
                Color.FromArgb(245, 247, 250);

            dgvrequested.RowHeadersVisible = false;

            // ---------- COLUMN READONLY (UNCHANGED LOGIC) ----------
            foreach (DataGridViewColumn col in dgvrequested.Columns)
            {
                if (col.Name == "PrintBtn")
                {
                    col.ReadOnly = false;     // ✅ MUST
                    col.Width = 90;
                }
                else
                {
                    col.ReadOnly = true;
                }
            }


            //  dgvrequested.RowHeaderMouseClick += (s, e) => dgvrequested.ClearSelection();

            // ---------- HEADER TEXT ----------
            SetHeader("request_id", "Request ID");
            SetHeader("branch_name", "Branch");
            SetHeader("student_id", "Student ID");
            SetHeader("student_full_name", "Student Name");
            SetHeader("mother_name", "Mother Name");
        //    SetHeader("gender", "Gender");
         //   SetHeader("dob", "Date of Birth");
        //    SetHeader("email", "Email");
          //  SetHeader("address", "Address");
            SetHeader("course_name", "Course Name");
            SetHeader("course_code", "Course Code");
            
            SetHeader("course_start_date", "Course Start Date");
            SetHeader("course_end_date", "Course End Date");
            SetHeader("teacher_name", "Teacher Name");
            SetHeader("marklist_code", "Marklist Code");
            SetHeader("max_mark", "Max Marks");
            SetHeader("obt_mark", "Obtained Marks");
            SetHeader("grade", "Grade");
            SetHeader("exam_center", "Exam Center");
            SetHeader("examination_date", "Exam Date");
            SetHeader("exam_year", "Year");
            SetHeader("ReqStatus", "Request Status");
            SetHeader("PrintStatus", "Print Status");
            SetHeader("created_at", "Requested On");
            SetHeader("photo", "Student Photo");
           // SetHeader("bid", "branch_id");
            SetHeader("cid", "Course Id");

            SetHeader("institute_name", "Institute Name");
            SetHeader("course_id", "Course Id");
            

          //  SetHeader("dob", "dob");

            // ---------- FORMAT DATES ----------
            // if (dgvrequested.Columns.Contains("dob"))
            //  dgvrequested.Columns["dob"].DefaultCellStyle.Format = "dd-MM-yyyy";
            if (dgvrequested.Columns.Contains("examination_date"))
            {
                dgvrequested.Columns["examination_date"].DefaultCellStyle.Format = "dd-MM-yyyy";
            }
            ///
            if (dgvrequested.Columns.Contains("created_at"))
                dgvrequested.Columns["created_at"].DefaultCellStyle.Format = "dd-MM-yyyy hh:mm tt";

            if (dgvrequested.Columns.Contains("student_id"))
                dgvrequested.Columns["student_id"].Visible = true;


            if (dgvrequested.Columns.Contains("mother_name"))
                dgvrequested.Columns["mother_name"].Visible = false;

            if (dgvrequested.Columns.Contains("course_start_date"))
                dgvrequested.Columns["course_start_date"].Visible = false;

            if (dgvrequested.Columns.Contains("course_end_date"))
                dgvrequested.Columns["course_end_date"].Visible = false;

            if (dgvrequested.Columns.Contains("teacher_name"))
                dgvrequested.Columns["teacher_name"].Visible = false;

            if (dgvrequested.Columns.Contains("marklist_code"))
                dgvrequested.Columns["marklist_code"].Visible = false;

            if (dgvrequested.Columns.Contains("max_mark"))
                dgvrequested.Columns["max_mark"].Visible = false;

            if (dgvrequested.Columns.Contains("obt_mark"))
                dgvrequested.Columns["obt_mark"].Visible = false;

            if (dgvrequested.Columns.Contains("grade"))
                dgvrequested.Columns["grade"].Visible = false;

            if (dgvrequested.Columns.Contains("exam_center"))
                dgvrequested.Columns["exam_center"].Visible = false;

            if (dgvrequested.Columns.Contains("examination_date"))
                dgvrequested.Columns["examination_date"].Visible = false;

            if (dgvrequested.Columns.Contains("exam_year"))
                dgvrequested.Columns["exam_year"].Visible = false;


            if (dgvrequested.Columns.Contains("ReqStatus"))
                dgvrequested.Columns["ReqStatus"].Visible = false;


            if (dgvrequested.Columns.Contains("PrintStatus"))
                dgvrequested.Columns["PrintStatus"].Visible = false;

            if (dgvrequested.Columns.Contains("created_at"))
                dgvrequested.Columns["created_at"].Visible = false;


            if (dgvrequested.Columns.Contains("photo"))
                dgvrequested.Columns["photo"].Visible = false;

            if (dgvrequested.Columns.Contains("cid"))
                dgvrequested.Columns["cid"].Visible = false;


            // ---------- HIDE HEAVY / UNUSED ----------
            if (dgvrequested.Columns.Contains("student_photo"))
                dgvrequested.Columns["student_photo"].Visible = false;

            // if (dgvrequested.Columns.Contains("branch_id"))
            //  dgvrequested.Columns["branch_id"].Visible = false;

            //   if (dgvrequested.Columns.Contains("course_id"))
            //  dgvrequested.Columns["course_id"].Visible = false;
        }

        // ================= SET HEADER HELPER =================
        private void SetHeader(string columnName, string headerText)
        {
            if (dgvrequested.Columns.Contains(columnName))
            {
                dgvrequested.Columns[columnName].HeaderText = headerText;
            }
        }
       
        private void dgvrequested_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (dgvrequested.Columns[e.ColumnIndex].Name == "PrintBtn")
            {
                PrintSingleCertificate(e.RowIndex);
            }
        }

        private async void PrintSingleCertificate(int rowIndex)
        {
            try
            {
                var row = dgvrequested.Rows[rowIndex];
                string outputFolder = @"C:\TempCer";

                // Auto create if not exists
                if (!Directory.Exists(outputFolder))
                {
                    Directory.CreateDirectory(outputFolder);
                }

                // 🖨 Printer select

                PrinterSettings printerSettings = new PrinterSettings();

                string baseUrl = AppSession.BaseUrl + "student_photos/";


                    // ✅ Photo toggle by checkbox
                    string photoPath = "";
                    if (chkPrintPhoto.Checked)
                    {
                        photoPath = baseUrl +
                                    row.Cells["branch_name"].Value?.ToString() + "_" +
                                    row.Cells["student_id"].Value?.ToString() + ".jpg";
                    } 

                    // 🔹 Data Mapping
                    var info = new CertificateInfo
                    {
                        Name = row.Cells["student_full_name"].Value?.ToString(),
                        Center = row.Cells["branch_name"].Value?.ToString(),
                        SerialNumber = row.Cells["course_code"].Value?.ToString(),
                        Course = row.Cells["course_name"].Value?.ToString(),
                        Grade = row.Cells["grade"].Value?.ToString(),
                        Year = row.Cells["exam_year"].Value?.ToString(),
                        ExamDate = ConvertToDate(row.Cells["examination_date"].Value),
                        DateFrom = ConvertToDate(row.Cells["course_start_date"].Value),
                        DateTo = ConvertToDate(row.Cells["course_end_date"].Value),
                        PhotoPath = photoPath,   // ✅ changed

                        student_id = row.Cells["student_id"].Value?.ToString(),
                        course_id = row.Cells["course_id"].Value?.ToString(),
                        course_code = row.Cells["course_code"].Value?.ToString(),
                        branch_name = row.Cells["branch_name"].Value?.ToString(),

                        institute_name =  row.Cells["institute_name"].Value?.ToString()

                    };

                   // bool printWithBg = false;
                    bool printWithBg = chkUseDefaultTemplate.Checked;
 

                // ✅ DIRECT PRINT WITHOUT SAVING PNG
                using (Bitmap cert = _processor.CreateCertificate(info, printWithBg))
                    {
                        PrintImageDirect(cert);
                    }

                // System.Windows.MessageBox.Show("Certificate Printed Successfully ✅");
                //ShowSuccess("Certificate Printed Successfully ✅");
                ToastControl.ShowToast(this,
                                    "Certificate printed successfully.",
                                    ToastControl.ToastType.Success,
                                    3);


                // ✅ UPDATE STATUS
                int requestId = Convert.ToInt32(
                        row.Cells["request_id"].Value
                    );

                    await UpdatePrintStatus(requestId);
                    await InsertCertificateAsync(info.SerialNumber, info.Name, info.Course, info.branch_name, info.course_id, info.ExamDate.ToString("yyyy-MM-dd"), info.Year);

                    // 🔄 GRID REFRESH
                    await LoadRequestedCertificates(); 
            }
            catch (Exception ex)
            {
                //System.Windows.Forms.MessageBox.Show("Print Error: " + ex.Message);
                ToastControl.ShowToast(this,
    "Print error: " + ex.Message,
    ToastControl.ToastType.Error,
    4);

                //ShowSuccess("Print Error ❌\n" + ex.Message);

            }
        }

        public async Task<string> InsertCertificateAsync(string certificate_no,string student_full_name,string course_name , string branch_name, string course_code, string examination_date, string exam_year)
        {
            try
            {
                string apiUrl =
              AppSession.ApiFolder + "insert_certificate_afterprint.php";

                var certificateData = new
                {
                    certificate_no = certificate_no,
                    student_full_name = student_full_name,
                    course_name = course_name,
                    branch_name = branch_name ,
                    course_code = course_code,
                    examination_date = examination_date,
                    exam_year = exam_year
                };

                string json = JsonConvert.SerializeObject(certificateData);

                using (HttpClient client = new HttpClient())
                {
                    var content = new StringContent(
                        json,
                        Encoding.UTF8,
                        "application/json"
                    );

                    HttpResponseMessage response =
                        await client.PostAsync(apiUrl, content);

                    string result =
                        await response.Content.ReadAsStringAsync();

                    return result;
                }
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        } 
        private async Task UpdatePrintStatus(int requestId)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var obj = new
                    {
                        request_id = requestId
                    };

                    string json = JsonConvert.SerializeObject(obj);

                    var content = new StringContent(
                        json,
                        Encoding.UTF8,
                        "application/json"
                    );

                    var response = await client.PostAsync(
                        AppSession.ApiFolder + "update_certificate_print_status.php",
                        content
                    );

                    string result = await response.Content.ReadAsStringAsync();

                    Console.WriteLine(result); // debug
                }
            }
            catch (Exception ex)
            {
                //System.Windows.MessageBox.Show("Status update error: " + ex.Message);
                ToastControl.ShowToast(this,
    "Status update failed: " + ex.Message,
    ToastControl.ToastType.Error,
    4);
            }
        } 

        private void PrintImageDirect(Image img )
        {
            using (PrintDocument pd = new PrintDocument())
            { 
                // ✅ Use Default Printer
              
                pd.DefaultPageSettings.Landscape = true;
                pd.DefaultPageSettings.Margins = new Margins(0, 0, 0, 0);

                pd.PrintPage += (sender, e) =>
                {
                    Rectangle m = e.PageBounds;
                    e.Graphics.DrawImage(img, m);
                };

                pd.Print();
            }
        } 

        private void ShowSuccess(string message)
        {
            using (SuccessPopup popup = new SuccessPopup(message))
            {
                popup.ShowDialog();
            }
        }


        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dgvrequested.Rows)
            {
                if (!row.IsNewRow)
                {
                    // We get the status of the row
                    string status = row.Cells["ReqStatus"].Value?.ToString();

                    if (status == "Approved")
                    {
                        // If already approved, keep it checked
                        row.Cells["Select"].Value = true;
                    }
                    else
                    {
                        // FIX: Use 'checkBox1.Checked' here! 
                        // This refers to the state of the checkbox you just clicked.
                        row.Cells["Select"].Value = checkBox1.Checked;
                    }
                }
            }
        } 
        private void CheckAllRows(bool shouldCheck)
        {
            foreach (DataGridViewRow row in dgvrequested.Rows)
            {
                if (!row.IsNewRow)
                {
                    row.Cells["Select"].Value = shouldCheck;
                }
            }
        }
        private void dgvrequested_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            // 2. Check if the "Select" column was the one clicked
            if (dgvrequested.Columns[e.ColumnIndex].Name == "PrintBtn")
            {
                // Get the current value (handle nulls safely)
                object val = dgvrequested.Rows[e.RowIndex].Cells["PrintBtn"].Value;
                bool isChecked = (val != null && (bool)val);

                if (isChecked)
                {
                    dgvrequested.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.LightBlue;
                }
                else
                {
                    dgvrequested.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.White;
                }
            }
        } 
 
        private void PrintCertificate(string imagePath, PrinterSettings settings)
        {
            try
            {
                PrintDocument pd = new PrintDocument();
                pd.PrinterSettings = settings;
                pd.DefaultPageSettings.Landscape = true;
                pd.DefaultPageSettings.Margins = new Margins(0, 0, 0, 0);

                bool a4Found = false;
                foreach (PaperSize size in pd.PrinterSettings.PaperSizes)
                {
                    if (size.Kind == PaperKind.A4)
                    {
                        pd.DefaultPageSettings.PaperSize = size;
                        a4Found = true;
                        break;
                    }
                }

                if (!a4Found)
                {
                    pd.DefaultPageSettings.PaperSize =
                        new PaperSize("A4 Custom", 827, 1169);
                }

                Image img = Image.FromFile(imagePath);

                pd.PrintPage += (s, e) =>
                {
                    Rectangle m = e.PageBounds;
                    e.Graphics.DrawImage(img, m);
                };

                pd.Print();

                // 🔹 IMPORTANT — Dispose after print
                img.Dispose();
                pd.Dispose();
            }
            catch (Exception ex)
            {
                // System.Windows.MessageBox.Show("Printing Error: " + ex.Message);
                ToastControl.ShowToast(this,
     "Printing failed: " + ex.Message,
     ToastControl.ToastType.Error,
     4);
            }
        } 
        private DateTime ConvertToDate(object cellValue)
        {
            if (cellValue != null && DateTime.TryParse(cellValue.ToString(), out DateTime result))
            {
                return result;
            }
            return DateTime.Now; // जर तारीख नसेल तर आजची तारीख डिफॉल्ट म्हणून
        }

        private readonly CertificateProcessor _processor = new CertificateProcessor();

         
        private void btnprint_Click(object sender, EventArgs e)
        {


            // 1. Save करण्यासाठी फोल्डर निवडा
            using (var fbd = new FolderBrowserDialog())
            {
                fbd.Description = "Select folder to save generated certificates";
                if (fbd.ShowDialog() != DialogResult.OK) return;

                string outputFolder = fbd.SelectedPath;

                // 2. [NEW] प्रिंटर निवडा (लूप सुरू होण्याआधी एकदाच)
                PrinterSettings selectedPrinterSettings = null;
                using (PrintDialog pd = new PrintDialog())
                {
                    pd.AllowSomePages = false;
                    pd.AllowSelection = false;
                    pd.UseEXDialog = true;

                    if (DialogResult.OK == pd.ShowDialog())
                    {
                        selectedPrinterSettings = pd.PrinterSettings;
                    }
                    else
                    {
                        // जर प्रिंटर निवडला नाही, तर प्रोसेस थांबवायची की फक्त सेव्ह करायचे?
                        // सध्या आपण फक्त सेव्ह करण्यासाठी पुढे जाऊया (Printing Skip होईल)
                        // System.Windows.MessageBox.Show("No printer selected. Certificates will be saved but NOT printed.");
                        ToastControl.ShowToast(this,
     "No printer selected. Files will be saved only.",
     ToastControl.ToastType.Warning,
     4);
                    }
                }

                int successCount = 0;
                int errorCount = 0;
                StringBuilder errorLog = new StringBuilder();

                try
                {
                    this.Cursor = Cursors.WaitCursor;

                    foreach (DataGridViewRow row in dgvrequested.Rows)
                    {
                        if (row.IsNewRow) continue;

                        // Checkbox check
                        bool isChecked = false;
                        if (row.Cells["Select"].Value != null)
                            isChecked = (bool)row.Cells["Select"].Value;

                        if (isChecked)
                        {
                            try
                            {

                                string baseUrl =
                               AppSession.BaseUrl +"student_photos/";
                                // 3. डेटा मॅपिंग
                                var info = new CertificateInfo
                                {
                                    Name = row.Cells["student_full_name"].Value?.ToString(),
                                    Center = row.Cells["branch_name"].Value?.ToString(),
                                    SerialNumber = row.Cells["marklist_code"].Value?.ToString(),
                                    Course = row.Cells["course_name"].Value?.ToString(),
                                    Grade = row.Cells["grade"].Value?.ToString(),
                                    Year = row.Cells["exam_year"].Value?.ToString(),
                                    ExamDate = ConvertToDate(row.Cells["examination_date"].Value),
                                    DateFrom = ConvertToDate(row.Cells["course_start_date"].Value),
                                    DateTo = ConvertToDate(row.Cells["course_end_date"].Value),
                                    PhotoPath = baseUrl + "" + row.Cells["branch_name"].Value?.ToString()+"_"+ row.Cells["student_id"].Value?.ToString()+".jpg",
                                    student_id = row.Cells["student_id"].Value?.ToString()

                                };
                                //bool printWithBg = false;
                                bool printWithBg = chkUseDefaultTemplate.Checked;
                                // 4. जनरेट आणि सेव्ह
                                string savedFilePath = "";
                                using (Bitmap cert = _processor.CreateCertificate(info, printWithBg))
                                {
                                    string safeName = info.Name.Replace(" ", "_");
                                    string fileName = $"{safeName}_{info.SerialNumber}.png";
                                    foreach (char c in Path.GetInvalidFileNameChars()) fileName = fileName.Replace(c, '_');

                                    savedFilePath = Path.Combine(outputFolder, fileName);
                                    cert.Save(savedFilePath, System.Drawing.Imaging.ImageFormat.Png);
                                }

                                // 5. [NEW] लगेच प्रिंट करा (Send to Printer)
                                if (selectedPrinterSettings != null && File.Exists(savedFilePath))
                                {
                                    PrintCertificate(savedFilePath, selectedPrinterSettings);
                                }

                                successCount++;
                            }
                            catch (Exception ex)
                            {
                                errorCount++;
                                string sName = row.Cells["student_full_name"].Value?.ToString() ?? "Unknown";
                                errorLog.AppendLine($"Failed for {sName}: {ex.Message}");
                            }
                        }
                    }

                    string message = $"Process Completed!\nSaved & Sent to Print: {successCount}\nFailed: {errorCount}";
                    if (errorCount > 0) message += $"\n\nErrors:\n{errorLog}";
                    //   System.Windows.MessageBox.Show(message, "Report", MessageBoxButtons.OK, errorCount > 0 ? MessageBoxIcon.Warning : MessageBoxIcon.Information);

                    ToastControl.ShowToast(this,
    $"Completed: {successCount} success, {errorCount} failed.",
    errorCount > 0 ? ToastControl.ToastType.Warning : ToastControl.ToastType.Success,
    4);
                }
                catch (Exception ex)
                {
                    // System.Windows.MessageBox.Show("Critical Error: " + ex.Message);
                    ToastControl.ShowToast(this,
     "Critical error: " + ex.Message,
     ToastControl.ToastType.Error,
     5);
                }
                finally
                {
                    this.Cursor = Cursors.Default;
                }
            }
        }

        private async void rdbPrinted_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbPrinted.Checked)
            {
                await LoadBranchList();
                await LoadRequestedCertificates();
            }
        }

        private async void rdbNotPrinted_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbNotPrinted.Checked)
            {
                await LoadBranchList();
                await LoadRequestedCertificates();
            }
        }

        private async void cmbBranchList_SelectedIndexChanged(object sender, EventArgs e)
        {
            await LoadRequestedCertificates();

        }

        private (string reqStatus, string printStatus)
    GetStatusFilter()
        {
            if (rdbPrinted.Checked)
            {
                return ("Done", "Printed");
            }
            else
            {
                return ("Requested", "Not Printed");
            }
        }

    }
}

    // ================= DATA MODELS =================
    public class RequestedCertificate
    {
        public int request_id { get; set; }

    //    public int? branch_id { get; set; }
        public string branch_name { get; set; }

        public int? student_id { get; set; }
        public string student_full_name { get; set; }
        public string mother_name { get; set; }
     //   public string gender { get; set; }
      //  public DateTime? dob { get; set; }
     //   public string email { get; set; }
     //   public string address { get; set; }
        public string student_photo { get; set; }

        public int? course_id { get; set; }
        public string course_name { get; set; }
     //   public string course_code { get; set; }
        public DateTime? course_start_date { get; set; }
        public DateTime? course_end_date { get; set; }

        public string teacher_name { get; set; }
        public string marklist_code { get; set; }

        public int max_mark { get; set; }
        public int obt_mark { get; set; }
        public string grade { get; set; }
        public string exam_center { get; set; }         
        public DateTime? examination_date { get; set; } // Matches DATE
        public string exam_year { get; set; }           // Matches VARCHAR (holds "2026")

    public string course_code { get; set; }
    public string ReqStatus { get; set; }
        public string PrintStatus { get; set; }

        public DateTime created_at { get; set; }


    public string institute_name { get; set; }

}

    public class ApiResponse<T>
    {
        public bool status { get; set; }
        public int count { get; set; }
        public List<T> data { get; set; }
    }

