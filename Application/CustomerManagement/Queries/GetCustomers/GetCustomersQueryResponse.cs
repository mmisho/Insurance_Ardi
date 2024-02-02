using Application.CustomerManagement.Dtos;
namespace Application.CustomerManagement.Queries.GetCustomers
{
    public class GetCustomersQueryResponse
    {
        public IEnumerable<CustomerDtoModel>? Customers { get; set; }
    }
}
