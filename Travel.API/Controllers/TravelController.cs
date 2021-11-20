using Travel.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Travel.Domain;

namespace Travel.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TravelController : ControllerBase
    {
        private readonly ILogger<TravelController> _logger;
        private readonly ITravelService _travelService;

        public TravelController(ILogger<TravelController> logger, ITravelService travelService)
        {
            _logger = logger;
            _travelService = travelService;
        }

        [HttpGet("GetAirlines")]
        public async Task<IEnumerable<Airline>> GetAirlines()
        {
            return await _travelService.GetAirlines();
        }

        [HttpGet("GetAirlinesAsync")]
        public async IAsyncEnumerable<Airline> GetAirlinesAsync()
        {
            //return await _travelService.GetAirlinesAsync();
            var airlines = await _travelService.GetAirlinesAsync();
            await foreach (var airline in airlines)
            {
                yield return airline;
            }
        }

        [HttpPost]
        public async Task SaveAirlines(IEnumerable<Airline> airlines)
        {
            await _travelService.SaveAirlines(airlines);
        }
    }
}
