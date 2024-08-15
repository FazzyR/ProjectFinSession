using System.ComponentModel.DataAnnotations;

namespace ProjectFinSession.ViewModels
{
    public class CreateProductViewModel
    {
        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }


        [Display(Name = "Description")]
        public string Description { get; set; }


        [Display(Name = "ImageUrl")]
        public string? ImageUrl { get; set; }=string.Empty;


        [Required]
        [Display(Name = "Price")]
        public decimal Price { get; set; }
    }
}
