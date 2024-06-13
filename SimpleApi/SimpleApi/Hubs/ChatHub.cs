using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using SimpleEntites;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace SimpleApi.Hubs
{
    [Authorize]
    public class ChatHub : Hub
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ChatHub(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public void SendMessage(string message)
        {
            string userName = GetUserNameFromToken();
            Clients.All.SendAsync("NewMessages", userName, message);
        }

        private string GetUserNameFromToken()
        {
            var token = _httpContextAccessor.HttpContext.Request.Cookies[CookiesKeys.AccessToken];
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadJwtToken(token);
            string userName = jsonToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;

            return userName;
        }
    }
}
