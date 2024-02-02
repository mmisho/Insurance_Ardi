using Application.ProductManagement.Dtos;
using Domain.ProductManagement.Repository;
using MediatR;

namespace Application.ProductManagement.Queries.GetProducts
{
    public class GetProductsQueryHandler : IRequestHandler<GetProductsQueryRequest, GetProductsQueryResponse>
    {
        private readonly IProductRepository _productRepository;

        public GetProductsQueryHandler(
            IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<GetProductsQueryResponse> Handle(GetProductsQueryRequest request, CancellationToken cancellationToken)
        {
            var products = await _productRepository.GetAllAsync();

            var response = new GetProductsQueryResponse
            {
                Products = products.Select(x => new ProductDtoModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                })
            };

            return response;
        }
    }
}
