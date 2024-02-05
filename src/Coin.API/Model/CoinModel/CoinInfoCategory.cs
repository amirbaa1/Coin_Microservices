using Newtonsoft.Json;

namespace Coin.API.Model.CoinModel;

public class CoinInfoCategory
{
    [JsonProperty("id")]
    public string Id { get; set; }
    [JsonProperty("name")]
    public string name { get; set; }
    [JsonProperty("title")]
    public string title { get; set; }
    [JsonProperty("num_tokens")]
    public int NumberToken { get; set; }
    [JsonProperty("avg_price_change")]
    public double? AvgPriceChange { get; set; }
    [JsonProperty("market_cap_change")]
    public decimal? MarketCapChange { get; set; }
    [JsonProperty("volume_change")]
    public decimal? VolumeChange { get; set; }
    [JsonProperty("last_updated")] 
    public DateTime LastUpdate { get; set; }
    [JsonProperty("coins")]
    public List<CoinInfo>? coin { get; set; }
}