using MediatR;

namespace Application.PolicyManagement.Commands.Delete
{
    public record DeletePolicyCommand(Guid Id) : IRequest;
}
