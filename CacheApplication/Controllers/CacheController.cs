using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace CacheApplication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CacheController : ControllerBase
    {
        private readonly ILogger<CacheController> _logger;
        private readonly IDistributedCache _cache;

        public CacheController(ILogger<CacheController> logger, IDistributedCache cache)
        {
            _logger = logger;
            _cache = cache;
        }

        //[Route("GetCacheDataAsync/{cacheKey}")]
        [HttpGet("GetCacheDataAsync")]
        public async Task<string> GetCacheDataAsync(string cacheKey)
        {
            var data = await _cache.GetStringAsync(cacheKey);
            if (data == null)
            {
                data = DateTime.Now + "/" + Guid.NewGuid();
                await _cache.SetStringAsync(cacheKey, data);
            }
            return data;
        }

        [HttpGet("GetCachedWeatherForecastAsync")]
        public async Task<string> GetCachedWeatherForecastAsync()
        {
            var cacheKey = DateTime.Now.Date.ToShortDateString();
            var data = await _cache.GetStringAsync(cacheKey);
            if (data == null)
            {
                string jsonString = JsonSerializer.Serialize(new WeatherForecast() { Date = DateTime.Now.Date, Summary = $"Forecast for {DateTime.Now.Date}", TemperatureC = new Random().Next(0, 50) });
                await _cache.SetStringAsync(cacheKey, jsonString);
            }
            return data;
        }
    }
}
