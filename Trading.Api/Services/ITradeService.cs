using Trading.Api.Dto;
using Trading.Api.Helpers;
using Trading.Api.Models;

namespace Trading.Api.Services
{
    public interface ITradeService
    {
        Task<Trade> ExecuteTradeAsync(Trade trade);
        Task<PagedResult<TradeDto>> GetTradesAsync(int pageNumber, int pageSize);
        Task<Trade?> GetTradeByIdAsync(int id);
    }
}
