namespace pricer_checker.Models.Dtos
{
    public class AddPriceRecordDto
    {
        public decimal Price { get; set; }
        public DateTime Date { get; set; }
        public Guid ProductId { get; set; }
    }
}
