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
        public async Task<IActionResult> GetProductById(Guid id)
        {
            var product = await _productRepository.GetByIdAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(AddProductDto addProductDto)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdProduct = await _productRepository.AddProductAsync(addProductDto);

            return Ok(createdProduct); 
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public IActionResult DeleteProduct(Guid id)
        {
            var productModel = dbContext.Products.FirstOrDefault(x => x.Id == id);

            if (productModel == null)
            {
                return NotFound();
            }

            var priceRecords = dbContext.PriceRecords.Where(pr => pr.ProductId == id).ToList();

            dbContext.PriceRecords.RemoveRange(priceRecords);
            dbContext.Products.Remove(productModel);
            dbContext.SaveChanges();

            return NoContent();
        }
    }
}