using System.ComponentModel.DataAnnotations;

namespace DealerAPI.Models
{
    public class CreateDealerDto
    {
        [Required]
        [MaxLength(25)]
        public string Name { get; set; }
        public string Description { get; set; }
        public string ContactEmail { get; set; }
        public string ContactNumber { get; set; }
        [Required]
        [MaxLength(25)]
        public string City { get; set; }
        [Required]
        [MaxLength(25)]
        public string Street { get; set; }
        [Required]
        [MaxLength(25)]
        public string HouseNumber { get; set; }
        public string PostalCode { get; set; }
    }
}
