namespace chuipala_ws.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Absence")]
    public partial class Absence : Event
    {
        [Display(Name = "Commence le")]
        public DateTime StartDateTime { get; set; }
        [Display(Name = "Termine le")]
        public DateTime StopDateTime { get; set; }
        
        public virtual ICollection<AbsenceClasses> AbsenceClasses { get; set; }

        override public IEnumerable<Class> ConcernedClasses {
            get
            {
                var l = new List<Class>();
                foreach (AbsenceClasses ac in AbsenceClasses)
                {
                    l.Add(ac.Class);
                }
                
                return l;
            }
        }
        
    }
}