using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace chuipala_ws.Models
{
    public class FullDelayDTO
    {
        public string StudentFullName { get; set; }
        public string Reason { get; set; }
        public int Value { get; set; }
        public string ValueUnit { get; set; }
        public int EventID { get; set; }
        public string ConcernedDate { get; set; }

        public ClassDTO ConcernedClass { get; set; }
    }
}