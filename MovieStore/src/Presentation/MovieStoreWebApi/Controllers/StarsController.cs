using Application.ActionFilters;
using Application.Features.Stars.Commands.Create;
using Application.Features.Stars.Commands.Delete;
using Application.Features.Stars.Commands.Update;
using Application.Features.Stars.Queries.GetById;
using Application.Features.Stars.Queries.List;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieStoreWebApi.Controllers.Base;

namespace MovieStoreWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StarsController : BaseController
    {
        [HttpGet("{Id}")]
        [Authorize(Policy = "Customer")]
        public async Task<IActionResult> GetAsync([FromRoute] GetStarByIdQuery getStarByIdQuery)
        {
            return Ok(await Mediator.Send(getStarByIdQuery));
        }

        [HttpGet]
        [DynamicQuery]
        [Authorize(Policy = "Customer")]
        public async Task<IActionResult> ListAsync([FromQuery] GetStarsListQuery getStarsListQuery)
        {
            return Ok(await Mediator.Send(getStarsListQuery, HttpContext.RequestAborted));
        }

        [HttpPost]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> AddAsync([FromBody] CreateStarCommand createStarCommand)
        {
            return Created("", await Mediator.Send(createStarCommand));
        }

        [HttpPut]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> UpdateAsync([FromBody] UpdateStarCommand updateStarCommand)
        {
            return Ok(await Mediator.Send(updateStarCommand));
        }

        [HttpDelete("{Id}")]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> DeleteAsync([FromRoute] DeleteStarCommand deleteStarCommand)
        {
            return Ok(await Mediator.Send(deleteStarCommand));
        }
    }
}
