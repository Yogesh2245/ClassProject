using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMSDesk
{
    public class CertificateInfo
    {
        public string Name { get; set; }
        public string Center { get; set; }
        public string SerialNumber { get; set; }
        public string Course { get; set; }
        public string Grade { get; set; }
        public string Year { get; set; }
        public DateTime ExamDate { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public string PhotoPath { get; set; }


        public string student_id { get; set; } 
        public string branch_name { get; set; }

        public string course_id { get; set; }
        public string course_code { get; set; }
        
         public string institute_name { get; set; }
    }
}
