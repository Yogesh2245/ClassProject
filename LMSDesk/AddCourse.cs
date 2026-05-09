
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
    public partial class AddCourse : Form
    { 
        public AddCourse()
        {
            InitializeComponent();
            ApplyUI();
            StyleGroupBox(groupBox2);
           
            StyleButton(button1, Color.FromArgb(32, 64, 128));
        }
       

        private void ApplyUI()
        {
            this.SuspendLayout();
            this.BackColor = Color.FromArgb(245, 247, 250);
            this.DoubleBuffered = true;


            label12.BackColor = Color.FromArgb(64, 64, 64);
            label12.ForeColor = Color.White;
            label12.Font = new Font("Segoe UI", 13, FontStyle.Bold);

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
            btn.Height = 36;
            btn.Cursor = Cursors.Hand;
        }
        private void button1_Click(object sender, EventArgs e)
        { SaveCourseDetails(); }
        private async void SaveCourseDetails()
        {
            // 🔹 Validation
            if (string.IsNullOrWhiteSpace(txtCourseName.Text) ||
                string.IsNullOrWhiteSpace(txtCourseCode.Text) ||
                string.IsNullOrWhiteSpace(txtInstructorName.Text) ||
                cmbCourseDuration.SelectedIndex == -1 ||
                comboxCourseCategory.SelectedIndex == -1 ||
                numUDCourseFee.Value <= 0 ||
                string.IsNullOrWhiteSpace(txtCourseDescription.Text) )
             //   (!rdbBegginer.Checked && !rdbIntermediate.Checked && !rdbAdvanced.Checked) ||
              //  (!rdbOnline.Checked && !rdbOffline.Checked))
            {
                ValidationPopup popup = new ValidationPopup(
                    "⚠️ Please fill all required fields."
                );
                popup.Show();
                return;
            }


            // 🔹 Prepare course object
            var course = new CourseModel
            {
            course_name = txtCourseName.Text.Trim(),
            course_code = txtCourseCode.Text.Trim(),
            instructor_name = txtInstructorName.Text.Trim(),
            course_duration = cmbCourseDuration.Text,
            start_date = dtpStartDate.Value.ToString("yyyy-MM-dd"),
            end_date = dtpEndDate.Value.ToString("yyyy-MM-dd"),
            course_category = comboxCourseCategory.Text,
            course_level = rdbBegginer.Checked ? "Beginner" : rdbIntermediate.Checked ? "Intermediate" : "Advanced",
            course_fee = (int)numUDCourseFee.Value,
            course_mode = rdbOnline.Checked ? "Online" : "Offline",
            course_description = txtCourseDescription.Text.Trim()
        };

            string json = JsonConvert.SerializeObject(course);
            var content = new StringContent(json, Encoding.UTF8, "application/json");


            string license = AppSession.LicenseName;

            // 🔹 Call API
            using (HttpClient client = new HttpClient())
        {
            try
            {

                    HttpResponseMessage response =
                                            await client.PostAsync(
                                                AppSession.ApiFolder +"course.php?license=" + license,
                                                content
                                            );

                    string result = await response.Content.ReadAsStringAsync();

                    if (!response.IsSuccessStatusCode)
                    {
                        MessageBox.Show(result, "API Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    ApiResponse apiResponse =
                        JsonConvert.DeserializeObject<ApiResponse>(result);

                    if (apiResponse.status)
                    {
                        new SuccessPopup("Course Added Successfully ✅").Show();
                        ClearForm();
                    }
                    else
                    {
                        MessageBox.Show("",
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            txtCourseName.Clear();
            txtCourseCode.Clear();
            txtInstructorName.Clear();
            cmbCourseDuration.SelectedIndex = -1;
            dtpStartDate.Value = DateTime.Now;
            dtpEndDate.Value = DateTime.Now;
            comboxCourseCategory.SelectedIndex = -1;
            rdbBegginer.Checked = false;
            rdbIntermediate.Checked = false;
            rdbAdvanced.Checked = false;
            rdbOnline.Checked = false;
            rdbOffline.Checked = false;
            txtCourseDescription.Clear();
            numUDCourseFee.Value = numUDCourseFee.Minimum;

        }

      
        private void panel2_Paint(object sender, PaintEventArgs e)
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

        private void AddCourse_Load(object sender, EventArgs e)
        {

        }
    }
}


// 🔹 Course model
public class CourseModel
{
    public string course_name { get; set; }
    public string course_code { get; set; }
    public string instructor_name { get; set; }
    public string course_duration { get; set; }
    public string start_date { get; set; }
    public string end_date { get; set; }
    public string course_category { get; set; }
    public string course_level { get; set; }
    public int course_fee { get; set; }
    public string course_mode { get; set; }
    public string course_description { get; set; }
}
