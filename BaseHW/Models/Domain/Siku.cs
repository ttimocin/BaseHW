using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BaseHW.Models.Domain
{
    public class Siku
    {
        public int Id { get; set; }
        [Required]
        public string? Title { get; set; }
        public string? Renk { get; set; }
        
        public string? Image { get; set; }  //stores movie image name with extension (eg, image0001.jpg)
        [Required]
        public string? Seri {get; set; }
        [Required]
        public string? Director { get; set; }

        [NotMapped]
        public IFormFile? ImageFile { get; set; }
        [NotMapped]
        [Required]
        public List<int>? Sikuyears { get; set; }
        [NotMapped]

        public IEnumerable<SelectListItem>? SikuyearList { get; set; }
        [NotMapped]
        public string ? SikuyearNames { get; set; }
        
        [NotMapped]
        public MultiSelectList ? MultiSikuyearList { get; set; }
    }
}
