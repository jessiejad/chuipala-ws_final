using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;

namespace chuipala_ws.Models
{
    // Vous pouvez ajouter des données de profil pour l'utilisateur en ajoutant plus de propriétés à votre classe ApplicationUser ; consultez http://go.microsoft.com/fwlink/?LinkID=317594 pour en savoir davantage.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Notez qu'authenticationType doit correspondre à l'élément défini dans CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Ajouter les revendications personnalisées de l’utilisateur ici
            return userIdentity;
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager, string authenticationType)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            // Add custom user claims here
            return userIdentity;
        }

        [Display(Name = "Nom")]
        public string Name { get; set; }

        [Display(Name = "Prénom")]
        public string FirstName { get; set; }
                
        public string DisplayFullName
        {
            get
            {
                string dspName = string.IsNullOrWhiteSpace(this.Name) ? "" : this.Name;
                string dspFirstName = string.IsNullOrWhiteSpace(this.FirstName) ? "" : this.FirstName;
                return string.Format("{0} {1}", dspFirstName, dspName);
            }
        }

        public string Type
        {
            get
            {
                if (IsStudent) return "Etudiant";
                if (IsProfessor) return "Professeur";
                return "Utilisateur";
            }
        }

        [Display(Name = "Est Etudiant")]
        public bool IsStudent
        {
            get
            {
                return (this is Student);
            }
        }

        [Display(Name = "Est Professeur")]
        public bool IsProfessor
        {
            get
            {
                return (this is Professor);
            }
        }
    }

    public class ApplicationRole : IdentityRole
    {
        public ApplicationRole() : base() { }
        public ApplicationRole(string name) : base(name) { }
        public string Description { get; set; }

    }
}