using MediatR;

namespace Application.PolicyManagement.Queries.GetPolicy
{
    public class GetPolicyQueryRequest : IRequest<GetPolicyQueryResponse>
    {
        public Guid Id { get; set; }
    }
}
