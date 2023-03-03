using Application.ActionFilters;
using Application.Features.Users.Commands.Create;
using Application.Features.Users.Commands.Delete;
using Application.Features.Users.Commands.Update;
using Application.Features.Users.Queries.GetById;
using Application.Features.Users.Queries.List;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieStoreWebApi.Controllers.Base;

namespace MovieStoreWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize("Admin")]
    public class UsersController : BaseController
    {
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetAsync([FromRoute] GetUserByIdQuery getUserByIdQuery)
        {
            return Ok(await Mediator.Send(getUserByIdQuery));
        }

        [HttpGet]
        [DynamicQuery]
        public async Task<IActionResult> ListAsync([FromQuery] GetUsersListQuery getUserListQuery)
        {
            return Ok(await Mediator.Send(getUserListQuery));
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CreateUserCommand createUserCommand)
        {
            return Created("", await Mediator.Send(createUserCommand));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] UpdateUserCommand updateUserCommand)
        {
            return Ok(await Mediator.Send(updateUserCommand));
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] DeleteUserCommand deleteUserCommand)
        {
            return Ok(await Mediator.Send(deleteUserCommand));
        }
    }
}