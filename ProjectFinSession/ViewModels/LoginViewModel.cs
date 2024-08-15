using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace ProjectFinSession.ViewModels
{
    public class LoginViewModel
    {

        [Required]
        [EmailAddress]
        [Display(Name ="Email")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        public IEnumerable<SelectListItem>? RoleList { get; set; }
        public string? RoleSelected { get; set; }

        public string? ReturnUrl { get; set; }

    }
}
