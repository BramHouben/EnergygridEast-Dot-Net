using EnergyMarketApi.Model.RabbitMq;
using EnergyMarketApi.Model.ToFrontend;
using System;
using System.Collections.Generic;
using System.Data;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace EnergyMarketApi.Logic
{
    public class MarketLogic
    {
        private readonly string _energyApiUrl;
        private readonly string _bearer;

        public MarketLogic()
        {
            _energyApiUrl = Environment.GetEnvironmentVariable("ENERGYMARKET_APIURL");
            if (string.IsNullOrEmpty(_energyApiUrl))
            {
                throw new NoNullAllowedException("ENERGYMARKET_APIURL empty");
            }

            _bearer = GetBearer().Result;
        }

        private async Task<string> GetBearer()
        {
            var login = new
            {
                Username = Environment.GetEnvironmentVariable("ENERGYMARKET_USERNAME"),
                Password = Environment.GetEnvironmentVariable("ENERGYMARKET_PASSWORD")
            };

            using var httpClient = new HttpClient();
            HttpResponseMessage result = await httpClient.PostAsJsonAsync($"{_energyApiUrl}login", login);
            return await result.Content.ReadAsStringAsync();
        }

        public async Task<List<EnergyHistoryViewmodel>> All()
        {
            using var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {_bearer}");
            return await httpClient.GetFromJsonAsync<List<EnergyHistoryViewmodel>>($"{_energyApiUrl}offer/own");
        }

        public async Task Buy(EnergyRabbitMq energy)
        {
            using var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {_bearer}");
            await httpClient.PostAsJsonAsync($"{_energyApiUrl}buy", energy);
        }

        public async Task Sell(EnergyRabbitMq energy)
        {
            using var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {_bearer}");
            await httpClient.PostAsJsonAsync($"{_energyApiUrl}offer", energy);
        }
    }
}
