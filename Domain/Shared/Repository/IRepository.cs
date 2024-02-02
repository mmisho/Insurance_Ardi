namespace Domain.Shared.Repository
{
    public interface IRepository<TAggregateRoot>
    {
        Task<IEnumerable<TAggregateRoot>> GetAllAsync();

        Task<TAggregateRoot?> OfIdAsync(Guid id);

        Task InsertAsync(TAggregateRoot aggregateRoot);

        void Update(TAggregateRoot aggregateRoot);

        void Delete(TAggregateRoot aggregateRoot);
    }
}
