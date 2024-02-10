using Newtonsoft.Json;

namespace Coin.API.Model.CoinModel;

public class CoinInfo
{
    [JsonProperty("id")]
    public string Id { get; set; }
    [JsonProperty("rank")]
    public int rank { get; set; }
    [JsonProperty("name")]
    public string Name { get; set; }
    [JsonProperty("symbol")]
    public string Symbol { get; set; }
    [JsonProperty("slug")]
    public string slug { get; set; }
    [JsonProperty("cmc_rank")]
    public int cmc_rank { get; set; }
    [JsonProperty("platform")]
    public CoinPlatform? CoinPlatform { get; set; }
    [JsonProperty("quote")]
    public Dictionary<string, CoinUsdQuote> Quote { get; set; }
    
}