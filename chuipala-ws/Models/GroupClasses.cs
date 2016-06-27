namespace chuipala_ws.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("GroupClasses")]
    public partial class GroupClasses
    {
        [Key, Column(Order = 0)]
        public int GroupID { get; set; }
        public virtual Group Group { get; set; }
        
        [Key, Column(Order = 1)]
        public int ClassID { get; set; }
        public virtual Class Class { get; set; }

        public string getGroupName
        {
            get
            {
                if (Group == null)
                {
                    return "";
                }
                return Group.Name;
            }
        }
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