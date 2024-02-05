using Newtonsoft.Json;

namespace Coin.API.Model.CoinModel;

public class CoinStatus
{
    [JsonProperty("timestamp")]
    public DateTime TimesTamp { get; set; }
    [JsonProperty("error_code")]
    public int ErrorCode  { get; set; }
    [JsonProperty("error_message")]
    public string ErrorMessage { get; set; }
    [JsonProperty("elapsed")]
    public string Elapsed { get; set; }
    [JsonProperty("credit_count")]
    public int CreditCount { get; set; }
}