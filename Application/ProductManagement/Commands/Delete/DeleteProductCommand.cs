using MediatR;

namespace Application.ProductManagement.Commands.Delete
{
    public record DeleteProductCommand(Guid Id) : IRequest;
}
