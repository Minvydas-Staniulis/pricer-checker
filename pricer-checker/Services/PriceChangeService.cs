using Microsoft.EntityFrameworkCore;
using pricer_checker.Data;

namespace pricer_checker.Services
{
    public class PriceChangeService
    {
        private readonly ApplicationDbContext _context;

        public PriceChangeService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<decimal> CalculatePriceChange(Guid productId, TimeSpan timeSpan)
        {
            var now = DateTime.UtcNow;
            var pastDate = now - timeSpan;

            var prices = await _context.PriceRecords
                .Where(pr => pr.ProductId == productId && pr.Date >= pastDate)
                .OrderBy(pr => pr.Date)
                .ToListAsync();
        
            if(prices.Count < 2 )
            {
                return 0;
            }

            var initialPrice = prices.First().Price;
            var latestPrice = prices.Last().Price;

            return latestPrice - initialPrice;
        }
    }
}
