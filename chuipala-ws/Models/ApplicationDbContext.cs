using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace chuipala_ws.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<Professor> Professors { get; set; }
        public virtual DbSet<Group> Groups { get; set; }
        public virtual DbSet<Class> Classes { get; set; }
        public virtual DbSet<Subject> Subjects { get; set; }
        public virtual DbSet<GroupStudents> GroupStudents { get; set; }
        public virtual DbSet<GroupClasses> GroupClasses { get; set; }
        public virtual DbSet<Event> Events { get; set; }
        public virtual DbSet<Absence> Absences { get; set; }
        public virtual DbSet<Delay> Delays { get; set; }
        public virtual DbSet<AbsenceClasses> AbsenceClasses { get; set; }

        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        static ApplicationDbContext()
        {
            Database.SetInitializer<ApplicationDbContext>(new ApplicationDbInitializer());
        }

    }
}