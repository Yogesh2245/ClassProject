using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LMSDesk
{
    public partial class InstituteList : Form
    { 
        private List<Institute> InstituteRecords = new List<Institute>();

        string license = AppSession.LicenseName;

        public InstituteList()
        {
            InitializeComponent();
          
            this.Load += InstituteList_Load;
        }

        private async void InstituteList_Load(object sender, EventArgs e)
        {
            await LoadInstitutes();
        }

        private async Task LoadInstitutes()
        {
            try
            {
                string apiUrl = AppSession.ApiFolder +"getInstitute.php?license="+license;

                using (HttpClient client = new HttpClient())
                {
                    client.Timeout = TimeSpan.FromSeconds(30);

                    HttpResponseMessage response = await client.GetAsync(apiUrl);

                    if (!response.IsSuccessStatusCode)
                    {
                        MessageBox.Show($"API Error: {(int)response.StatusCode}");
                        return;
                    }

                    string json = await response.Content.ReadAsStringAsync();

                    InstituteApiResponse apiResponse =
                        JsonConvert.DeserializeObject<InstituteApiResponse>(json);


                    if (apiResponse != null && apiResponse.status)
                    {
                        // Save to global list
                        InstituteRecords = apiResponse.data;

                        // Bind to Grid
                        dataGridView3.AutoGenerateColumns = true;
                        dataGridView3.DataSource = null; // Clear existing
                        dataGridView3.DataSource = InstituteRecords;

                        // Apply header formatting
                        SetInstituteGridHeaders();
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
                    "Error loading institutes:\n" + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void SetInstituteGridHeaders()
        {
            // 1. DATA PROTECTION (Read-Only)
            dataGridView3.ReadOnly = true;
            dataGridView3.AllowUserToAddRows = false;
            dataGridView3.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView3.BorderStyle = BorderStyle.None;
            dataGridView3.BackgroundColor = Color.White;

            // 2. THE FIX: PREVENT COMPRESSED HEADERS
            // 'AllCells' ensures columns are wide enough for the full Header text.
            dataGridView3.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

            // Ensure scrollbars appear so you can scroll right to see all data
            dataGridView3.ScrollBars = ScrollBars.Both;
            dataGridView3.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(32, 64, 128);
            dataGridView3.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridView3.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);

            dataGridView3.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            dataGridView3.DefaultCellStyle.SelectionBackColor = Color.FromArgb(220, 230, 250);
            dataGridView3.DefaultCellStyle.SelectionForeColor = Color.Black;

            dataGridView3.RowHeadersVisible = false;
            dataGridView3.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 247, 250);

            // 3. HEADER DESIGN & STYLING
            // dataGridView3.EnableHeadersVisualStyles = false;
            // DataGridViewCellStyle headerStyle = new DataGridViewCellStyle();
            // headerStyle.BackColor = System.Drawing.Color.FromArgb(45, 45, 45); // Professional Dark Gray
            //headerStyle.ForeColor = System.Drawing.Color.White;
            // headerStyle.Font = new System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Bold);
            // headerStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            // dataGridView3.ColumnHeadersDefaultCellStyle = headerStyle;
            //dataGridView3.ColumnHeadersHeight = 45;

            Rename("institute_id", "Institute ID");
            Rename("institute_name", "Institute Name");
            Rename("institute_code", "Institute Code");
            Rename("institute_type", "Institute Type");
            Rename("established_date", "Established Date");
            Rename("number_of_courses", "Courses");
            Rename("email", "Email");
            Rename("mobile_number", "Mobile");
            Rename("address", "Address");
            Rename("website", "Website");
            Rename("city", "City");
            Rename("state", "State");
            Rename("pincode", "Pincode");

         //   dataGridView3.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        


            // Optional
            
        }

        private void Rename(string column, string header)
        {
            if (dataGridView3.Columns.Contains(column))
                dataGridView3.Columns[column].HeaderText = header;
        }

        private void InstituteList_Load_1(object sender, EventArgs e)
        {

        }
    }

    // 🔹 API response
    public class InstituteApiResponse
    {
        public bool status { get; set; }
        public List<Institute> data { get; set; }
    }

    // 🔹 Institute model
    public class Institute
    {
        public int institute_id { get; set; }
        public string institute_name { get; set; }
        public string institute_code { get; set; }
        public string institute_type { get; set; }
        public string established_date { get; set; }
        public int number_of_courses { get; set; }
        public string email { get; set; }
        public string mobile_number { get; set; }
        public string address { get; set; }
        public string website { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string pincode { get; set; }
    }
}
