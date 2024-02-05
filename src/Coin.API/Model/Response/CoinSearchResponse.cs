using Coin.API.Model.CoinModel;
using Newtonsoft.Json;

namespace Coin.API.Model.Response;

public class CoinSearchResponse
{

    [JsonProperty("Data")]
    public Dictionary<string, List<CoinSearch>> Data { get; set; }
    [JsonProperty("status")]
    public CoinStatus Status { get; set; }
}