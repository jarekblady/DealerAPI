using System.ComponentModel.DataAnnotations;

namespace DealerAPI.Models
{
    public class UpdateDealerDto
    {
        [Required]
        [MaxLength(25)]
        public string Name { get; set; }
        public string Description { get; set; }
        public string ContactEmail { get; set; }
        public string ContactNumber { get; set; }
    }
}
