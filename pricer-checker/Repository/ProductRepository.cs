using Microsoft.EntityFrameworkCore;
using pricer_checker.Data;
using pricer_checker.Helpers;
using pricer_checker.Interfaces;
using pricer_checker.Models.Entities;

namespace pricer_checker.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Product>> GetAllAsync(QueryObject query)
        {
            var products = _context.Products.AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.Name))
            {
                products = products.Where(p => p.Name.Contains(query.Name));
            }

            return await products.ToListAsync();
        }

        public Task<bool> ProductExists(Guid id)
        {
            return _context.Products.AnyAsync(p => p.Id == id);
        }

    }
}
