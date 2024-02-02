using MediatR;

namespace Application.ProductManagement.Commands.Update
{
    public class UpdateProductCommand : IRequest
    {
        public Guid Id { get; private set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; } 

        public void SetId(Guid id)
        {
            Id = id;
        }
    }
}
