using Microsoft.EntityFrameworkCore;
using Trading.Api.Data;
using Trading.Api.Models;

namespace Trading.Api.Repositories.Imp
{
    public class TradeRepository : ITradeRepository
    {
        private readonly TradeDbContext _context;

        public TradeRepository(TradeDbContext context)
        {
            _context = context;
        }

        public async Task<Trade> AddTradeAsync(Trade trade)
        {
            _context.Trades.Add(trade);
            await _context.SaveChangesAsync();
            return trade;
        }

        public async Task<IEnumerable<Trade>> GetTradesAsync()
        {
            return await _context.Trades.ToListAsync();
        }

        public async Task<Trade?> GetTradeByIdAsync(int id)
        {
            return await _context.Trades.FindAsync(id);
        }
    }
}
