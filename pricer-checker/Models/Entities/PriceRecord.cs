namespace pricer_checker.Models.Entities
{
    public class PriceRecord
    {
        public Guid Id { get; set; }
        public decimal Price { get; set; }
        public DateTime Date { get; set; }
        public Guid ProductId { get; set; }
    }
}
