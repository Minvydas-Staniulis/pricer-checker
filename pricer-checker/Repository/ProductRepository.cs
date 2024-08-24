using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using pricer_checker.Data;
using pricer_checker.Helpers;
using pricer_checker.Interfaces;
using pricer_checker.Models.Dtos;
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

        public async Task<Product> GetByIdAsync(Guid id)
        {
            return await _context.Products.FindAsync(id);
        }

        public async Task<Product> AddProductAsync(AddProductDto addProductDto)
        {
            var productEntity = new Product()
            {
                Name = addProductDto.Name,
                Category = addProductDto.Category,
                ImageUri = addProductDto.ImageUri,
            };

            await _context.Products.AddAsync(productEntity);
            await _context.SaveChangesAsync();

            return productEntity;
        }

        public Task<bool> ProductExists(Guid id)
        {
            return _context.Products.AnyAsync(p => p.Id == id);
        }

    }
}
