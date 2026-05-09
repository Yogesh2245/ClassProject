using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LMSDesk
{
    public partial class DashbordForm1 : Form
    {

        public string InstituteName { get; }
  
        Button currentActiveButton = null;

        public DashbordForm1(string instituteName)
        {
            InitializeComponent();

            InstituteName = instituteName;
            lblInstitute.Text = instituteName; // 👈 your label
        } 
       
        private void AddHoverEffect(Button btn)
        {
            Color defaultColor = Color.FromArgb(64, 64, 64);
            Color hoverColor = Color.FromArgb(37, 99, 235);
            Color activeColor = Color.DodgerBlue;

            Point originalLocation = btn.Location;

            btn.MouseEnter += (s, e) =>
            {
                if (btn != currentActiveButton)   // 👈 IMPORTANT
                {
                    btn.BackColor = hoverColor;
                    btn.ForeColor = Color.White;
                }

                btn.Cursor = Cursors.Hand;
                btn.Location = new Point(originalLocation.X, originalLocation.Y - 4);
            };

            btn.MouseLeave += (s, e) =>
            {
                if (btn != currentActiveButton)   // 👈 IMPORTANT
                {
                    btn.BackColor = defaultColor;
                    btn.ForeColor = Color.White;
                }

                btn.Location = originalLocation;
            };
        }
        private void LoadFormIntoPanel(Form form)
        {
            panelContent.Controls.Clear();

            form.TopLevel = false;
            form.FormBorderStyle = FormBorderStyle.None;
            form.Dock = DockStyle.Fill;

            panelContent.Controls.Add(form);
            form.Show();
        }

        private void DashbordForm1_Load(object sender, EventArgs e)
        {
            AddHoverEffect(button1);
            AddHoverEffect(btnCertificate);
            AddHoverEffect(butnmarklist);

            SetActiveButton(button1);
        } 

        private void DashbordForm1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        } 
        private void butnmarklist_Click(object sender, EventArgs e)
        {
            SetActiveButton(butnmarklist);

            LoadFormIntoPanel(new marklistprint());
        }
        private void SetActiveButton(Button btn)
        {
            Color defaultColor = Color.FromArgb(64, 64, 64);
            Color activeColor = Color.DodgerBlue;

            Button[] sidebarButtons =
            {
                button1,
                btnCertificate,
                butnmarklist
            };

            foreach (Button b in sidebarButtons)
            {
                b.BackColor = defaultColor;
                b.ForeColor = Color.White;
            }

            currentActiveButton = btn;
            btn.BackColor = activeColor;
        }

        private void btnCertificate_Click(object sender, EventArgs e)
        {
            SetActiveButton(btnCertificate);
            LoadFormIntoPanel(new requested());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SetActiveButton(button1);

            // Home dashboard / default form load करायचा असेल तर
            panelContent.Controls.Clear();

            // किंवा default form
            // LoadFormIntoPanel(new HomeDashboard());
        }

        private void btnAddInstitute_Click(object sender, EventArgs e)
        {
            SetActiveButton(butnmarklist);

            LoadFormIntoPanel(new Reg_InstituteMain());
        }

        private void btnPrintCertificate_Click(object sender, EventArgs e)
        {
            SetActiveButton(butnmarklist);

            LoadFormIntoPanel(new InstitutePrintForm());
        }
    }
}
