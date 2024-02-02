using Domain.ProductManagement.Repository;
using Domain.Shared;
using MediatR;

namespace Application.ProductManagement.Commands.Delete
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand>
    {
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteProductCommandHandler(
            IProductRepository productRepository,
            IUnitOfWork unitOfWork)
        {
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.OfIdAsync(request.Id);

            if (product == null)
            {
                throw new KeyNotFoundException($"{nameof(product)} was not found for Id: {request.Id}");
            }

            _productRepository.Delete(product);
            await _unitOfWork.SaveAsync();

            return Unit.Value;
        }
    }
}
