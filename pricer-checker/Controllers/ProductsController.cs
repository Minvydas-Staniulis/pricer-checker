using Microsoft.AspNetCore.Mvc;
using pricer_checker.Data;
using pricer_checker.Helpers;
using pricer_checker.Models.Dtos;
using pricer_checker.Models.Entities;
using pricer_checker.Interfaces;

namespace pricer_checker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IProductRepository _productRepository;
        public ProductsController(ApplicationDbContext dbContext, IProductRepository productRepo)
        {
            this.dbContext = dbContext;
            _productRepository = productRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts([FromQuery] QueryObject query)
        {
            var allProducts = await _productRepository.GetAllAsync(query);

            return Ok(allProducts);
        }

        [HttpGet]
        [Route("{id:guid}")]
        public IActionResult GetProductById(Guid id)
        {
            var product = dbContext.Products.Find(id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [HttpPost]
        public IActionResult AddProduct(AddProductDto addProductDto)
        {
            var productEntity = new Product()
            {
                Name = addProductDto.Name,
                Category = addProductDto.Category,
                Price = addProductDto.Price,
                Date = addProductDto.Date,
                ImageUri = addProductDto.ImageUri,
            };

            dbContext.Products.Add(productEntity);
            dbContext.SaveChanges();

            return Ok(productEntity); 
        }
    }
}