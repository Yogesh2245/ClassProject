using QRCoder;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace LMSDesk
{
     public class CertificateEngine
    {

        private const string DefaultFontFamily = "Gill Sans MT";

        public Bitmap Generate(string center, string name, string course, string examDate,
                               string grade, string durationFrom, string durationTo,
                               string serialNo, string gbepYear, string photoPath,
                               bool includeBackground)
        {
            int targetWidth = 3508;
            int targetHeight = 2480;

            string templatePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Template.png");
            Bitmap bitmap = new Bitmap(targetWidth, targetHeight);

            using (Graphics g = Graphics.FromImage(bitmap))
            {
                g.Clear(Color.White);

                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

                // ===== TEMPLATE =====
                if (includeBackground && System.IO.File.Exists(templatePath))
                {
                    using (Image rawTemplate = Image.FromFile(templatePath))
                    {
                        g.DrawImage(rawTemplate, 0, 0, targetWidth, targetHeight);
                    }
                }

                // ================= ALIGNMENT CONTROLS =================
                float globalYOffset = -60;     // Full text move
                float nameBlockOffset = 45;   // Name block down
                float metaOffset = 40;        // Header meta down
                float photoOffsetX = -230;     // Photo left/right
                float photoOffsetY = 15;      // Photo up/down
                // =====================================================
                 

                using (Brush navyBrush = new SolidBrush(Color.FromArgb(29, 83, 137)))
                using (Brush redBrush = new SolidBrush(Color.FromArgb(255, 42, 42)))
                using (Brush labelBrush = new SolidBrush(Color.FromArgb(107, 83, 85))) 
                using (Brush darkRedBrush = new SolidBrush(Color.FromArgb(255, 0, 0))) 


                //using (Font centerLabelFont = new Font(DefaultFontFamily, 44, FontStyle.Bold))
                using (Font centerLabelFont = new Font(DefaultFontFamily, 42, FontStyle.Bold))
                // using (Font scriptLabelFont = new Font(DefaultFontFamily, 62, FontStyle.Regular))
                using (Font scriptLabelFont = new Font(DefaultFontFamily, 42, FontStyle.Regular))
               // using (Font nameFont = new Font(DefaultFontFamily, 105, FontStyle.Italic))
                using (Font nameFont = new Font(DefaultFontFamily, 78, FontStyle.Regular))
                using (Font boldDataFont = new Font(DefaultFontFamily, 34, FontStyle.Bold))
                using (Font metaFont = new Font(DefaultFontFamily, 28, FontStyle.Regular))
 

                {
                    // ===== LINE 1: Name of Center =====
                    string l1 = $"Name of Center: {center}";
                    g.DrawString(
                        l1,
                        centerLabelFont,
                        darkRedBrush,
                        (targetWidth - g.MeasureString(l1, centerLabelFont).Width) / 2,
                        1230 + globalYOffset + nameBlockOffset
                    );

                    // ===== LINE 2: This is to certify that... =====
                    string l2_s = "This is to certify that ";
                    string l2_e = " has successfully completed the";

                    float wL2a = g.MeasureString(l2_s, scriptLabelFont).Width;
                    float maxNameWidth = 1200;
                    Font fittedNameFont = FitFont(g, name, nameFont, maxNameWidth);
                    float wName = g.MeasureString(name, fittedNameFont).Width;
                    float wL2b = g.MeasureString(l2_e, scriptLabelFont).Width;

                    float startX2 = (targetWidth - (wL2a + wName + wL2b)) / 2;
                    float minMargin = 200;
                    if (startX2 < minMargin) startX2 = minMargin;

                    float line2BaseY = 1350 + globalYOffset + nameBlockOffset;

                    // script text
                    g.DrawString(l2_s, scriptLabelFont, labelBrush, startX2, line2BaseY);

                    // name alignment
                    float nameAscentFix = g.MeasureString("Ag", fittedNameFont).Height - g.MeasureString("Ag", scriptLabelFont).Height;

                    g.DrawString(name, fittedNameFont, navyBrush,startX2 + wL2a,line2BaseY - nameAscentFix / 2);

                    // ending script
                    g.DrawString(l2_e, scriptLabelFont, labelBrush,startX2 + wL2a + wName,line2BaseY);

                    // ===== LINE 3: certification course in... =====
                    // गॅप कमी करण्यासाठी 1460 ऐवजी 1420 केले आहे
                    float line3Y = 1420 + globalYOffset + nameBlockOffset;

                    string l3_a = "certification course in ";
                    string l3_b = "& passed the exam held in the month of";

                    float w3a = g.MeasureString(l3_a, scriptLabelFont).Width;
                    float wCourse = g.MeasureString(course, boldDataFont).Width;
                    float w3b = g.MeasureString(l3_b, scriptLabelFont).Width;
                    float wDate = g.MeasureString(examDate, boldDataFont).Width;

                    float startX3 = (targetWidth - (w3a + wCourse + w3b + wDate)) / 2;

                    g.DrawString(l3_a, scriptLabelFont, labelBrush, startX3, line3Y);

                    g.DrawString(course, boldDataFont, redBrush,startX3 + w3a,line3Y + 8); // ठळक मजकूर अलाइन करण्यासाठी +8

                    g.DrawString(l3_b, scriptLabelFont, labelBrush,startX3 + w3a + wCourse,line3Y);

                    g.DrawString(examDate, boldDataFont, redBrush,startX3 + w3a + wCourse + w3b,line3Y + 8);

                    // ===== LINE 4: & was placed in the... =====
                    // गॅप कमी करण्यासाठी 1550 ऐवजी 1495 केले आहे
                    float line4Y = 1495 + globalYOffset + nameBlockOffset;

                    string l4_a = "& was placed in the ";
                    string l4_b = " Course Duration from ";
                    string dates = $"{durationFrom} To {durationTo}";
                    string gradeText = grade + " Grade";

                    float w4a = g.MeasureString(l4_a, scriptLabelFont).Width;
                    float wGrade = g.MeasureString(gradeText, boldDataFont).Width;
                    float w4b = g.MeasureString(l4_b, scriptLabelFont).Width;
                    float wDates = g.MeasureString(dates, boldDataFont).Width;

                    float startX4 = (targetWidth - (w4a + wGrade + w4b + wDates)) / 2;

                    g.DrawString(l4_a, scriptLabelFont, labelBrush, startX4, line4Y);

                    g.DrawString(gradeText, boldDataFont, redBrush,startX4 + w4a,line4Y + 8);

                    g.DrawString(l4_b, scriptLabelFont, labelBrush,startX4 + w4a + wGrade,line4Y);

                    g.DrawString(dates, boldDataFont, redBrush,startX4 + w4a + wGrade + w4b,line4Y + 8);

                    // ===== METADATA =====
                    g.DrawString($"GBEP: {gbepYear}", metaFont, Brushes.DimGray,305,255 + globalYOffset + metaOffset);
                    g.DrawString($"Sr. No.: {serialNo}", metaFont, Brushes.DimGray,2650,255 + globalYOffset + metaOffset);
                }

                // ===== PHOTO =====
                Image photoImg = null;

                try
                {
                    if (!string.IsNullOrEmpty(photoPath))
                    {
                        if (photoPath.StartsWith("http"))
                        {
                            using (var wc = new System.Net.WebClient())
                            {
                                byte[] bytes = wc.DownloadData(photoPath);
                                using (var ms = new MemoryStream(bytes))
                                {
                                    photoImg = Image.FromStream(ms);
                                }
                            }
                        }
                        else if (System.IO.File.Exists(photoPath))
                        {
                            photoImg = Image.FromFile(photoPath);
                        }
                    }
                }
                catch { photoImg = null; }

                if (photoImg != null)
                {
                    using (photoImg)
                    {
                       // g.DrawImage(photoImg,2900 + photoOffsetX,345 + photoOffsetY,400,450);
                        g.DrawImage(photoImg, 2980 + photoOffsetX, 345 + photoOffsetY, 320, 360);
                    }
                }

                // ===== QR =====
                string code = "http://gbep.in/verifydoc.php?type=cer&id=" + serialNo;

                using (QRCodeGenerator qrGen = new QRCodeGenerator())
                using (QRCodeData qrData = qrGen.CreateQrCode(code, QRCodeGenerator.ECCLevel.Q))
                using (QRCode qrCode = new QRCode(qrData))
                using (Bitmap qrImg = qrCode.GetGraphic(15, Color.Black, Color.Transparent, false))
                {
                    g.DrawImage(qrImg, 316, 1750, 180, 180);
                }
            }

            return bitmap;
        } 

        private Font FitFont(Graphics g, string text, Font baseFont, float maxWidth)
        {
            float size = baseFont.Size;

            // safety loop
            for (int i = 0; i < 20; i++)
            {
                using (Font testFont = new Font(baseFont.FontFamily, size, baseFont.Style))
                {
                    if (g.MeasureString(text, testFont).Width <= maxWidth)
                        return new Font(baseFont.FontFamily, size, baseFont.Style);
                }
                size -= 3; // थोडं जास्त step
                if (size < 55) break; // minimum readable size for certificate name
            }

            return new Font(baseFont.FontFamily, 55, baseFont.Style); // final fallback
        }
    }
}