using Application.PolicyManagement.Dtos;

namespace Application.CustomerManagement.Dtos
{
    public class CustomerDtoModel
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PersonalNumber { get; set; } = string.Empty;
        public DateTimeOffset DateOfBirthUtc { get; set; }
        public IEnumerable<PolicyDtoModel>? Policies { get; set; }
    }
}
