using Microsoft.AspNetCore.Mvc;

namespace SimpleApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class SimpleController : ControllerBase
    {
        public static List<string> names = new List<string>()
        {
            "Haim1",
            "Haim2",
            "Haim3",
        };

        [HttpGet]
        public string GetUserFirstName()
        {
            return "Haim";
        }

        //http:localhost:111/api/simple/GetUserFirstNameByIdQuery?id=2
        [HttpGet]
        public string GetUserFirstNameByIdQuery([FromQuery(Name = "id")] string id)
        {
            return names[int.Parse(id) - 1];
        }

        //http:localhost:111/api/simple/GetUserFirstNameByIdRoute/2
        [HttpGet("{id?}")]
        public string GetUserFirstNameByIdRoute([FromRoute] string id)
        {
            return names[int.Parse(id) - 1];
        }

        [HttpPost]
        public void AddUserName([FromBody] User user)
        {
            names.Add(user.userName);
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
            return Ok(names);
        }
    }
}
