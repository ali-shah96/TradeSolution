using Trading.Api.Models;
using AutoMapper;
using Trading.Api.Dto;

namespace Trading.Api.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Trade, TradeDto>().ReverseMap();
        }
    }
}
