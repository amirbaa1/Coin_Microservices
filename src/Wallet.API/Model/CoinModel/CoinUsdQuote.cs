using Microsoft.VisualBasic;
using Newtonsoft.Json;

namespace Wallet.API.Model.CoinModel;

public class CoinUsdQuote
{
    [JsonProperty("price")]
    public double Price { get; set; }
    [JsonProperty("percent_change_1h")]
    public double percent_change_1h { get; set; }
    [JsonProperty("percent_change_24h")]
    public double percent_change_24h { get; set; }
    [JsonProperty("percent_change_7d")]
    public double percent_change_7d { get; set; }
    [JsonProperty("volume_24h")]
    public double volume_24h { get; set; }
    [JsonProperty("last_updated")]
    public DateTime Last_updated { get; set; }
    
}