using System.Collections.Generic;

namespace DealerAPI.Entities
{
    public class Dealer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ContactEmail { get; set; }
        public string ContactNumber { get; set; }
        //public virtual User CreatedBy { get; set; }

        public int AddressId { get; set; }

        public virtual Address Address { get; set; }
        public virtual List<Car> Cars { get; set; }
    }
}
