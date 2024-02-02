using Application.ProductManagement.Dtos;

namespace Application.ProductManagement.Queries.GetProducts
{
    public class GetProductsQueryResponse
    {
        public IEnumerable<ProductDtoModel>? Products { get; set; }
    }
}
