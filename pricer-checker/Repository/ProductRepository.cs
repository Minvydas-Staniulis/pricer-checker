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

        public async Task<List<ProductDto>> GetAllAsync(QueryObject query)
        {
            var products = _context.Products.AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.Name))
            {
                products = products.Where(p => p.Name.Contains(query.Name));
            }

            var productsWithPriceRecords = await products.Select(p => new
            {
                Product = p,
                LastPriceRecord = _context.PriceRecords.Where(pr => pr.ProductId == p.Id).OrderByDescending(pr => pr.Date).FirstOrDefault()
            }).ToListAsync();

            var result = productsWithPriceRecords.Select(p => new ProductDto
            {
                Id = p.Product.Id,
                Name = p.Product.Name,
                Category = p.Product.Category,
                ImageUri = p.Product.ImageUri,
                LastPrice = p.LastPriceRecord?.Price,
                LastPriceDate = p.LastPriceRecord?.Date
            }).ToList();

            return result;
        }

        public async Task<ProductDto> GetByIdAsync(Guid id)
        {
            var product = await _context.Products
                                        .Where(p => p.Id == id)
                                        .FirstOrDefaultAsync();

            if (product == null)
            {
                return null;
            }

            var lastPriceRecord = await _context.PriceRecords
                                                .Where(pr => pr.ProductId == id)
                                                .OrderByDescending(pr => pr.Date)
                                                .FirstOrDefaultAsync();

            var result = new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Category = product.Category,
                ImageUri = product.ImageUri,
                LastPrice = lastPriceRecord?.Price,
                LastPriceDate = lastPriceRecord?.Date
            };

            return result;
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
