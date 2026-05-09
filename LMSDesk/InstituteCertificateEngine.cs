using QRCoder;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMSDesk
{
    public class InstituteCertificateEngine
    {
        private const string FontFamilyName = "Times New Roman";

        public Bitmap Generate(string instName, string regNo, string address,
                                string city, string district, string validFrom,
                                string validTo, string atcNo, string gbepYear,
                                string directorName, string date, string place,
                                bool includeBackground)
        {
            int width = 3508;
            int height = 2480;
            Bitmap bitmap = new Bitmap(width, height);
            string templatePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "InstTemplate.png");

            using (Graphics g = Graphics.FromImage(bitmap))
            {
                if (includeBackground)
                {
                    g.Clear(Color.White);
                    if (File.Exists(templatePath))
                    {
                        using (Image img = Image.FromFile(templatePath))
                        { g.DrawImage(img, 0, 0, width, height); }
                    }
                }
                else
                {
                    g.Clear(Color.Transparent);
                }

                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;

                using (Brush darkRed = new SolidBrush(Color.FromArgb(165, 42, 42)))
                using (Brush navyBlue = new SolidBrush(Color.FromArgb(29, 83, 137)))
                using (Font smallMetaFont = new Font(FontFamilyName, 28, FontStyle.Bold))
                using (Font titleFont = new Font(FontFamilyName, 45, FontStyle.Bold | FontStyle.Italic))
                using (Font certifyFont = new Font(FontFamilyName, 40, FontStyle.Regular))
                using (Font instNameFont = new Font(FontFamilyName, 70, FontStyle.Bold))
                using (Font addressFont = new Font(FontFamilyName, 38, FontStyle.Regular))
                using (Font bodyFont = new Font(FontFamilyName, 40, FontStyle.Regular))
                using (Font bodyFontBold = new Font(FontFamilyName, 40, FontStyle.Bold)) // बोल्ड करण्यासाठी नवीन फॉन्ट
                using (Font footerFont = new Font(FontFamilyName, 30, FontStyle.Bold))
                {
                    // १. टॉप मेटॉडेटा
                    g.DrawString($"GBEP: {gbepYear}", smallMetaFont, Brushes.Black, 300, 260);
                    g.DrawString($"Reg. No.: {regNo}", smallMetaFont, Brushes.Black, 2550, 260);

                    // २. Franchisee title
                    float y = 1210;
                    string t1 = "Franchisee for Training Center";
                    g.DrawString(t1, titleFont, navyBlue, (width - g.MeasureString(t1, titleFont).Width) / 2, y);

                    // ३. "This is certify that" आणि "Institute Name"
                    y += 105;
                    string prefix = "This is certify that ";
                    float prefixWidth = g.MeasureString(prefix, certifyFont).Width;
                    float nameWidth = g.MeasureString(instName, instNameFont).Width;
                    float totalWidth = prefixWidth + nameWidth;
                    float startX = (width - totalWidth) / 2;

                    g.DrawString(prefix, certifyFont, darkRed, startX, y + 25);
                    g.DrawString(instName, instNameFont, darkRed, startX + prefixWidth, y);

                    // ४. पत्ता
                    y += 95;
                    string fullAddr = $"{address}, {city}. Dist. {district}.";
                    g.DrawString(fullAddr, addressFont, darkRed, (width - g.MeasureString(fullAddr, addressFont).Width) / 2, y);

                    // ५. पॅराग्राफ मजकूर (फक्त बोर्डाचे नाव बोल्ड करणे)
                    y += 95;
                    string part1 = "is approved computer Training Center of ";
                    string part2 = "Global Business Education Promotion Board and Research Center";
                    //for double quote
                    //string part2 = "\"Global Business Education Promotion Board and Research Center\"";


                    float w1 = g.MeasureString(part1, bodyFont).Width;
                    float w2 = g.MeasureString(part2, bodyFontBold).Width;
                    float t3StartX = (width - (w1 + w2)) / 2;

                    // पहिला भाग (Regular)
                    g.DrawString(part1, bodyFont, Brushes.Black, t3StartX, y);
                    // दुसरा भाग (Bold)
                    g.DrawString(part2, bodyFontBold, Brushes.Black, t3StartX + w1, y);

                    y += 50;
                    string t4 = "to provide guidelines of various programme under department of GBEP Education";
                    g.DrawString(t4, bodyFont, Brushes.Black, (width - g.MeasureString(t4, bodyFont).Width) / 2, y);

                    // ६. सेंटर व्हॅलिडिटी
                    y += 480;
                    string validity = $"Center Valid From {validFrom} To {validTo} ATC Reg. No. {atcNo}";
                    g.DrawString(validity, footerFont, Brushes.Black, (width - g.MeasureString(validity, footerFont).Width) / 2, y);

                    // ७. QR CODE
                    string qrData = "https://gbep.in/verifydoc.php?type=inst&id=" + atcNo;
                    using (QRCodeGenerator qrGen = new QRCodeGenerator())
                    using (QRCodeData data = qrGen.CreateQrCode(qrData, QRCodeGenerator.ECCLevel.Q))
                    using (QRCode qrCode = new QRCode(data))
                    using (Bitmap qrImg = qrCode.GetGraphic(20, Color.Black, Color.Transparent, false))
                    {
                        g.DrawImage(qrImg, 280, 1960, 160, 160);
                    }
                }
            }
            return bitmap;
        }
    }
}