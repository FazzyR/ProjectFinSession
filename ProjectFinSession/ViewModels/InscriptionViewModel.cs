using System.ComponentModel.DataAnnotations;

namespace ProjectFinSession.ViewModels
{
    public class InscriptionViewModel
    {
        [Required]
        [Display(Name = "Nom")]

        public string Nom { get; set; }

        [Required]
        [Display(Name = "Prenom")]

        public string Prenom { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name ="Email")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }


        [Required]
        [Display(Name = "Adresse")]

        public string Adresse { get; set; }



        public string? ReturnUrl { get; set; }
    }
}
