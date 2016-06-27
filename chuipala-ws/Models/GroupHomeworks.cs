namespace chuipala_ws.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("GroupHomeworks")]
    public partial class GroupHomeworks
    {
        [Key, Column(Order = 0)]
        public int GroupID { get; set; }
        public virtual Group Group { get; set; }
        
        [Key, Column(Order = 1)]
        public int HomeworkID { get; set; }
        public virtual Homework Homework { get; set; }
        
    }
}