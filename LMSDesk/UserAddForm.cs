using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net.Http;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LMSDesk
{
 public partial class UserAddForm : Form
    {

        string license = AppSession.LicenseName;
        public UserAddForm()
          {
            InitializeComponent();
            this.Load += UserAddForm_Load;
            ApplyUI();
            StyleButton(btnSave, Color.FromArgb(32, 64, 128));
            StyleButton(btnClear, Color.FromArgb(108, 117, 125));

            StyleGroupBox(groupBox1);
        }
        private void ApplyUI()
        {
            this.SuspendLayout();
            this.BackColor = Color.FromArgb(245, 247, 250);
            this.DoubleBuffered = true;
            label1.BackColor = Color.FromArgb(64, 64, 64);
            label1.ForeColor = Color.White;
            label1.Font = new Font("Segoe UI", 13, FontStyle.Bold);

            StyleInputs(this);
            this.ResumeLayout();
        }
        private void StyleInputs(Control parent)
        {
            foreach (Control c in parent.Controls)
            {
                // ✅ TEXTBOX
                if (c is TextBox txt)
                {
                    txt.Font = new Font("Segoe UI", 10);
                    txt.BorderStyle = BorderStyle.FixedSingle;
                    txt.BackColor = Color.White;
                    txt.ForeColor = Color.Black;
                }
        // ✅ NUMERIC
                else if (c is NumericUpDown num)
                {
                    num.Font = new Font("Segoe UI", 10);
                    num.BackColor = Color.White;
                    num.ForeColor = Color.Black;
                }

                // ✅ DATE
                else if (c is DateTimePicker dtp)
                {
                    dtp.Font = new Font("Segoe UI", 10);
                    dtp.CalendarForeColor = Color.Black;
                    dtp.CalendarMonthBackground = Color.White;
                }
              else if (c is RadioButton rb)
                {
                    rb.Font = new Font("Segoe UI", 9.5f);
                    rb.ForeColor = Color.Black;
                    rb.BackColor = Color.Transparent;
                    rb.AutoSize = true;
                }

                // ✅ CheckBox
                else if (c is CheckBox chk)
                {
                    chk.Font = new Font("Segoe UI", 9.5f);
                    chk.ForeColor = Color.Black;
                    chk.BackColor = Color.Transparent;
                    chk.AutoSize = true;
                }
              if (c.HasChildren)
                    StyleInputs(c);
            }
        }
        private void StyleGroupBox(GroupBox grp)
        {
            grp.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            grp.ForeColor = Color.FromArgb(32, 64, 128);
            grp.BackColor = Color.White;
        }
        private void StyleButton(Button btn, Color bg)
        {
            btn.BackColor = bg;
            btn.ForeColor = Color.White;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            btn.Height = 34;
            btn.Width = 63;
            btn.Cursor = Cursors.Hand;
        }
        private async void UserAddForm_Load(object sender, EventArgs e)
        {
            await LoadInstitutes();
        }

        // ---------------- LOAD INSTITUTES ----------------
        private async Task LoadInstitutes()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string response = await client.GetStringAsync(
                        AppSession.ApiFolder + "getInstitute.php?license=" + license); // Replace with your actual API URL

                    var apiResult = JsonConvert.DeserializeObject<InstituteApiResponse>(response);

                    if (apiResult == null || !apiResult.status || apiResult.data == null || apiResult.data.Count == 0)
                    {
                        MessageBox.Show("No institutes found or API returned invalid data.");
                        cmbInstitute.DataSource = null;
                        return;
                    }

                    // Bind to ComboBox
                    cmbInstitute.DataSource = apiResult.data;
                    cmbInstitute.DisplayMember = "institute_name";
                    cmbInstitute.ValueMember = "institute_id";
                    cmbInstitute.SelectedIndex = -1; // select first by default
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading institutes: " + ex.Message);
                cmbInstitute.DataSource = null;
            }
        }
      
        // ---------------- SAVE USER ----------------
        private async void btnSave_Click_1(object sender, EventArgs e)
        {
            try
            {
                // ================= VALIDATE FIELDS =================
                if (cmbInstitute.Items.Count == 0 || cmbInstitute.SelectedIndex == -1)
                {
                    MessageBox.Show("Please select an institute.");
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtFullName.Text))
                {
                    MessageBox.Show("Please enter full name.");
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtUsername.Text))
                {
                    MessageBox.Show("Please enter username.");
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtMobile.Text))
                {
                    MessageBox.Show("Please enter mobile number.");
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtPassword.Text))
                {
                    MessageBox.Show("Please enter password.");
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtConfirmPassword.Text))
                {
                    MessageBox.Show("Please confirm password.");
                    return;
                }

                if (!rdbActive.Checked && !rdbInactive.Checked)
                {
                    MessageBox.Show("Please select status (Active/Inactive).");
                    return;
                }

                if (txtPassword.Text != txtConfirmPassword.Text)
                {
                    MessageBox.Show("Passwords do not match.");
                    return;
                }

                // ================= PREPARE DATA =================
                int statusValue = rdbActive.Checked ? 1 : 0;

                var user = new
                {
                    institute_id = Convert.ToInt32(cmbInstitute.SelectedValue),
                    full_name = txtFullName.Text.Trim(),
                    username = txtUsername.Text.Trim(),
                    mobile_number = txtMobile.Text.Trim(),
                    password = txtPassword.Text.Trim(),
                    status = statusValue
                };

                string json = JsonConvert.SerializeObject(user);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                // ================= SEND API REQUEST =================
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = await client.PostAsync(
                        AppSession.ApiFolder + "AddUsers.php?license=" +license, content); // Replace with your API URL

                    string result = await response.Content.ReadAsStringAsync();

                    if (string.IsNullOrWhiteSpace(result))
                    {
                        MessageBox.Show("API returned empty response.");
                        return;
                    }

                    var apiResponse = JsonConvert.DeserializeObject<ApiResponse>(result);

                    if (apiResponse == null)
                    {
                        MessageBox.Show("Failed to parse API response.");
                        return;
                    }

                    if (apiResponse.status)
                    {
                        MessageBox.Show("User added successfully!");
                        ClearForm();
                    }
                    else
                    {
                        MessageBox.Show(apiResponse.message ?? "Failed to add user.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving user: " + ex.Message);
            }
        }

        // ---------------- CLEAR FORM ----------------
        private void btnClear_Click_1(object sender, EventArgs e)
        {
            ClearForm();
        }
        private void ClearForm()
        {
            cmbInstitute.SelectedIndex = -1;
            txtFullName.Clear();
            txtUsername.Clear();
            txtMobile.Clear();
            txtPassword.Clear();
            txtConfirmPassword.Clear();
            rdbActive.Checked = false;
            rdbInactive.Checked = false;
        }

        // ================= CLASSES =================
        public class ApiResponse
        {
            public bool status { get; set; }
            public string message { get; set; }
        }

        public class InstituteItem
        {
            [JsonProperty("institute_id")]
            public int institute_id { get; set; }

            [JsonProperty("institute_name")]
            public string institute_name { get; set; }
        }

        public class InstituteApiResponse
        {
            public bool status { get; set; }
            public List<InstituteItem> data { get; set; }
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

        private void cmbInstitute_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
