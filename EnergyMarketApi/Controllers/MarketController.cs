using EnergyMarketApi.Logic;
using Microsoft.AspNetCore.Mvc;

namespace EnergyMarketApi.Controllers
{
    [Route("market")]
    [ApiController]
    public class MarketController : ControllerBase
    {
        private readonly MarketLogic _marketLogic;

        public MarketController(MarketLogic marketLogic)
        {
            _marketLogic = marketLogic;
        }

        public void Sell()
        {

        }
    }
}