using Domain.CustomerManagement;
using Domain.PolicyManagement.Enums;
using Domain.ProductManagement;
using Domain.Shared;

namespace Domain.PolicyManagement
{
    public class Policy : BaseEntity<Guid>
    {
        public Policy()
        {
        }

        public Policy(decimal premium, Customer customer, Product product, DateTimeOffset startDate, DateTimeOffset endDate)
        {
            ValidatePolicy(premium, customer, product, startDate, endDate);

            Premium = premium;
            CustomerId = customer.Id;
            ProductId = product.Id;
            StartDateUtc = startDate;
            EndDateUtc = endDate;
            Status = Status.Active;
        }

        public void ChangeDetails(decimal premium, Customer customer, Product product, Status status, DateTimeOffset startDate, DateTimeOffset endDate)
        {
            ValidatePolicy(premium, customer, product, startDate, endDate);

            Premium = premium;
            CustomerId = customer.Id;
            ProductId = product.Id;
            StartDateUtc = startDate;
            EndDateUtc = endDate;
            Status = status;
        }

        public override Guid Id { get; set; }
        public decimal Premium { get; private set; }
        public DateTimeOffset StartDateUtc { get; private set; }
        public DateTimeOffset EndDateUtc { get; private set; }
        public Status Status { get; private set; }
        public Guid? CustomerId { get; private set; }
        public Product? Product { get; set; }
        public Guid? ProductId { get; private set; }

        private static void ValidatePolicy(decimal premium, Customer customer, Product product, DateTimeOffset startDate, DateTimeOffset endDate)
        {
            if (premium < 0)
            {
                throw new ArgumentException($"{nameof(premium)} cannot be less than zero.");
            }

            if (startDate.Date < DateTimeOffset.UtcNow.Date)
            {
                throw new ArgumentException($"{startDate} must be greater or equal to today's date");
            }

            if (endDate < startDate)
            {
                throw new ArgumentException($"{endDate} must be greater than {startDate}'s date");
            }

            if (customer == null)
            {
                throw new ArgumentNullException(nameof(customer));
            }

            if (product == null)
            {
                throw new ArgumentNullException(nameof(product));
            }
        }
    }
}
