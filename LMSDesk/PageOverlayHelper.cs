using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;

public static class PageOverlayHelper
{
    // ================= ADD TOP CORNER CODES =================
    public static Bitmap AddTopCornerCodes(
      Bitmap originalPage,
      string leftCode,
      string rightCode)
    {
        Bitmap bmp = new Bitmap(originalPage);

        using (Graphics g = Graphics.FromImage(bmp))
        {
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;

            using (Font font = new Font("Calibri", 10, FontStyle.Regular))
            using (Brush brush = new SolidBrush(Color.FromArgb(120, 30, 20)))
            {
                float margin = 50;

                // 🔧 MOVE MORE UP HERE
                float topY = 91f;   // ← Change this value anytime

                // ===== LEFT =====
                if (!string.IsNullOrWhiteSpace(leftCode))
                {
                    g.DrawString(
                        "GBEP: " + leftCode,
                        font,
                        brush,
                        new PointF(95f, topY)
                    );
                }
                // GBEP/Sr No
                // ===== RIGHT =====
                if (!string.IsNullOrWhiteSpace(rightCode))
                {
                    string rightText = "Sr No: " + rightCode;

                    SizeF size = g.MeasureString(rightText, font);

                    g.DrawString(
                        rightText,
                        font,
                        brush,
                        new PointF(
                            bmp.Width - size.Width - margin,
                            topY
                        )
                    );
                }
            }
        }

        return bmp;
    }

}
