using EnergyMarketApi.Logic;
using Microsoft.AspNetCore.Mvc;

namespace EnergyMarketApi.Controllers
{
    [Route("energymarket")]
    [ApiController]
    public class EnergyController : ControllerBase
    {
        private readonly MarketLogic _marketLogic;

        public EnergyController(MarketLogic marketLogic)
        {
            _marketLogic = marketLogic;
        }


    }
}