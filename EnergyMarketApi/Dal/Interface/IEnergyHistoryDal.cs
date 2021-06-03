using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EnergyMarketApi.Model.Dto;

namespace EnergyMarketApi.Dal.Interface
{
    public interface IEnergyHistoryDal
    {
        public Task Add(EnergyHistoryDto energyHistory);
        public Task Remove(List<Guid> uuidCollection);
        public Task<List<EnergyHistoryDto>> All(int total);
    }
}