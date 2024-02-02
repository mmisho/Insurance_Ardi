using MediatR;

namespace Application.CustomerManagement.Queries.GetCustomer
{
    public class GetCustomerQueryRequest : IRequest<GetCustomerQueryResponse>
    {
        public Guid Id { get; set; }
    }
}
