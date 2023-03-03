using Application.ActionFilters;
using Application.Features.Directors.Commands.Create;
using Application.Features.Directors.Commands.Delete;
using Application.Features.Directors.Commands.Update;
using Application.Features.Directors.Queries.GetById;
using Application.Features.Directors.Queries.List;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieStoreWebApi.Controllers.Base;

namespace MovieStoreWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DirectorsController : BaseController
    {
        [HttpGet("{Id}")]
        [Authorize(Policy = "Customer")]
        public async Task<IActionResult> GetAsync([FromRoute] GetDirectorByIdQuery getDirectorByIdQuery)
        {
            return Ok(await Mediator.Send(getDirectorByIdQuery));
        }

        [HttpGet]
        [DynamicQuery]
        [Authorize(Policy = "Customer")]
        public async Task<IActionResult> ListAsync([FromQuery] GetDirectorsListQuery getDirectorsListQuery)
        {
            return Ok(await Mediator.Send(getDirectorsListQuery, HttpContext.RequestAborted));
        }

        [HttpPost]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> AddAsync([FromBody] CreateDirectorCommand createDirectorCommand)
        {
            return Created("", await Mediator.Send(createDirectorCommand));
        }

        [HttpPut]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> UpdateAsync([FromBody] UpdateDirectorCommand updateDirectorCommand)
        {
            return Ok(await Mediator.Send(updateDirectorCommand));
        }

        [HttpDelete("{Id}")]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> DeleteAsync([FromRoute] DeleteDirectorCommand deleteDirectorCommand)
        {
            return Ok(await Mediator.Send(deleteDirectorCommand));
        }
    }
}
