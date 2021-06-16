using AutoMapper;
using EnergyMarketApi.Model.Dto;
using EnergyMarketApi.Model.ToFrontend;

namespace EnergyMarketApi.Model.Helper
{
    public static class AutoMapperConfig
    {
        public static MapperConfiguration Config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<EnergyHistoryDto, EnergyHistoryViewmodel>();
        });
    }
}