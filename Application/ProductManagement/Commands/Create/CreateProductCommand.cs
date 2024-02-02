using MediatR;

namespace Application.ProductManagement.Commands.Create
{
    public record CreateProductCommand(string Name, string? Description) : IRequest;
}
