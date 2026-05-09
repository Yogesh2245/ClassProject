using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMSDesk
{
    public class CertificateProcessor
    {
        private readonly CertificateEngine _engine = new CertificateEngine();

        public Bitmap CreateCertificate(CertificateInfo info, bool includeBackground)
        {

            // 1. Format Year (e.g., 2025 -> 2025-26)
            string formattedGbep = info.Year;
            if (!string.IsNullOrEmpty(info.Year) && info.Year.Length == 4 && int.TryParse(info.Year, out int year))
            {
                formattedGbep = $"{year}-{(year + 1) % 100:D2}";
            }

            // 2. Format Dates
            string durFrom = info.DateFrom.ToString("MMM yyyy");
            string durTo = info.DateTo.ToString("MMM yyyy");
            string examDate = info.ExamDate.ToString("MMM yyyy");

            // 3. Generate via Engine
             // here On Certificate Name OF Center : After Thant Center Name is also Printed e.g. Miraj:GBEP1001 ; 
            //return _engine.Generate(
            //       info.institute_name+ " "+ info.Center, info.Name, info.Course, examDate,
            //       info.Grade, durFrom, durTo, info.SerialNumber,
            //       formattedGbep, info.PhotoPath,
            //       includeBackground // <--- ही नवीन व्हॅल्यू
            //   );


            // 3. Generate via Engine
            return _engine.Generate(
                   info.institute_name, info.Name, info.Course, examDate,
                   info.Grade, durFrom, durTo, info.SerialNumber,
                   formattedGbep, info.PhotoPath,
                   includeBackground // <--- ही नवीन व्हॅल्यू
               );
        }
    }
}
