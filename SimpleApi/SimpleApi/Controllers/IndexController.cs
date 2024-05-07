using Microsoft.AspNetCore.Mvc;
using SimpleBL.Interfaces;
using SimpleBL.Services;

namespace SimpleApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class IndexController : Controller
    {
        private readonly IIndexBL _indexBL;

        public IndexController(IIndexBL indexBL)
        {
            _indexBL = indexBL;
        }


        [HttpGet]
        public IActionResult GetIndex()
        {
            try
            {
                int index = _indexBL.GetIndex();
                return Ok(index);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
