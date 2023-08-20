using Application.Features.Starships.Queries.GetList;
using Application.Features.Starships.Queries.GeyById;
using Business.Features.Starships.Commands.Create;
using Core.Application.Requests;
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

        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] PageRequest request)
        {
            GetListStarshipQuery getListStarshipQuery=new GetListStarshipQuery() { PageRequest = request };
            var response = await Mediator.Send(getListStarshipQuery);
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            GetByIdStarshipQuery getListStarshipQuery = new GetByIdStarshipQuery() { Id = id };
            var response = await Mediator.Send(getListStarshipQuery);
            return Ok(response);
        }
    }
}
