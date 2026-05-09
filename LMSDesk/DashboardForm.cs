using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace LMSDesk
{
    public partial class DashboardForm : Form
    {
        private string userRole;
        public int InstituteId { get; }
        public string InstituteName { get; }
        public string UserName { get; }

        public DashboardForm(string username, string fullName, string institute,string role)
        {
            InitializeComponent();

            lblUsername.Text = username;
            lblFullName.Text = fullName;
            lblInstitute.Text = institute;
            this.userRole = role;
            RestrictAccess();
        }

        private void RestrictAccess()
        {
           
            if (this.userRole == "Admin")
            {

            }
        }
 
        private void btnLogout_Click(object sender, EventArgs e)
        {
            LoginForm login = new LoginForm();
            login.Show();

            this.Hide(); // close dashboard
        } 

        private bool _isReloadingForm = false;

        private bool isEnglish = true;
       // private bool _isLicenceLoading = true;
        private readonly string _baseUrl = "";
      //  public string BaseUrl => _baseUrl;
        private string _license = "";
        private readonly int _appId = 1;
        private readonly int _adminUserId = 126 ;     
        private readonly string cachePath =
    Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
    "Ranangan", "licence_cache.json");
         

        // Stats values
        private int _total, _visited, _color, _voted;

        // Cache timing
        private static DateTime _lastStatsLoadTime;
        private static readonly TimeSpan _cacheDuration = TimeSpan.FromSeconds(45);

        private Form _currentLoadedForm; 

        private int _totalVoters;
        private int _maleCount;
        private int _femaleCount;
        private int _seniorCount;

        private int _visitedDone;
        private int _surveyDone;
        private int _colorDone;
        private int _castDone;

        private int _votedDone;
        private int _favorStrong;
        private double _favorOppRatio;
        private int _slipPrinted;

        // simple holder for chart points
        private class ChartPoint
        {
            public string Label { get; set; }
            public double Value { get; set; }
        }

        // chart data
        private List<ChartPoint> _boothCounts = new List<ChartPoint>();
        private List<ChartPoint> _ageBuckets = new List<ChartPoint>();
        private List<ChartPoint> _favorByArea = new List<ChartPoint>();
        private List<ChartPoint> _pieVoted = new List<ChartPoint>();
        private List<ChartPoint> _pieColors = new List<ChartPoint>();
         
        public DashboardForm()
        {
            InitializeComponent(); 
           // _baseUrl = Constants.GetBaseUrl();

            picImportDone.Visible = false;
            progressImport.Value = 0;
            lblImportStatus.Text = "Import not started yet.";
            lblImportHeader.Text = $"Imports from license: {_license}";
            // BackgroundWorker settings
      
        }
        private void EnsureCacheDir()
        {
            string dir = Path.GetDirectoryName(cachePath);
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);
        } 
        private bool ShouldReloadStats()
        {
            return (DateTime.Now - _lastStatsLoadTime) > _cacheDuration;
        }
        private bool ShouldReloadDashboardData()
        {
            return (DateTime.Now - _lastStatsLoadTime) > _cacheDuration;
        } 

        private void BtnAdvancedFilter_Click(object sender, EventArgs e)
        {
            UpdateContentPanel(isEnglish ? "Advanced Filter" : "आधुनिक फिल्टर", "You are now viewing the Advanced Filter module");
            HighlightButton(btnAdvancedFilter);
            
        } 

        private void UpdateContentPanel(string title, string description)
        {
         //   lblContentTitle.Text = title;
         //   lblContentDescription.Text = description;
        }



        private void HighlightButton(Button activeButton)
        {
            Color defaultColor = Color.FromArgb(55, 65, 81);
            Color activeColor = Color.FromArgb(59, 130, 246);

            Button[] buttons =
            {
        button1,
        btnMaster,
        btnAssignedCourse,
        btnLiveCourses,
        btnCertificate
    };

            foreach (Button btn in buttons)
            {
                btn.BackColor = defaultColor;
                btn.ForeColor = Color.White;
            }

            activeButton.BackColor = activeColor;
        }
        // Reusable method to load UserControl in main panel
        private void LoadModule(UserControl control)
        {
            control.Dock = DockStyle.Fill;
            panelContent.Controls.Clear();
            panelContent.Controls.Add(control);
 
        } 
       
        public void LoadModuleForm(Form form)
        {
            if (panelContent.IsDisposed) return;

            // 🔒 REMOVE previous embedded form safely
            if (_currentLoadedForm != null)
            {
                try
                {
                    panelContent.Controls.Remove(_currentLoadedForm);
                    _currentLoadedForm.Dispose();
                }
                catch (Exception e) { Console.WriteLine(e.ToString()); }

                _currentLoadedForm = null;
            }

            // 🔥 SET new form
            _currentLoadedForm = form;

            form.TopLevel = false;
            form.FormBorderStyle = FormBorderStyle.None;
            form.Dock = DockStyle.Fill;

            panelContent.Controls.Clear();
            panelContent.Controls.Add(form);

            form.Show();   // ✅ ONLY Dashboard calls Show()
        }

        private void AddHoverEffect(Button btn)
        {
            Color hoverColor = Color.FromArgb(37, 99, 235);   // Hover blue
            Color activeColor = Color.FromArgb(59, 130, 246); // Active blue
            Color defaultColor = Color.FromArgb(55, 65, 81);

            Point originalLocation = btn.Location;

            btn.MouseEnter += (s, e) =>
            {
                if (btn.BackColor != activeColor)   // Active असेल तर change नाही
                {
                    btn.BackColor = hoverColor;
                    btn.ForeColor = Color.White;
                }

                btn.Cursor = Cursors.Hand;
                btn.Location = new Point(originalLocation.X, originalLocation.Y - 4);
            };

            btn.MouseLeave += (s, e) =>
            {
                if (btn.BackColor != activeColor)   // Active असेल तर reset नाही
                {
                    btn.BackColor = defaultColor;
                    btn.ForeColor = Color.White;
                }

                btn.Location = originalLocation;
            };
        }

        private async void DashboardForm_Load(object sender, EventArgs e)
        {
            AddHoverEffect(button1);          // 🔥 ADD THIS
            AddHoverEffect(btnMaster);
            AddHoverEffect(btnAssignedCourse);
            AddHoverEffect(btnLiveCourses);
            AddHoverEffect(btnCertificate); 
            UpdateContentPanel(isEnglish ? " " : "", "");
            HighlightButton(button1); 
            
        } 

        private void button1_Click(object sender, EventArgs e)
        {
           // UpdateContentPanel(isEnglish ? "Voter List" : "मतदार यादी", "You are now viewing the Voter List module");
            HighlightButton(button1);
            panelContent.Controls.Clear();   // Home default view

        } 
        private void btnAssignedCourse_Click(object sender, EventArgs e)
        {
           // UpdateContentPanel(isEnglish ? "" : "", ""); 
            HighlightButton((Button)sender);
            LoadModuleForm(new StudentProfile(this));
           
        }
        private void btnMaster_Click(object sender, EventArgs e)
        {
            HighlightButton((Button)sender);
            LoadModuleForm(new coursematser(this.userRole)); // हा बरोबर आहे
        }
        private void btnLiveCourses_Click(object sender, EventArgs e)
        {
           // UpdateContentPanel(isEnglish ? "" : "", "");
            HighlightButton((Button)sender); 
            LoadModuleForm(new LiveCourses(this));
        } 
        private void btnCertificate_Click(object sender, EventArgs e)
        {
            UpdateContentPanel(isEnglish ? "" : "", "");
            HighlightButton(btnListGroup);
           
            LoadModuleForm(new printcertificate(this));
        } 
        private void DashboardForm_FormClosing(object sender, FormClosingEventArgs e)
        { 
            Application.Exit();

        }

        // ================= SAFE DISPOSE (OPTIONAL) =================
        private void SafeDisposeForm(Form f)
        {
            try
            {
                if (f != null && !f.IsDisposed)
                    f.Dispose();
            }
            catch { }
        } 
 
    }
}
class LicenceItem
{
    public string LicenceName { get; set; }
    public string DisplayName { get; set; }
    public string BasePath { get; set; }

    public override string ToString()
    {
        return DisplayName; // ComboBox shows only DisplayName
    }
}
