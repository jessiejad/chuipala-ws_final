namespace chuipala_ws.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Class")]
    public partial class Class
    {
        [Display(Name = "Référence")]
        public int ClassID { get; set; }

        [Display(Name = "Enseignant")]
        public string ProfessorID { get; set; }
        public virtual Professor Professor { get; set; }
        [Display(Name = "Matière")]
        public int SubjectID { get; set; }
        public virtual Subject Subject { get; set; }
        public virtual ICollection<GroupClasses> GroupClasses { get; set; }

        [Display(Name = "Commence le")]
        public DateTime StartDateTime { get; set; }
        [Display(Name = "Termine le")]
        public DateTime EndDateTime { get; set; }

        [Display(Name = "Enseignant")]
        public string ProfessorIdentity
        {
            get
            {
                if(Professor != null)
                {
                    return Professor.DisplayFullName;
                }
                return "Aucun Professeur";
            }
        }

        [Display(Name = "Matière")]
        public string SubjectIdentity
        {
            get
            {
                if (Subject != null)
                {
                    return Subject.FullName;
                }
                return "Aucune Matière";
            }
        }

        [Display(Name = "Matière")]
        public string SubjectLabel
        {
            get
            {
                if (Subject != null)
                {
                    return Subject.Label;
                }
                return "Aucune Matière";
            }
        }

        [Display(Name = "Cours")]
        public string ClassIdentity
        {
            get
            {
                return ClassID.ToString() + " - " + SubjectIdentity + " (" + ProfessorIdentity + ")";
            }
        }
        
        public IEnumerable<string> GroupsNames
        {
            get
            {
                var lst = new List<string>();
                if (GroupClasses == null)
                {
                    return lst;
                }
                foreach(GroupClasses gc in GroupClasses)
                {
                    lst.Add(gc.getGroupName);
                }
                return lst;
            }
        }
    }
}