using Application.CustomerManagement.Dtos;
using Application.PolicyManagement.Dtos;
using Application.ProductManagement.Dtos;
using Domain.CustomerManagement.Repository;
using MediatR;

namespace Application.CustomerManagement.Queries.GetCustomer
{
    public class GetCustomerQueryHandler : IRequestHandler<GetCustomerQueryRequest, GetCustomerQueryResponse>
    {
        private readonly ICustomerRepository _customerRepository;

        public GetCustomerQueryHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<GetCustomerQueryResponse> Handle(GetCustomerQueryRequest request, CancellationToken cancellationToken)
        {
            var customer = await _customerRepository.OfIdWithNavAsync(request.Id);

            if (customer == null)
            {
                throw new KeyNotFoundException($"{nameof(customer)} was not found for Id: {request.Id}");
            }

            var response = new GetCustomerQueryResponse
            {
                Customer = new CustomerDtoModel
                {
                    Id = customer.Id,
                    FirstName = customer.FirstName,
                    LastName = customer.LastName,
                    Email = customer.Email,
                    PersonalNumber = customer.PersonalNumber,
                    DateOfBirthUtc = customer.DateOfBirthUtc,
                    Policies = customer.Policies?.Select(x => new PolicyDtoModel
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
                        : null
                    }),
                }
            };

            return response;
        }
    }
}
