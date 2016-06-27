using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace chuipala_ws.Models
{
    public class ClassEventsDTO
    {
        public List<AbsenceDTO> Absences { get; set; }
        public List<DelayDTO> Delays { get; set; }
        public int NbAbsences;
        public int NbDelays;

        public ClassEventsDTO ()
        {
            Absences = new List<AbsenceDTO>();
            Delays = new List<DelayDTO>();
        }

        public void CalculateNbs()
        {
            NbAbsences = Absences.Count;
            NbDelays = Delays.Count;
        }
    }
}