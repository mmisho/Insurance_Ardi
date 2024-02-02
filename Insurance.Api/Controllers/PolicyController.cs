using Application.PolicyManagement.Commands.Create;
using Application.PolicyManagement.Commands.Delete;
using Application.PolicyManagement.Commands.Update;
using Application.PolicyManagement.Queries.GetPolicies;
using Application.PolicyManagement.Queries.GetPolicy;
using Domain.PolicyManagement.Enums;
using Microsoft.AspNetCore.Mvc;

namespace Insurance.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PolicyController : BaseApiController
    {
        [HttpGet]
        [ProducesResponseType(typeof(GetPoliciesQueryResponse), StatusCodes.Status200OK)]
        public async Task<ActionResult<GetPoliciesQueryResponse>> GetAllAsync(Status? status)
        {
            var request = new GetPoliciesQueryRequest { Status = status };

            return Ok(await Mediator.Send(request));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(GetPolicyQueryResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<GetPolicyQueryResponse>> GetAsync(Guid id)
        {
            var request = new GetPolicyQueryRequest { Id = id };

            return Ok(await Mediator.Send(request));
        }

        [HttpPost]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult> CreateAsync([FromBody] CreatePolicyCommand command)
        {
            _ = await Mediator.Send(command);

            return StatusCode(201);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> UpdateAsync([FromRoute] Guid id, [FromBody] UpdatePolicyCommand command)
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
            var command = new DeletePolicyCommand(id);
            _ = await Mediator.Send(command);

            return Ok();
        }
    }
}
