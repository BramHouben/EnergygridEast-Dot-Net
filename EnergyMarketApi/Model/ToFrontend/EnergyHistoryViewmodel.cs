using EnergyMarketApi.Enum;
using System;

namespace EnergyMarketApi.Model.ToFrontend
{
    public class EnergyHistoryViewmodel
    {
        public Guid Uuid { get; set; }
        public double Balance { get; set; }
        public DateTime DateTime { get; set; }
        public ActionType EnergyHistoryType { get; set; }
    }
}