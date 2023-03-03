using Application.ActionFilters;
using Application.Features.Movies.Commands.Create;
using Application.Features.Movies.Commands.Delete;
using Application.Features.Movies.Commands.Update;
using Application.Features.Movies.Queries.GetById;
using Application.Features.Movies.Queries.List;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieStoreWebApi.Controllers.Base;

namespace MovieStoreWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : BaseController
    {
        [HttpGet("{Id}")]
        [Authorize(Policy = "Customer")]
        public async Task<IActionResult> GetAsync([FromRoute] GetMovieByIdQuery getMovieByIdQuery)
        {
            return Ok(await Mediator.Send(getMovieByIdQuery));
        }

        [HttpGet]
        [DynamicQuery]
        [Authorize(Policy = "Customer")]
        public async Task<IActionResult> ListAsync([FromQuery] GetMoviesListQuery getMoviesListQuery)
        {
            return Ok(await Mediator.Send(getMoviesListQuery, HttpContext.RequestAborted));
        }

        [HttpPost]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> AddAsync([FromBody] CreateMovieCommand createMovieCommand)
        {
            return Created("", await Mediator.Send(createMovieCommand));
        }

        [HttpPut]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> UpdateAsync([FromBody] UpdateMovieCommand updateMovieCommand)
        {
            return Ok(await Mediator.Send(updateMovieCommand));
        }

        [HttpDelete("{Id}")]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> DeleteAsync([FromRoute] DeleteMovieCommand deleteMovieCommand)
        {
            return Ok(await Mediator.Send(deleteMovieCommand));
        }
    }
}
