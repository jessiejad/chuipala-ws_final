using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace chuipala_ws.Models
{
    public class FullAbsenceDTO
    {
        public string PersonFullName { get; set; }
        public string Reason { get; set; }
        public string StartDate { get; set; }
        public string StopDate { get; set; }
        public string StartTime { get; set; }
        public string StopTime { get; set; }
        public int EventID { get; set; }

        public List<ClassDTO> ConcernedClasses { get; set; } = new List<ClassDTO>();
    }
}