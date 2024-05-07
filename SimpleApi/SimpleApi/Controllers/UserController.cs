using Microsoft.AspNetCore.Mvc;
using SimpleBL;
using SimpleBL.Interfaces;
using SimpleBL.Services;
using SimpleEntites;

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


        [HttpGet]
        public IActionResult GetUserFirstName()
        {
            try
            {
                string name = _userBL.GetUserFirstName();
                return Ok(name);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        //http:localhost:111/api/simple/GetUserFirstNameByIdQuery?id=2
        [HttpGet]
        public IActionResult GetUserFirstNameByIdQuery([FromQuery(Name = "id")] string id)
        {
            try
            {
                string name = _userBL.GetUserFirstNameByIdQuery(id);
                return Ok(name);
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        //http:localhost:111/api/simple/GetUserFirstNameByIdRoute/2
        [HttpGet("{id?}")]
        public IActionResult GetUserFirstNameByIdRoute([FromRoute] string id)
        {
            try
            {
                string name = _userBL.GetUserFirstNameByIdRoute(id);
                return Ok(name);
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
        public IActionResult GetAllUserNaems()
        {
            try
            {
                List<string> names = _userBL.GetAllUserNaems();
                return Ok(names);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
