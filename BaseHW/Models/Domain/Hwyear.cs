using System.ComponentModel.DataAnnotations;

namespace BaseHW.Models.Domain
{
    public class Hwyear
    {
        public int Id { get; set; }
        [Required]
        public string? HwyearName { get; set; }

    }
}
