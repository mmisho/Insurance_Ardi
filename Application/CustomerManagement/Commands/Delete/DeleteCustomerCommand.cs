using MediatR;

namespace Application.CustomerManagement.Commands.Delete
{
    public record DeleteCustomerCommand(Guid CustomerId) : IRequest;
}
