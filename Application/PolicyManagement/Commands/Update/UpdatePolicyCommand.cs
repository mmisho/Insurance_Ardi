using Domain.PolicyManagement.Enums;
using MediatR;

namespace Application.PolicyManagement.Commands.Update
{
    public class UpdatePolicyCommand : IRequest
    {
        public Guid Id { get; private set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }
        public decimal Premium { get;  set; }
        public Status Status { get; set; }
        public Guid CustomerId { get;  set; }
        public Guid ProductId { get;  set; }

        public void SetId(Guid id)
        {
            Id = id;
        }
    }
}
