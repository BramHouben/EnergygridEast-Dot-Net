namespace EnergyMarketApi.Model.Helper
{
    public static class RabbitMqRouting
    {
        public static readonly string BuyEnergy = "energymarket.balance.buy";
        public static readonly string SellEnergy = "energymarket.balance.sell";
    }
}