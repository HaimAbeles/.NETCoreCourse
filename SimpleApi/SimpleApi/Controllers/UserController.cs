using Microsoft.AspNetCore.Mvc;
using SimpleBL.Interfaces;
using SimpleDB.EF.Models;

namespace SimpleApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class UserController : ControllerBase
    {
        private readonly IUserBL _userBL;
        private readonly ILogger<UserController> _logger;

        public UserController(IUserBL userBL, ILogger<UserController> logger)
        {
            _userBL = userBL;
            _logger = logger;
        }

        //http:localhost:111/api/simple/GetUserFirstNameByIdQuery?id=2
        [HttpGet]
        public IActionResult GetUserFirstNameByIdQuery([FromQuery(Name = "id")] string id)
        {
            try
            {
                User user = _userBL.GetUserFirstNameByIdQuery(id);
                return Ok(user);
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error on GetUserFirstNameByIdQuery, Message: {ex.Message}," +
                    $" InnerException: {ex.InnerException}, StackTrace: {ex.StackTrace}");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        //http:localhost:111/api/simple/GetUserFirstNameByIdRoute/2
        [HttpGet("{id?}")]
        public IActionResult GetUserByIdRoute([FromRoute] string id)
        {
            try
            {
                User user = _userBL.GetUserByIdRoute(id);
                return Ok(user);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error on GetUserByIdRoute, Message: {ex.Message}," +
                    $" InnerException: {ex.InnerException}, StackTrace: {ex.StackTrace}");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        public IActionResult AddUserName([FromBody] User user)
        {
            try
            {
                _userBL.AddUserName(user);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error on AddUserName, Message: {ex.Message}," +
                    $" InnerException: {ex.InnerException}, StackTrace: {ex.StackTrace}");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }


        [HttpGet]
        public IActionResult GetStatus()
        {
            return Ok("Haim"); //200
            return NotFound(); // 404
            return NoContent(); //204
            return BadRequest(); //400
            return StatusCode(StatusCodes.Status500InternalServerError, "הודעת שגיאה");
        }

        [HttpGet]
        public IActionResult GetAllUsers()
        {
            try
            {
                List<User> users = _userBL.GetAllUsers();
                return Ok(users);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error on GetAllUsers, Message: {ex.Message}," +
                    $" InnerException: {ex.InnerException}, StackTrace: {ex.StackTrace}");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("{id?}")]
        public IActionResult GetUsersByClassId([FromRoute] string id)
        {
            try
            {
                List<User> users = _userBL.GetUsersByClassId(id);
                return Ok(users);
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error on GetUsersByClassId, Message: {ex.Message}," +
                    $" InnerException: {ex.InnerException}, StackTrace: {ex.StackTrace}");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPut]
        public IActionResult UpdateUser(User user) 
        {
            try
            {
                _userBL.UpdateUser(user);
                return Ok();
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error on UpdateUser, Message: {ex.Message}," +
                    $" InnerException: {ex.InnerException}, StackTrace: {ex.StackTrace}");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
