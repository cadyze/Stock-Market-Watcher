using Microsoft.AspNetCore.Mvc;
using StockTracker.Models;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace StockTracker.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {

            //AggregateBarModel? resp = new BarAggregateController().Gecet().Result;
            //Console.WriteLine($"{resp.ticker}: Closing Price = {resp.results[0].c}");
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(20, 200),
                Summary = "HAHA"
            })
            .ToArray();
        }
    }
}