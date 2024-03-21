using Wallet.API.Model.CoinModel;
using Newtonsoft.Json;

namespace Wallet.API.Model.Response;

public class CoinCategoryResponseListCoin
{
    [JsonProperty("status")]
    public CoinStatus Status { get; set; }
    [JsonProperty("data")] 
    public CoinInfoCategory CoinCate { get; set; }
    
    // [JsonProperty("data/coins")]
    // public List<CoinCategoryListCoin>? coinList { get; set; }
}