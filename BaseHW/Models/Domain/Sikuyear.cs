using System.ComponentModel.DataAnnotations;

namespace BaseHW.Models.Domain
{
    public class Sikuyear
    {
        public int Id { get; set; }
        [Required]
        public string? SikuyearName { get; set; }

    }
}
