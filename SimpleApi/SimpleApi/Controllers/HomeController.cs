using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using SimpleEntites;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace SimpleApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMemoryCache _memoryCache;
        public HomeController(ILogger<HomeController> logger, IMemoryCache memoryCache)
        {
            _logger = logger;
            _memoryCache = memoryCache;
        }

        [HttpGet]
        public IActionResult GetHeader()
        {
            try
            {
                var token = Request.Cookies[CookiesKeys.AccessToken];
                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadJwtToken(token);
                string userName = jsonToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
                return Ok($"דף הבית - {userName}");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error on GetHeader, Message: {ex.Message}," +
                    $" InnerException: {ex.InnerException}, StackTrace: {ex.StackTrace}");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        public IActionResult GetFooter()
        {
            try
            {
                string value = "";
                if(!_memoryCache.TryGetValue(CacheKeys.Footer, out value))
                {
                    value = "כל הזכויות שמורות לקוביית פלאפל";
                    _memoryCache.Set(CacheKeys.Footer, value, new MemoryCacheEntryOptions
                    {
                        AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(10)
                    });
                }
                return Ok(value);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error on GetFooter, Message: {ex.Message}," +
                    $" InnerException: {ex.InnerException}, StackTrace: {ex.StackTrace}");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
