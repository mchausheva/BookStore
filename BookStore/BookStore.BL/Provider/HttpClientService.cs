using BookStore.Models.Configurations;
using BookStore.Models.Models;
using BookStore.Models.Models.Users;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace BookStore.BL.Provider
{
    public class HttpClientService
    {
        private HttpClient _httpClient;
        private readonly IOptions<HttpClientSettings> _options;

        public HttpClientService(HttpClient httpClient, IOptions<HttpClientSettings> options)
        {
            _httpClient = httpClient;
            _options = options;

            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            _httpClient.BaseAddress = new Uri(_options.Value.UrlBaseAddress);
        }
        public async Task<AuthorAdditionalInfo> AdditionalInfo()
        {
            var response = await _httpClient.GetAsync(_httpClient.BaseAddress);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();

            var addInfo = JsonConvert.DeserializeObject<AuthorAdditionalInfo>(result);

            return addInfo;
        }
    }
}