using AutoMapper;
using MassTransit;
using Trading.Api.Dto;
using Trading.Api.Helpers;
using Trading.Api.Models;
using Trading.Api.Repositories;
using Trading.Api.Services;
using static MassTransit.ValidationResultExtensions;

namespace TradingPublisher.Services.Imp
{
    public class TradeService : ITradeService
    {
        private readonly ITradeRepository _tradeRepository;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly ILogger<TradeService> _logger;
        private readonly IMapper _mapper;

        public TradeService(ITradeRepository tradeRepository, IPublishEndpoint publishEndpoint, ILogger<TradeService> logger, IMapper mapper)
        {
            _tradeRepository = tradeRepository;
            _publishEndpoint = publishEndpoint;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<Trade> ExecuteTradeAsync(Trade trade)
        {
            try
            {
                var savedTrade = await _tradeRepository.AddTradeAsync(trade);

                // Publish event
                await _publishEndpoint.Publish(savedTrade);

                return savedTrade;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error executing trade for symbol {Symbol}", trade.Symbol);
                throw;
            }
        }

        public async Task<PagedResult<TradeDto>> GetTradesAsync(int pageNumber, int pageSize)
        {
            try
            {
                _logger.LogInformation("Fetching trades page {PageNumber} with size {PageSize}", pageNumber, pageSize);
                var allTrades = await _tradeRepository.GetTradesAsync();
                return PagedResult<TradeDto>.Create(_mapper.Map<IEnumerable<TradeDto>> (allTrades), pageNumber, pageSize);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching trades page {PageNumber}", pageNumber);
                throw;
            }
        }

        public async Task<Trade?> GetTradeByIdAsync(int id)
        {
            return await _tradeRepository.GetTradeByIdAsync(id);
        }
    }
}
