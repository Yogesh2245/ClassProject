using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LMSDesk
{
    public partial class InstitutePrintForm : Form
    {
        private readonly string baseUrl = "https://gbep.in/api/";
        private Bitmap certificateBitmap; // प्रिंटसाठी इमेज स्टोअर करण्यासाठी

        public InstitutePrintForm()
        {
            InitializeComponent();
            this.Load += async (s, e) => await LoadInstituteData();
        }

 
        private async Task LoadInstituteData()
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var response = await client.GetAsync(baseUrl + "get_institutes.php");
                    var result = await response.Content.ReadAsStringAsync();
                    dynamic res = JsonConvert.DeserializeObject(result);

                    if (res.status == true)
                    {
                        dataGridView1.DataSource = res.data.ToObject<DataTable>();

                        // 'Print' बटन कॉलम नसेल तर तयार करा
                        if (!dataGridView1.Columns.Contains("btnPrint"))
                        {
                            DataGridViewButtonColumn btnPrint = new DataGridViewButtonColumn();
                            btnPrint.Name = "btnPrint";
                            btnPrint.HeaderText = "Action";
                            btnPrint.Text = "Print Certificate";
                            btnPrint.UseColumnTextForButtonValue = true;

                            // १. आधी कॉलम ॲड करा
                            dataGridView1.Columns.Add(btnPrint);

                            // २. आता त्याचा DisplayIndex 0 वर सेट करा म्हणजे तो सुरुवातीला येईल
                            btnPrint.DisplayIndex = 0;
                        }

                        // --- हेडरची नावे बदलण्यासाठी खालील कोड वापरा ---
                        if (dataGridView1.Columns.Contains("id")) dataGridView1.Columns["id"].HeaderText = "ID";
                        if (dataGridView1.Columns.Contains("institute_name")) dataGridView1.Columns["institute_name"].HeaderText = "Institute Name";
                        if (dataGridView1.Columns.Contains("institute_code")) dataGridView1.Columns["institute_code"].HeaderText = "ATC Code";
                        if (dataGridView1.Columns.Contains("reg_no")) dataGridView1.Columns["reg_no"].HeaderText = "Registration No";
                        if (dataGridView1.Columns.Contains("full_address")) dataGridView1.Columns["full_address"].HeaderText = "Address";
                        if (dataGridView1.Columns.Contains("city_taluka")) dataGridView1.Columns["city_taluka"].HeaderText = "City/Taluka";
                        if (dataGridView1.Columns.Contains("district")) dataGridView1.Columns["district"].HeaderText = "District";
                        if (dataGridView1.Columns.Contains("director_name")) dataGridView1.Columns["director_name"].HeaderText = "Director Name";
                        if (dataGridView1.Columns.Contains("mobile_number")) dataGridView1.Columns["mobile_number"].HeaderText = "Mobile";
                        if (dataGridView1.Columns.Contains("email_id")) dataGridView1.Columns["email_id"].HeaderText = "Email ID";
                        if (dataGridView1.Columns.Contains("registration_date")) dataGridView1.Columns["registration_date"].HeaderText = "Reg. Date";

                        if (dataGridView1.Columns.Contains("establishment_year")) dataGridView1.Columns["establishment_year"].HeaderText = "Est. Year";
                        if (dataGridView1.Columns.Contains("facilities")) dataGridView1.Columns["facilities"].HeaderText = "Facilities";
                        if (dataGridView1.Columns.Contains("institute_type")) dataGridView1.Columns["institute_type"].HeaderText = "Inst. Type";
                        if (dataGridView1.Columns.Contains("valid_from")) dataGridView1.Columns["valid_from"].HeaderText = "Valid From";
                        if (dataGridView1.Columns.Contains("valid_to")) dataGridView1.Columns["valid_to"].HeaderText = "Valid To";
                        if (dataGridView1.Columns.Contains("print_status")) dataGridView1.Columns["print_status"].HeaderText = "Print Status";

                        // अनावश्यक कॉलम्स लपवण्यासाठी (उदा. created_at):
                        if (dataGridView1.Columns.Contains("created_at")) dataGridView1.Columns["created_at"].Visible = false;


                        // ३. ग्रीड डिझाइन अजून सुधारण्यासाठी (पर्यायी पण महत्त्वाचे)
                        dataGridView1.EnableHeadersVisualStyles = false;
                        dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.Navy; // हेडरचा बॅकग्राउंड रंग
                        dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White; // हेडरचा टेक्स्ट रंग
                        dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading data: " + ex.Message);
                }
            }
        }

        // ३. प्रिंट प्रिव्ह्यू दाखवणे
        private void ShowPrintPreview()
        {
            PrintDocument pd = new PrintDocument();
            pd.DefaultPageSettings.Landscape = true; // सर्टिफिकेट आडवे (Landscape) असते
            pd.PrintPage += (s, ev) =>
            {
                // इमेज कागदाच्या आकारानुसार फिट करा
                Rectangle m = ev.PageBounds;
                ev.Graphics.DrawImage(certificateBitmap, m);
            };

            PrintPreviewDialog ppd = new PrintPreviewDialog();
            ppd.Document = pd;
            ppd.WindowState = FormWindowState.Maximized;
            ppd.ShowDialog();
        }

        

        private void checkPrePrinted_CheckedChanged(object sender, EventArgs e)
        {
            if (checkPrePrinted.Checked)
            {
                // युजरला कल्पना द्या
                // MessageBox.Show("आता फक्त मजकूर प्रिंट होईल. कृपया प्रिंटरमध्ये छापील कागद टाका.");
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dataGridView1.Columns[e.ColumnIndex].Name == "btnPrint")
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                try
                {
                    // १. आधी चेक करा की ग्रीडमध्ये हा कॉलम खरोखर आहे का?
                    if (!dataGridView1.Columns.Contains("reg_no"))
                    {
                        MessageBox.Show("Error: 'reg_no' कॉलम ग्रीडमध्ये सापडला नाही. कृपया API तपासा.");
                        return;
                    }

                    int currentYear = DateTime.Now.Year;
                    int nextYear = currentYear + 1;
                    string dynamicGbepYear = $"{currentYear}-{(nextYear % 100):D2}";

                    InstituteCertificateEngine engine = new InstituteCertificateEngine();

                    // २. डेटा सुरक्षितपणे फेच करा
                    string instName = row.Cells["institute_name"].Value?.ToString() ?? "";
                    string regNo = row.Cells["reg_no"].Value?.ToString() ?? "N/A";
                    string address = row.Cells["full_address"].Value?.ToString() ?? "";
                    string city = row.Cells["city_taluka"].Value?.ToString() ?? "";
                    string dist = row.Cells["district"].Value?.ToString() ?? "";
                    string instCode = row.Cells["institute_code"].Value?.ToString() ?? "";

                    // ३. तारखा सुरक्षितपणे हँडल करा
                    string vFrom = DateTime.Now.ToString("dd/MM/yyyy");
                    string vTo = DateTime.Now.AddYears(1).ToString("dd/MM/yyyy");

                    if (row.Cells["registration_date"].Value != DBNull.Value && row.Cells["registration_date"].Value != null)
                    {
                        DateTime regDate = Convert.ToDateTime(row.Cells["registration_date"].Value);
                        vFrom = regDate.ToString("dd/MM/yyyy");
                        vTo = regDate.AddYears(1).ToString("dd/MM/yyyy");
                    }

                    // InstitutePrintForm.cs मध्ये
                    // १. आधी व्हेरिएबल्स तयार करा (जे तुम्ही आधीच केले आहेत)
                    string director = row.Cells["director_name"].Value?.ToString() ?? "";
                    string place = row.Cells["city_taluka"].Value?.ToString() ?? "";
                    string printDate = DateTime.Now.ToString("dd/MM/yyyy");

                    // २. 'includeBackground' साठी चेकबॉक्सची उलट व्हॅल्यू पाठवा
                    // जर 'Pre-printed' कागद वापरायचा असेल (Checked), तर बॅकग्राउंड नको (false)
                    // जर साधा कोरा कागद असेल (Unchecked), तर बॅकग्राउंड हवा (true)
                   // bool showBackground = !checkPrePrinted.Checked;
                    // जर वरच्या कोडने काम झाले नाही, तर हे वापरून पहा:
                   // bool showBackground = checkPrePrinted.Checked ? false : true;

                    bool showBackground = checkPrePrinted.Checked;

                    // ३. इंजिन कॉल करा
                    certificateBitmap = engine.Generate(
                        instName,
                        regNo,
                        address,
                        city,
                        dist,
                        vFrom,
                        vTo,
                        instCode,
                        dynamicGbepYear,
                        director,   // Director Name
                        printDate,  // Date
                        place,       // Place
                        showBackground
                    );

                    // ४. प्रिव्ह्यू दाखवा
                    if (certificateBitmap != null)
                    {
                        ShowPrintPreview();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("सर्टिफिकेट जनरेट करताना त्रुटी आली: " + ex.Message);
                }
            }
        }
    }
}
