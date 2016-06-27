namespace chuipala_ws.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AbsenceClasses")]
    public partial class AbsenceClasses
    {
        [Key, Column(Order = 0)]
        public int EventID { get; set; }
        public virtual Absence Absence { get; set; }

        [Key, Column(Order = 1)]
        public int ClassID { get; set; }
        public virtual Class Class { get; set; }

    }
}