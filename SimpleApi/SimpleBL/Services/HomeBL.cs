using Microsoft.Extensions.Options;
using SimpleBL.Interfaces;
using SimpleEntites;


namespace SimpleBL.Services
{
    public class HomeBL : IHomeBL
    {
        private readonly IRestApiGW _restApiGW;
        private readonly AppSettings _appSettings;
        public HomeBL(IRestApiGW restApiGW, IOptions<AppSettings> options)
        {

            _restApiGW = restApiGW;
            _appSettings = options.Value;

        }

        public List<Post> GetAllPosts()
        {
            return _restApiGW.ApiRequest<List<Post>>(new ApiRequestModel
            {
                baseUrl = _appSettings.jsonplaceholderApi,
                relativeUrl = JsonplaceholderRelativeApiKeys.Posts,
                method = EHttpRequestType.GET,
            });
        }
    }
}
