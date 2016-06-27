namespace chuipala_ws.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Homework")]
    public class Homework
    {
        [Display(Name = "Référence")]
        public int HomeworkID { get; set; }
        [Display(Name = "Libellé")]
        public string Label { get; set; }
        [Display(Name = "A rendre le")]
        public DateTime Delivery { get; set; }
        [Display(Name = "En ligne depuis")]
        public DateTime Creation { get; set; }

        [Display(Name = "Professeur")]
        public string ProfessorID { get; set; }
        public virtual Professor Professor { get; set; }

        public virtual ICollection<GroupHomeworks> GroupHomeworks { get; set; }
        public virtual ICollection<Tip> Tips { get; set; }

    }
}