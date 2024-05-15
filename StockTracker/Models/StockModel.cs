namespace StockTracker.Models
{
    public class StockModel
    {
        public AggregateBarModel? aggregateBarModel { get; set; }
        public string? predictedPrice { get; set; }
        public StockModel(AggregateBarModel abModel) 
        { 
            this.aggregateBarModel = abModel;
        }
    }
}
