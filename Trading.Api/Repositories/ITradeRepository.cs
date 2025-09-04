using Trading.Api.Models;

namespace Trading.Api.Repositories
{
    public interface ITradeRepository
    {
        Task<Trade> AddTradeAsync(Trade trade);
        Task<IEnumerable<Trade>> GetTradesAsync();
        Task<Trade?> GetTradeByIdAsync(int id);
    }
}
