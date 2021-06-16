using System;

namespace EnergyMarketApi.Model.ToFrontend
{
    public class EnergyHistoryViewmodel
    {
        public Guid Id { get; set; }
        public double Amount { get; set; }
        public double AmountTotal { get; set; }
        public double Price { get; set; }
    }
}