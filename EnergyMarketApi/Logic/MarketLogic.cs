using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using EnergyMarketApi.Model.RabbitMq;

namespace EnergyMarketApi.Logic
{
    public class MarketLogic
    {
        private readonly string _energyApiUrl;
        private readonly string _bearer;

        public MarketLogic()
        {
            _energyApiUrl = Environment.GetEnvironmentVariable("ENERGYMARKET_APIURL");
            _bearer = GetBearer().Result;
        }

        public async Task<string> GetBearer()
        {
            var login = new
            {
                Username = Environment.GetEnvironmentVariable("ENERGYMARKET_USERNAME"),
                Password = Environment.GetEnvironmentVariable("ENERGYMARKET_PASSWORD")
            };

            using var httpClient = new HttpClient();
            HttpResponseMessage result = await httpClient.PostAsJsonAsync($"{_energyApiUrl}login", login);
            return result.Content.ToString();
        }

        public async Task Buy(EnergyRabbitMq energy)
        {
            using var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("Bearer ", _bearer);
            await httpClient.PostAsJsonAsync($"{_energyApiUrl}buy", energy);
        }

        public async Task Sell(EnergyRabbitMq energy)
        {
            using var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("Bearer ", _bearer);
            await httpClient.PostAsJsonAsync($"{_energyApiUrl}offer", energy);
        }
    }
}