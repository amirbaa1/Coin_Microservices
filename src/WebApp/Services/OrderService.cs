using Newtonsoft.Json;
using WebApp.Model.OrderModel;

namespace WebApp.Services;

public class OrderService : IOrderService
{
    private readonly HttpClient _httpClient;
    public OrderService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<OrderModel>> GetOrder(string username)
    {
        var response = await _httpClient.GetAsync($"/order/{username}");

        var read = await response.Content.ReadAsStringAsync(); // error 401

        if (response.IsSuccessStatusCode)
        {
            return JsonConvert.DeserializeObject<List<OrderModel>>(read)!;
        }

        throw new Exception("Something went wrong when calling the API!");
    }
}