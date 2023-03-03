using Application.Features.Auth.Commands.SignIn;
using Application.Features.Auth.Commands.SignInWithRefreshToken;
using Application.Features.Auth.Commands.SignUp;
using Microsoft.AspNetCore.Mvc;
using MovieStoreWebApi.Controllers.Base;

namespace MovieStoreWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : BaseController
    {
        [HttpPut("[action]")]
        public async Task<IActionResult> SignInAsync([FromBody] SignInCommand signInCommand)
        {
            return Ok(await Mediator.Send(signInCommand, HttpContext.RequestAborted));
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> SignUpAsync([FromBody] SignUpCommand signUpCommand)
        {
            return Ok(await Mediator.Send(signUpCommand, HttpContext.RequestAborted));
        }

        [HttpPut("[action]")]
        public async Task<IActionResult> RefreshTokenSignInAsync([FromBody] RefreshTokenSignInCommand refreshTokenSignInCommand)
        {
            return Ok(await Mediator.Send(refreshTokenSignInCommand));
        }
    }
}