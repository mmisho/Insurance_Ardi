using Application.PolicyManagement.Dtos;
using Application.ProductManagement.Dtos;
using Domain.PolicyManagement.Repository;
using MediatR;

namespace Application.PolicyManagement.Queries.GetPolicies
{
    public class GetPoliciesQueryHandler : IRequestHandler<GetPoliciesQueryRequest, GetPoliciesQueryResponse>
    {
        private readonly IPolicyRepository _policyRepository;

        public GetPoliciesQueryHandler(IPolicyRepository policyRepository)
        {
            _policyRepository = policyRepository;
        }

        public async Task<GetPoliciesQueryResponse> Handle(GetPoliciesQueryRequest request, CancellationToken cancellationToken)
        {
            var policies = await _policyRepository.GetAllAsync(request.Status);

            var response = new GetPoliciesQueryResponse
            {
                Policies = policies.Select(x => new PolicyDtoModel
                {
                    Id = x.Id,
                    Premium = x.Premium,
                    StartDateUtc = x.StartDateUtc,
                    EndDateUtc = x.EndDateUtc,
                    Status = x.Status,
                    Product = x.Product != null ? new ProductDtoModel
                    {
                        Id = x.Product.Id,
                        Name = x.Product.Name,
                        Description = x.Product.Description,
                    }
                    : null
                })
            };

            return response;
        }
    }
}
