namespace chuipala_ws.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("GroupStudents")]
    public partial class GroupStudents
    {
        [Key, Column(Order = 0)]
        public int GroupID { get; set; }
        public virtual Group Group { get; set; }
        
        [Key, Column(Order = 1)]
        public string StudentID { get; set; }
        public virtual Student Student { get; set; }

        /*
        public int NbStudents
        {
            get
            {
                return (this.Group.NbStudents);
            }
        }*/
    }
}