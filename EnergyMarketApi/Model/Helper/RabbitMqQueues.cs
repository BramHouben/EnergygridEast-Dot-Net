namespace EnergyMarketApi.Model.Helper
{
    public static class RabbitMqQueues
    {
        public static readonly string BuyEnergyQueue = "energymarket-buy";
        public static readonly string SellEnergyQueue = "energymarket-sell";
    }
}