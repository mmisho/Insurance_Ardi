using Domain.CustomerManagement.Enums;
using Domain.PolicyManagement;
using Domain.Shared;

namespace Domain.CustomerManagement
{
    public class Customer : BaseEntity<Guid>
    {
        public Customer()
        {
            FirstName = string.Empty;
            LastName = string.Empty;
            Email = string.Empty;
            PersonalNumber = string.Empty;
        }

        public Customer(string firstName, string lastName, string email, string personalNumber, DateTimeOffset dateOfBirth, Gender gender)
        {
            ValidateCustomer(firstName, lastName, email, personalNumber);

            FirstName = firstName;
            LastName = lastName;
            Email = email;
            PersonalNumber = personalNumber;
            DateOfBirthUtc = dateOfBirth;
            Gender = gender;
        }

        public void ChangeDetails(string firstName, string lastName, string email, string personalNumber, DateTimeOffset dateOfBirth, Gender gender)
        {
            ValidateCustomer(firstName, lastName, email, personalNumber);

            FirstName = firstName;
            LastName = lastName;
            Email = email;
            PersonalNumber = personalNumber;
            DateOfBirthUtc = dateOfBirth;
            Gender = gender;
        }

        public override Guid Id { get; set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }
        public string PersonalNumber { get; private set; }
        public DateTimeOffset DateOfBirthUtc { get; private set; }
        public Gender Gender { get; private set; }
        public List<Policy>? Policies { get; set; }

        private static void ValidateCustomer(string name, string lastName, string email, string personalNumber)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (string.IsNullOrEmpty(lastName))
            {
                throw new ArgumentNullException(nameof(lastName));
            }

            if (string.IsNullOrEmpty(email))
            {
                throw new ArgumentNullException(nameof(email));
            }

            if (string.IsNullOrEmpty(personalNumber))
            {
                throw new ArgumentNullException(nameof(personalNumber));
            }
        }
    }
}
