using Domain.Shared.Repository;

namespace Domain.CustomerManagement.Repository
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        Task<Customer?> OfIdWithNavAsync(Guid id);
    }
}
