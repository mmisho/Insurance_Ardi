using Application.PolicyManagement.Dtos;
using Application.ProductManagement.Dtos;
using Domain.PolicyManagement.Repository;
using MediatR;

namespace Application.PolicyManagement.Queries.GetPolicy
{
    public class GetPolicyQueryHandler : IRequestHandler<GetPolicyQueryRequest, GetPolicyQueryResponse>
    {
        private readonly IPolicyRepository _policyRepository;

        public GetPolicyQueryHandler(
            IPolicyRepository policyRepository)
        {
            _policyRepository = policyRepository;
        }

        public async Task<GetPolicyQueryResponse> Handle(GetPolicyQueryRequest request, CancellationToken cancellationToken)
        {
            var policy = await _policyRepository.OfIdAsync(request.Id);

            if (policy == null)
            {
                throw new KeyNotFoundException($"{nameof(policy)} was not found for Id: {request.Id}");
            }

            var response = new GetPolicyQueryResponse
            {
                Policy = new PolicyDtoModel
                {
                    Id = policy.Id,
                    StartDateUtc = policy.StartDateUtc,
                    EndDateUtc = policy.EndDateUtc,
                    Premium = policy.Premium,
                    Status = policy.Status,
                    Product = policy.Product != null ? new ProductDtoModel
                    {
                        Id = policy.Product.Id,
                        Name = policy.Product.Name,
                        Description = policy.Product.Description,
                    }
                    : null,
                }
            };

            return response;
        }
    }
}
