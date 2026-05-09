using System.Collections.Generic;

namespace LMSDesk
{
    // MOVE THIS OUTSIDE THE OTHER CLASS
    public class SubjectItem
    {
        public string SubjectName { get; set; }
        public string MaxMarks { get; set; }
        public string MarksObtained { get; set; }
        public string Remarks { get; set; }
    }

    public class MarkSheetData
    {
        public string CandidateName { get; set; }

        public string student_id { get; set; }
        public string GuardianName { get; set; }
        public string DateOfBirth { get; set; }
        public string Duration { get; set; }
        public string Examination { get; set; }
        public string ProfileSrNo { get; set; }
        public string CourseName { get; set; }
        public string InstituteName { get; set; }
        public string Branch_name { get; set; }
        public string SessionYear { get; set; }
        public string Grade { get; set; }
        public System.Drawing.Image Photo { get; set; }
        public System.Drawing.Image InsPhoto { get; set; }

        public int? course_id { get; set; }

        public bool PrintPhoto { get; set; }



        public List<SubjectItem> Subjects { get; set; } = new List<SubjectItem>();
    }
}