using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace SpazaStock.Server.Services
{
    public class GroceryApiService
    {
        private readonly HttpClient _httpClient;
        private const string RapidApiHost = "grocery-api2.p.rapidapi.com";
    private const string RapidApiKey = "YOUR_RAPIDAPI_KEY";

        public GroceryApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<GroceryProduct>> FetchAmazonProductsAsync(string query, int page = 1)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"https://grocery-api2.p.rapidapi.com/amazon?query={query}&country=us&page={page}");
            request.Headers.Add("x-rapidapi-host", RapidApiHost);
            request.Headers.Add("x-rapidapi-key", RapidApiKey);
            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<GroceryProduct>>(json) ?? new();
        }

        public async Task<List<GroceryProduct>> FetchWalmartProductsAsync(string query, int page = 1)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"https://grocery-api2.p.rapidapi.com/walmart?query={query}&page={page}");
            request.Headers.Add("x-rapidapi-host", RapidApiHost);
            request.Headers.Add("x-rapidapi-key", RapidApiKey);
            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<GroceryProduct>>(json) ?? new();
        }

        public async Task<decimal> GetUsdToZarRateAsync()
        {
            var response = await _httpClient.GetAsync("https://api.exchangerate-api.com/v4/latest/USD");
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            var doc = JsonDocument.Parse(json);
            var rate = doc.RootElement.GetProperty("rates").GetProperty("ZAR").GetDecimal();
            return rate;
        }
    }

    public class GroceryProduct
    {
        public string title { get; set; }
        public decimal price { get; set; }
        public string image { get; set; }
        public string url { get; set; }
        public string store { get; set; }
    }
}
