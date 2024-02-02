using Domain.CustomerManagement.Enums;
using MediatR;

namespace Application.CustomerManagement.Commands.Create
{
    public record CreateCustomerCommand(string FirstName, string LastName, string Email, string PersonalNumber, DateTimeOffset DateOfBirth, Gender Gender) : IRequest;
}
