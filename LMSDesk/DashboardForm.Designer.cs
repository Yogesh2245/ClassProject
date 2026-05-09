using System.Drawing;
using System.Windows.Forms;
 


namespace LMSDesk
{
    partial class DashboardForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DashboardForm));
            this.panelSidebar = new System.Windows.Forms.Panel();
            this.btnLiveCourses = new System.Windows.Forms.Button();
            this.btnAssignedCourse = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.btnMaster = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.lblImportHeader = new System.Windows.Forms.Label();
            this.picImportDone = new System.Windows.Forms.PictureBox();
            this.lblImportUser = new System.Windows.Forms.Label();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.lblImportStatus = new System.Windows.Forms.Label();
            this.progressImport = new System.Windows.Forms.ProgressBar();
            this.flowLayoutPanel3 = new System.Windows.Forms.FlowLayoutPanel();
            this.lineDivider2 = new System.Windows.Forms.Panel();
            this.lblVotingDayIcon = new System.Windows.Forms.Label();
            this.lblSettingsIcon = new System.Windows.Forms.Label();
            this.lineDivider1 = new System.Windows.Forms.Panel();
            this.lblAnalysisIcon = new System.Windows.Forms.Label();
            this.lblVoterListIcon = new System.Windows.Forms.Label();
            this.btnCertificate = new System.Windows.Forms.Button();
            this.btnDuplicateList = new System.Windows.Forms.Button();
            this.lblDuplicateListIcon = new System.Windows.Forms.Label();
            this.btnMasters = new System.Windows.Forms.Button();
            this.lblMastersIcon = new System.Windows.Forms.Label();
            this.btnAdvancedFilter = new System.Windows.Forms.Button();
            this.lblAdvancedFilterIcon = new System.Windows.Forms.Label();
            this.btnOfficeManagement = new System.Windows.Forms.Button();
            this.lblOfficeManagementIcon = new System.Windows.Forms.Label();
            this.btnSurveyManagement = new System.Windows.Forms.Button();
            this.lblSurveyManagementIcon = new System.Windows.Forms.Label();
            this.btnListGroup = new System.Windows.Forms.Button();
            this.lblListGroupIcon = new System.Windows.Forms.Label();
            this.panelContent = new System.Windows.Forms.Panel();
            this.panel7 = new System.Windows.Forms.Panel();
            this.panelHeader = new System.Windows.Forms.Panel();
            this.lblFullName = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblUsername = new System.Windows.Forms.Label();
            this.lblInstitute = new System.Windows.Forms.Label();
            this.panelToggleLanguage = new System.Windows.Forms.Panel();
            this.panelSidebar.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picImportDone)).BeginInit();
            this.flowLayoutPanel1.SuspendLayout();
            this.panelContent.SuspendLayout();
            this.panelHeader.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelSidebar
            // 
            this.panelSidebar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(28)))), ((int)(((byte)(38)))));
            this.panelSidebar.Controls.Add(this.btnLiveCourses);
            this.panelSidebar.Controls.Add(this.btnAssignedCourse);
            this.panelSidebar.Controls.Add(this.button1);
            this.panelSidebar.Controls.Add(this.btnMaster);
            this.panelSidebar.Controls.Add(this.panel3);
            this.panelSidebar.Controls.Add(this.flowLayoutPanel3);
            this.panelSidebar.Controls.Add(this.lineDivider2);
            this.panelSidebar.Controls.Add(this.lblVotingDayIcon);
            this.panelSidebar.Controls.Add(this.lblSettingsIcon);
            this.panelSidebar.Controls.Add(this.lineDivider1);
            this.panelSidebar.Controls.Add(this.lblAnalysisIcon);
            this.panelSidebar.Controls.Add(this.lblVoterListIcon);
            this.panelSidebar.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelSidebar.Location = new System.Drawing.Point(0, 0);
            this.panelSidebar.Name = "panelSidebar";
            this.panelSidebar.Size = new System.Drawing.Size(270, 749);
            this.panelSidebar.TabIndex = 0;
            // 
            // btnLiveCourses
            // 
            this.btnLiveCourses.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.btnLiveCourses.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLiveCourses.FlatAppearance.BorderSize = 0;
            this.btnLiveCourses.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLiveCourses.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.btnLiveCourses.ForeColor = System.Drawing.Color.White;
            this.btnLiveCourses.Location = new System.Drawing.Point(12, 289);
            this.btnLiveCourses.Name = "btnLiveCourses";
            this.btnLiveCourses.Size = new System.Drawing.Size(248, 53);
            this.btnLiveCourses.TabIndex = 34;
            this.btnLiveCourses.Text = "Live Courses";
            this.btnLiveCourses.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLiveCourses.UseVisualStyleBackColor = false;
            this.btnLiveCourses.Click += new System.EventHandler(this.btnLiveCourses_Click);
            // 
            // btnAssignedCourse
            // 
            this.btnAssignedCourse.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.btnAssignedCourse.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAssignedCourse.FlatAppearance.BorderSize = 0;
            this.btnAssignedCourse.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAssignedCourse.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.btnAssignedCourse.ForeColor = System.Drawing.Color.White;
            this.btnAssignedCourse.Location = new System.Drawing.Point(12, 228);
            this.btnAssignedCourse.Name = "btnAssignedCourse";
            this.btnAssignedCourse.Size = new System.Drawing.Size(248, 53);
            this.btnAssignedCourse.TabIndex = 31;
            this.btnAssignedCourse.Text = "Student Profile";
            this.btnAssignedCourse.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAssignedCourse.UseVisualStyleBackColor = false;
            this.btnAssignedCourse.Click += new System.EventHandler(this.btnAssignedCourse_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(130)))), ((int)(((byte)(246)))));
            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(12, 110);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(250, 52);
            this.button1.TabIndex = 22;
            this.button1.Text = "Home";
            this.button1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnMaster
            // 
            this.btnMaster.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.btnMaster.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMaster.FlatAppearance.BorderSize = 0;
            this.btnMaster.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMaster.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.btnMaster.ForeColor = System.Drawing.Color.White;
            this.btnMaster.Location = new System.Drawing.Point(12, 170);
            this.btnMaster.Name = "btnMaster";
            this.btnMaster.Size = new System.Drawing.Size(248, 50);
            this.btnMaster.TabIndex = 33;
            this.btnMaster.Text = "Masters";
            this.btnMaster.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnMaster.UseVisualStyleBackColor = false;
            this.btnMaster.Click += new System.EventHandler(this.btnMaster_Click);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.flowLayoutPanel2);
            this.panel3.Controls.Add(this.lblImportHeader);
            this.panel3.Controls.Add(this.picImportDone);
            this.panel3.Controls.Add(this.lblImportUser);
            this.panel3.Controls.Add(this.flowLayoutPanel1);
            this.panel3.Controls.Add(this.progressImport);
            this.panel3.Location = new System.Drawing.Point(27, 677);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(10, 10);
            this.panel3.TabIndex = 27;
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Location = new System.Drawing.Point(81, 112);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(139, 28);
            this.flowLayoutPanel2.TabIndex = 13;
            // 
            // lblImportHeader
            // 
            this.lblImportHeader.AutoSize = true;
            this.lblImportHeader.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblImportHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(163)))), ((int)(((byte)(175)))));
            this.lblImportHeader.Location = new System.Drawing.Point(81, 0);
            this.lblImportHeader.Name = "lblImportHeader";
            this.lblImportHeader.Size = new System.Drawing.Size(86, 20);
            this.lblImportHeader.TabIndex = 2;
            this.lblImportHeader.Text = "Admin User";
            this.lblImportHeader.Visible = false;
            // 
            // picImportDone
            // 
            this.picImportDone.Location = new System.Drawing.Point(138, 0);
            this.picImportDone.Name = "picImportDone";
            this.picImportDone.Size = new System.Drawing.Size(49, 50);
            this.picImportDone.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picImportDone.TabIndex = 5;
            this.picImportDone.TabStop = false;
            // 
            // lblImportUser
            // 
            this.lblImportUser.AutoSize = true;
            this.lblImportUser.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblImportUser.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(163)))), ((int)(((byte)(175)))));
            this.lblImportUser.Location = new System.Drawing.Point(81, 18);
            this.lblImportUser.Name = "lblImportUser";
            this.lblImportUser.Size = new System.Drawing.Size(125, 20);
            this.lblImportUser.TabIndex = 6;
            this.lblImportUser.Text = "Importing From....";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.lblImportStatus);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(81, 69);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(199, 18);
            this.flowLayoutPanel1.TabIndex = 7;
            // 
            // lblImportStatus
            // 
            this.lblImportStatus.AutoSize = true;
            this.lblImportStatus.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblImportStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(163)))), ((int)(((byte)(175)))));
            this.lblImportStatus.Location = new System.Drawing.Point(3, 0);
            this.lblImportStatus.Name = "lblImportStatus";
            this.lblImportStatus.Size = new System.Drawing.Size(82, 20);
            this.lblImportStatus.TabIndex = 4;
            this.lblImportStatus.Text = "Imp. Status";
            // 
            // progressImport
            // 
            this.progressImport.Location = new System.Drawing.Point(81, 41);
            this.progressImport.Name = "progressImport";
            this.progressImport.Size = new System.Drawing.Size(274, 22);
            this.progressImport.TabIndex = 3;
            // 
            // flowLayoutPanel3
            // 
            this.flowLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.flowLayoutPanel3.Location = new System.Drawing.Point(0, 717);
            this.flowLayoutPanel3.Name = "flowLayoutPanel3";
            this.flowLayoutPanel3.Size = new System.Drawing.Size(270, 32);
            this.flowLayoutPanel3.TabIndex = 24;
            // 
            // lineDivider2
            // 
            this.lineDivider2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(85)))), ((int)(((byte)(99)))));
            this.lineDivider2.Location = new System.Drawing.Point(12, 357);
            this.lineDivider2.Name = "lineDivider2";
            this.lineDivider2.Size = new System.Drawing.Size(250, 1);
            this.lineDivider2.TabIndex = 21;
            // 
            // lblVotingDayIcon
            // 
            this.lblVotingDayIcon.AutoSize = true;
            this.lblVotingDayIcon.Font = new System.Drawing.Font("Segoe UI Symbol", 13F);
            this.lblVotingDayIcon.ForeColor = System.Drawing.Color.White;
            this.lblVotingDayIcon.Location = new System.Drawing.Point(203, 132);
            this.lblVotingDayIcon.Name = "lblVotingDayIcon";
            this.lblVotingDayIcon.Size = new System.Drawing.Size(30, 25);
            this.lblVotingDayIcon.TabIndex = 18;
            this.lblVotingDayIcon.Text = "🗳️";
            // 
            // lblSettingsIcon
            // 
            this.lblSettingsIcon.AutoSize = true;
            this.lblSettingsIcon.Font = new System.Drawing.Font("Segoe UI Symbol", 13F);
            this.lblSettingsIcon.ForeColor = System.Drawing.Color.White;
            this.lblSettingsIcon.Location = new System.Drawing.Point(160, 122);
            this.lblSettingsIcon.Name = "lblSettingsIcon";
            this.lblSettingsIcon.Size = new System.Drawing.Size(27, 25);
            this.lblSettingsIcon.TabIndex = 14;
            this.lblSettingsIcon.Text = "⚙️";
            // 
            // lineDivider1
            // 
            this.lineDivider1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(85)))), ((int)(((byte)(99)))));
            this.lineDivider1.Location = new System.Drawing.Point(14, 102);
            this.lineDivider1.Name = "lineDivider1";
            this.lineDivider1.Size = new System.Drawing.Size(250, 1);
            this.lineDivider1.TabIndex = 12;
            // 
            // lblAnalysisIcon
            // 
            this.lblAnalysisIcon.AutoSize = true;
            this.lblAnalysisIcon.Font = new System.Drawing.Font("Segoe UI Symbol", 13F);
            this.lblAnalysisIcon.ForeColor = System.Drawing.Color.White;
            this.lblAnalysisIcon.Location = new System.Drawing.Point(107, 122);
            this.lblAnalysisIcon.Name = "lblAnalysisIcon";
            this.lblAnalysisIcon.Size = new System.Drawing.Size(30, 25);
            this.lblAnalysisIcon.TabIndex = 7;
            this.lblAnalysisIcon.Text = "📊";
            // 
            // lblVoterListIcon
            // 
            this.lblVoterListIcon.AutoSize = true;
            this.lblVoterListIcon.Font = new System.Drawing.Font("Segoe UI Symbol", 13F);
            this.lblVoterListIcon.ForeColor = System.Drawing.Color.White;
            this.lblVoterListIcon.Location = new System.Drawing.Point(64, 122);
            this.lblVoterListIcon.Name = "lblVoterListIcon";
            this.lblVoterListIcon.Size = new System.Drawing.Size(30, 25);
            this.lblVoterListIcon.TabIndex = 1;
            this.lblVoterListIcon.Text = "📋";
            // 
            // btnCertificate
            // 
            this.btnCertificate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.btnCertificate.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCertificate.FlatAppearance.BorderSize = 0;
            this.btnCertificate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCertificate.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.btnCertificate.ForeColor = System.Drawing.Color.White;
            this.btnCertificate.Location = new System.Drawing.Point(24, 52);
            this.btnCertificate.Name = "btnCertificate";
            this.btnCertificate.Size = new System.Drawing.Size(250, 53);
            this.btnCertificate.TabIndex = 35;
            this.btnCertificate.Text = "Print Certificate";
            this.btnCertificate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCertificate.UseVisualStyleBackColor = false;
            this.btnCertificate.Click += new System.EventHandler(this.btnCertificate_Click);
            // 
            // btnDuplicateList
            // 
            this.btnDuplicateList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.btnDuplicateList.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDuplicateList.FlatAppearance.BorderSize = 0;
            this.btnDuplicateList.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDuplicateList.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.btnDuplicateList.ForeColor = System.Drawing.Color.White;
            this.btnDuplicateList.Location = new System.Drawing.Point(298, 44);
            this.btnDuplicateList.Name = "btnDuplicateList";
            this.btnDuplicateList.Size = new System.Drawing.Size(28, 25);
            this.btnDuplicateList.TabIndex = 19;
            this.btnDuplicateList.Text = "Duplicate List";
            this.btnDuplicateList.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDuplicateList.UseVisualStyleBackColor = false;
            // 
            // lblDuplicateListIcon
            // 
            this.lblDuplicateListIcon.AutoSize = true;
            this.lblDuplicateListIcon.Font = new System.Drawing.Font("Segoe UI Symbol", 13F);
            this.lblDuplicateListIcon.ForeColor = System.Drawing.Color.White;
            this.lblDuplicateListIcon.Location = new System.Drawing.Point(350, 50);
            this.lblDuplicateListIcon.Name = "lblDuplicateListIcon";
            this.lblDuplicateListIcon.Size = new System.Drawing.Size(30, 25);
            this.lblDuplicateListIcon.TabIndex = 20;
            this.lblDuplicateListIcon.Text = "🔄";
            // 
            // btnMasters
            // 
            this.btnMasters.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.btnMasters.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMasters.FlatAppearance.BorderSize = 0;
            this.btnMasters.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMasters.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.btnMasters.ForeColor = System.Drawing.Color.White;
            this.btnMasters.Location = new System.Drawing.Point(173, 36);
            this.btnMasters.Name = "btnMasters";
            this.btnMasters.Size = new System.Drawing.Size(78, 33);
            this.btnMasters.TabIndex = 15;
            this.btnMasters.Text = "Masters";
            this.btnMasters.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnMasters.UseVisualStyleBackColor = false;
            // 
            // lblMastersIcon
            // 
            this.lblMastersIcon.AutoSize = true;
            this.lblMastersIcon.Font = new System.Drawing.Font("Segoe UI Symbol", 13F);
            this.lblMastersIcon.ForeColor = System.Drawing.Color.White;
            this.lblMastersIcon.Location = new System.Drawing.Point(137, 44);
            this.lblMastersIcon.Name = "lblMastersIcon";
            this.lblMastersIcon.Size = new System.Drawing.Size(30, 25);
            this.lblMastersIcon.TabIndex = 16;
            this.lblMastersIcon.Text = "📚";
            // 
            // btnAdvancedFilter
            // 
            this.btnAdvancedFilter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.btnAdvancedFilter.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAdvancedFilter.FlatAppearance.BorderSize = 0;
            this.btnAdvancedFilter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdvancedFilter.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.btnAdvancedFilter.ForeColor = System.Drawing.Color.White;
            this.btnAdvancedFilter.Location = new System.Drawing.Point(475, 16);
            this.btnAdvancedFilter.Name = "btnAdvancedFilter";
            this.btnAdvancedFilter.Size = new System.Drawing.Size(250, 50);
            this.btnAdvancedFilter.TabIndex = 4;
            this.btnAdvancedFilter.Text = "Advanced Filter";
            this.btnAdvancedFilter.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAdvancedFilter.UseVisualStyleBackColor = false;
            // 
            // lblAdvancedFilterIcon
            // 
            this.lblAdvancedFilterIcon.AutoSize = true;
            this.lblAdvancedFilterIcon.Font = new System.Drawing.Font("Segoe UI Symbol", 13F);
            this.lblAdvancedFilterIcon.ForeColor = System.Drawing.Color.White;
            this.lblAdvancedFilterIcon.Location = new System.Drawing.Point(617, 22);
            this.lblAdvancedFilterIcon.Name = "lblAdvancedFilterIcon";
            this.lblAdvancedFilterIcon.Size = new System.Drawing.Size(30, 25);
            this.lblAdvancedFilterIcon.TabIndex = 5;
            this.lblAdvancedFilterIcon.Text = "🔍";
            // 
            // btnOfficeManagement
            // 
            this.btnOfficeManagement.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.btnOfficeManagement.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOfficeManagement.FlatAppearance.BorderSize = 0;
            this.btnOfficeManagement.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOfficeManagement.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.btnOfficeManagement.ForeColor = System.Drawing.Color.White;
            this.btnOfficeManagement.Location = new System.Drawing.Point(272, 9);
            this.btnOfficeManagement.Name = "btnOfficeManagement";
            this.btnOfficeManagement.Size = new System.Drawing.Size(161, 38);
            this.btnOfficeManagement.TabIndex = 10;
            this.btnOfficeManagement.Text = "Office Management";
            this.btnOfficeManagement.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOfficeManagement.UseVisualStyleBackColor = false;
            // 
            // lblOfficeManagementIcon
            // 
            this.lblOfficeManagementIcon.AutoSize = true;
            this.lblOfficeManagementIcon.Font = new System.Drawing.Font("Segoe UI Symbol", 13F);
            this.lblOfficeManagementIcon.ForeColor = System.Drawing.Color.White;
            this.lblOfficeManagementIcon.Location = new System.Drawing.Point(403, 16);
            this.lblOfficeManagementIcon.Name = "lblOfficeManagementIcon";
            this.lblOfficeManagementIcon.Size = new System.Drawing.Size(30, 25);
            this.lblOfficeManagementIcon.TabIndex = 11;
            this.lblOfficeManagementIcon.Text = "🏢";
            // 
            // btnSurveyManagement
            // 
            this.btnSurveyManagement.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.btnSurveyManagement.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSurveyManagement.FlatAppearance.BorderSize = 0;
            this.btnSurveyManagement.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSurveyManagement.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.btnSurveyManagement.ForeColor = System.Drawing.Color.White;
            this.btnSurveyManagement.Location = new System.Drawing.Point(0, 13);
            this.btnSurveyManagement.Name = "btnSurveyManagement";
            this.btnSurveyManagement.Size = new System.Drawing.Size(71, 43);
            this.btnSurveyManagement.TabIndex = 8;
            this.btnSurveyManagement.Text = "Survey Management";
            this.btnSurveyManagement.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSurveyManagement.UseVisualStyleBackColor = false;
            // 
            // lblSurveyManagementIcon
            // 
            this.lblSurveyManagementIcon.AutoSize = true;
            this.lblSurveyManagementIcon.Font = new System.Drawing.Font("Segoe UI Symbol", 13F);
            this.lblSurveyManagementIcon.ForeColor = System.Drawing.Color.White;
            this.lblSurveyManagementIcon.Location = new System.Drawing.Point(19, 22);
            this.lblSurveyManagementIcon.Name = "lblSurveyManagementIcon";
            this.lblSurveyManagementIcon.Size = new System.Drawing.Size(30, 25);
            this.lblSurveyManagementIcon.TabIndex = 9;
            this.lblSurveyManagementIcon.Text = "📝";
            // 
            // btnListGroup
            // 
            this.btnListGroup.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.btnListGroup.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnListGroup.FlatAppearance.BorderSize = 0;
            this.btnListGroup.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnListGroup.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.btnListGroup.ForeColor = System.Drawing.Color.White;
            this.btnListGroup.Location = new System.Drawing.Point(124, 3);
            this.btnListGroup.Name = "btnListGroup";
            this.btnListGroup.Size = new System.Drawing.Size(87, 36);
            this.btnListGroup.TabIndex = 2;
            this.btnListGroup.Text = "List Group";
            this.btnListGroup.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnListGroup.UseVisualStyleBackColor = false;
            // 
            // lblListGroupIcon
            // 
            this.lblListGroupIcon.AutoSize = true;
            this.lblListGroupIcon.Font = new System.Drawing.Font("Segoe UI Symbol", 13F);
            this.lblListGroupIcon.ForeColor = System.Drawing.Color.White;
            this.lblListGroupIcon.Location = new System.Drawing.Point(181, 13);
            this.lblListGroupIcon.Name = "lblListGroupIcon";
            this.lblListGroupIcon.Size = new System.Drawing.Size(30, 25);
            this.lblListGroupIcon.TabIndex = 3;
            this.lblListGroupIcon.Text = "👥";
            // 
            // panelContent
            // 
            this.panelContent.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(24)))), ((int)(((byte)(39)))));
            this.panelContent.Controls.Add(this.panel7);
            this.panelContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContent.Location = new System.Drawing.Point(270, 60);
            this.panelContent.Name = "panelContent";
            this.panelContent.Size = new System.Drawing.Size(973, 689);
            this.panelContent.TabIndex = 2;
            // 
            // panel7
            // 
            this.panel7.BackColor = System.Drawing.Color.Red;
            this.panel7.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel7.Location = new System.Drawing.Point(0, 0);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(973, 1);
            this.panel7.TabIndex = 12;
            // 
            // panelHeader
            // 
            this.panelHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(41)))), ((int)(((byte)(55)))));
            this.panelHeader.Controls.Add(this.lblFullName);
            this.panelHeader.Controls.Add(this.lblTitle);
            this.panelHeader.Controls.Add(this.panel2);
            this.panelHeader.Controls.Add(this.panel1);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(270, 0);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(973, 60);
            this.panelHeader.TabIndex = 1;
            // 
            // lblFullName
            // 
            this.lblFullName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(41)))), ((int)(((byte)(55)))));
            this.lblFullName.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFullName.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.lblFullName.Location = new System.Drawing.Point(435, 14);
            this.lblFullName.Name = "lblFullName";
            this.lblFullName.Size = new System.Drawing.Size(232, 36);
            this.lblFullName.TabIndex = 16;
            this.lblFullName.Text = "label1";
            this.lblFullName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 28F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(4, 6);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(198, 51);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Welcome ";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnCertificate);
            this.panel2.Controls.Add(this.btnListGroup);
            this.panel2.Controls.Add(this.lblListGroupIcon);
            this.panel2.Controls.Add(this.btnSurveyManagement);
            this.panel2.Controls.Add(this.lblSurveyManagementIcon);
            this.panel2.Controls.Add(this.btnOfficeManagement);
            this.panel2.Controls.Add(this.lblOfficeManagementIcon);
            this.panel2.Controls.Add(this.btnMasters);
            this.panel2.Controls.Add(this.lblDuplicateListIcon);
            this.panel2.Controls.Add(this.btnDuplicateList);
            this.panel2.Controls.Add(this.lblMastersIcon);
            this.panel2.Controls.Add(this.btnAdvancedFilter);
            this.panel2.Controls.Add(this.lblAdvancedFilterIcon);
            this.panel2.Location = new System.Drawing.Point(236, 14);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(10, 10);
            this.panel2.TabIndex = 14;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblUsername);
            this.panel1.Controls.Add(this.lblInstitute);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(685, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(288, 60);
            this.panel1.TabIndex = 13;
            // 
            // lblUsername
            // 
            this.lblUsername.AutoSize = true;
            this.lblUsername.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUsername.ForeColor = System.Drawing.Color.Transparent;
            this.lblUsername.Location = new System.Drawing.Point(178, 25);
            this.lblUsername.Name = "lblUsername";
            this.lblUsername.Size = new System.Drawing.Size(81, 18);
            this.lblUsername.TabIndex = 16;
            this.lblUsername.Text = "Admin User";
            // 
            // lblInstitute
            // 
            this.lblInstitute.AutoSize = true;
            this.lblInstitute.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInstitute.ForeColor = System.Drawing.Color.Transparent;
            this.lblInstitute.Location = new System.Drawing.Point(16, 25);
            this.lblInstitute.Name = "lblInstitute";
            this.lblInstitute.Size = new System.Drawing.Size(81, 18);
            this.lblInstitute.TabIndex = 15;
            this.lblInstitute.Text = "Admin User";
            // 
            // panelToggleLanguage
            // 
            this.panelToggleLanguage.Location = new System.Drawing.Point(0, 0);
            this.panelToggleLanguage.Name = "panelToggleLanguage";
            this.panelToggleLanguage.Size = new System.Drawing.Size(200, 100);
            this.panelToggleLanguage.TabIndex = 0;
            // 
            // DashboardForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(24)))), ((int)(((byte)(39)))));
            this.ClientSize = new System.Drawing.Size(1243, 749);
            this.Controls.Add(this.panelContent);
            this.Controls.Add(this.panelHeader);
            this.Controls.Add(this.panelSidebar);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "DashboardForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DashboardForm_FormClosing);
            this.Load += new System.EventHandler(this.DashboardForm_Load);
            this.panelSidebar.ResumeLayout(false);
            this.panelSidebar.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picImportDone)).EndInit();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.panelContent.ResumeLayout(false);
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        // Panels
        private System.Windows.Forms.Panel panelSidebar;
        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Panel panelContent;
        private System.Windows.Forms.Panel panelToggleLanguage;
        private System.Windows.Forms.Panel lineDivider1;
        private System.Windows.Forms.Panel lineDivider2;

        // Header controls
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblVoterListIcon;

        private System.Windows.Forms.Button btnListGroup;
        private System.Windows.Forms.Label lblListGroupIcon;

        private System.Windows.Forms.Button btnAdvancedFilter;
        private System.Windows.Forms.Label lblAdvancedFilterIcon;
        private System.Windows.Forms.Label lblAnalysisIcon;

        private System.Windows.Forms.Button btnSurveyManagement;
        private System.Windows.Forms.Label lblSurveyManagementIcon;

        private System.Windows.Forms.Button btnOfficeManagement;
        private System.Windows.Forms.Label lblOfficeManagementIcon;
        private System.Windows.Forms.Label lblSettingsIcon;

        private System.Windows.Forms.Button btnMasters;
        private System.Windows.Forms.Label lblMastersIcon;
        private System.Windows.Forms.Label lblVotingDayIcon;

        private System.Windows.Forms.Button btnDuplicateList;
        private System.Windows.Forms.Label lblDuplicateListIcon;

 
        private Label lblFavorStrongValue;
        private Label lblFavorOppRatioValue;
        private Label lblSlipPrintedValue;
        private System.Windows.Forms.PictureBox picImportDone;
        private System.Windows.Forms.Label lblImportStatus;
        private System.Windows.Forms.ProgressBar progressImport;
        private System.Windows.Forms.Label lblImportHeader;
        private System.Windows.Forms.Label lblImportUser;
        private Panel panel7;
        private Button button1;
      
        private FlowLayoutPanel flowLayoutPanel1;
        private FlowLayoutPanel flowLayoutPanel2;
        private FlowLayoutPanel flowLayoutPanel3;
        private Panel panel1;
        private Panel panel2;
        private Panel panel3;
        private Label lblInstitute;
        private Label lblFullName;
        private Button btnAssignedCourse;
        private Button btnMaster;
        private Button btnLiveCourses;
        private Label lblUsername;
        private Button btnCertificate;
    }
}