using System;
using Microsoft.AspNetCore.Connections;
using RabbitMQ.Client;

namespace EnergyMarketApi.RabbitMq
{
    public class RabbitMqChannel
    {
        public IModel GetChannel()
        {
            var rabbitMqFactory = new ConnectionFactory
            {
                HostName = Environment.GetEnvironmentVariable("RABBITMQ_HOSTNAME"),
                UserName = Environment.GetEnvironmentVariable("RABBITMQ_USERNAME"),
                Password = Environment.GetEnvironmentVariable("RABBITMQ_PASSWORD")
            };

            IConnection connection = null;

            var attempts = 0;
            while (attempts < 10)
            {
                try
                {
                    attempts++;
                    connection = rabbitMqFactory.CreateConnection();
                    break;
                }
                catch (Exception)
                {
                    Console.WriteLine("RabbitMq connection could not be reached, attempting again in 5 seconds");
                    System.Threading.Thread.Sleep(5000);
                }
            }
            if (connection == null)
            {
                throw new ConnectionAbortedException();
            }

            return connection.CreateModel();
        }
    }
}