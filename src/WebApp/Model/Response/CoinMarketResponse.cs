using Newtonsoft.Json;
using WebApp.Model.CoinModel;

namespace WebApp.Model.Response;

public class CoinMarketResponse
{
    public List<CoinInfo> Data { get; set; }
    [JsonProperty("status")]
    public CoinStatus CoinStatus { get; set; }
} 