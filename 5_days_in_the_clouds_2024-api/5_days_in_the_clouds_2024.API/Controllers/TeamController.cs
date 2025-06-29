using _5_days_in_the_clouds_2024.Application.Features.Team.Commands.CreateTeam;
using _5_days_in_the_clouds_2024.Application.Features.Team.Quieries.GetTeamById;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace _5_days_in_the_clouds_2024.API.Controllers
{
    [Route("teams")]
    [ApiController]
    public class TeamController : ApiControllerBase
    {
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetTeamById(string id)
        {
            var response = await Mediator.Send(new GetTeamByIdQuery { Id = id });
            return Ok(response);
        }
        [HttpPost]
        public async Task<IActionResult> CreateTeam([FromBody] CreateTeamCommand command)
        {
            var response = await Mediator.Send(command);
            return Ok(response);
        }
    }
}
