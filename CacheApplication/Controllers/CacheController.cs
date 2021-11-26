using Couchbase;
using Couchbase.Extensions.Caching;
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
        ICouchbaseCacheBucketProvider _couchbaseCacheBucketProvider;
        public CacheController(ILogger<CacheController> logger, IDistributedCache cache, ICouchbaseCacheBucketProvider couchbaseCacheBucketProvider)
        {
            _logger = logger;
            _cache = cache;
            _couchbaseCacheBucketProvider = couchbaseCacheBucketProvider;
        }

        //[Route("GetCacheDataAsync/{cacheKey}")]
        [HttpGet("GetCacheDataAsync")]
        public async Task<string> GetCacheDataAsync(string cacheKey)
        {
            //          var options = new ClusterOptions()
            //.WithConnectionString("couchbase://localhost")
            //.WithCredentials(username: "Administrator", password: "password")
            //.WithBuckets("Cache-Sample");            
            //          var cluster = await Cluster.ConnectAsync(options);
            //var bucket =await _couchbaseCacheBucketProvider.GetBucketAsync().ConfigureAwait(false);
            //var cluster = bucket.Cluster;
            //await cluster.WaitUntilReadyAsync(TimeSpan.FromSeconds(10));
            var data = await _cache.GetStringAsync(cacheKey);
            if (data == null)
            {
                data = DateTime.Now + "/" + Guid.NewGuid();
                await _cache.SetStringAsync(cacheKey, data);
            }
            return data;
        }

        [HttpGet("GetCachedWeatherForecastAsync")]
        public async Task<WeatherForecast> GetCachedWeatherForecastAsync()
        {
            var cacheKey = DateTime.Now.Date.ToShortDateString();
            var weatherForecast = new WeatherForecast() { Date = DateTime.Now, Summary = "Sample weather", TemperatureC = 35 };
            byte[] utf8bytesJson = JsonSerializer.SerializeToUtf8Bytes(weatherForecast);
            try
            {
                weatherForecast = await _cache.GetAsync<WeatherForecast>(cacheKey);
            }
            catch (CouchbaseException exception)
            {
                var options = new DistributedCacheEntryOptions() { AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(10) };
                await _cache.SetAsync<WeatherForecast>(cacheKey, weatherForecast, options);
                //string jsonString = JsonSerializer.Serialize(new WeatherForecast() { Date = DateTime.Now.Date, Summary = $"Forecast for {DateTime.Now.Date}", TemperatureC = new Random().Next(0, 50) });
                //await _cache.SetStringAsync(cacheKey, jsonString);
                weatherForecast = await _cache.GetAsync<WeatherForecast>(cacheKey);
            }
            return weatherForecast;
        }
    }
}
