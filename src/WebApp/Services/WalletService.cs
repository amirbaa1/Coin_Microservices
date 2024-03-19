
using System.Text.Json;
using WebApp.Model;

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

                return JsonSerializer.Deserialize<List<WalletModel>>(read, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
            catch (Exception ex)
            {
                throw new Exception("Something went wrong when calling api.");
            }
        }
    }
}
