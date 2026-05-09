using QRCoder;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.IO;
using System.Reflection.Emit;
using System.Windows.Forms;

public static class MarkListEngine
{
    // --- EASY SETTINGS: Change fonts and sizes here ---
    // --- REFINED SETTINGS ---

    private static readonly string DefaultFontFamily = "Gill Sans MT";
    private static readonly string MainFontFamily = "Gill Sans MT";
    private static readonly string HeaderFontFamily = "Gill Sans MT";
    private static readonly string HeaderFamily = "Gill Sans MT";
    private static readonly string HeaderTable = "Gill Sans MT";

    private static readonly Font HeaderFont = new Font(MainFontFamily, 11, FontStyle.Bold);
    private static readonly Font CandidateNameFont = new Font(MainFontFamily, 19, FontStyle.Bold);

    private static readonly Font LabelFont = new Font(MainFontFamily, 14, FontStyle.Regular); // ← Bold काढला
    private static readonly Font Labeltop = new Font(MainFontFamily, 12, FontStyle.Regular);

    //private static readonly Font TableHeaderFont = new Font(MainFontFamily, 13, FontStyle.Bold);
    private static readonly Font TableHeaderFont = new Font(MainFontFamily, 13, FontStyle.Regular);
    private static readonly Font TableCellFont = new Font(MainFontFamily, 13, FontStyle.Regular);




    // Color palette sampled from your image
    private static readonly Color ColorMaroon = Color.FromArgb(120, 30, 20); // GBEP/Sr No
    private static readonly Color ColorNavy = Color.FromArgb(30, 45, 90);   // Candidate Name
    private static readonly Color ColorLabel = Color.FromArgb(40, 40, 40);  // Details text
                                                                            // --------------------------------------------------

    public static Bitmap Render(LMSDesk.MarkSheetData d)
    {
        string templatePath = Path.Combine(Application.StartupPath, "template1.jpg");
        Bitmap bmp = new Bitmap(templatePath);

        // ===== BACKGROUND PRINT CONTROL =====
        bool PRINT_BACKGROUND = false;

        using (Graphics gbg = Graphics.FromImage(bmp))
        {
            if (!PRINT_BACKGROUND)
                gbg.FillRectangle(Brushes.White, 0, 0, bmp.Width, bmp.Height);
        }

        using (Graphics g = Graphics.FromImage(bmp))
        {
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;

            // 🔧 MASTER OFFSET (MOVE ENTIRE SECTION)
            int MOVE_UP = 40;   // ← Adjust here only

            using (Brush textBrush = new SolidBrush(Color.Black))
            using (Brush textBrushB = new SolidBrush(ColorNavy))
            using (Brush textBrushO = new SolidBrush(ColorMaroon))
            using (Brush textL = new SolidBrush(ColorLabel))
            using (Pen tablePen = new Pen(Color.Black, 1.5f))
            using (Pen tablePen1 = new Pen(Color.Yellow))
            {
                float margin = 40;
 

                int HEADER_TOP = -10;   // 🔧 adjust here
                int NAME_TOP = 20;       // Candidate name
                int BODY_TOP = -60;       // Details + table + photo

 
                float headerY = 0;
 

                // ===== HEADER TOP LOCK =====

               // float headerY = 2;   // absolute top safe

                // LEFT
                g.DrawString("" + "",Labeltop,textBrushO,new PointF(margin, headerY));

                // RIGHT
                SizeF srSize = g.MeasureString("" + "",Labeltop);

                g.DrawString("" + "",Labeltop,textBrushO,new PointF(bmp.Width - srSize.Width - margin, headerY)
                );
                 
                if (d.InsPhoto != null)
                    g.DrawImage(d.InsPhoto, 700, 119 + MOVE_UP, 95, 100);

                // ================= NAME =================

                int BLOCK_SHIFT_UP = 0;   // 🔧 adjust whole lower area
                 
                g.DrawString(
                    d.CandidateName.ToUpper(),
                    CandidateNameFont,
                    textBrushB,
                    new RectangleF(0, 485 + MOVE_UP + NAME_TOP, bmp.Width, 50),
                    new StringFormat { Alignment = StringAlignment.Center }
                );
                 
                //float col1X = 120, col2X = 550, colonOffset = 115;

               

                float col1X = 110;
                float col2X = 500;     // ← थोडं left ला आणलं
                float colonOffset = 120; // ← label पासून gap वाढवला



                int DETAILS_SHIFT = BLOCK_SHIFT_UP;

            

                DrawDetailLine(g, "Mother Name", d.GuardianName,col1X, 544 + MOVE_UP + DETAILS_SHIFT,colonOffset, LabelFont, textL);
                DrawDetailLine(g, "Date of Birth", d.DateOfBirth,col2X, 544 + MOVE_UP + DETAILS_SHIFT,colonOffset, LabelFont, textL);
                DrawDetailLine(g, "Duration", d.Duration,col1X, 585 + MOVE_UP + DETAILS_SHIFT,colonOffset, LabelFont, textL);
                DrawDetailLine(g, "Examination", d.Examination,col1X, 620 + MOVE_UP + DETAILS_SHIFT,colonOffset, LabelFont, textL);
                DrawDetailLine(g, "Roll No.", d.Branch_name + "-" + d.student_id,col2X, 620 + MOVE_UP + DETAILS_SHIFT,colonOffset, LabelFont, textL);
                DrawDetailLine(g, "Course", d.CourseName + "(#" + d.course_id + ")",col1X, 655 + MOVE_UP + DETAILS_SHIFT,colonOffset, LabelFont, textL);
                DrawDetailLine(g, "Branch", d.InstituteName + "(" + d.Branch_name + ")",col1X, 690 + MOVE_UP + DETAILS_SHIFT,colonOffset, LabelFont, textL);



                // ================= TABLE =================
                int tX = 130;
              //  int tY = 730 + MOVE_UP;
                int tY = 730 + MOVE_UP + BLOCK_SHIFT_UP;
                 
                int tW = 630;

                int colSubjectW = 300;
                int colMaxW = 110;
                int colObtW = 110;
                int colRemW = tW - (colSubjectW + colMaxW + colObtW);

                int headerHeight = 60;
                int rowHeight = 45;
                int totalRowHeight = 50;

                int subjectCount = d.Subjects?.Count ?? 0;
                int calculatedTableHeight = headerHeight + (subjectCount * rowHeight) + totalRowHeight;

                StringFormat centerBoth = new StringFormat
                {
                    Alignment = StringAlignment.Center,
                    LineAlignment = StringAlignment.Center
                };

                // Border
                g.DrawRectangle(tablePen, tX, tY, tW, calculatedTableHeight);
                g.DrawLine(tablePen, tX, tY + headerHeight, tX + tW, tY + headerHeight);

                // Headers
                g.DrawString("SUBJECT", TableHeaderFont, textBrush,new RectangleF(tX, tY, colSubjectW, headerHeight), centerBoth);

                g.DrawString("MAXIMUM\nMARKS", TableHeaderFont, textBrush, new RectangleF(tX + colSubjectW, tY, colMaxW, headerHeight), centerBoth);

                g.DrawString("MARKS\nOBTAINED", TableHeaderFont, textBrush,new RectangleF(tX + colSubjectW + colMaxW, tY, colObtW, headerHeight), centerBoth);

                g.DrawString("REMARK", TableHeaderFont, textBrush,new RectangleF(tX + colSubjectW + colMaxW + colObtW, tY, colRemW, headerHeight), centerBoth);

                int currentTotalMax = 0;
                int currentTotalObt = 0;

                for (int i = 0; i < subjectCount; i++)
                {
                    var sub = d.Subjects[i];
                    int rowY = tY + headerHeight + (i * rowHeight);

                    g.DrawLine(tablePen, tX, rowY + rowHeight, tX + tW, rowY + rowHeight);

                    g.DrawString(sub.SubjectName, LabelFont, textBrush,new RectangleF(tX + 10, rowY, colSubjectW - 10, rowHeight), centerBoth);

                    g.DrawString(sub.MaxMarks, LabelFont, textBrush,new RectangleF(tX + colSubjectW, rowY, colMaxW, rowHeight), centerBoth);

                    g.DrawString(sub.MarksObtained, LabelFont, textBrush,new RectangleF(tX + colSubjectW + colMaxW, rowY, colObtW, rowHeight), centerBoth);

                    g.DrawString(sub.Remarks, LabelFont, textBrush,new RectangleF(tX + colSubjectW + colMaxW + colObtW, rowY, colRemW, rowHeight), centerBoth);

                    int.TryParse(sub.MaxMarks, out int m);
                    int.TryParse(sub.MarksObtained, out int o);

                    currentTotalMax += m;
                    currentTotalObt += o;
                }

                int finalRowY = tY + headerHeight + (subjectCount * rowHeight);

                g.DrawString("Grand Total", TableHeaderFont, textBrush,new RectangleF(tX, finalRowY, colSubjectW, totalRowHeight), centerBoth);
                g.DrawString(currentTotalMax.ToString(), LabelFont, textBrush,new RectangleF(tX + colSubjectW, finalRowY, colMaxW, totalRowHeight), centerBoth);
                g.DrawString(currentTotalObt.ToString(), LabelFont, textBrush,new RectangleF(tX + colSubjectW + colMaxW, finalRowY, colObtW, totalRowHeight), centerBoth);
                g.DrawString(d.Grade, LabelFont, textBrush,new RectangleF(tX + colSubjectW + colMaxW + colObtW, finalRowY, colRemW, totalRowHeight), centerBoth);

                // Vertical lines
                g.DrawLine(tablePen, tX + colSubjectW, tY, tX + colSubjectW, tY + calculatedTableHeight);
                g.DrawLine(tablePen, tX + colSubjectW + colMaxW, tY, tX + colSubjectW + colMaxW, tY + calculatedTableHeight);
                g.DrawLine(tablePen, tX + colSubjectW + colMaxW + colObtW, tY, tX + colSubjectW + colMaxW + colObtW, tY + calculatedTableHeight);

                // ================= PHOTO =================
                //if (d.Photo != null)
                //    g.DrawImage(d.Photo, 138, 965 + MOVE_UP, 105, 125);

                /*if (d.PrintPhoto && d.Photo != null)
                {
                    g.DrawImage(d.Photo, 145, 870 + MOVE_UP, 105, 125);
                }*/

                if (d.PrintPhoto && d.Photo != null)
                {
                    // X = 650 (उजव्या बाजूला), Y = 870 + MOVE_UP
                    g.DrawImage(d.Photo, 650, 940+ MOVE_UP, 105, 125);
                }

                // ================= RECTANGLE =================
                int rectX = 108;
                int rectY = 1005 + MOVE_UP;
                int rectW = 200;
                int rectH = 35;

                //SignatureBox
                // g.DrawRectangle(tablePen1, rectX, rectY, rectW, rectH);

                // ================= QR =================
                /*  Bitmap qr = GenerateQrCode(d.ProfileSrNo);

                  if (qr != null)
                  {
                      int qrX = 120;
                      int qrY = 870 + MOVE_UP;
                     // int qrSize = 130;
                      int qrSize = 110;

                      g.DrawImage(qr, qrX, qrY, qrSize, qrSize);
                  }*/

                Bitmap qr = GenerateQrCode(d.ProfileSrNo);

                if (qr != null)
                {
                    // QR कोड डाव्या बाजूला (Left side) 
                    int qrX = 130;
                    int qrY = 940 + MOVE_UP;
                    int qrSize = 110;

                    g.DrawImage(qr, qrX, qrY, qrSize, qrSize);
                }
            }
        }

        return bmp;
    }

    // ================= QR CODE GENERATOR =================
    private static Bitmap GenerateQrCode(string qrText)
    {

        string code = "http://gbep.in/verifydoc.php?type=mar&id=" + qrText;

        if (string.IsNullOrWhiteSpace(code))
            return null;

        using (QRCodeGenerator qrGenerator = new QRCodeGenerator())
        {
            QRCodeData qrData = qrGenerator.CreateQrCode(
                code,
                QRCodeGenerator.ECCLevel.Q // High quality
            );

            using (QRCode qrCode = new QRCode(qrData))
            {
                Bitmap qrImage = qrCode.GetGraphic(
                    pixelsPerModule: 18,     // Size density
                    darkColor: Color.Black,
                    lightColor: Color.White,
                    drawQuietZones: true
                );

                return qrImage;
            }
        }
    }

    // Helper method to draw "Label : Value" with consistent spacing
    private static void DrawDetailLine(Graphics g, string label, string value, float x, float y, float colonOffset, Font font, Brush brush)
    {
        // Draws the label (e.g., Parent Name)
        g.DrawString(label, font, brush, x, y);
        g.DrawString(": " + value, font, brush, x + colonOffset, y);
    }
}