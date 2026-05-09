using System.Windows.Forms;

namespace LMSDesk
{
    partial class FinalMarklistPrint
    {
        private System.ComponentModel.IContainer components = null;

        // Layout Containers
        private System.Windows.Forms.TableLayoutPanel tlMain;
        private System.Windows.Forms.Panel pnlSidebar;
        private System.Windows.Forms.FlowLayoutPanel flpVerticalStack;
        private System.Windows.Forms.GroupBox grpPersonal;
        private System.Windows.Forms.GroupBox grpAcademic;
        private System.Windows.Forms.FlowLayoutPanel flpAcademicInner;
        private System.Windows.Forms.Panel pnlButtonContainer;
        private System.Windows.Forms.FlowLayoutPanel flpSubInputRow;

        // Inputs
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.TextBox txtGuardian;
        private System.Windows.Forms.DateTimePicker dtpDOB;
        private System.Windows.Forms.TextBox txtDuration;
        private System.Windows.Forms.TextBox txtSerial;
        private System.Windows.Forms.DateTimePicker dtpExam;
        private System.Windows.Forms.TextBox txtCourse;
        private System.Windows.Forms.TextBox txtInstitute;
        private System.Windows.Forms.TextBox txtGrade;
        private System.Windows.Forms.TextBox txtSession;

        // Dynamic Subject Grid Controls
        private System.Windows.Forms.DataGridView dgvSubjects;
        private System.Windows.Forms.TextBox txtSubName;
        private System.Windows.Forms.TextBox txtSubMax;
        private System.Windows.Forms.TextBox txtSubObt;
        private System.Windows.Forms.TextBox txtSubRem;
        private System.Windows.Forms.Button btnAddSubject;
        private System.Windows.Forms.Label lblSubjectTitle;

        // Action Controls
        private System.Windows.Forms.PictureBox picMarkList;
        private System.Windows.Forms.Button btnPreview;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnUploadPhoto;
        private System.Windows.Forms.Button btnUploadLogo;
        private System.Windows.Forms.DataGridViewButtonColumn btnDeleteCol;

        // Labels
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblGuardian;
        private System.Windows.Forms.Label lblDOB;
        private System.Windows.Forms.Label lblDuration;
        private System.Windows.Forms.Label lblExam;
        private System.Windows.Forms.Label lblSerial;
        private System.Windows.Forms.Label lblCourse;
        private System.Windows.Forms.Label lblInstitute;
        private System.Windows.Forms.Label lblGrade;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tlMain = new System.Windows.Forms.TableLayoutPanel();
            this.pnlSidebar = new System.Windows.Forms.Panel();
            this.flpVerticalStack = new System.Windows.Forms.FlowLayoutPanel();
            this.grpPersonal = new System.Windows.Forms.GroupBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblBranchCOde = new System.Windows.Forms.Label();
            this.lblStudentId = new System.Windows.Forms.Label();
            this.lblCourseId = new System.Windows.Forms.Label();
            this.lblcoursecode = new System.Windows.Forms.Label();
            this.pcbphoto = new System.Windows.Forms.PictureBox();
            this.lblName = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.marklisid = new System.Windows.Forms.Label();
            this.lblGuardian = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtGuardian = new System.Windows.Forms.TextBox();
            this.btnUploadPhoto = new System.Windows.Forms.Button();
            this.lblDOB = new System.Windows.Forms.Label();
            this.dtpDOB = new System.Windows.Forms.DateTimePicker();
            this.grpAcademic = new System.Windows.Forms.GroupBox();
            this.flpAcademicInner = new System.Windows.Forms.FlowLayoutPanel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblExam = new System.Windows.Forms.Label();
            this.dtpExam = new System.Windows.Forms.DateTimePicker();
            this.lblDuration = new System.Windows.Forms.Label();
            this.lblSerial = new System.Windows.Forms.Label();
            this.txtDuration = new System.Windows.Forms.TextBox();
            this.btnUploadLogo = new System.Windows.Forms.Button();
            this.txtSession = new System.Windows.Forms.TextBox();
            this.txtSerial = new System.Windows.Forms.TextBox();
            this.txtInstitute = new System.Windows.Forms.TextBox();
            this.lblCourse = new System.Windows.Forms.Label();
            this.lblInstitute = new System.Windows.Forms.Label();
            this.txtCourse = new System.Windows.Forms.TextBox();
            this.lblSubjectTitle = new System.Windows.Forms.Label();
            this.flpSubInputRow = new System.Windows.Forms.FlowLayoutPanel();
            this.txtSubName = new System.Windows.Forms.TextBox();
            this.txtSubMax = new System.Windows.Forms.TextBox();
            this.txtSubObt = new System.Windows.Forms.TextBox();
            this.txtSubRem = new System.Windows.Forms.TextBox();
            this.btnAddSubject = new System.Windows.Forms.Button();
            this.dgvSubjects = new System.Windows.Forms.DataGridView();
            this.Subject = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Maxmium_Marks = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Marks_Obtained = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Remarks = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnDeleteCol = new System.Windows.Forms.DataGridViewButtonColumn();
            this.panel3 = new System.Windows.Forms.Panel();
            this.lblGrade = new System.Windows.Forms.Label();
            this.txtGrade = new System.Windows.Forms.TextBox();
            this.pnlButtonContainer = new System.Windows.Forms.Panel();
            this.btnprint = new System.Windows.Forms.Button();
            this.btnPreview = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.picMarkList = new System.Windows.Forms.PictureBox();
            this.chkPrintPhoto = new System.Windows.Forms.CheckBox();
            this.tlMain.SuspendLayout();
            this.pnlSidebar.SuspendLayout();
            this.flpVerticalStack.SuspendLayout();
            this.grpPersonal.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pcbphoto)).BeginInit();
            this.grpAcademic.SuspendLayout();
            this.flpAcademicInner.SuspendLayout();
            this.panel2.SuspendLayout();
            this.flpSubInputRow.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSubjects)).BeginInit();
            this.panel3.SuspendLayout();
            this.pnlButtonContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picMarkList)).BeginInit();
            this.SuspendLayout();
            // 
            // tlMain
            // 
            this.tlMain.ColumnCount = 2;
            this.tlMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 486F));
            this.tlMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlMain.Controls.Add(this.pnlSidebar, 0, 0);
            this.tlMain.Controls.Add(this.picMarkList, 1, 0);
            this.tlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlMain.Location = new System.Drawing.Point(0, 0);
            this.tlMain.Name = "tlMain";
            this.tlMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlMain.Size = new System.Drawing.Size(1250, 749);
            this.tlMain.TabIndex = 0;
            // 
            // pnlSidebar
            // 
            this.pnlSidebar.Controls.Add(this.flpVerticalStack);
            this.pnlSidebar.Controls.Add(this.pnlButtonContainer);
            this.pnlSidebar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlSidebar.Location = new System.Drawing.Point(3, 3);
            this.pnlSidebar.Name = "pnlSidebar";
            this.pnlSidebar.Size = new System.Drawing.Size(480, 743);
            this.pnlSidebar.TabIndex = 0;
            // 
            // flpVerticalStack
            // 
            this.flpVerticalStack.AutoScroll = true;
            this.flpVerticalStack.BackColor = System.Drawing.Color.White;
            this.flpVerticalStack.Controls.Add(this.grpPersonal);
            this.flpVerticalStack.Controls.Add(this.grpAcademic);
            this.flpVerticalStack.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpVerticalStack.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpVerticalStack.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.flpVerticalStack.Location = new System.Drawing.Point(0, 0);
            this.flpVerticalStack.Name = "flpVerticalStack";
            this.flpVerticalStack.Size = new System.Drawing.Size(480, 627);
            this.flpVerticalStack.TabIndex = 0;
            this.flpVerticalStack.WrapContents = false;
            // 
            // grpPersonal
            // 
            this.grpPersonal.Controls.Add(this.panel1);
            this.grpPersonal.Location = new System.Drawing.Point(3, 3);
            this.grpPersonal.Name = "grpPersonal";
            this.grpPersonal.Size = new System.Drawing.Size(452, 168);
            this.grpPersonal.TabIndex = 0;
            this.grpPersonal.TabStop = false;
            this.grpPersonal.Text = "PERSONAL DETAILS";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.chkPrintPhoto);
            this.panel1.Controls.Add(this.lblBranchCOde);
            this.panel1.Controls.Add(this.lblStudentId);
            this.panel1.Controls.Add(this.lblCourseId);
            this.panel1.Controls.Add(this.lblcoursecode);
            this.panel1.Controls.Add(this.pcbphoto);
            this.panel1.Controls.Add(this.lblName);
            this.panel1.Controls.Add(this.txtName);
            this.panel1.Controls.Add(this.marklisid);
            this.panel1.Controls.Add(this.lblGuardian);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.txtGuardian);
            this.panel1.Controls.Add(this.btnUploadPhoto);
            this.panel1.Controls.Add(this.lblDOB);
            this.panel1.Controls.Add(this.dtpDOB);
            this.panel1.Location = new System.Drawing.Point(6, 19);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(443, 143);
            this.panel1.TabIndex = 1;
            // 
            // lblBranchCOde
            // 
            this.lblBranchCOde.AutoSize = true;
            this.lblBranchCOde.Location = new System.Drawing.Point(308, 187);
            this.lblBranchCOde.Name = "lblBranchCOde";
            this.lblBranchCOde.Size = new System.Drawing.Size(14, 15);
            this.lblBranchCOde.TabIndex = 12;
            this.lblBranchCOde.Text = "0";
            // 
            // lblStudentId
            // 
            this.lblStudentId.AutoSize = true;
            this.lblStudentId.Location = new System.Drawing.Point(366, 149);
            this.lblStudentId.Name = "lblStudentId";
            this.lblStudentId.Size = new System.Drawing.Size(14, 15);
            this.lblStudentId.TabIndex = 12;
            this.lblStudentId.Text = "0";
            // 
            // lblCourseId
            // 
            this.lblCourseId.AutoSize = true;
            this.lblCourseId.Location = new System.Drawing.Point(346, 173);
            this.lblCourseId.Name = "lblCourseId";
            this.lblCourseId.Size = new System.Drawing.Size(14, 15);
            this.lblCourseId.TabIndex = 11;
            this.lblCourseId.Text = "0";
            // 
            // lblcoursecode
            // 
            this.lblcoursecode.AutoSize = true;
            this.lblcoursecode.Location = new System.Drawing.Point(384, 173);
            this.lblcoursecode.Name = "lblcoursecode";
            this.lblcoursecode.Size = new System.Drawing.Size(14, 15);
            this.lblcoursecode.TabIndex = 10;
            this.lblcoursecode.Text = "0";
            // 
            // pcbphoto
            // 
            this.pcbphoto.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pcbphoto.Location = new System.Drawing.Point(363, 0);
            this.pcbphoto.Name = "pcbphoto";
            this.pcbphoto.Size = new System.Drawing.Size(78, 78);
            this.pcbphoto.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pcbphoto.TabIndex = 9;
            this.pcbphoto.TabStop = false;
            // 
            // lblName
            // 
            this.lblName.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblName.Location = new System.Drawing.Point(15, 12);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(89, 23);
            this.lblName.TabIndex = 0;
            this.lblName.Text = "Full Name:";
            // 
            // txtName
            // 
            this.txtName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtName.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtName.Location = new System.Drawing.Point(123, 8);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(224, 27);
            this.txtName.TabIndex = 1;
            // 
            // marklisid
            // 
            this.marklisid.AutoSize = true;
            this.marklisid.Location = new System.Drawing.Point(21, -1);
            this.marklisid.Name = "marklisid";
            this.marklisid.Size = new System.Drawing.Size(14, 15);
            this.marklisid.TabIndex = 8;
            this.marklisid.Text = "0";
            this.marklisid.TextChanged += new System.EventHandler(this.marklisid_TextChanged);
            // 
            // lblGuardian
            // 
            this.lblGuardian.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGuardian.Location = new System.Drawing.Point(13, 37);
            this.lblGuardian.Name = "lblGuardian";
            this.lblGuardian.Size = new System.Drawing.Size(100, 23);
            this.lblGuardian.TabIndex = 2;
            this.lblGuardian.Text = "Parent Name:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(31, 193);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 15);
            this.label1.TabIndex = 7;
            this.label1.Text = "label1";
            // 
            // txtGuardian
            // 
            this.txtGuardian.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtGuardian.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtGuardian.Location = new System.Drawing.Point(123, 37);
            this.txtGuardian.Name = "txtGuardian";
            this.txtGuardian.Size = new System.Drawing.Size(224, 27);
            this.txtGuardian.TabIndex = 3;
            // 
            // btnUploadPhoto
            // 
            this.btnUploadPhoto.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUploadPhoto.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUploadPhoto.Location = new System.Drawing.Point(121, 99);
            this.btnUploadPhoto.Name = "btnUploadPhoto";
            this.btnUploadPhoto.Size = new System.Drawing.Size(181, 23);
            this.btnUploadPhoto.TabIndex = 6;
            this.btnUploadPhoto.Text = "UPLOAD CANDIDATE PHOTO";
            // 
            // lblDOB
            // 
            this.lblDOB.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDOB.Location = new System.Drawing.Point(13, 66);
            this.lblDOB.Name = "lblDOB";
            this.lblDOB.Size = new System.Drawing.Size(100, 23);
            this.lblDOB.TabIndex = 4;
            this.lblDOB.Text = "Date of Birth:";
            // 
            // dtpDOB
            // 
            this.dtpDOB.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpDOB.Location = new System.Drawing.Point(123, 66);
            this.dtpDOB.Name = "dtpDOB";
            this.dtpDOB.Size = new System.Drawing.Size(146, 27);
            this.dtpDOB.TabIndex = 5;
            // 
            // grpAcademic
            // 
            this.grpAcademic.Controls.Add(this.flpAcademicInner);
            this.grpAcademic.Location = new System.Drawing.Point(3, 177);
            this.grpAcademic.Name = "grpAcademic";
            this.grpAcademic.Size = new System.Drawing.Size(455, 604);
            this.grpAcademic.TabIndex = 1;
            this.grpAcademic.TabStop = false;
            this.grpAcademic.Text = "ACADEMIC DETAILS";
            // 
            // flpAcademicInner
            // 
            this.flpAcademicInner.Controls.Add(this.panel2);
            this.flpAcademicInner.Controls.Add(this.lblSubjectTitle);
            this.flpAcademicInner.Controls.Add(this.flpSubInputRow);
            this.flpAcademicInner.Controls.Add(this.dgvSubjects);
            this.flpAcademicInner.Controls.Add(this.panel3);
            this.flpAcademicInner.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpAcademicInner.Location = new System.Drawing.Point(3, 16);
            this.flpAcademicInner.Name = "flpAcademicInner";
            this.flpAcademicInner.Size = new System.Drawing.Size(449, 648);
            this.flpAcademicInner.TabIndex = 0;
            this.flpAcademicInner.WrapContents = false;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.panel2.Controls.Add(this.lblExam);
            this.panel2.Controls.Add(this.dtpExam);
            this.panel2.Controls.Add(this.lblDuration);
            this.panel2.Controls.Add(this.lblSerial);
            this.panel2.Controls.Add(this.txtDuration);
            this.panel2.Controls.Add(this.btnUploadLogo);
            this.panel2.Controls.Add(this.txtSession);
            this.panel2.Controls.Add(this.txtSerial);
            this.panel2.Controls.Add(this.txtInstitute);
            this.panel2.Controls.Add(this.lblCourse);
            this.panel2.Controls.Add(this.lblInstitute);
            this.panel2.Controls.Add(this.txtCourse);
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(441, 142);
            this.panel2.TabIndex = 9;
            // 
            // lblExam
            // 
            this.lblExam.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblExam.Location = new System.Drawing.Point(136, 9);
            this.lblExam.Name = "lblExam";
            this.lblExam.Size = new System.Drawing.Size(100, 23);
            this.lblExam.TabIndex = 2;
            this.lblExam.Text = "Exam Date";
            // 
            // dtpExam
            // 
            this.dtpExam.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpExam.Location = new System.Drawing.Point(137, 35);
            this.dtpExam.Name = "dtpExam";
            this.dtpExam.Size = new System.Drawing.Size(121, 27);
            this.dtpExam.TabIndex = 3;
            // 
            // lblDuration
            // 
            this.lblDuration.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDuration.Location = new System.Drawing.Point(12, 9);
            this.lblDuration.Name = "lblDuration";
            this.lblDuration.Size = new System.Drawing.Size(118, 23);
            this.lblDuration.TabIndex = 0;
            this.lblDuration.Text = "Course Duration:";
            // 
            // lblSerial
            // 
            this.lblSerial.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSerial.Location = new System.Drawing.Point(260, 9);
            this.lblSerial.Name = "lblSerial";
            this.lblSerial.Size = new System.Drawing.Size(100, 23);
            this.lblSerial.TabIndex = 4;
            this.lblSerial.Text = "Serial No:";
            // 
            // txtDuration
            // 
            this.txtDuration.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDuration.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDuration.Location = new System.Drawing.Point(14, 35);
            this.txtDuration.Name = "txtDuration";
            this.txtDuration.Size = new System.Drawing.Size(118, 27);
            this.txtDuration.TabIndex = 1;
            // 
            // btnUploadLogo
            // 
            this.btnUploadLogo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUploadLogo.Location = new System.Drawing.Point(482, 68);
            this.btnUploadLogo.Name = "btnUploadLogo";
            this.btnUploadLogo.Size = new System.Drawing.Size(10, 23);
            this.btnUploadLogo.TabIndex = 16;
            this.btnUploadLogo.Text = "UPLOAD INSTITUTE LOGO";
            // 
            // txtSession
            // 
            this.txtSession.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSession.Location = new System.Drawing.Point(476, 108);
            this.txtSession.Name = "txtSession";
            this.txtSession.Size = new System.Drawing.Size(260, 23);
            this.txtSession.TabIndex = 18;
            // 
            // txtSerial
            // 
            this.txtSerial.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSerial.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSerial.Location = new System.Drawing.Point(264, 35);
            this.txtSerial.Name = "txtSerial";
            this.txtSerial.Size = new System.Drawing.Size(115, 27);
            this.txtSerial.TabIndex = 5;
            // 
            // txtInstitute
            // 
            this.txtInstitute.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtInstitute.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtInstitute.Location = new System.Drawing.Point(137, 107);
            this.txtInstitute.Name = "txtInstitute";
            this.txtInstitute.Size = new System.Drawing.Size(242, 27);
            this.txtInstitute.TabIndex = 9;
            // 
            // lblCourse
            // 
            this.lblCourse.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCourse.Location = new System.Drawing.Point(15, 75);
            this.lblCourse.Name = "lblCourse";
            this.lblCourse.Size = new System.Drawing.Size(100, 23);
            this.lblCourse.TabIndex = 6;
            this.lblCourse.Text = "Course Name:";
            // 
            // lblInstitute
            // 
            this.lblInstitute.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInstitute.Location = new System.Drawing.Point(15, 107);
            this.lblInstitute.Name = "lblInstitute";
            this.lblInstitute.Size = new System.Drawing.Size(100, 23);
            this.lblInstitute.TabIndex = 8;
            this.lblInstitute.Text = "Branch :";
            // 
            // txtCourse
            // 
            this.txtCourse.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCourse.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCourse.Location = new System.Drawing.Point(137, 71);
            this.txtCourse.Name = "txtCourse";
            this.txtCourse.Size = new System.Drawing.Size(242, 27);
            this.txtCourse.TabIndex = 7;
            // 
            // lblSubjectTitle
            // 
            this.lblSubjectTitle.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubjectTitle.Location = new System.Drawing.Point(3, 148);
            this.lblSubjectTitle.Name = "lblSubjectTitle";
            this.lblSubjectTitle.Size = new System.Drawing.Size(302, 23);
            this.lblSubjectTitle.TabIndex = 10;
            this.lblSubjectTitle.Text = "ENTRY SUBJECT :";
            // 
            // flpSubInputRow
            // 
            this.flpSubInputRow.Controls.Add(this.txtSubName);
            this.flpSubInputRow.Controls.Add(this.txtSubMax);
            this.flpSubInputRow.Controls.Add(this.txtSubObt);
            this.flpSubInputRow.Controls.Add(this.txtSubRem);
            this.flpSubInputRow.Controls.Add(this.btnAddSubject);
            this.flpSubInputRow.Location = new System.Drawing.Point(0, 171);
            this.flpSubInputRow.Margin = new System.Windows.Forms.Padding(0);
            this.flpSubInputRow.Name = "flpSubInputRow";
            this.flpSubInputRow.Size = new System.Drawing.Size(452, 30);
            this.flpSubInputRow.TabIndex = 11;
            // 
            // txtSubName
            // 
            this.txtSubName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSubName.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSubName.Location = new System.Drawing.Point(3, 3);
            this.txtSubName.Name = "txtSubName";
            this.txtSubName.Size = new System.Drawing.Size(130, 27);
            this.txtSubName.TabIndex = 0;
            // 
            // txtSubMax
            // 
            this.txtSubMax.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSubMax.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSubMax.Location = new System.Drawing.Point(139, 3);
            this.txtSubMax.Name = "txtSubMax";
            this.txtSubMax.Size = new System.Drawing.Size(80, 27);
            this.txtSubMax.TabIndex = 1;
            // 
            // txtSubObt
            // 
            this.txtSubObt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSubObt.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSubObt.Location = new System.Drawing.Point(225, 3);
            this.txtSubObt.Name = "txtSubObt";
            this.txtSubObt.Size = new System.Drawing.Size(80, 27);
            this.txtSubObt.TabIndex = 2;
            // 
            // txtSubRem
            // 
            this.txtSubRem.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSubRem.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSubRem.Location = new System.Drawing.Point(311, 3);
            this.txtSubRem.Name = "txtSubRem";
            this.txtSubRem.Size = new System.Drawing.Size(72, 27);
            this.txtSubRem.TabIndex = 3;
            // 
            // btnAddSubject
            // 
            this.btnAddSubject.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddSubject.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddSubject.Location = new System.Drawing.Point(389, 3);
            this.btnAddSubject.Name = "btnAddSubject";
            this.btnAddSubject.Size = new System.Drawing.Size(57, 24);
            this.btnAddSubject.TabIndex = 12;
            this.btnAddSubject.Text = "+";
            // 
            // dgvSubjects
            // 
            this.dgvSubjects.AllowUserToAddRows = false;
            this.dgvSubjects.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvSubjects.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvSubjects.ColumnHeadersHeight = 35;
            this.dgvSubjects.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvSubjects.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Subject,
            this.Maxmium_Marks,
            this.Marks_Obtained,
            this.Remarks,
            this.btnDeleteCol});
            this.dgvSubjects.Location = new System.Drawing.Point(3, 204);
            this.dgvSubjects.Name = "dgvSubjects";
            this.dgvSubjects.RowHeadersVisible = false;
            this.dgvSubjects.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSubjects.Size = new System.Drawing.Size(443, 150);
            this.dgvSubjects.TabIndex = 13;
            // 
            // Subject
            // 
            this.Subject.HeaderText = "Subject";
            this.Subject.Name = "Subject";
            this.Subject.Width = 125;
            // 
            // Maxmium_Marks
            // 
            this.Maxmium_Marks.HeaderText = "Maxmium\nMarks";
            this.Maxmium_Marks.Name = "Maxmium_Marks";
            this.Maxmium_Marks.Width = 90;
            // 
            // Marks_Obtained
            // 
            this.Marks_Obtained.HeaderText = "Obtained\nMarks";
            this.Marks_Obtained.Name = "Marks_Obtained";
            this.Marks_Obtained.Width = 90;
            // 
            // Remarks
            // 
            this.Remarks.HeaderText = "Remarks";
            this.Remarks.Name = "Remarks";
            this.Remarks.Width = 80;
            // 
            // btnDeleteCol
            // 
            this.btnDeleteCol.HeaderText = "Action";
            this.btnDeleteCol.Name = "btnDeleteCol";
            this.btnDeleteCol.Text = "-";
            this.btnDeleteCol.UseColumnTextForButtonValue = true;
            this.btnDeleteCol.Width = 60;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.lblGrade);
            this.panel3.Controls.Add(this.txtGrade);
            this.panel3.Location = new System.Drawing.Point(3, 360);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(223, 37);
            this.panel3.TabIndex = 17;
            // 
            // lblGrade
            // 
            this.lblGrade.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGrade.Location = new System.Drawing.Point(3, 10);
            this.lblGrade.Name = "lblGrade";
            this.lblGrade.Size = new System.Drawing.Size(79, 23);
            this.lblGrade.TabIndex = 14;
            this.lblGrade.Text = "Final Grade:";
            // 
            // txtGrade
            // 
            this.txtGrade.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtGrade.Location = new System.Drawing.Point(88, 5);
            this.txtGrade.Name = "txtGrade";
            this.txtGrade.Size = new System.Drawing.Size(115, 23);
            this.txtGrade.TabIndex = 15;
            // 
            // pnlButtonContainer
            // 
            this.pnlButtonContainer.BackColor = System.Drawing.Color.White;
            this.pnlButtonContainer.Controls.Add(this.btnprint);
            this.pnlButtonContainer.Controls.Add(this.btnPreview);
            this.pnlButtonContainer.Controls.Add(this.btnSave);
            this.pnlButtonContainer.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlButtonContainer.Location = new System.Drawing.Point(0, 627);
            this.pnlButtonContainer.Name = "pnlButtonContainer";
            this.pnlButtonContainer.Size = new System.Drawing.Size(480, 116);
            this.pnlButtonContainer.TabIndex = 1;
            // 
            // btnprint
            // 
            this.btnprint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnprint.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnprint.Location = new System.Drawing.Point(284, 8);
            this.btnprint.Name = "btnprint";
            this.btnprint.Size = new System.Drawing.Size(123, 40);
            this.btnprint.TabIndex = 2;
            this.btnprint.Text = "PRINT";
            this.btnprint.Click += new System.EventHandler(this.btnprint_Click);
            // 
            // btnPreview
            // 
            this.btnPreview.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPreview.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPreview.Location = new System.Drawing.Point(5, 8);
            this.btnPreview.Name = "btnPreview";
            this.btnPreview.Size = new System.Drawing.Size(140, 40);
            this.btnPreview.TabIndex = 0;
            this.btnPreview.Text = "GENERATE PREVIEW";
            // 
            // btnSave
            // 
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Location = new System.Drawing.Point(153, 8);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(123, 40);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "SAVE AS IMAGE";
            // 
            // picMarkList
            // 
            this.picMarkList.BackColor = System.Drawing.Color.White;
            this.picMarkList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picMarkList.Location = new System.Drawing.Point(489, 3);
            this.picMarkList.Name = "picMarkList";
            this.picMarkList.Size = new System.Drawing.Size(758, 743);
            this.picMarkList.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picMarkList.TabIndex = 1;
            this.picMarkList.TabStop = false;
            // 
            // chkPrintPhoto
            // 
            this.chkPrintPhoto.AutoSize = true;
            this.chkPrintPhoto.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkPrintPhoto.Location = new System.Drawing.Point(323, 81);
            this.chkPrintPhoto.Name = "chkPrintPhoto";
            this.chkPrintPhoto.Size = new System.Drawing.Size(119, 23);
            this.chkPrintPhoto.TabIndex = 13;
            this.chkPrintPhoto.Text = "PRINT PHOTO";
            this.chkPrintPhoto.UseVisualStyleBackColor = true;
            this.chkPrintPhoto.CheckedChanged += new System.EventHandler(this.chkPrintPhoto_CheckedChanged);
            // 
            // FinalMarklistPrint
            // 
            this.ClientSize = new System.Drawing.Size(1250, 749);
            this.Controls.Add(this.tlMain);
            this.Name = "FinalMarklistPrint";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Enterprise Mark List System";
            this.Load += new System.EventHandler(this.FinalMarklistPrint_Load);
            this.tlMain.ResumeLayout(false);
            this.pnlSidebar.ResumeLayout(false);
            this.flpVerticalStack.ResumeLayout(false);
            this.grpPersonal.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pcbphoto)).EndInit();
            this.grpAcademic.ResumeLayout(false);
            this.flpAcademicInner.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.flpSubInputRow.ResumeLayout(false);
            this.flpSubInputRow.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSubjects)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.pnlButtonContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picMarkList)).EndInit();
            this.ResumeLayout(false);

        }
        private System.Windows.Forms.DataGridViewTextBoxColumn Subject;
        private System.Windows.Forms.DataGridViewTextBoxColumn Maxmium_Marks;
        private System.Windows.Forms.DataGridViewTextBoxColumn Marks_Obtained;
        private System.Windows.Forms.DataGridViewTextBoxColumn Remarks;
        private Label label1;
        public Label marklisid;
        private Panel panel1;
        private Panel panel2;
        private PictureBox pcbphoto;
        private Button btnprint;
        public Label lblCourseId;
        public Label lblcoursecode;
        public Label lblStudentId;
        public Label lblBranchCOde;
        private Panel panel3;
        private CheckBox chkPrintPhoto;
    }
}