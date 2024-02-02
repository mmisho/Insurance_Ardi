using Domain.CustomerManagement.Enums;
using Domain.PolicyManagement.Enums;
using MediatR;

namespace Application.CustomerManagement.Commands.Update
{
    public class UpdateCustomerCommand() : IRequest
    {
        public Guid Id { get; private set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PersonalNumber { get; set; } = string.Empty;
        public DateTimeOffset DateOfBirth { get; set; }
        public Gender Gender { get; set; }

        public void SetId(Guid id)
        {
            Id = id;
        }
    }
}
