using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SimpleBL.Interfaces;
using SimpleDB.EF.Models;
using SimpleDB.Interfaces;
using SimpleEntites;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SimpleBL.Services
{
    public class UsersBL : IUsersBL
    {
        private readonly ILogger<UsersBL> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly AppSettings _appSettings;
        private readonly IUsersDB _usersDB;
        private readonly IMapper _mapper;
        public UsersBL(ILogger<UsersBL> logger, IHttpContextAccessor httpContextAccessor,
            IOptions<AppSettings> options, IUsersDB usersDB, IMapper mapper)
        {
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
            _appSettings = options.Value;
            _usersDB = usersDB;
            _mapper = mapper;
        }
        public bool Login(UserLoginDTO userLoginDTO)
        {
            User userMapped = _mapper.Map<User>(userLoginDTO);
            User userFromDb = _usersDB.Login(userMapped);
            if (userFromDb is not null)
            {
                CreateUserToken(userFromDb.UserName);
                byte[] bytearray = Encoding.ASCII.GetBytes(userFromDb.UserName);
                _httpContextAccessor.HttpContext.Session.Set("username", bytearray);
                return true;
            }
            return false;
        }

        private void CreateUserToken(string userName)
        {
            string newAccessToken = this.GenerateAccessToken(userName);
            CookieOptions cookieTokenOptions = new CookieOptions()
            {
                HttpOnly = true,
                Secure = false,
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.Now.AddMinutes(_appSettings.Jwt.ExpireMinutes)
            };
            _httpContextAccessor.HttpContext.Response.Cookies.Append(CookiesKeys.AccessToken, newAccessToken, cookieTokenOptions);
        }

        private string GenerateAccessToken(string userName)
        {
            var jwtSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.Jwt.SecretKey));
            var credentials = new SigningCredentials(jwtSecurityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, userName)
            };
            var token = new JwtSecurityToken(
                _appSettings.Jwt.Issuer,
                _appSettings.Jwt.Audience,
                claims,
                expires: DateTime.Now.AddMinutes(_appSettings.Jwt.ExpireMinutes),
                signingCredentials: credentials
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
