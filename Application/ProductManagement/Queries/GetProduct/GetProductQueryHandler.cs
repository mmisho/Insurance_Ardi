using Application.ProductManagement.Dtos;
using Domain.ProductManagement.Repository;
using MediatR;

namespace Application.ProductManagement.Queries.GetProduct
{
    public class GetProductQueryHandler : IRequestHandler<GetProductQueryRequest, GetProductQueryResponse>
    {
        private readonly IProductRepository _productRepository;

        public GetProductQueryHandler(
            IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<GetProductQueryResponse> Handle(GetProductQueryRequest request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.OfIdAsync(request.Id);

            if (product == null)
            {
                throw new KeyNotFoundException($"{nameof(request)} was not found for Id: {request.Id}");
            }

            var response = new GetProductQueryResponse
            {
                Product = new ProductDtoModel
                {
                    Id = product.Id,
                    Name = product.Name,
                    Description = product.Description,
                }
            };

            return response;
        }
    }
}
