using Newtonsoft.Json;
using WebApp.Model.CoinModel;

namespace WebApp.Model.Response;

public class CoinCategoryResponseListCoin
{
    [JsonProperty("status")] public CoinStatus Status { get; set; }
    // [JsonProperty("data")] 
    // public CoinInfoCategory CoinCate { get; set; }

    [JsonProperty("coinCate")]
    public CoinInfoCategory coinCate { get; set; }
}