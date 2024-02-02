using Application.CustomerManagement.Commands.Create;
using Application.CustomerManagement.Commands.Delete;
using Application.CustomerManagement.Commands.Update;
using Application.CustomerManagement.Queries.GetCustomer;
using Application.CustomerManagement.Queries.GetCustomers;
using Microsoft.AspNetCore.Mvc;

namespace Insurance.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : BaseApiController
    {
        [HttpGet]
        [ProducesResponseType(typeof(GetCustomersQueryResponse), StatusCodes.Status200OK)]
        public async Task<ActionResult<GetCustomersQueryResponse>> GetAllAsync()
        {
            var request = new GetCustomersQueryRequest();

            return Ok(await Mediator.Send(request));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(GetCustomerQueryResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<GetCustomerQueryResponse>> GetAsync(Guid id)
        {
            var request = new GetCustomerQueryRequest { Id = id };

            return Ok(await Mediator.Send(request));
        }

        [HttpPost]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult> CreateAsync([FromBody] CreateCustomerCommand command)
        {
            _ = await Mediator.Send(command);

            return StatusCode(201);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> UpdateAsync([FromRoute] Guid id, [FromBody] UpdateCustomerCommand command)
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
            var command = new DeleteCustomerCommand(id);
            _ = await Mediator.Send(command);

            return Ok();
        }
    }
}
