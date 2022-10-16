using System.ComponentModel.DataAnnotations;

namespace DealerAPI.Models
{
    public class CreateCarDto
    {
        [Required]
        public string Make { get; set; }
        [Required]
        public string Model { get; set; }
        [Required]
        public int Year { get; set; }
        [Required]
        public string Fuel { get; set; }
        [Required]
        public int EnginePower { get; set; }
        [Required]
        public decimal Price { get; set; }
        public string Description { get; set; }
        public int DealerId { get; set; }
    }
}
