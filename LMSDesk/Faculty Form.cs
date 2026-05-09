
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
using System.Text.RegularExpressions;

namespace LMSDesk
{
    public partial class Faculty_Form : Form
    {

        string license = AppSession.LicenseName;

        public Faculty_Form()
        {
            InitializeComponent();
            ApplyUI();
         
            StyleButton(btnSave, Color.FromArgb(32, 64, 128));
        }

        private void ApplyUI()
        {
            this.SuspendLayout();
            this.BackColor = Color.FromArgb(245, 247, 250);

            label14.BackColor = Color.FromArgb(64, 64, 64);
            label14.ForeColor = Color.White;
            label14.Font = new Font("Segoe UI", 13, FontStyle.Bold);

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

                // ✅ COMBOBOX
                else if (c is ComboBox cmb)
                {
                    cmb.Font = new Font("Segoe UI", 10);
                    cmb.FlatStyle = FlatStyle.Flat;
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
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!IsValidEmail(txtEmail_Id.Text))
            {
                MessageBox.Show("Invalid Email ID! Please use name@domain.com", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmail_Id.Focus();
                return; // Stop here if email is wrong
            }

            SaveFacultyDetails();

        }

        private async void SaveFacultyDetails()
        {
            // 🔹 Validation (same quality as Student)
            if (string.IsNullOrWhiteSpace(txtTeacherName.Text) ||
                (!rdbMale.Checked && !rdbFemale.Checked) ||
                string.IsNullOrWhiteSpace(txtContact.Text) ||
                string.IsNullOrWhiteSpace(txtEmail_Id.Text) ||
                string.IsNullOrWhiteSpace(txtAddress.Text) ||
                (!cbUnmarried.Checked && !cbMarried.Checked) ||
                string.IsNullOrWhiteSpace(txtQualification.Text) ||
                string.IsNullOrWhiteSpace(txtClassesYouCanTeach.Text) ||
                string.IsNullOrWhiteSpace(txtPreferableTeachingArea.Text) ||
                string.IsNullOrWhiteSpace(txtSubjectYouCanTeach.Text) ||
                nudExperience.Value <= 0 ||
                string.IsNullOrWhiteSpace(txtFindAboutUs.Text))
            {
                Timer timer = new Timer();
                timer.Interval = 2000; // 2 seconds
                timer.Tick += (s, e) =>
                {
                    timer.Stop();
                    SendKeys.Send("{ENTER}");
                };
                timer.Start();

                {
                    ValidationPopup popup = new ValidationPopup(
                        "⚠️ Please fill all required fields."
                    );
                    popup.Show();
                    return;
                }

            }

            // 🔹 Prepare faculty object
            var faculty = new FacultyModel
            {
            teacher_name = txtTeacherName.Text.Trim(),
            gender = rdbMale.Checked ? "Male" : "Female",
            contact = txtContact.Text.Trim(),
            email = txtEmail_Id.Text.Trim(),
            address = txtAddress.Text.Trim(),
            marital_status = cbUnmarried.Checked ? "Unmarried" : "Married",
            qualification = txtQualification.Text.Trim(),
            classes_can_teach = txtClassesYouCanTeach.Text.Trim(),
            preferable_teaching_area = txtPreferableTeachingArea.Text.Trim(),
            subjects_can_teach = txtSubjectYouCanTeach.Text.Trim(),
            experience_years = (int)nudExperience.Value,
            find_about_us = txtFindAboutUs.Text.Trim(),
            date_of_joining = dtpDateOfJoining.Value.ToString("yyyy-MM-dd")
        };

            string json = JsonConvert.SerializeObject(faculty);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response =
                       await client.PostAsync(AppSession.ApiFolder + "faculty.php?license=" +license, content);
                        

                    string result = await response.Content.ReadAsStringAsync();

                    if (string.IsNullOrWhiteSpace(result))
                    {
                        MessageBox.Show("API returned empty response",
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    if (!result.TrimStart().StartsWith("{"))
                    {
                        MessageBox.Show("API returned non-JSON response:\n\n" + result,
                            "Server Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    ApiResponse apiResponse =
                        JsonConvert.DeserializeObject<ApiResponse>(result);

                    if (apiResponse.status)
                    {
                        SuccessPopup popup =
                            new SuccessPopup(" Saved Successfully ✅");

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
            txtTeacherName.Clear();
            rdbMale.Checked = false;
            rdbFemale.Checked = false;
            txtContact.Clear();
            txtEmail_Id.Clear();
            txtAddress.Clear();
            cbUnmarried.Checked = false;
            cbMarried.Checked = false;
            txtQualification.Clear();
            txtClassesYouCanTeach.Clear();
            txtPreferableTeachingArea.Clear();
            txtSubjectYouCanTeach.Clear();
            txtClassesYouCanTeach.Clear();
            nudExperience.Value = nudExperience.Minimum;
            txtFindAboutUs.Clear();
            dtpDateOfJoining.Value = DateTime.Now;
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

        private void Faculty_Form_Load(object sender, EventArgs e)
        {

        }
    }
}
public class FacultyModel
{
    public string teacher_name { get; set; }
    public string gender { get; set; }
    public string contact { get; set; }
    public string email { get; set; }
    public string address { get; set; }
    public string marital_status { get; set; }
    public string qualification { get; set; }
    public string classes_can_teach { get; set; }
    public string preferable_teaching_area { get; set; }
    public string subjects_can_teach { get; set; }
    public int experience_years { get; set; }
    public string find_about_us { get; set; }
    public string date_of_joining { get; set; }
} 