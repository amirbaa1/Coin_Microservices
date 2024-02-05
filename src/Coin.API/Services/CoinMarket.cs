using Coin.API.Model;
using Microsoft.Extensions.Options;
using Coin.API.Model.Response;
using Newtonsoft.Json;

namespace Coin.API.Services;

public class CoinMarket : ICoinMarket
{
    private readonly GetCoin _getcoin;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ApiRequestService _apiRequestService;

    public CoinMarket(IOptions<GetCoin> getcoin, IHttpClientFactory httpClientFactory,
        ApiRequestService apiRequestService)
    {
        _getcoin = getcoin.Value;
        _httpClientFactory = httpClientFactory;
        _apiRequestService = apiRequestService;
    }

    public async Task<CoinMarketResponse> CoinMarketGenerator()
    {
        try
        {
            string apiUrl = "https://pro-api.coinmarketcap.com/v1/cryptocurrency/map";
            string DataCoin = await _apiRequestService.SendRequest(apiUrl, _getcoin.Key);
            if (DataCoin != null)
            {
                return JsonConvert.DeserializeObject<CoinMarketResponse>(DataCoin)!;
            }
            else
            {
                return null;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            return null;
        }
    }

    public async Task<string> GetAllCoin()
    {
        try
        {
            string apiUrl = "https://pro-api.coinmarketcap.com/v1/cryptocurrency/listings/latest";
            string DataCoin = await _apiRequestService.SendRequest(apiUrl, _getcoin.Key);
            if (DataCoin != null)
            {
                return DataCoin;
            }
            else
            {
                return null;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<CoinSearchResponse> GetBySymbol(string symbol)
    {
        try
        {
            string apiUrl = $"https://pro-api.coinmarketcap.com/v2/cryptocurrency/quotes/latest?symbol={symbol}";
            string DataCoin = await _apiRequestService.SendRequest(apiUrl, _getcoin.Key);
            if (DataCoin != null)
            {
                return JsonConvert.DeserializeObject<CoinSearchResponse>(DataCoin)!;
            }
            else
            {
                return null;
            }
        }
        catch (Exception ex)
        {
            // _logger.LogError($"Error : {ex.Message}");
            return null;
        }
    }

    public async Task<CoinSearchResponse> GetByName(string name)
    {
        try
        {
            string apiurl = $"https://pro-api.coinmarketcap.com/v2/cryptocurrency/quotes/latest?name={name}";
            string DataCoin = await _apiRequestService.SendRequest(apiurl, _getcoin.Key);
            if (DataCoin != null)
            {
                return JsonConvert.DeserializeObject<CoinSearchResponse>(DataCoin)!;
            }
            else
            {
                return null;
            }
        }
        catch (Exception ex)
        {
            // _logger.LogError($"Error : {ex.Message}");
            return null;
        }
    }

    public async Task<CoinCategoryResponse> GetAllCategory()
    {
        try
        {
            string apiUrlCate = "https://pro-api.coinmarketcap.com/v1/cryptocurrency/categories";
            string DataCoinCate = await _apiRequestService.SendRequest(apiUrlCate, _getcoin.Key);
            if (DataCoinCate != null)
            {
                return JsonConvert.DeserializeObject<CoinCategoryResponse>(DataCoinCate)!;
            }
            else
            {
                Console.WriteLine("Link Data null");
                return null;
            }
        }
        catch (Exception ex)
        {
            // _logger.LogError($"Error : {ex.Message}");
            Console.WriteLine($"error : {ex.Message}");

            return null;
        }
    }

    public async Task<CoinCategoryResponseListCoin> GetByIdCategory(string id)
    {
        try
        {
            string apiUrlCate = $"https://pro-api.coinmarketcap.com/v1/cryptocurrency/category?id={id}";
            string DataListCoin = await _apiRequestService.SendRequest(apiUrlCate, _getcoin.Key);
            if (DataListCoin != null)
            {
                return JsonConvert.DeserializeObject<CoinCategoryResponseListCoin>(DataListCoin)!;
            }

            return null;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"error : {ex}");
            throw;
        }
    }
}