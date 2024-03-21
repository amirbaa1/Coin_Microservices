using Wallet.API.Model.CoinModel;
using Newtonsoft.Json;

namespace Wallet.API.Model.Response;

public class CoinSearchResponse
{

    [JsonProperty("Data")]
    public Dictionary<string, List<CoinSearch>> Data { get; set; }
    [JsonProperty("status")]
    public CoinStatus Status { get; set; }
}