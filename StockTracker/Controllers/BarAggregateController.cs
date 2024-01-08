using Microsoft.AspNetCore.Mvc;
using StockTracker.Models;
using System.Net.Http.Headers;
using System.Text.Json;

namespace StockTracker.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BarAggregateController : ControllerBase
    {
        [HttpGet("ticker/{ticker}")]
        public async Task<AggregateBarModel> Get(string ticker)
        {
            //Get current date to last month in the proper format
            DateTime todayDate = DateTime.Now;
            DateTime prevMonth = todayDate.Date.AddMonths(-1);

            string currentDate = todayDate.ToString("yyyy-MM-dd");
            string prevDate = prevMonth.ToString("yyyy-MM-dd");
            Console.WriteLine(currentDate);
            Console.WriteLine(prevDate);

            Console.WriteLine($"Ticker Given: {ticker}");
            string API_KEY = "1KBpAIdt0vyd1MK9jOpbg1H9bapCjA2N";
            string URL = $"https://api.polygon.io/v2/aggs/ticker/{ticker}/range/1/day/{prevDate}/{currentDate}?apiKey={API_KEY}";

            // Authenticate API
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(URL);

            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            Console.WriteLine("Getting shit");
            string resp = await client.GetStringAsync(URL);
            Console.WriteLine(resp);
            AggregateBarModel? model = JsonSerializer.Deserialize<AggregateBarModel>(resp);

            return model;
        }

        //[HttpGet]
        //public async Task<AggregateBarModel> Get()
        //{
        //    string ticker = "AAPL";
        //    Console.WriteLine($"Ticker Given: {ticker}");
        //    string API_KEY = "1KBpAIdt0vyd1MK9jOpbg1H9bapCjA2N";
        //    string URL = $"https://api.polygon.io/v2/aggs/ticker/{ticker}/range/1/day/2023-01-09/2023-01-10?apiKey={API_KEY}";

        //    // Authenticate API
        //    HttpClient client = new HttpClient();
        //    client.BaseAddress = new Uri(URL);

        //    client.DefaultRequestHeaders.Accept.Add(
        //        new MediaTypeWithQualityHeaderValue("application/json"));

        //    Console.WriteLine("Getting shit");
        //    string resp = await client.GetStringAsync(URL);
        //    Console.WriteLine(resp);
        //    AggregateBarModel? model = JsonSerializer.Deserialize<AggregateBarModel>(resp);

        //    return model;
        //}
    }
}
