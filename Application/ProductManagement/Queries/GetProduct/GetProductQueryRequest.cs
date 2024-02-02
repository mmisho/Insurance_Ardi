using MediatR;

namespace Application.ProductManagement.Queries.GetProduct
{
    public class GetProductQueryRequest : IRequest<GetProductQueryResponse> 
    {
        public Guid Id { get; set; }
    }
}
