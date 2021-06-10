using AutoMapper;
using EnergyMarketApi.Logic;
using EnergyMarketApi.Model.Dto;
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
                List<EnergyHistoryDto> historyCollection = await _marketLogic.All(4);
                return _mapper.Map<List<EnergyHistoryViewmodel>>(historyCollection);
            }
            catch (ArgumentOutOfRangeException)
            {
                return UnprocessableEntity();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
