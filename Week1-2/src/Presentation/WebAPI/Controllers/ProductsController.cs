using Application.Features.Products.Commands.Create;
using Application.Features.Products.Commands.Delete;
using Application.Features.Products.Commands.Patch;
using Application.Features.Products.Commands.Update;
using Application.Features.Products.Dtos;
using Application.Features.Products.Queries.GetById;
using Application.Features.Products.Queries.List;
using Application.Features.Products.Queries.OData;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using WebAPI.Controllers.Base;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : BaseController
    {
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetAsync([FromRoute] GetProductByIdQuery getProductByIdQuery)
        {
            ProductGetByIdDto product = await Mediator.Send(getProductByIdQuery);
            return Ok(product);
        }

        [HttpGet]
        public async Task<IActionResult> ListAsync([FromQuery] ProductListQuery productListQuery)
        {
            #region Warnings
            /* Warning
                When we use FromQuery attribute then we can bind the collections like Array, IEnumerable etc. only with indices. So, we can't use this endpoint with swagger.

                Solutions;
                    1.) We can get the Dynamic object with [FromBody] attribute but we can't use that attribute in Get Actions.
                    2.) Parse json data from swagger into object like this;
                    -----------------------------------------------------------------------------------------------------------------------------
                    | var dynamicSort = HttpContext.Request.Query.SingleOrDefault(x => x.Key == "Dynamic.Sort" && !x.Value.Contains(null));     |
                    | if (dynamicSort.Value.Count > 0)                                                                                          |
                    | {                                                                                                                         |
                    |     List<Sort> sorts = new(dynamicSort.Value.Count);                                                                      |
                    |     foreach (var sort in dynamicSort.Value)                                                                               |
                    |         sorts.Add(JsonConvert.DeserializeObject<Sort>(sort!)!);                                                           |
                    |     if (sorts is not null)                                                                                                |
                    |         productListQuery.Dynamic.Sort = sorts.AsEnumerable<Sort>();                                                       |
                    | }                                                                                                                         |
                    -----------------------------------------------------------------------------------------------------------------------------
                    3.) Bind collections with indices like this; (We should use different API UI like Postman for do this)
                    https://localhost:7196/api/products?Dynamic.Sort[0].Field=unitPrice&Dynamic.Sort[0].Dir=asc
                    4.) We can write a custom query binder.
            */
            #endregion

            IList<ProductListDto> products = await Mediator.Send(productListQuery);
            return Ok(products);
        }

        [HttpGet("[action]")]
        [EnableQuery] // This attribute is required for use OData at this endpoint. Also, swagger ui is couldn't use for this endpoint.
        public async Task<IActionResult> ODataAsync()
        {
            ProductODataQuery productODataQuery = new();
            IQueryable<ProductODataDto> oDataProducts = await Mediator.Send(productODataQuery);
            return Ok(oDataProducts);
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync([FromBody] CreateProductCommand createProductCommand)
        {
            ProductCreatedDto productCreatedDto = await Mediator.Send(createProductCommand);
            return Ok(productCreatedDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync([FromRoute] string id, [FromBody] UpdateProductCommand updateProductCommand)
        {
            updateProductCommand.Id = id;
            ProductUpdatedDto productUpdatedDto = await Mediator.Send(updateProductCommand);
            return Ok(productUpdatedDto);
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] DeleteProductCommand deleteProductCommand)
        {
            ProductDeletedDto productDeletedDto = await Mediator.Send(deleteProductCommand);
            return Ok(productDeletedDto);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateAsync([FromRoute] string id, [FromBody] JsonPatchDocument<UpdateProductDto> productPatchDocument)
        {
            UpdateProductByPatchCommand updateProductByPatchCommand = new()
            {
                Id = id,
                ProductPatchDocument = productPatchDocument
            };
            ProductUpdatedDto productUpdatedDto = await Mediator.Send(updateProductByPatchCommand);
            return Ok(productUpdatedDto);
        }
    }
}