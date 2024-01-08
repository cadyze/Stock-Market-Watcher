namespace StockTracker.Models
{
    public class AggregateBarModel
    {
        public string ticker { get; set; }

        public List<DayTradeResponse> results { get; set; }
    }

    public class DayTradeResponse
    {
        public float c { get; set; }
        public float h { get; set; }
        public float l { get; set; }
        public float o { get; set; }
        public float t { get; set; }
        public float v { get; set; }
    }
}
