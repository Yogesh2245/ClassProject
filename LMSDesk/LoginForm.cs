using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LMSDesk
{
    public partial class LoginForm : Form
    {
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(
            int nLeftRect,
            int nTopRect,
            int nRightRect,
            int nBottomRect,
            int nWidthEllipse,
            int nHeightEllipse
        );

        public LoginForm()
        {
            InitializeComponent();
            
            this.FormBorderStyle = FormBorderStyle.None;
            this.Region = Region.FromHrgn(
                CreateRoundRectRgn(0, 0, Width, Height, 30, 30)
            );
            AppSession.SetBaseUrl("https://gbep.in/");

            //  ApplyModernDesign();
        }

        

        private async void LoginForm_Load(object sender, EventArgs e)
        {
          //  await LoadInstitutesAsync();

            rdbinstitutelogin.Checked = false;
            panelBranch.Visible = true;
            panelinstitute.Visible = false;








            string filePath = Application.StartupPath + @"\set.db";

            if (File.Exists(filePath))
            {
                string setting = File.ReadAllText(filePath).Trim().ToLower();

                if (setting == "branch")
                {
                    // Select radio
                    rdbbranchlogin.Checked = true;

                    // Hide same radio button
                    rdbbranchlogin.Visible = false;
                    rdbinstitutelogin.Visible = false; // optional if you want both hidden

                    // Show panel
                    panelinstitute.Visible = false;
                    panelBranch.Visible = true;
                    panelBranch.BringToFront();
                }
                else if (setting == "inst")
                {
                    // Select radio
                    rdbinstitutelogin.Checked = true;

                    // Hide same radio button
                    rdbbranchlogin.Visible = false;
                    rdbinstitutelogin.Visible = false; // optional

                    // Show panel
                    panelinstitute.Visible = true;
                    panelBranch.Visible = false;
                    panelinstitute.BringToFront();
                }
            }
            else
            {
                // File not exist → allow manual selection
                rdbbranchlogin.Visible = true;
                rdbinstitutelogin.Visible = true;
            }
        }


        // ===================== LOGIN =====================
        private async void btnLogin_Click_1(object sender, EventArgs e)
        {
           


            string login = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Enter username/mobile and password");
                return;
            }

            var loginData = new
            {
             
                login = login,
                password = password
            };

            string json = JsonConvert.SerializeObject(loginData);

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var content = new StringContent(json, Encoding.UTF8, "application/json");



                    String apiUrl = AppSession.ApiFolder + "login.php?license=" + txtLicense.Text;
                    // ✅ FIXED URL STRING
                    var response = await client.PostAsync(
                      apiUrl,
                        content
                    );

                    if (!response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("Server error");
                        return;
                    }

                    string result = await response.Content.ReadAsStringAsync();
                    string cleanJson = ExtractJson(result);

                    if (string.IsNullOrEmpty(cleanJson))
                    {
                        MessageBox.Show("Invalid server response:\n" + result);
                        return;
                    }

                    LoginApiResponse data =
                        JsonConvert.DeserializeObject<LoginApiResponse>(cleanJson);


                    if (data != null && data.status && data.user != null)
                    {
                      
                        string license = txtLicense.Text.Trim();
                        if (ValidateLicense(license))
                        {
                            AppSession.SetLicense(license);  
                           

                        }
                        DashboardForm dash = new DashboardForm(
                         data.user.username,
                         data.user.full_name,
                         txtLicense.Text,
                         data.user.role
                     );

                        dash.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show(
                            data?.message ?? "Invalid login details",
                            "Login Failed",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error
                        );
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Login Error");
            }
        }

        private string ExtractJson(string response)
        {
            int start = response.IndexOf('{');
            int end = response.LastIndexOf('}');

            if (start >= 0 && end >= 0 && end > start)
            {
                return response.Substring(start, end - start + 1);
            }

            return null;
        }

        private bool ValidateLicense(string licName)
        {
            return true;
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void rdbbranchlogin_CheckedChanged(object sender, EventArgs e)
        {
         
        }

        private void rdbinstitutelogin_CheckedChanged_1(object sender, EventArgs e)
        {
       
        }

        private async void button5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private async  void button6_Click(object sender, EventArgs e)
        { 
            button6.Enabled = false;
            string login = txtUsername1.Text.Trim();

            string password = txtPassword1.Text.Trim();

            if (login.Equals("8308090164") && password.Equals("8308090164"))
            {

                string license = txtLicense.Text.Trim();
                if (ValidateLicense(license))
                {
                    AppSession.SetLicense(license);
                }
                DashbordForm1 dashbordForm1 = new DashbordForm1(license);
                dashbordForm1.Show(); this.Hide();
            }
            else { MessageBox.Show("Wrong Cred.", "Wrong Username or Password", MessageBoxButtons.OK, MessageBoxIcon.Information); }

        }







        private void txtPassword_TextChanged(object sender, EventArgs e)
        {

        }

        private void rdbinstitutelogin_CheckedChanged(object sender, EventArgs e)
        {
            // if (rdbinstitutelogin.Checked) { panelInstitutelogin.Visible = true; panelSuperadminlogin.Visible = false; } else { panelInstitutelogin.Visible = false; panelSuperadminlogin.Visible = true;  }
        }



        // ===================== MODELS =====================
        public class LoginApiResponse
        {
            public bool status { get; set; }
            public string message { get; set; }
            public LoginUser user { get; set; }
        }

        public class LoginUser
        {
            public string username { get; set; }
            public string full_name { get; set; }
            public string role { get; set; }
        }

        public class InstituteResponse
        {
            public bool status { get; set; }
            public InstituteModel[] data { get; set; }
        }

        // ✅ FIXED MODEL
        public class InstituteModel
        {
            public int institute_id { get; set; }
            public string institute_name { get; set; }
        }

        private void rdbinstitutelogin_Click(object sender, EventArgs e)
        {
            if (rdbinstitutelogin.Checked)
            {
                panelinstitute.Visible = true;
                panelBranch.Visible = false;
                panelinstitute.BringToFront();
            }
        }

        private void rdbbranchlogin_Click(object sender, EventArgs e)
        {
            if (rdbbranchlogin.Checked)
            {
                panelinstitute.Visible = false;
                panelBranch.Visible = true;
                panelBranch.BringToFront();
            }
        }




        // all design code aaaded here start

        private void ApplyModernDesign()
        {
            // Form
            this.FormBorderStyle = FormBorderStyle.None;
            this.BackColor = Color.FromArgb(245, 247, 250); // light modern bg
            this.Padding = new Padding(2);

            // Rounded form
            this.Region = Region.FromHrgn(
                CreateRoundRectRgn(0, 0, Width, Height, 25, 25)
            );

            // Panel Cards
            StyleCard(panelBranch);
            StyleCard(panelinstitute);

            // Textboxes
            StyleTextbox(txtLicense);
            StyleTextbox(txtUsername);
            StyleTextbox(txtPassword);
            StyleTextbox(txtUsername1);
            StyleTextbox(txtPassword1);

            // Buttons
            StyleButton(btnLogin, Color.FromArgb(0, 120, 215)); // Blue
            StyleButton(btnCancel, Color.FromArgb(232, 17, 35)); // Red
            StyleButton(button6, Color.FromArgb(0, 120, 215));
            StyleButton(button5, Color.FromArgb(232, 17, 35));

            // Radio Buttons
            rdbbranchlogin.ForeColor = Color.FromArgb(64, 64, 64);
            rdbinstitutelogin.ForeColor = Color.FromArgb(64, 64, 64);

            // Labels
            ApplyLabelFont(this);

            // Logo panel bg
            pictureBox1.BackColor = Color.White;
        }

        private void StyleCard(Panel panel)
        {
            panel.BackColor = Color.White;
            panel.Padding = new Padding(20);
            panel.BorderStyle = BorderStyle.None;

            panel.Region = Region.FromHrgn(
                CreateRoundRectRgn(0, 0, panel.Width, panel.Height, 20, 20)
            );
        }

        private void StyleTextbox(TextBox txt)
        {
            txt.BorderStyle = BorderStyle.None;
            txt.BackColor = Color.FromArgb(240, 240, 240);
            txt.Font = new Font("Segoe UI", 11F);
            txt.Height = 30;
            txt.Padding = new Padding(10);

            // Bottom border line
            Panel line = new Panel();
            line.Height = 2;
            line.Dock = DockStyle.Bottom;
            line.BackColor = Color.FromArgb(0, 120, 215);
            txt.Controls.Add(line);
        }

        private void StyleButton(Button btn, Color baseColor)
        {
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.BackColor = baseColor;
            btn.ForeColor = Color.White;
            btn.Font = new Font("Segoe UI Semibold", 10.5F);
            btn.Height = 42;
            btn.Width = 110;
            btn.Cursor = Cursors.Hand;

            btn.Resize += (s, e) =>
            {
                btn.Region = Region.FromHrgn(
                    CreateRoundRectRgn(0, 0, btn.Width, btn.Height, 12, 12)
                );
            };

            // Hover
            btn.MouseEnter += (s, e) =>
                btn.BackColor = ControlPaint.Light(baseColor);

            btn.MouseLeave += (s, e) =>
                btn.BackColor = baseColor;
        }


        private void ApplyLabelFont(Control parent)
        {
            foreach (Control c in parent.Controls)
            {
                if (c is Label lbl)
                {
                    lbl.Font = new Font("Segoe UI Semibold", 10F);
                    lbl.ForeColor = Color.FromArgb(64, 64, 64);
                }

                if (c.HasChildren)
                    ApplyLabelFont(c);
            }
        }








    }
}  