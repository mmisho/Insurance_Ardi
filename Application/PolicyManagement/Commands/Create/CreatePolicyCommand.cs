using MediatR;
namespace Application.PolicyManagement.Commands.Create
{
    public record CreatePolicyCommand(decimal Premium, DateTimeOffset StartDate, DateTimeOffset EndDate, Guid CustomerId, Guid ProductId) : IRequest;
}
