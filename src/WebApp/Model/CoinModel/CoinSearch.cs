using Microsoft.VisualBasic;
using Newtonsoft.Json;

namespace WebApp.Model.CoinModel;

public class CoinSearch
{
    [JsonProperty("id")]
    public int Id { get; set; }
    [JsonProperty("name")]
    public string Name { get; set; }
    [JsonProperty("Symbol")]
    public string Symbol { get; set; }
    [JsonProperty("last_updated")]
    public DateTime last_updated { get; set; }
    [JsonProperty("slug")]
    public string slug { get; set; }
    [JsonProperty("quote")]
    public Dictionary<string, CoinUsdQuote> Quote { get; set; }
    // public CoinQuote quote { get; set; }
}