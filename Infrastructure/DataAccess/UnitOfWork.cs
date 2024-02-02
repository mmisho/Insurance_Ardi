using Domain.Shared;

namespace Infrastructure.DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly EFDbContext _dbContext;

        public UnitOfWork(EFDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task SaveAsync()
        {
            using var transaction = await _dbContext.Database.BeginTransactionAsync();

            await _dbContext.SaveChangesAsync();

            await transaction.CommitAsync();
        }
    }
}
