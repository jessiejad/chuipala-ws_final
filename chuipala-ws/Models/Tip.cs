namespace chuipala_ws.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Tip")]
    public partial class Tip
    {
        [Display(Name = "Référence")]
        public int TipID { get; set; }
        [Display(Name = "Libellé")]
        public string Label { get; set; }

        [Display(Name = "Devoir")]
        public int HomeworkID { get; set; }
        public virtual Homework Homework { get; set; }
    }
}