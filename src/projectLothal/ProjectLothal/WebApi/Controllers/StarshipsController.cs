using Application.Features.Starships.Commands.Delete;
using Application.Features.Starships.Commands.Update;
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

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateStarshipCommand request)
        {
            var response = await Mediator.Send(request);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var response = await Mediator.Send(new DeleteStarshipCommand { Id=id});
            return Ok(response);
        }
    }
}
