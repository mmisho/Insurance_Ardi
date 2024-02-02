using Application.CustomerManagement.Dtos;
using Application.PolicyManagement.Dtos;
using Application.ProductManagement.Dtos;
using Domain.CustomerManagement.Repository;
using MediatR;

namespace Application.CustomerManagement.Queries.GetCustomers
{
    public class GetCustomersQueryHandler : IRequestHandler<GetCustomersQueryRequest, GetCustomersQueryResponse>
    {
        private readonly ICustomerRepository _customerRepository;

        public GetCustomersQueryHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<GetCustomersQueryResponse> Handle(GetCustomersQueryRequest request, CancellationToken cancellationToken)
        {
            var customer = await _customerRepository.GetAllAsync();

            var response = new GetCustomersQueryResponse
            {
                Customers = customer.Select(x => new CustomerDtoModel
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Email = x.Email,
                    PersonalNumber = x.PersonalNumber,
                    DateOfBirthUtc = x.DateOfBirthUtc,
                    Policies = x.Policies?.Select(x => new PolicyDtoModel
                    {
                        Id = x.Id,
                        StartDateUtc = x.StartDateUtc,
                        EndDateUtc = x.EndDateUtc,
                        Premium = x.Premium,
                        Status = x.Status,
                        Product = x.Product != null ? new ProductDtoModel
                        {
                            Id = x.Product.Id,
                            Name = x.Product.Name,
                            Description = x.Product.Description,
                        }
                        : null,
                    })
                })
            };

            return response;
        }
    }
}
