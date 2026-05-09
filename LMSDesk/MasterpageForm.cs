using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace LMSDesk
{
    /*public partial class coursematser : Form
    {
        private string role;

  
        public coursematser( string userRole)
        {
            InitializeComponent();

            this.role = userRole;
            this.Load += coursematser_Load;

            RestrictAccess();
        }

        private void RestrictAccess()
        {
            // 
            if (this.role == "Admin")
            {
             
                btnInstituteMaster.Visible = false;
                btnInstituteList.Visible = false;
            }
            else if (this.role == "SuperAdmin")
            {
                btnInstituteMaster.Visible = true; 
                btnInstituteList.Visible = true;
            }
        }

        private DashboardForm _dashboard;
       


        // 🔹 DLL IMPORT (MUST be here)
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(
            int nLeftRect,
            int nTopRect,
            int nRightRect,
            int nBottomRect,
            int nWidthEllipse,
            int nHeightEllipse
        );

        // 🔹 Constructor
        public coursematser(DashboardForm dashboard)
        {
            InitializeComponent();
            _dashboard = dashboard;

            // Load event attach (OK)
          
        }

        // 🔹 Form Load
        private void coursematser_Load(object sender, EventArgs e)
        {
            // Buttons must be already loaded → so Load event is correct
            RoundButton(btnAddStudent, 20);
            RoundButton(btnCourses, 20);
            RoundButton(btnFacultyForm, 20);
            RoundButton(btnInstituteMaster, 20);
            RoundButton(btnCertificationForm, 20);
            RoundButton(btnStudentList, 20);
            RoundButton(btnCourseList, 20);
            RoundButton(btnFacultyList, 20);
            RoundButton(btnInstituteList, 20);
            RoundButton(btnCertificationList, 20);
            RoundButton(btnUserAddForm, 20);

            // Add hover animations
            AddHoverEffect(btnAddStudent);
            AddHoverEffect(btnCourses);
            AddHoverEffect(btnFacultyForm);
            AddHoverEffect(btnInstituteMaster);
            AddHoverEffect(btnCertificationForm);
            AddHoverEffect(btnStudentList);
            AddHoverEffect(btnCourseList);
            AddHoverEffect(btnFacultyList);
            AddHoverEffect(btnInstituteList);
            AddHoverEffect(btnCertificationList);
            AddHoverEffect(btnUserAddForm);
        }

        // 🔹 Round Button Method
        private void RoundButton(Button btn, int radius)
        {
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;

            btn.Region = Region.FromHrgn(
                CreateRoundRectRgn(
                    0,
                    0,
                    btn.Width,
                    btn.Height,
                    radius,
                    radius
                )
            );
        }
        private void AddHoverEffect(Button btn)
        {
            Color originalBackColor = btn.BackColor;
            Color originalForeColor = btn.ForeColor;
            Point originalLocation = btn.Location;

            btn.MouseEnter += (s, e) =>
            {
                btn.BackColor = Color.MediumOrchid; // Professional Blue
                btn.ForeColor = Color.White;
                btn.Cursor = Cursors.Hand;

                btn.Location = new Point(
                    originalLocation.X,
                    originalLocation.Y - 4
                );
            };

            btn.MouseLeave += (s, e) =>
            {
                btn.BackColor = originalBackColor;
                btn.ForeColor = originalForeColor;
                btn.Location = originalLocation;
            };
        }



        private void btnAddStudent_Click(object sender, EventArgs e)
        {
            OpenFormInpanel1_Paint(new AddStudent()); 
        }

        private void OpenFormInpanel1_Paint(Form childForm)
        {
            panel1.Controls.Clear();          // panel1 = content panel
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;

            panel1.Controls.Add(childForm);
            childForm.Show();
        } 
        private void btnCourses_Click(object sender, EventArgs e)
        {
            OpenFormInpanel1_Paint(new AddCourse());
        }

        private void btnFacultyForm_Click(object sender, EventArgs e)
        {
            OpenFormInpanel1_Paint(new Faculty_Form());
        }

        private void btnInstituteMaster_Click(object sender, EventArgs e)
        {
            OpenFormInpanel1_Paint(new Institute_Registration());
        }

        private void btnCertificationForm_Click(object sender, EventArgs e)
        {
            OpenFormInpanel1_Paint(new CertificationForm());
        }

        private void btnStudentList_Click(object sender, EventArgs e)
        {
            OpenFormInpanel1_Paint(new StudentLIst());
        }

        private void btnCourseList_Click(object sender, EventArgs e)
        {
            OpenFormInpanel1_Paint(new CourseList());
        }

        private void btnFacultyList_Click(object sender, EventArgs e)
        {
            OpenFormInpanel1_Paint(new FacultyList());
        }

        private void btnInstituteList_Click(object sender, EventArgs e)
        {
            OpenFormInpanel1_Paint(new InstituteList());
        }

        private void btnCertificationList_Click(object sender, EventArgs e)
        {
            OpenFormInpanel1_Paint(new CertificationList());
        }

        private void btnUserAddForm_Click(object sender, EventArgs e)
        {
            OpenFormInpanel1_Paint(new UserAddForm());
        }



    }*/

    public partial class coursematser : Form
    {
        private string role;
        private DashboardForm _dashboard;

        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(
            int nLeftRect, int nTopRect, int nRightRect, int nBottomRect,
            int nWidthEllipse, int nHeightEllipse
        );

        // जुना कन्स्ट्रक्टर (Dashboard साठी)
        public coursematser(DashboardForm dashboard)
        {
            InitializeComponent();
            _dashboard = dashboard;
        }

        // रोल (Role) साठी कन्स्ट्रक्टर
        public coursematser(string userRole)
        {
            InitializeComponent();
            this.role = userRole;
            this.Load += coursematser_Load;
            RestrictAccess();
        }

        private void RestrictAccess()
        {
            if (this.role == "Admin")
            {
                btnInstituteMaster.Visible = false;
                btnInstituteList.Visible = false;
            }
            else if (this.role == "SuperAdmin")
            {
                btnInstituteMaster.Visible = true;
                btnInstituteList.Visible = true;
            }
        }

        private void coursematser_Load(object sender, EventArgs e)
        {
            RoundButton(btnAddStudent, 20);
            RoundButton(btnCourses, 20);
            RoundButton(btnFacultyForm, 20);
            RoundButton(btnInstituteMaster, 20);
            RoundButton(btnCertificationForm, 20);
            RoundButton(btnStudentList, 20);
            RoundButton(btnCourseList, 20);
            RoundButton(btnFacultyList, 20);
            RoundButton(btnInstituteList, 20);
            RoundButton(btnCertificationList, 20);
            RoundButton(btnUserAddForm, 20);

            AddHoverEffect(btnAddStudent);
            AddHoverEffect(btnCourses);
            AddHoverEffect(btnFacultyForm);
            AddHoverEffect(btnInstituteMaster);
            AddHoverEffect(btnCertificationForm);
            AddHoverEffect(btnStudentList);
            AddHoverEffect(btnCourseList);
            AddHoverEffect(btnFacultyList);
            AddHoverEffect(btnInstituteList);
            AddHoverEffect(btnCertificationList);
            AddHoverEffect(btnUserAddForm);
        }

        private void RoundButton(Button btn, int radius)
        {
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, btn.Width, btn.Height, radius, radius));
        }

        private void AddHoverEffect(Button btn)
        {
            Color originalBackColor = btn.BackColor;
            Color originalForeColor = btn.ForeColor;
            Point originalLocation = btn.Location;

            btn.MouseEnter += (s, e) =>
            {
                btn.BackColor = Color.MediumOrchid;
                btn.ForeColor = Color.White;
                btn.Cursor = Cursors.Hand;
                btn.Location = new Point(originalLocation.X, originalLocation.Y - 4);
            };

            btn.MouseLeave += (s, e) =>
            {
                btn.BackColor = originalBackColor;
                btn.ForeColor = originalForeColor;
                btn.Location = originalLocation;
            };
        }

        // 🔥 हे महत्वाचे आहे: ही मेथड आता 'public' आहे जेणेकरून StudentList मधून वापरता येईल
        public void OpenFormInpanel1_Paint(Form childForm)
        {
            // तुमच्या डिझाईनमध्ये या पॅनेलचे नाव 'panel1' असल्याची खात्री करा
            if (panel1 != null)
            {
                panel1.Controls.Clear();
                childForm.TopLevel = false;
                childForm.FormBorderStyle = FormBorderStyle.None;
                childForm.Dock = DockStyle.Fill;
                panel1.Controls.Add(childForm);
                childForm.Show();
            }
            else
            {
                MessageBox.Show("Error: 'panel1' not found on coursematser form.");
            }
        }

        private void btnAddStudent_Click(object sender, EventArgs e) => OpenFormInpanel1_Paint(new AddStudent());
        private void btnCourses_Click(object sender, EventArgs e) => OpenFormInpanel1_Paint(new AddCourse());
        private void btnFacultyForm_Click(object sender, EventArgs e) => OpenFormInpanel1_Paint(new Faculty_Form());
        private void btnInstituteMaster_Click(object sender, EventArgs e) => OpenFormInpanel1_Paint(new Institute_Registration());
        private void btnCertificationForm_Click(object sender, EventArgs e) => OpenFormInpanel1_Paint(new CertificationForm());
        private void btnStudentList_Click(object sender, EventArgs e) => OpenFormInpanel1_Paint(new StudentLIst());
        private void btnCourseList_Click(object sender, EventArgs e) => OpenFormInpanel1_Paint(new CourseList());
        private void btnFacultyList_Click(object sender, EventArgs e) => OpenFormInpanel1_Paint(new FacultyList());
        private void btnInstituteList_Click(object sender, EventArgs e) => OpenFormInpanel1_Paint(new InstituteList());
        private void btnCertificationList_Click(object sender, EventArgs e) => OpenFormInpanel1_Paint(new CertificationList());
        private void btnUserAddForm_Click(object sender, EventArgs e) => OpenFormInpanel1_Paint(new UserAddForm());
    }
}

