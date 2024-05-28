﻿using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SimpleBL.Interfaces;
using SimpleDB.EF.Models;
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
        public UsersBL(ILogger<UsersBL> logger, IHttpContextAccessor httpContextAccessor, IOptions<AppSettings> options)
        {
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
            _appSettings = options.Value;
        }
        public bool Login(UserLogin user)
        {
            if (user.UserName == "Brachi" && user.Password == "1234")
            {
                CreateUserToken(user.UserName);
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