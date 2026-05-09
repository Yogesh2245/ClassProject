using Newtonsoft.Json;
using System;
using System.Drawing;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LMSDesk
{
    public partial class GenCertificationList : Form
    {
        private readonly CertificateProcessor _processor = new CertificateProcessor();
        private string _studentPhotoPath = "";
        private Bitmap _currentCert = null;
        public GenCertificationList()
        {
           InitializeComponent();
        }

        private void btnGenerateCertificate_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                // Create the object from your existing class
                var info = new CertificateInfo
                {
                    Name = txtStNamee.Text,
                    Center = txtInstituteNamee.Text,
                    SerialNumber = txtSerial.Text,
                    Course = txtCSname.Text,
                    Grade = txtGrade.Text,
                    Year = dtpYear.Text,
                    ExamDate = dtpExamDate.Value,
                    DateFrom = dtpdateFrom.Value,
                    DateTo = dtpDateTo.Value,


                };

                // REUSABLE: You can now call this from any variable!
                if (_currentCert != null) _currentCert.Dispose();
               // _currentCert = _processor.CreateCertificate(info);
                _currentCert = _processor.CreateCertificate(info, false);


                // Display Preview
                if (pbPreview.Image != null) pbPreview.Image.Dispose();
                pbPreview.Image = (Bitmap)_currentCert.Clone();

                //  btnSave.Enabled = true;
                MessageBox.Show("Certificate Generated Successfully!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void dgvLiveCourses_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void txtInstituteName_TextChanged(object sender, EventArgs e)
        {

        }

       
    }

}
