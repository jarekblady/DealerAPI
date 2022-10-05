namespace DealerAPI.Models
{
    public class CarDto
    {
        public int Id { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public string Fuel { get; set; }
        public int EnginePower { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }

    }
}
