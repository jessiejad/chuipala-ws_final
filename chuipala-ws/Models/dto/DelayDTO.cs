using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace chuipala_ws.Models
{
    public class DelayDTO
    {
        public string PersonFullName { get; set; }
        public string Reason { get; set; }
        public int Value { get; set; }
        public string ValueUnit { get; set; }
        public int EventID { get; set; }
        public string ConcernedDate { get; set; }
    }
}