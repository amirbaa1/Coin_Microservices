using Wallet.API.Model.CoinModel;
using Newtonsoft.Json;

namespace Wallet.API.Model.Response;

public class CoinMarketResponse
{
    public List<CoinInfo> Data { get; set; }
    [JsonProperty("status")]
    public CoinStatus CoinStatus { get; set; }
}