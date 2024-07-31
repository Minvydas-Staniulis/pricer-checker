using pricer_checker.Models.Entities;
using pricer_checker.Models.Enums;

namespace pricer_checker.Models.Dtos
{
    public class AddProductDto
    {
        public required string Name { get; set; }
        public required CategoryEnum Category { get; set; }
        public required string ImageUri { get; set; }
    }
}
