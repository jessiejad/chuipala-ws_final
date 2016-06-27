namespace chuipala_ws.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Delay")]
    public partial class Delay : Event
    {
        [Display(Name = "Cours")]
        public int ClassID { get; set; }
        public virtual Class Class { get; set; }
        
        override public IEnumerable<Class> ConcernedClasses {
            get
            {
                var l = new List<Class>();
                l.Add(Class);
                return l;
            }
        }

        public string ConcernedDate()
        {
            if(Class != null)
            {
                return Class.StartDateTime.ToString("dd/MM/yyy");
            }
            return "";
        }
    }
}