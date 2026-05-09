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
    public partial class marklistprint : Form
    {
        string license = AppSession.LicenseName;

        public marklistprint()
        {
            InitializeComponent();
            //dgvmark.CellContentClick += dgvmark_CellContentClick;
            dgvmark.CellMouseEnter += dgvmark_CellMouseEnter;
            dgvmark.CellMouseLeave += dgvmark_CellMouseLeave;
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

        private async  void marklistprint_Load(object sender, EventArgs e)
        {
            InitLoader();

            await LoadBranchList();
            await LoadMarklistDetails();
            //SetupGrid();
          
        }
        private void SetupGrid()
        {
            // ❗ Already exists check
            if (dgvmark.Columns.Contains("Print"))
                return;

            DataGridViewButtonColumn printColumn =
                new DataGridViewButtonColumn();

            printColumn.Name = "Print";
            printColumn.HeaderText = "";
            printColumn.Text = "Print";
            printColumn.UseColumnTextForButtonValue = true;
            printColumn.Width = 70;

            dgvmark.Columns.Insert(0, printColumn);
        }

        private void dgvmark_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                if (dgvmark.Columns[e.ColumnIndex].Name == "Print")
                {
                    dgvmark.Cursor = Cursors.Hand;
                }
                else
                {
                    dgvmark.Cursor = Cursors.Default;
                }
            }
        }

        private void dgvmark_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            dgvmark.Cursor = Cursors.Default;
        } 
        private void dgvmark_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dgvmark.IsCurrentCellDirty)
            {
                // This forces the checkbox to 'commit' the change as soon as you click it
                dgvmark.CommitEdit(DataGridViewDataErrorContexts.Commit);
                // Add this inside your SetupGrid method
                dgvmark.CurrentCellDirtyStateChanged += dgvmark_CurrentCellDirtyStateChanged;
            } 
        }
        /* private async Task LoadMarklistDetails()
         {
             try
             {
                 dgvmark.DataSource = null;
                 dgvmark.Columns.Clear();              // ✅ VERY IMPORTANT
                 dgvmark.AutoGenerateColumns = true;   // ✅ MUST BE BEFORE DATASOURCE


                 using (HttpClient client = new HttpClient())
                 {
                     var content = new StringContent("{}", Encoding.UTF8, "application/json");

                     var response = await client.PostAsync(
                        AppSession.ApiFolder + "get_marklistdetails.php?license=" +license,
                         content
                     );

                     string json = await response.Content.ReadAsStringAsync();

                     var result = JsonConvert.DeserializeObject<ApiResponse<MarkListDetail>>(json);

                     if (result != null && result.status)
                     {
                         dgvmark.DataSource = result.data;

                         SetupMarklisdetails(); // ✅ AFTER DATASOURCE

                         SetupGrid();
                     }
                 }
             }
             catch (Exception ex)
             {
                 MessageBox.Show("Error: " + ex.Message);
             }
         }*/



        private async Task LoadMarklistDetails()
        {
            try
            {
                ShowLoader();   // 🔹 START LOADER

                //dgvmark.DataSource = null;
                //dgvmark.Columns.Clear();
                //dgvmark.AutoGenerateColumns = true;


                dgvmark.DataSource = null;

                // Prevent flicker + duplicate
                if (dgvmark.Columns.Count > 0)
                    dgvmark.Columns.Clear();

                dgvmark.AutoGenerateColumns = true;


                string branch = "";
                string printStatus = "";

                if (cmbBranchList.SelectedIndex > 0)
                    branch = cmbBranchList.Text;

                if (rdbPrinted.Checked)
                    printStatus = "Printed";
                else if (rdbNotPrinted.Checked)
                    printStatus = "Not Printed";

                var postData = new
                {
                    branch_name = branch,
                    PrintStatus = printStatus
                };

                string jsonPost =
                    JsonConvert.SerializeObject(postData);

                using (HttpClient client = new HttpClient())
                {
                    var content = new StringContent(
                        jsonPost,
                        Encoding.UTF8,
                        "application/json"
                    );

                    var response = await client.PostAsync(
                        AppSession.ApiFolder +
                        "get_marklistdetails.php?license=" + license,
                        content
                    );

                    string json =
                        await response.Content.ReadAsStringAsync();

                    var result =
                        JsonConvert.DeserializeObject<
                        ApiResponse<MarkListDetail>>(json);

                    if (result != null && result.status)
                    {
                        dgvmark.DataSource = result.data;

                        SetupMarklisdetails();
                        SetupGrid();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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
                ShowLoader();
                using (HttpClient client = new HttpClient())
                {
                    var postData = new
                    {
                        branch_list = true
                    };

                    string jsonPost =
                        JsonConvert.SerializeObject(postData);

                    var content = new StringContent(
                        jsonPost,
                        Encoding.UTF8,
                        "application/json"
                    );

                    var response = await client.PostAsync(
                        AppSession.ApiFolder +
                        "get_marklistdetails.php?license=" + license,
                        content
                    );

                    string json =
                        await response.Content.ReadAsStringAsync();

                    var result =
                        JsonConvert.DeserializeObject<
                        ApiResponse<BranchModel>>(json);

                    if (result != null && result.status)
                    {
                        cmbBranchList.Items.Clear();

                        cmbBranchList.Items.Add("All Branch");

                        foreach (var item in result.data)
                        {
                            cmbBranchList.Items.Add(item.Branch_name);
                        }

                        cmbBranchList.SelectedIndex = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                HideLoader();   // 🔹 STOP LOADER
            }
        }

        public class BranchModel
        {
            public string Branch_name { get; set; }
        }

        private void SetupMarklisdetails()
        {
            dgvmark.ReadOnly = false;
            dgvmark.AllowUserToAddRows = false;
            dgvmark.SelectionMode = DataGridViewSelectionMode.CellSelect;
            dgvmark.MultiSelect = false;

            // ✅ Prevent header text cutting
            dgvmark.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvmark.ScrollBars = ScrollBars.Both;

            // ✅ Clean grid look
            dgvmark.BorderStyle = BorderStyle.None;
            dgvmark.BackgroundColor = Color.White;
            dgvmark.GridColor = Color.FromArgb(230, 230, 230);

            // ---------- HEADER STYLE ----------
            dgvmark.EnableHeadersVisualStyles = false;
            dgvmark.ColumnHeadersHeight = 45;

            dgvmark.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(32, 64, 128);
            dgvmark.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvmark.ColumnHeadersDefaultCellStyle.Font =
                new Font("Segoe UI", 10, FontStyle.Bold);
            dgvmark.ColumnHeadersDefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleLeft;

            // ---------- ROW STYLE ----------
            dgvmark.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            dgvmark.DefaultCellStyle.SelectionBackColor =
                Color.FromArgb(220, 230, 250);
            dgvmark.DefaultCellStyle.SelectionForeColor = Color.Black;

            dgvmark.AlternatingRowsDefaultCellStyle.BackColor =
                Color.FromArgb(245, 247, 250);

            dgvmark.RowHeadersVisible = false;

            // ---------- COLUMN READONLY (LOGIC SAME AS YOURS) ----------
            foreach (DataGridViewColumn col in dgvmark.Columns)
            {
                if (col.Name == "Select")
                {
                    col.ReadOnly = false;   // ✔ checkbox works same
                    col.Width = 60;         // UI only
                }
                else
                {
                    col.ReadOnly = true;    // ✔ unchanged
                }
            }


            SetHeader("MarkListTrNumber", "Tr. No");
            SetHeader("Marklist_id", "Marklist ID");
            SetHeader("Marklist_Code", "Marklist Code");

            SetHeader("student_id", "Student ID");
            SetHeader("student_name", "Student Name");

            SetHeader("course_name", "Course Name");
            SetHeader("mother_name", "Mother Name");

            SetHeader("duration", "Duration");

            SetHeader("examination_date", "Examination Date");
            SetHeader("exam_center", "Exam Center");

            SetHeader("institute_name", "Institute Name");
            SetHeader("Branch_name", "Branch Name");

            SetHeader("admin_name", "Admin Name");
            SetHeader("remark_note", "Remarks");

            SetHeader("created_at", "Created Date");

            SetHeader("PrintStatus", "Print Status");

            SetHeader("course_code", "Course Code");
            SetHeader("course_id", "Course Id");
            




            if (dgvmark.Columns.Contains("examination_date"))
            {
                dgvmark.Columns["examination_date"].DefaultCellStyle.Format = "dd-MM-yyyy";
            }
            ///
            if (dgvmark.Columns.Contains("created_at"))
                dgvmark.Columns["created_at"].DefaultCellStyle.Format = "dd-MM-yyyy hh:mm tt";

            if (dgvmark.Columns.Contains("examination_date"))
                dgvmark.Columns["examination_date"].Visible = false;

            if (dgvmark.Columns.Contains("exam_center"))
                dgvmark.Columns["exam_center"].Visible = false;

            if (dgvmark.Columns.Contains("institute_name"))
                dgvmark.Columns["institute_name"].Visible = false;

         //   if (dgvmark.Columns.Contains("created_at"))
              //  dgvmark.Columns["created_at"].Visible = false; 
            if (dgvmark.Columns.Contains("student_id"))
                dgvmark.Columns["student_id"].Visible = false;

            if (dgvmark.Columns.Contains("duration"))
                dgvmark.Columns["duration"].Visible = false;


            if (dgvmark.Columns.Contains("mother_name"))
                dgvmark.Columns["mother_name"].Visible = false;

            if (dgvmark.Columns.Contains("MarkListTrNumber"))
                dgvmark.Columns["MarkListTrNumber"].Visible = false;   // Tr.No

            if (dgvmark.Columns.Contains("Marklist_Code"))
                dgvmark.Columns["Marklist_Code"].Visible = false;      // MarkList Code

            if (dgvmark.Columns.Contains("student_id"))
                dgvmark.Columns["student_id"].Visible = false;         // Student Id

            if (dgvmark.Columns.Contains("admin_name"))
                dgvmark.Columns["admin_name"].Visible = false;         // Admin Name

            if (dgvmark.Columns.Contains("remark_note"))
                dgvmark.Columns["remark_note"].Visible = false;        // Remarks


            if (dgvmark.Columns.Contains("course_code"))
                dgvmark.Columns["course_code"].Visible = false;

            if (dgvmark.Columns.Contains("course_id"))
                dgvmark.Columns["course_id"].Visible = false;

             

            if (dgvmark.Columns.Contains("PrintStatus"))
            {
                dgvmark.Columns["PrintStatus"].Width = 110;
            }

        }
        private void SetHeader(string columnName, string headerText)
        {
            if (dgvmark.Columns.Contains(columnName))
            {
                dgvmark.Columns[columnName].HeaderText = headerText;
            }
        }  
        
        private async void dgvmark_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            // Check if Print button clicked
            /* if (dgvmark.Columns[e.ColumnIndex].Name == "Print")
             {
                 string marklisid = dgvmark.Rows[e.RowIndex]
                     .Cells["Marklist_id"].Value?.ToString();


                 FinalMarklistPrint pfm = new FinalMarklistPrint();
                 pfm.marklisid.Text = marklisid;
                 pfm.SelectedMarklistId = marklisid;   
                 pfm.ShowDialog();
             }*/

            if (dgvmark.Columns[e.ColumnIndex].Name == "Print")
            {
                string marklisid =
                    dgvmark.Rows[e.RowIndex]
                    .Cells["Marklist_id"].Value?.ToString();


                string course_id =
       dgvmark.Rows[e.RowIndex]
       .Cells["course_id"].Value?.ToString();



                string course_code =
  dgvmark.Rows[e.RowIndex]
  .Cells["course_code"].Value?.ToString();

                FinalMarklistPrint pfm =
                    new FinalMarklistPrint();
                pfm.lblcoursecode.Text = course_code;
                pfm.lblCourseId.Text = course_id;
                pfm.SelectedMarklistId = marklisid;

                pfm.ShowDialog();

                // 🔄 GRID REFRESH AFTER CLOSE
                await LoadMarklistDetails();
            }
        }
      public class MarkListDetail
        {
            public int MarkListTrNumber { get; set; }
            public string Marklist_id { get; set; }
            public string Marklist_Code { get; set; }
            public int student_id { get; set; }
            public string student_name { get; set; }
            public string course_name { get; set; }
           
            [JsonProperty("mother_name")]
            public string Mother_name { get; set; }

            public string duration { get; set; }
            public DateTime? examination_date { get; set; }
            public string exam_center { get; set; }
            public string institute_name { get; set; }
            public string Branch_name { get; set; }
            public string admin_name { get; set; }
            public string remark_note { get; set; }

            public string PrintStatus { get; set; }
            
            public DateTime created_at { get; set; }


            public string course_id { get; set; }

            public string course_code { get; set; }

        }

        private async void cmbBranchList_SelectedIndexChanged(object sender, EventArgs e)
        {
            await LoadMarklistDetails();

        }

        private async void rdbPrinted_CheckedChanged(object sender, EventArgs e)
        {
            await LoadMarklistDetails();
        }

        private async void rdbNotPrinted_CheckedChanged(object sender, EventArgs e)
        {
            await LoadMarklistDetails();

        }
    }
    } 