using _5_days_in_the_clouds_2024.Application.Features.Match.Commands.CreateMatch;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace _5_days_in_the_clouds_2024.API.Controllers
{
    [Route("matches")]
    [ApiController]
    public class MatchController : ApiControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateMatch([FromBody] CreateMatchCommand command)
        {
            var response = await Mediator.Send(command);
            return Ok(response);
        }
    }
}
