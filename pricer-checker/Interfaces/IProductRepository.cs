using pricer_checker.Helpers;
using pricer_checker.Models.Entities;

namespace pricer_checker.Interfaces
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAllAsync(QueryObject query);
    }
}
