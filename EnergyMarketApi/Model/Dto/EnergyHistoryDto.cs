using System;
using EnergyMarketApi.Enum;

namespace EnergyMarketApi.Model.Dto
{
    public class EnergyHistoryDto
    {
        public Guid Uuid { get; set; }
        public double Balance { get; set; }
        public DateTime DateTime { get; set; }
        public ActionType EnergyHistoryType { get; set; }
    }
}