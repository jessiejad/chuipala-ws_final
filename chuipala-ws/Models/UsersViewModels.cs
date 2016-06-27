using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace chuipala_ws.Models
{
    /*public class CreateUserViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Courrier électronique*")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "La chaîne {0} doit comporter au moins {2} caractères.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Mot de passe*")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmer le mot de passe*")]
        [Compare("Password", ErrorMessage = "Le mot de passe et le mot de passe de confirmation ne correspondent pas.")]
        public string ConfirmPassword { get; set; }
        
        [Required]
        [Display(Name = "Nom*")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Prénom*")]
        public string FirstName { get; set; }
        
        [Required]
        [Display(Name = "Professeur")]
        public bool IsProfessor { get; set; }

        [Required]
        [Display(Name = "Etudiant")]
        public bool IsStudent { get; set; }
    }*/

    public class CreateUserViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Courrier électronique")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "La chaîne {0} doit comporter au moins {2} caractères.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Mot de passe")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmer le mot de passe")]
        [Compare("Password", ErrorMessage = "Le mot de passe et le mot de passe de confirmation ne correspondent pas.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name = "Nom")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Prénom")]
        public string FirstName { get; set; }

    }
}
