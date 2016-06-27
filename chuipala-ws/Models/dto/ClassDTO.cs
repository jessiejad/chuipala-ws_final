using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace chuipala_ws.Models
{
    public class ClassDTO
    {
        public int ClassID { get; set; }

        public DateTime FullStartDate { get; set; }
        public DateTime PreSetDelayDate { get; set; }
        public DateTime FullEndDate { get; set; }

        public string StartDate { get; set; }
        public string EndDate { get; set; }

        public string StartTime { get; set; }
        public string EndTime { get; set; }

        public string SubjectLabel { get; set; }
        public string ProfessorFullName { get; set; }
        public int NbDelays { get; set; }
        public int NbAbsences { get; set; }
        public bool IsProfessorAbsent { get; set; }
        public bool IsProfessorLate { get; set; }

        public bool IsUserAbsent { get; set; }
        public bool IsUserLate { get; set; }

        public IEnumerable<string> Groups { get; set; }
    }
}