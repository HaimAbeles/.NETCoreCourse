using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using SimpleBL.Interfaces;
using SimpleEntites;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace SimpleBL.Services
{
    public class RestApiGW : IRestApiGW
    {
        private readonly ILogger<RestApiGW> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        public RestApiGW(ILogger<RestApiGW> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
        }
        public T ApiRequest<T>(ApiRequestModel apiRequestModel)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            using (HttpClient client = _httpClientFactory.CreateClient())
            {
                string stringJson;
                HttpRequestMessage httpRequestMessage = new HttpRequestMessage();
                httpRequestMessage.RequestUri = new Uri(apiRequestModel.baseUrl + apiRequestModel.relativeUrl);
    
                switch (apiRequestModel.method)
                {
                    case EHttpRequestType.POST:
                        httpRequestMessage.Method = HttpMethod.Post;
                        stringJson = JsonSerializer.Serialize(apiRequestModel.data);
                        httpRequestMessage.Content = new StringContent(stringJson, Encoding.UTF8, "application/json");
                        break;

                    case EHttpRequestType.GET:
                        httpRequestMessage.Method = HttpMethod.Get;
                        break;
                }
                response = client.SendAsync(httpRequestMessage).Result;
                return GetModelFromResponse<T>(apiRequestModel, response);
            }
        }

        private T GetModelFromResponse<T>(ApiRequestModel apiRequestModel, HttpResponseMessage responseMessage)
        {
            string responseBody = responseMessage.Content.ReadAsStringAsync().Result;
            try
            {
                responseMessage.EnsureSuccessStatusCode();
                T jsonData = JsonSerializer.Deserialize<T>(responseBody);
                if (jsonData == null)
                    _logger.LogError($"Failed on response from Pulseem service in url: {apiRequestModel.baseUrl + apiRequestModel.relativeUrl}, Body is null FullResponseBody: {responseBody}");
                return jsonData;
            }
            catch (HttpRequestException e)
            {
                _logger.LogError($"Failed on HttpClient in url: {apiRequestModel.baseUrl + apiRequestModel.relativeUrl}, to connect pulseem service, Message: {e.Message}, FullResponseBody: {responseBody} StackTrace: {e.StackTrace}, ex: {e}");
                return default;
            }
        }
    }

}
