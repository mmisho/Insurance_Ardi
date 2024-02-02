using Application.PolicyManagement.Dtos;

namespace Application.PolicyManagement.Queries.GetPolicies
{
    public class GetPoliciesQueryResponse 
    {
        public IEnumerable<PolicyDtoModel>? Policies { get; set; }
    }
}
