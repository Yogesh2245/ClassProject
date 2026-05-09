namespace LMSDesk
{
    partial class ToastControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Error = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Error
            // 
            this.Error.Location = new System.Drawing.Point(246, 397);
            this.Error.Margin = new System.Windows.Forms.Padding(2);
            this.Error.Name = "Error";
            this.Error.Size = new System.Drawing.Size(56, 19);
            this.Error.TabIndex = 1;
            this.Error.Text = "Error";
            this.Error.UseVisualStyleBackColor = true;
            this.Error.Click += new System.EventHandler(this.Error_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(246, 349);
            this.button1.Margin = new System.Windows.Forms.Padding(2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(56, 19);
            this.button1.TabIndex = 2;
            this.button1.Text = "Success";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // ToastControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.Error);
            this.Controls.Add(this.button1);
            this.Name = "ToastControl";
            this.Size = new System.Drawing.Size(549, 765);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button Error;
        private System.Windows.Forms.Button button1;
    }
}
