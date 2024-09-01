using pricer_checker.Models.Enums;

namespace pricer_checker.Models.Dtos
{
    public class ProductDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public CategoryEnum Category { get; set; }
        public string ImageUri { get; set; }
        public decimal? LastPrice { get; set; }
        public DateTime? LastPriceDate { get; set; }
    }
}
