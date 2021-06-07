using EnergyMarketApi.Logic;
using EnergyMarketApi.Model.Helper;
using EnergyMarketApi.Model.RabbitMq;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace EnergyMarketApi.RabbitMq.Consumers
{
    public class SellConsumer : IConsumer
    {
        private readonly IModel _channel;
        private readonly MarketLogic _marketLogic;

        public SellConsumer(IModel channel, MarketLogic marketLogic)
        {
            _channel = channel;
            _marketLogic = marketLogic;
        }

        public void Consume()
        {
            _channel.ExchangeDeclare(RabbitMqExchange.EnergyMarketExchange, ExchangeType.Direct);
            _channel.QueueDeclare(RabbitMqQueues.BuyEnergyQueue, true, false, false, null);
            _channel.QueueBind(RabbitMqQueues.BuyEnergyQueue, RabbitMqExchange.EnergyMarketExchange, RabbitMqRouting.BuyEnergy);
            _channel.BasicQos(0, 10, false);

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += async (sender, e) =>
            {

                byte[] body = e.Body.ToArray();
                string json = Encoding.UTF8.GetString(body);
                var energy = Newtonsoft.Json.JsonConvert.DeserializeObject<EnergyRabbitMq>(json);

                await _marketLogic.Sell(energy);
            };

            _channel.BasicConsume(RabbitMqQueues.BuyEnergyQueue, true, consumer);
        }
    }
}