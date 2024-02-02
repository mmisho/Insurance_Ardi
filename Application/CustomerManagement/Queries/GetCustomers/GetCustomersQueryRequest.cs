using MediatR;

namespace Application.CustomerManagement.Queries.GetCustomers
{
    public class GetCustomersQueryRequest : IRequest<GetCustomersQueryResponse>
    {
    }
}
