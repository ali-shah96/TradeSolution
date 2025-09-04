using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Trading.Api.Dto;
using Trading.Api.Models;
using Trading.Api.Services;

namespace Trading.Api.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class TradesController : ControllerBase
    {
        private readonly ITradeService _tradeService;
        private readonly IMapper _mapper;

        public TradesController(ITradeService tradeService, IMapper mapper)
        {
            _tradeService = tradeService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> ExecuteTrade([FromBody] TradeDto tradeDto)
        {
            var trade = _mapper.Map<Trade>(tradeDto);
            var result = await _tradeService.ExecuteTradeAsync(trade);
            return Ok(_mapper.Map<TradeDto>(result));
        }

        [HttpGet]
        public async Task<IActionResult> GetTrades([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var result = await _tradeService.GetTradesAsync(pageNumber, pageSize);
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetTradeById(int id)
        {
            var trade = await _tradeService.GetTradeByIdAsync(id);

            if (trade == null)
                return NotFound($"Trade with ID {id} not found.");

            return Ok(_mapper.Map<TradeDto>(trade));
        }
    }
}
