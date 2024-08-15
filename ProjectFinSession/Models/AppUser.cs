using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectFinSession.Models
{
    public class AppUser : IdentityUser
    {
        [Required]
        public string Nom { get; set; }

        [Required]
        public string Prenom { get; set; }

        [Required]
        public string Adresse { get; set; }

        public int? CartId { get; set; }

        [ForeignKey("CartId")]
        public cart Cart { get; set; }
    }
}
