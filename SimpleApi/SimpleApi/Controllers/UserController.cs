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

        public UserController(IUserBL userBL)
        {
            _userBL = userBL;
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
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
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
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
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
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
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
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
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
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
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
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
