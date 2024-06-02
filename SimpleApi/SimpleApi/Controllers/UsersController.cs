using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleApi.Attributes;
using SimpleBL.Interfaces;
using SimpleDB.EF.Models;
using SimpleEntites;

namespace SimpleApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class UsersController : Controller
    {
        private readonly ILogger<UsersController> _logger;
        private readonly IUsersBL _usersBL;

        public UsersController(ILogger<UsersController> logger, IUsersBL usersBL)
        {
            _logger = logger;
            _usersBL = usersBL;
        }


        [MyCustom]
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login(UserLoginDTO user)
        {
            try
            {
                bool isExist = _usersBL.Login(user);
                if (!isExist)
                    return NotFound();
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error on Login, Message: {ex.Message}," +
                    $" InnerException: {ex.InnerException}, StackTrace: {ex.StackTrace}");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        public IActionResult Logout()
        {
            try
            {
                Response.Cookies.Delete(CookiesKeys.AccessToken);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error on Logout, Message: {ex.Message}," +
                                       $" InnerException: {ex.InnerException}, StackTrace: {ex.StackTrace}");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }   
    }
}
