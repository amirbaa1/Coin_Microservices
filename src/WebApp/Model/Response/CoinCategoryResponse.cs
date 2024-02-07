using Coin.API.Model.CoinModel;
using Newtonsoft.Json;

namespace WebApp.Model.Response;

public class CoinCategoryResponse
{
    // public CoinStatus coin { get; set; }
    [JsonProperty("data")]
    // public Dictionary<string,List<CoinInfoCategory>> CoinCategory { get; set; }
    public List<CoinInfoCategory> CoinCategories { get; set; }
    // public CoinInfoCategory CoinCate { get; set; }
}