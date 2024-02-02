using MediatR;

namespace Application.ProductManagement.Queries.GetProducts
{
    public class GetProductsQueryRequest : IRequest<GetProductsQueryResponse>
    {
    }
}
