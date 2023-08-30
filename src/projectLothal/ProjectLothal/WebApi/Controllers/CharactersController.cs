using Application.Features.Characters.Queries.GetList;
using Application.Features.Characters.Queries.GetListByDynamic;
using Core.Application.Requests;
using Core.Persistance.Dynamic;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CharactersController : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] PageRequest request)
        {
            GetListCharacterQuery getListStarshipQuery = new GetListCharacterQuery() { PageRequest = request };
            var response = await Mediator.Send(getListStarshipQuery);
            return Ok(response);
        }

        [HttpPost("GetListByDynamic")]
        public async Task<IActionResult> GetListByDynamic([FromQuery] PageRequest request, [FromBody] DynamicQuery? dynamicQuery)
        {
            GetListByDynamicCharacterQuery getListStarshipQuery = new GetListByDynamicCharacterQuery() { PageRequest = request ,DynamicQuery=dynamicQuery};
            var response = await Mediator.Send(getListStarshipQuery);
            return Ok(response);
        }
    }
}
