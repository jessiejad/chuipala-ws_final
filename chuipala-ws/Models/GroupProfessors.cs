namespace chuipala_ws.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("GroupProfessors")]
    public partial class GroupProfessors
    {
        [Key, Column(Order = 0)]
        public int GroupID { get; set; }
        public virtual Group Group { get; set; }
        
        [Key, Column(Order = 1)]
        public string ProfessorID { get; set; }
        public virtual Professor Professor { get; set; }
        
    }
}