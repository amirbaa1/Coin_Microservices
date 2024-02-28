using Newtonsoft.Json;
using WebApp.Model.CoinModel;

namespace WebApp.Model.Response;

public class CoinCategoryResponse
{
    // public CoinStatus coin { get; set; }
    // [JsonProperty("coinCategories")]
    // public Dictionary<string,List<CoinInfoCategory>> CoinCategory { get; set; }
    public List<CoinInfoCategory> CoinCategories { get; set; }
    // public CoinInfoCategory CoinCate { get; set; }
}