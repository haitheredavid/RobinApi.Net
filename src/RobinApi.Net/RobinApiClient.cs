using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using RobinApi.Net.Exceptions;
using RobinApi.Net.Extensions;
using RobinApi.Net.Helpers;
using RobinApi.Net.Model;
using RobinApi.Net.Wrappers;

namespace RobinApi.Net
{ 
    public partial class RobinApiClient
    {
        private const string ApiBaseUrl = "https://api.robinpowered.com/v1.0/";
        private readonly HttpClient _httpClient;

        /// <summary>
        /// Get details about your current access token.
        /// </summary>
        /// <returns></returns>
        public async Task<AccessToken> GetAccessTokenDetails()
        {
            var urlBuilder = new StringBuilder("auth");
            var response = await _httpClient.GetAsync(urlBuilder.ToString()).ConfigureAwait(false);
            var jsonResult = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            if(response.IsSuccessStatusCode)
            {
                return JsonHelper.Deserialize<ApiWrapper<AccessToken>>(jsonResult).Data;
            }
            throw new RobinApiException(JsonHelper.Deserialize<ApiWrapper<object>>(jsonResult).Meta);
        }
        /// <summary>
        /// Instantiates a new RobinApiClient
        /// </summary>
        /// <param name="apiKey">API key which can be generated on robinpowered.com</param>
        public RobinApiClient(string apiKey)
        {
            if (string.IsNullOrWhiteSpace(apiKey))
                throw new ArgumentException("Parameter apiKey needs a value");

            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(ApiBaseUrl)
            };

            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Access-Token {apiKey}");
        }


        private string GetQueryString(Dictionary<string, string> parameters)
        {
            var queryString = string.Join("&",
                parameters.Select(
                    p =>
                        string.IsNullOrEmpty(p.Value)
                            ? $"{Uri.EscapeDataString(p.Key)}="
                            : $"{Uri.EscapeDataString(p.Key)}={Uri.EscapeDataString(p.Value)}"));
            return !string.IsNullOrWhiteSpace(queryString) ? "?" + queryString : string.Empty;
        }


        async Task<T> Get<T>(string end, KeyValuePair<string, string>[] query = null)
        {
            var response = await _httpClient.GetAsync(RobinQuery.Create(end, query)).ConfigureAwait(false);
            var jsonResult = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
      
            if(response.IsSuccessStatusCode)
            {
                return JsonHelper.Deserialize<ApiWrapper<T>>(jsonResult).Data;
            }
      
            throw new RobinApiException(JsonHelper.Deserialize<ApiWrapper<object>>(jsonResult).Meta);
        }

        public async Task<ApiWrapper<T>> GetResponse<T>(string end, KeyValuePair<string, string>[] query = null)
        {
            var response = await _httpClient.GetAsync(RobinQuery.Create(end, query)).ConfigureAwait(false);
            var jsonResult = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
      
            if(response.IsSuccessStatusCode)
            {
                return JsonHelper.Deserialize<ApiWrapper<T>>(jsonResult);
            }
      
            throw new RobinApiException(JsonHelper.Deserialize<ApiWrapper<object>>(jsonResult).Meta);
        }
    }

}
