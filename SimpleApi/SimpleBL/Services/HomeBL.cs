using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using SimpleBL.Interfaces;
using SimpleEntites;
using System.Text;


namespace SimpleBL.Services
{
    public class HomeBL : IHomeBL
    {
        private readonly IRestApiGW _restApiGW;
        private readonly AppSettings _appSettings;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public HomeBL(IRestApiGW restApiGW, IOptions<AppSettings> options, IHttpContextAccessor httpContextAccessor)
        {

            _restApiGW = restApiGW;
            _appSettings = options.Value;
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetUserName()
        {
            byte[] byteArray;
            _httpContextAccessor.HttpContext.Session.TryGetValue("username", out byteArray);
            string userName = Encoding.ASCII.GetString(byteArray);
            return userName;
        }

        public List<Blog> GetAllPosts()
        {
            return _restApiGW.ApiRequest<List<Blog>>(new ApiRequestModel
            {
                baseUrl = _appSettings.jsonplaceholderApi,
                relativeUrl = JsonplaceholderRelativeApiKeys.Blogs,
                method = EHttpRequestType.GET,
            });
        }
    }
}
