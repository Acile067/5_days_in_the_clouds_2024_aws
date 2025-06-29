using _5_days_in_the_clouds_2024.Application.Features.Player.Commands.CreatePlayer;
using _5_days_in_the_clouds_2024.Application.Features.Player.Quieries.GetPlayerById;
using _5_days_in_the_clouds_2024.Application.Features.Player.Quieries.GetPlayers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace _5_days_in_the_clouds_2024.API.Controllers
{
    [Route("players")]
    [ApiController]
    public class PlayerController : ApiControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetPlayers()
        {
            var response = await Mediator.Send(new GetPlayersQuery());
            return Ok(response);
        }
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetPlayerById(string id)
        {
            var response = await Mediator.Send(new GetPlayerByIdQuery { Id = id});
            return Ok(response);
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreatePlayer([FromBody] CreatePlayerCommand command)
        {
            var response = await Mediator.Send(command);
            return Ok(response);
        }
    }
}
