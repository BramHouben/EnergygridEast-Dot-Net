using System;

namespace EnergyMarketApi.Model.RabbitMq
{
    public class EnergyRabbitMq
    {
        public Guid OfferId { get; set; }
        public long Amount { get; set; }
    }
}