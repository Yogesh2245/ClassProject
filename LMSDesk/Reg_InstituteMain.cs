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
    public partial class Reg_InstituteMain : Form
    {

        private readonly string baseUrl = "https://gbep.in/api/";

        public Reg_InstituteMain()
        {
            InitializeComponent();
            this.Load += (s, e) => {
                LoadData();
                btnUpdate.Visible = false; // सुरुवातीला Update बटन लपवा
                btnSave.Visible = true;    // Save बटन दाखवा
            };
            txtInstituteCode.ReadOnly = true;
        }

        private async Task LoadData()
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var response = await client.GetAsync(baseUrl + "get_institutes.php");
                    var result = await response.Content.ReadAsStringAsync();

                    if (!result.Trim().StartsWith("{")) return;

                    dynamic res = JsonConvert.DeserializeObject(result);
                    if (res.status == true)
                    {
                        dataGridView1.DataSource = res.data.ToObject<DataTable>();

                        if (!dataGridView1.Columns.Contains("btnEdit"))
                        {
                            DataGridViewButtonColumn btnEdit = new DataGridViewButtonColumn();
                            btnEdit.Name = "btnEdit";
                            btnEdit.HeaderText = "Action";
                            btnEdit.Text = "Edit";
                            btnEdit.UseColumnTextForButtonValue = true;
                            dataGridView1.Columns.Add(btnEdit);
                        }

                        // जर आपण Save मोडमध्ये असू तरच नवीन कोड दाखवा
                        if (btnSave.Visible == true)
                        {
                            txtInstituteCode.Text = res.next_code;
                        }
                    }
                }
                catch (Exception ex) { }
            }
        }

        // --- सर्व फील्ड्स क्लियर करणे ---
        private void ClearFields()
        {
            txtInstName.Clear();
            txtAddress.Clear();
            txtCity.Clear();
            txtDistrict.Clear();
            txtDirector.Clear();
            txtMobile.Clear();
            txtEmail.Clear();
            txtYear.Clear();

            for (int i = 0; i < clbFacilities.Items.Count; i++)
                clbFacilities.SetItemChecked(i, false);

            cmbInstType.SelectedIndex = -1;
            dtpRegDate.Value = DateTime.Now;

            // बटनांची स्थिती पूर्ववत करा
            btnSave.Visible = true;
            btnUpdate.Visible = false;
        }

        

        private void Reg_InstituteMain_Load(object sender, EventArgs e)
        {
            LoadData();
        }

      

        private async void btnUpdate_Click(object sender, EventArgs e)
        {
            await ProcessRequest("update_institute.php", "अपडेट");
        }

        // --- कॉमन मेथड (Save आणि Update साठी) ---
        private async Task ProcessRequest(string apiUrl, string actionName)
        {
            List<string> selectedFacilities = new List<string>();
            foreach (var item in clbFacilities.CheckedItems)
            {
                selectedFacilities.Add(item.ToString());
            }

            var formData = new
            {
                institute_code = txtInstituteCode.Text,
                institute_name = txtInstName.Text,
                full_address = txtAddress.Text,
                city_taluka = txtCity.Text,
                district = txtDistrict.Text,
                director_name = txtDirector.Text,
                mobile_number = txtMobile.Text,
                email_id = txtEmail.Text,
                establishment_year = txtYear.Text,
                facilities = string.Join(", ", selectedFacilities),
                institute_type = cmbInstType.SelectedItem?.ToString() ?? "Other",
                registration_date = dtpRegDate.Value.ToString("yyyy-MM-dd")
            };

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var json = JsonConvert.SerializeObject(formData);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    var response = await client.PostAsync(baseUrl + apiUrl, content);
                    var result = await response.Content.ReadAsStringAsync();

                    dynamic res = JsonConvert.DeserializeObject(result);
                    if (res.status == true)
                    {
                        MessageBox.Show(actionName + " यशस्वी झाले!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ClearFields();
                        await LoadData();
                    }
                    else
                    {
                        MessageBox.Show("त्रुटी: " + res.message);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Connection Error: " + ex.Message);
                }
            }
        }

        private async void btnSave_Click_1(object sender, EventArgs e)
        {
            await ProcessRequest("register_institute_MAIN.php", "नोंदणी");
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dataGridView1.Columns[e.ColumnIndex].Name == "btnEdit")
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                txtInstituteCode.Text = row.Cells["institute_code"].Value?.ToString();
                txtInstName.Text = row.Cells["institute_name"].Value?.ToString();
                txtDirector.Text = row.Cells["director_name"].Value?.ToString();
                txtMobile.Text = row.Cells["mobile_number"].Value?.ToString();
                txtCity.Text = row.Cells["city_taluka"].Value?.ToString();
                txtDistrict.Text = row.Cells["district"].Value?.ToString();

                // हे नवीन ॲड करा (जर SELECT * वापरला असेल तर हे वर्क होईल)
                txtAddress.Text = row.Cells["full_address"].Value?.ToString();
                txtEmail.Text = row.Cells["email_id"].Value?.ToString();
                txtYear.Text = row.Cells["establishment_year"].Value?.ToString();

                btnSave.Visible = false;
                btnUpdate.Visible = true;
            }
        }
    }
}
