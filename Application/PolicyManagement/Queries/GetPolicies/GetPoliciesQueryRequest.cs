using Domain.PolicyManagement.Enums;
using MediatR;

namespace Application.PolicyManagement.Queries.GetPolicies
{
    public class GetPoliciesQueryRequest : IRequest<GetPoliciesQueryResponse>   
    {
        public Status? Status { get; set; }
    }
}
