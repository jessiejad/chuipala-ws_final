namespace chuipala_ws.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Subject")]
    public partial class Subject
    {
        public int SubjectID { get; set; }
        public virtual ICollection<Class> Classes { get; set; }

        [Display(Name = "Libellé")]
        public string Label { get; set; }
        [Display(Name = "Référence")]
        public string Reference { get; set; }
        
        public string FullName
        {
            get
            {
                return Reference + " - " + Label;
            }
        }

    }
}