using Application.ActionFilters;
using Application.Features.Orders.Commands.PlaceOrder;
using Application.Features.Orders.Queries.OrdersListByUserId;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieStoreWebApi.Controllers.Base;

namespace MovieStoreWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : BaseController
    {
        [HttpPost]
        [Authorize("Customer")]
        public async Task<IActionResult> PlaceOrderAsync([FromBody] PlaceOrderCommand placeOrderCommand)
        {
            return Created("", await Mediator.Send(placeOrderCommand));
        }

        [HttpGet]
        [Authorize("Admin")]
        [DynamicQuery]
        public async Task<IActionResult> GetUserOrdersAsync([FromQuery] GetOrdersListByUserIdQuery getOrdersListByUserIdQuery)
        {
            return Ok(await Mediator.Send(getOrdersListByUserIdQuery, HttpContext.RequestAborted));
        }
    }
}
