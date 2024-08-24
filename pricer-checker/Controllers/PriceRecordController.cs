using Microsoft.AspNetCore.Mvc;
using pricer_checker.Data;
using pricer_checker.Interfaces;
using pricer_checker.Models.Dtos;
using pricer_checker.Models.Entities;
using pricer_checker.Services;

namespace pricer_checker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PriceRecordController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IProductRepository _productRepo;
        private readonly PriceChangeService _priceChangeService;

        public PriceRecordController(ApplicationDbContext dbContext, IProductRepository productRepo, PriceChangeService priceChangeService)
        {
            _dbContext = dbContext;
            _productRepo = productRepo;
            _priceChangeService = priceChangeService;
        }

        [HttpGet]
        public IActionResult GetAllPriceRecords() 
        {
            var allPriceRecords = _dbContext.PriceRecords.ToList();
            
            return Ok(allPriceRecords);
        }

        [HttpGet]
        [Route("{productId:guid}")]
        public IActionResult GetPriceRecordsByProductId(Guid productId)
        {
            var priceRecords = _dbContext.PriceRecords
                .Where(pr => pr.ProductId == productId)
                .ToList();

            if (priceRecords == null || !priceRecords.Any())
            {
                return NotFound($"No price records found for ProductId {productId}");
            }

            return Ok(priceRecords);
        }

        [HttpPost]
        [Route("{productId:guid}")]
        public async Task<IActionResult> AddPriceRecord(AddPriceRecordDto addPriceRecordDto, Guid productId)
        {
            if (!await _productRepo.ProductExists(productId))
            {
                return BadRequest($"No Products found with ProductId {productId}");
            }

            var priceRecordEntity = new PriceRecord()
            {
                Price = addPriceRecordDto.Price,
                Date = addPriceRecordDto.Date,
                ProductId = addPriceRecordDto.ProductId,
            };

            _dbContext.PriceRecords.Add(priceRecordEntity);
            _dbContext.SaveChanges();

            return Ok(priceRecordEntity);
        }

        [HttpGet("price-change")]
        public async Task<IActionResult> GetPriceChange(Guid productId, int days)
        {
            var change = await _priceChangeService.CalculatePriceChange(productId, TimeSpan.FromDays(days));
            return Ok(new { priceChange = change });
        }


    }
}
