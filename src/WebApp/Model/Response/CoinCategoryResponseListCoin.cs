using Coin.API.Model.CoinModel;
using Newtonsoft.Json;

namespace WebApp.Model.Response;

public class CoinCategoryResponseListCoin
{
    [JsonProperty("status")]
    public CoinStatus Status { get; set; }
    [JsonProperty("data")] 
    public CoinInfoCategory CoinCate { get; set; }
    
    // [JsonProperty("data/coins")]
    // public List<CoinCategoryListCoin>? coinList { get; set; }
}