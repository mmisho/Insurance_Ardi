using Domain.PolicyManagement.Enums;
using Domain.Shared.Repository;
namespace Domain.PolicyManagement.Repository
{
    public interface IPolicyRepository : IRepository<Policy>
    {
        Task<IEnumerable<Policy>> GetAllAsync(Status? status = null);
    }
}
