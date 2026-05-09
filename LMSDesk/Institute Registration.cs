using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.AxHost;
using System.Text.RegularExpressions;

namespace LMSDesk
{
    public partial class Institute_Registration : Form
    {
        string license = AppSession.LicenseName;

        public Institute_Registration()
        {
            InitializeComponent();
            ApplyUI();
        }
        private void ApplyUI()
        {
            this.SuspendLayout();
            this.BackColor = Color.FromArgb(245, 247, 250);
            this.DoubleBuffered = true;

            // Main card
            panel1.BackColor = Color.White;

            // Header
            label13.BackColor = Color.FromArgb(64, 64, 64);
            label13.ForeColor = Color.White;
            label13.Font = new Font("Segoe UI", 13, FontStyle.Bold);

            StyleInputs(this);
           // StyleButton(btnRegister1, Color.FromArgb(0, 120, 215));

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

                // ✅ GroupBox (clean like Student)
                else if (c is GroupBox grp)
                {
                    grp.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
                    grp.ForeColor = Color.Black;
                    grp.BackColor = Color.Transparent;
                }

                if (c.HasChildren)
                    StyleInputs(c);
            }
        }

        private void StyleButton(Button btn, Color bg)
        {
            btn.BackColor = bg;
            btn.ForeColor = Color.White;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            btn.Height = 36;
            btn.Cursor = Cursors.Hand;
        }

        public bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email)) return false;
            // Standard email pattern
            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, pattern, RegexOptions.IgnoreCase);
        }
        private void btnRegister1_Click(object sender, EventArgs e)
        {
            if (!IsValidEmail(txtEmail.Text))
            {
                MessageBox.Show("Invalid Email ID! Please use name@domain.com", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmail.Focus();
                return; // Stop here if email is wrong
            }
            SaveInstituteDetails();
        }
        private async void SaveInstituteDetails()
        {
            // 🔹 Validation (same standard as Student)
            if (string.IsNullOrWhiteSpace(txtInstituteName.Text) ||
                string.IsNullOrWhiteSpace(txtInstituteCode.Text) ||
                comboInstituteType.SelectedIndex == -1 ||
                dtpEstablishedDate.Value.Date > DateTime.Now.Date ||
               // numericudCourses.Value <= 0 ||
                string.IsNullOrWhiteSpace(txtEmail.Text) ||
                string.IsNullOrWhiteSpace(txtMobileNumber.Text) ||
                string.IsNullOrWhiteSpace(txtAddress.Text) ||
                string.IsNullOrWhiteSpace(txtWebsite.Text) ||
                string.IsNullOrWhiteSpace(txtCity.Text) ||
                cmbState.SelectedIndex == -1 ||
                string.IsNullOrWhiteSpace(txtPincode.Text))

            {
                ValidationPopup popup = new ValidationPopup(
                    "⚠️ Please fill all required fields."
                );
                popup.Show();
                return;
            }

            // 🔹 Prepare institute object
            var institute = new
            {
                institute_name = txtInstituteName.Text.Trim(),
                institute_code = txtInstituteCode.Text.Trim(),
                institute_type = comboInstituteType.Text,
                established_date = dtpEstablishedDate.Value.ToString("yyyy-MM-dd"),
                number_of_courses = (int)numericudCourses.Value,
                email = txtEmail.Text.Trim(),
                mobile_number = txtMobileNumber.Text.Trim(),
                address = txtAddress.Text.Trim(),
                website = txtWebsite.Text.Trim(),
                city = txtCity.Text.Trim(),
                state = cmbState.Text,
                pincode = txtPincode.Text.Trim()
            };

            string json = JsonConvert.SerializeObject(institute);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response =
                        await client.PostAsync(AppSession.ApiFolder + "institute.php?license=" +license, content);

                    string result = await response.Content.ReadAsStringAsync();

                    if (string.IsNullOrWhiteSpace(result))
                    {
                        MessageBox.Show("API returned empty response",
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    if (!result.TrimStart().StartsWith("{"))
                    {
                        MessageBox.Show("API returned invalid response:\n\n" + result,
                            "Server Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    ApiResponse apiResponse =
                        JsonConvert.DeserializeObject<ApiResponse>(result);

                    if (apiResponse.status)
                    {
                        SuccessPopup popup =
                            new SuccessPopup("Register Saved Successfully ✅");

                        popup.Show();

                        ClearForm();
                    }
                    else
                    {
                        //MessageBox.Show(apiResponse.message,
                        //    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("API Error: " + ex.Message,
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }



        private void ClearForm()
        {
            txtInstituteName.Clear();
            txtInstituteCode.Clear();
            comboInstituteType.SelectedIndex = -1;
            dtpEstablishedDate.Value = DateTime.Now;
            txtEmail.Clear();
            txtMobileNumber.Clear();
            txtAddress.Clear();
            txtWebsite.Clear();
            txtCity.Clear();
            cmbState.SelectedIndex = -1;
            txtPincode.Clear();
            numericudCourses.Value = numericudCourses.Minimum;
        }

                                                                                                                                                                                

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void dtpEstablishedDate_ValueChanged(object sender, EventArgs e)
        {

        }

        private void Institute_Registration_Load(object sender, EventArgs e)
        {
            cmbState.SelectedIndex = 0;
        }

        private void txtInstituteName_TextChanged(object sender, EventArgs e)
        {

        }

      
        private void txtCity_TextChanged(object sender, EventArgs e)
        {

        }

       
        private void cmbState_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
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
                                                 panel1.Width - borderThickness,
                                                 panel1.Height - borderThickness);

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
                    panel1.Region = new Region(path);
                }
            }

        }

        private void txtInstituRegistration_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

public class InstituteModel
{
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
