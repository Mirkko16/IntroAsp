using System.ComponentModel.DataAnnotations;

namespace IntroAsp.Models.ViewModels
{
    public class BeerViewModel
    {

        [Required]
        [Display(Name = "IdCerveza")]
        public int BeerId { get; set; }

        [Required]
        [Display(Name="Nombre")]
        public string Name { get; set; }

        [Required]
        [Display(Name ="IdMarca")]
        public int BrandId { get; set; }

       
    }
}
