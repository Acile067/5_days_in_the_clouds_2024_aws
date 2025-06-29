using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace _5_days_in_the_clouds_2024.API.Controllers
{
    [Route("test")]
    [ApiController]
    public class TestController : ApiControllerBase
    {
        [HttpGet()]
        public IActionResult Test()
        {
            return Ok("Test successful!");
        }
    }
}
