namespace LMSDesk
{
    partial class ValidationPopup
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblvalid = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblvalid
            // 
            this.lblvalid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblvalid.Font = new System.Drawing.Font("Stencil", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblvalid.ForeColor = System.Drawing.Color.Maroon;
            this.lblvalid.Location = new System.Drawing.Point(0, 0);
            this.lblvalid.Name = "lblvalid";
            this.lblvalid.Size = new System.Drawing.Size(335, 41);
            this.lblvalid.TabIndex = 0;
            this.lblvalid.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ValidationPopup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.MistyRose;
            this.ClientSize = new System.Drawing.Size(335, 41);
            this.Controls.Add(this.lblvalid);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ValidationPopup";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ValidationPopup";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblvalid;
    }
}