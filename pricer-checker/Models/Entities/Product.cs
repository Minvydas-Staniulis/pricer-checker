using pricer_checker.Models.Enums;

namespace pricer_checker.Models.Entities
{
    public class Product
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required CategoryEnum Category { get; set; }
        public required string ImageUri { get; set; }
    }
}