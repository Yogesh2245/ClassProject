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
    public partial class ValidationPopup : Form
    {
        public ValidationPopup(string message)
        {
            InitializeComponent();
            lblvalid.Text = message;

            // Auto close after 2 sec
            Timer timer = new Timer();
            timer.Interval = 2000;
            timer.Tick += (s, e) =>
            {
                timer.Stop();
                this.Close();
            };
            timer.Start();
        }
    }
}