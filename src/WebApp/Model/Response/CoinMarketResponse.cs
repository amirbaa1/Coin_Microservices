using Coin.API.Model.CoinModel;
using Newtonsoft.Json;

namespace WebApp.Model.Response;

public class CoinMarketResponse
{
    public List<CoinInfo> Data { get; set; }
    [JsonProperty("status")]
    public CoinStatus CoinStatus { get; set; }
}