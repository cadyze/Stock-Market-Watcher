using Microsoft.AspNetCore.Mvc;
using StockTracker.Models;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Linq;
using System.Diagnostics;

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
            DateTime prevMonth = todayDate.Date.AddDays(-30);

            string currentDate = todayDate.ToString("yyyy-MM-dd");
            string prevDate = prevMonth.ToString("yyyy-MM-dd");

            Console.WriteLine($"Ticker Given: {ticker}");
            string API_KEY = "1KBpAIdt0vyd1MK9jOpbg1H9bapCjA2N";
            string URL = $"https://api.polygon.io/v2/aggs/ticker/{ticker}/range/1/day/{prevDate}/{currentDate}?apiKey={API_KEY}";

            // Authenticate API
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(URL);

            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            string resp = await client.GetStringAsync(URL);
            AggregateBarModel? abModel = JsonSerializer.Deserialize<AggregateBarModel>(resp);
            //StockModel stockModel = new StockModel(abModel);

            // Given a model that isn't null, run the Python model to predict
            //if(abModel != null && abModel.results != null)
            //{
            //    string stockDataPath = Path.Combine("Data", "stocks", ticker) + ".csv";
            //    string pyPath = Path.Combine("stock_predictor") + ".py";
            //    Console.WriteLine(stockDataPath + " | Does this .csv exist? -> " + System.IO.File.Exists(stockDataPath));
            //    Console.WriteLine(pyPath + " | Does the py model exist? -> " + System.IO.File.Exists(pyPath));
            //    Console.WriteLine("Number of abModel results: " + abModel.results.Count);

            //    int resultsCount = 20;
            //    DayTradeResponse[] results = abModel.results.Take(resultsCount).ToArray();

            //    float[] closingPrices = new float[resultsCount];
            //    float[] volumes = new float[resultsCount];

            //    for (int i = 0; i < resultsCount; i++)
            //    {
            //        closingPrices[i] = results[i].c;
            //        volumes[i] = results[i].v;
            //    }

            //    // Create a JSON Object to import to Python
            //    var jsonObject = new { csv = stockDataPath, closingPrices = closingPrices, volumes = volumes };
            //    string json = JsonSerializer.Serialize(jsonObject);

            //    string pythonExecutable = "python3";
            //    ProcessStartInfo psi = new ProcessStartInfo();
            //    psi.FileName = pythonExecutable;
            //    psi.Arguments = $"\"{pyPath}\" \"{json}\""; // Pass JSON object as an argument
            //    psi.UseShellExecute = false;
            //    psi.RedirectStandardOutput = true;
            //    psi.RedirectStandardError = true;

            //    // Start the process with the info specified
            //    using (Process process = Process.Start(psi))
            //    {
            //        using (StreamReader reader = process.StandardOutput)
            //        {
            //            string result = reader.ReadToEnd();
            //            Console.WriteLine("Result from Python: " + result);
            //        }
            //        using (StreamReader reader = process.StandardError)
            //        {
            //            string errors = reader.ReadToEnd();
            //            if (!string.IsNullOrEmpty(errors))
            //                Console.WriteLine("Errors: " + errors);
            //        }
            //    }
            //}

            return abModel;
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
