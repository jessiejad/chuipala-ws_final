namespace chuipala_ws.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Event")]
    public abstract class Event
    {
        [Display(Name = "Référence")]
        public int EventID { get; set; }
        [Display(Name = "Raison")]
        public string Reason { get; set; }
        [Display(Name = "Valeur")]
        public int Value { get; set; }
        [Display(Name = "Unité")]
        public string ValueUnit { get; set; }

        [Display(Name = "Concerne")]
        public string UserID { get; set; }
        public virtual ApplicationUser User { get; set; }

        public abstract IEnumerable<Class> ConcernedClasses { get; }
    }
}