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
   
    public partial class SuccessPopup : Form
    {
        private Timer closeTimer;
        private void lblMessage_Click(object sender, EventArgs e)
        {

        }

        public SuccessPopup(string message)
        {
            InitializeComponent();

            // ---- FORM STYLE ----
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.White;
            this.Size = new Size(400, 180);

            // ---- LABEL MESSAGE ----
            lblMessage.Text = message;
            lblMessage.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            lblMessage.ForeColor = Color.FromArgb(0, 0, 192); 
            lblMessage.TextAlign = ContentAlignment.MiddleCenter;

            // ---- AUTO CLOSE TIMER ----
            closeTimer = new Timer();
            closeTimer.Interval = 2000; // 2 seconds
            closeTimer.Tick += CloseTimer_Tick;
            closeTimer.Start();
        }

        private void CloseTimer_Tick(object sender, EventArgs e)
        {
            closeTimer.Stop();
            this.Close();
        }
    }
}