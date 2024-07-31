using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using pricer_checker.Data;
using pricer_checker.Models.Dtos;
using pricer_checker.Models.Entities;

namespace pricer_checker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PriceRecordController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public PriceRecordController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
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
        public IActionResult AddPriceRecord(AddPriceRecordDto addPriceRecordDto)
        {
            var product = _dbContext.Products.Find(addPriceRecordDto.ProductId);

            if (product == null)
            {
                return NotFound($"Product with Id {addPriceRecordDto.ProductId} not found.");
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



    }
}
