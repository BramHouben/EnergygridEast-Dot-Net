using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using EnergyMarketApi.Enum;
using EnergyMarketApi.Logic;
using EnergyMarketApi.Model.Dto;
using EnergyMarketApi.Model.ToFrontend;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EnergyMarketApi.Controllers
{
    [AuthorizedAction(new[] { AccountRole.CUSTOMER, AccountRole.LARGE_SCALE_CUSTOMER,
        AccountRole.UTILITY_COMPANY, AccountRole.RESPONSIBLE_PARTY, AccountRole.ADMIN })]
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

        [HttpGet("{total}")]
        public async Task<ActionResult<List<EnergyHistoryViewmodel>>> All(int total)
        {
            try
            {
                List<EnergyHistoryDto> historyCollection = await _marketLogic.All(total);
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