using Application.ProductManagement.Commands.Create;
using Application.ProductManagement.Commands.Delete;
using Application.ProductManagement.Commands.Update;
using Application.ProductManagement.Queries.GetProduct;
using Application.ProductManagement.Queries.GetProducts;
using Microsoft.AspNetCore.Mvc;

namespace Insurance.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : BaseApiController
    {
        [HttpGet]
        [ProducesResponseType(typeof(GetProductsQueryResponse), StatusCodes.Status200OK)]
        public async Task<ActionResult<GetProductsQueryResponse>> GetAllAsync()
        {
            var request = new GetProductsQueryRequest();

            return Ok(await Mediator.Send(request));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(GetProductQueryResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<GetProductQueryResponse>> GetAsync(Guid id)
        {
            var request = new GetProductQueryRequest { Id = id };

            return Ok(await Mediator.Send(request));
        }

        [HttpPost]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult> CreateAsync([FromBody] CreateProductCommand command)
        {
            _ = await Mediator.Send(command);

            return StatusCode(201);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> UpdateAsync([FromRoute] Guid id, [FromBody] UpdateProductCommand command)
        {
            command.SetId(id);
            _ = await Mediator.Send(command);

            return Ok();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteAsync(Guid id)
        {
            var command = new DeleteProductCommand(id);
            _ = await Mediator.Send(command);

            return Ok();
        }
    }
}
