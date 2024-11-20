using pricer_checker.Helpers;
using pricer_checker.Models.Dtos;
using pricer_checker.Models.Entities;

namespace pricer_checker.Interfaces
{
    public interface IProductRepository
    {
        Task<List<ProductDto>> GetAllAsync(QueryObject query);
        Task<ProductDto> GetByIdAsync(Guid id);

        Task<Product> AddProductAsync(AddProductDto addProductDto);
        Task<bool> ProductExists(Guid id);
    }
}
