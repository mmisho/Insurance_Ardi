using Domain.ProductManagement.Repository;
using Domain.Shared;
using MediatR;

namespace Application.ProductManagement.Commands.Update
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand>
    {
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateProductCommandHandler(
            IProductRepository productRepository,
            IUnitOfWork unitOfWork)
        {
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.OfIdAsync(request.Id);

            if (product == null)
            {
                throw new KeyNotFoundException($"{nameof(product)} was not found for Id: {request.Id}");
            }

            product.ChangeDetails(request.Name, request.Description);

            _productRepository.Update(product);
            await _unitOfWork.SaveAsync();

            return Unit.Value;
        }
    }
}
