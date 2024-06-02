using Microsoft.Extensions.Logging;
using SimpleBL.Interfaces;
using SimpleEntites;
using System.Text;
using System.Text.Json;

namespace SimpleBL.Services
{
    public class RestApiGW : IRestApiGW
    {
        private readonly ILogger<RestApiGW> _logger;
        public RestApiGW(ILogger<RestApiGW> logger)
        {
            _logger = logger;
        }
        public T ApiRequest<T>(ApiRequestModel apiRequestModel)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            using (HttpClient client = new HttpClient())
            {
                string stringJson;
                HttpRequestMessage httpRequestMessage = new HttpRequestMessage();
                httpRequestMessage.RequestUri = new Uri(apiRequestModel.baseUrl + apiRequestModel.relativeUrl);
    
                switch (apiRequestModel.method)
                {
                    case EHttpRequestType.POST:
                        httpRequestMessage.Method = HttpMethod.Post;
                        JsonSerializerOptions jso = new JsonSerializerOptions();
                        jso.Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping;
                        stringJson = JsonSerializer.Serialize(apiRequestModel.data, jso);
                        httpRequestMessage.Content = new StringContent(stringJson, Encoding.UTF8, "application/json");
                        break;

                    case EHttpRequestType.GET:
                        httpRequestMessage.Method = HttpMethod.Get;
                        break;
                }
                response = client.SendAsync(httpRequestMessage).Result;
                return GetModelFromResponse<T>(apiRequestModel, response, apiRequestModel.baseUrl, apiRequestModel.relativeUrl);
            }
        }

        private T GetModelFromResponse<T>(ApiRequestModel apiRequestModel, HttpResponseMessage responseMessage, string baseUrl, string relativeUrl)
        {
            string responseBody = responseMessage.Content.ReadAsStringAsync().Result;
            try
            {
                responseMessage.EnsureSuccessStatusCode();
                T jsonData = JsonSerializer.Deserialize<T>(responseBody);
                if (jsonData == null)
                    _logger.LogError($"Failed on response from Pulseem service in url: {baseUrl + relativeUrl}, Body is null FullResponseBody: {responseBody}");
                return jsonData;
            }
            catch (HttpRequestException e)
            {
                _logger.LogError($"Failed on HttpClient in url: {baseUrl + relativeUrl}, to connect pulseem service, Message: {e.Message}, FullResponseBody: {responseBody} StackTrace: {e.StackTrace}, ex: {e}");
                return default;
            }
        }
    }

}
