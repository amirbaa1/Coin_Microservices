
using Newtonsoft.Json;
using System.Net.Http;
using System.Text.Json;
using WebApp.Model;
using WebApp.Model.OrderModel;

namespace WebApp.Services
{
    public class WalletService : IWalletService
    {
        private readonly ILogger<WalletService> _logger;
        private readonly HttpClient _httpClient;

        public WalletService(ILogger<WalletService> logger, HttpClient httpClient)
        {
            _logger = logger;
            _httpClient = httpClient;
        }

        public async Task<List<WalletModel>> OnGetWalletByUserName(string userName)
        {
            try
            {
                var response = await _httpClient.GetAsync($"/wallet/{userName}");

                var read = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                return System.Text.Json.JsonSerializer.Deserialize<List<WalletModel>>(read, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
            catch (Exception ex)
            {
                throw new Exception("Something went wrong when calling api.");
            }
        }

        public async Task UpdateCoinWallet(string userame)
        {
            try
            {
                var empty = new StringContent(string.Empty);
                var response = await _httpClient.PutAsync($"/wallet/{userame}", empty);
                var read = await response.Content.ReadAsStringAsync(); 
                
                //return JsonSerializer.Deserialize<List<WalletModel>>(read);
                
                //return JsonConvert.DeserializeObject<List<WalletModel>>(read)!;
            }
            catch (Exception ex)
            {
                throw new Exception("Something went wrong when calling api.");
            }
        }
    }
}
