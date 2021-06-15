using AutoMapper;
using EnergyMarketApi.Logic;
using EnergyMarketApi.Model.ToFrontend;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EnergyMarketApi.Controllers
{
    [Route("energymarket")]
    [ApiController]
    public class EnergyController : ControllerBase
    {
        private readonly MarketLogic _marketLogic;
        private readonly IMapper _mapper;

        public EnergyController(MarketLogic marketLogic, IMapper mapper)
        {
            _marketLogic = marketLogic;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<EnergyHistoryViewmodel>>> All()
        {
            try
            {
                return await _marketLogic.All();
            }
            catch (ArgumentOutOfRangeException)
            {
                return UnprocessableEntity();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
