using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LMSDesk
{
    public partial class ToastControl : UserControl
    {
        public enum ToastType { Success, Error , Warning }

        private readonly Timer fadeTimer = new Timer { Interval = 15 };
        private readonly Timer displayTimer = new Timer();

        private bool fadingIn = true;
        private int alpha = 0; // 0-255
        private readonly int displayMs;

        private readonly string toastText;
        private readonly Color toastColor;

        public ToastControl(string message, ToastType type, int seconds = 3)
        {
            InitializeComponent();

            SetStyle(ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.UserPaint, true);

            Width = 350;
            Height = 60;
            displayMs = seconds * 1000;

            BackColor = Color.FromArgb(248, 249, 250);

            toastText = message;

            toastColor = (type == ToastType.Success)
               ? Color.FromArgb(75, 181, 67)
               : (type == ToastType.Warning)
                 ? Color.FromArgb(255, 193, 7)   // 🟡 Yellow
                 : Color.FromArgb(240, 147, 43);

            fadeTimer.Tick += FadeTimer_Tick;

            displayTimer.Tick += (s, e) =>
            {
                fadingIn = false;
                fadeTimer.Start();
            };
        }

        // ✅ Static method (use from anywhere)
        public static void ShowToast(Control host, string message, ToastType type, int seconds = 3)
        {
            var toast = new ToastControl(message, type, seconds);

            host.Controls.Add(toast);
            toast.BringToFront();

            toast.Location = new Point(
                host.ClientSize.Width - toast.Width - 20,
                20
            );

            toast.fadeTimer.Start();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            using (var path = GetRoundRect(ClientRectangle, 10))
            using (var bg = new SolidBrush(Color.FromArgb(alpha, BackColor)))
            using (var borderPen = new Pen(toastColor, 2))
            {
                e.Graphics.FillPath(bg, path);
                e.Graphics.DrawPath(borderPen, path);
            }

            // Icon
            Rectangle iconRect = new Rectangle(15, 18, 24, 24);

            using (var brush = new SolidBrush(Color.FromArgb(alpha, toastColor)))
                e.Graphics.FillEllipse(brush, iconRect);

            using (var pen = new Pen(Color.White, 2))
            {
                if (toastColor == Color.FromArgb(75, 181, 67))
                {
                    // ✔
                    e.Graphics.DrawLine(pen, iconRect.Left + 6, iconRect.Top + 12, iconRect.Left + 10, iconRect.Top + 16);
                    e.Graphics.DrawLine(pen, iconRect.Left + 10, iconRect.Top + 16, iconRect.Left + 18, iconRect.Top + 8);
                }
                else
                {
                    // ✖
                    e.Graphics.DrawLine(pen, iconRect.Left + 8, iconRect.Top + 8, iconRect.Left + 16, iconRect.Top + 16);
                    e.Graphics.DrawLine(pen, iconRect.Left + 16, iconRect.Top + 8, iconRect.Left + 8, iconRect.Top + 16);
                }
            }

            // Text
            using (var font = new Font("Segoe UI", 11.5f, FontStyle.Bold))
            using (var textBrush = new SolidBrush(Color.FromArgb(alpha, Color.Black)))
            {
                RectangleF textRect = new RectangleF(50, 18, Width - 60, Height - 36);
                e.Graphics.DrawString(toastText, font, textBrush, textRect);
            }
        }

        private void FadeTimer_Tick(object sender, EventArgs e)
        {
            alpha += fadingIn ? 20 : -20;

            if (alpha >= 240)
            {
                alpha = 240;
                fadeTimer.Stop();
                displayTimer.Interval = displayMs;
                displayTimer.Start();
            }
            else if (alpha <= 0)
            {
                alpha = 0;
                fadeTimer.Stop();

                Parent?.Controls.Remove(this);
                Dispose();
            }

            Invalidate();
        }

        private GraphicsPath GetRoundRect(Rectangle r, int radius)
        {
            int d = radius * 2;
            GraphicsPath path = new GraphicsPath();

            path.AddArc(r.Left, r.Top, d, d, 180, 90);
            path.AddArc(r.Right - d, r.Top, d, d, 270, 90);
            path.AddArc(r.Right - d, r.Bottom - d, d, d, 0, 90);
            path.AddArc(r.Left, r.Bottom - d, d, d, 90, 90);

            path.CloseFigure();
            return path;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ToastControl.ShowToast(this, "Worker added successfully!", ToastType.Success, 3);
        }

        private void Error_Click(object sender, EventArgs e)
        {
            ToastControl.ShowToast(this, "Error occurred!", ToastType.Error, 3);
        }
    }
}