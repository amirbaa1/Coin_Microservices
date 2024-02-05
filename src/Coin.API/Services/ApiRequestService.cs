using Coin.API.Model;
using Microsoft.Extensions.Options;

namespace Coin.API.Services;

public class ApiRequestService
{
    private readonly IHttpClientFactory _httpClientFactory;

    public ApiRequestService(IOptions<GetCoin> getcoin, IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<string> SendRequest(string ApiUrl, string ApiKey)
    {
        try
        {
            using (var client = _httpClientFactory.CreateClient())
            {
                client.DefaultRequestHeaders.Add("X-CMC_PRO_API_KEY", ApiKey);
                Console.WriteLine($"key : {ApiKey}");
                HttpResponseMessage responseMessage = await client.GetAsync(ApiUrl);
                if (responseMessage.IsSuccessStatusCode)
                {
                    return await responseMessage.Content.ReadAsStringAsync();
                }
                else
                {
                    Console.WriteLine($"Error: {responseMessage.StatusCode}, {responseMessage.ReasonPhrase}");
                    return "Error: {responseMessage.StatusCode}, {responseMessage.ReasonPhrase}";
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return $"Error: {e.Message}";
        }
    }
}