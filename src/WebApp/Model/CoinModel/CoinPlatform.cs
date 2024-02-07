using Newtonsoft.Json;

namespace WebApp.Model.CoinModel
{
    public class CoinPlatform
    {
        [JsonProperty("id")]
        public int id { get; set; }
        [JsonProperty("name")]
        public string name { get; set; }
        [JsonProperty("symbol")]
        public string symbol { get; set; }
        [JsonProperty("slug")]
        public string slug { get; set; }
        [JsonProperty("token_address")]
        public string TokenAddress { get; set; }
    }
}