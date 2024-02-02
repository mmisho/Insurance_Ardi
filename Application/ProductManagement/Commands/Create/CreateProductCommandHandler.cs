using Domain.ProductManagement;
using Domain.ProductManagement.Repository;
using Domain.Shared;
using MediatR;

namespace Application.ProductManagement.Commands.Create
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand>
    {
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateProductCommandHandler(
            IProductRepository productRepository,
            IUnitOfWork unitOfWork)
        {
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = new Product(request.Name, request.Description);

            await _productRepository.InsertAsync(product);
            await _unitOfWork.SaveAsync();

            return Unit.Value;
        }
    }
}
