using Business.Features.Starships.Commands.Create;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StarshipsController : BaseController
    {
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateStarshipCommand request)
        {
            var response = await Mediator.Send(request);
            return Ok(response);
        }
    }
}
