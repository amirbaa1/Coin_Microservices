using Newtonsoft.Json;
using WebApp.Model.CoinModel;

namespace WebApp.Model.Response;

public class CoinDetResponse
{
    [JsonProperty("Data")]
    public Dictionary<string, List<CoinSearch>> CoinSearches { get; set; }
    [JsonProperty("status")]
    public CoinStatus CoinStatus { get; set; }
}