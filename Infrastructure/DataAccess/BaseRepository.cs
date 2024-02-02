using Dapper;
using Domain.Shared.Repository;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DataAccess
{
    public abstract class BaseRepository<TContext, TDapperContext, TAggregateRoot> : IRepository<TAggregateRoot>
        where TContext : EFDbContext
        where TDapperContext : DapperDbContext
        where TAggregateRoot : class
    {
        protected readonly TContext _context;
        protected readonly TDapperContext _dapperContext;

        public BaseRepository(TContext context, TDapperContext dapperContext)
        {
            _context = context;
            _dapperContext = dapperContext;
        }

        public virtual async Task<IEnumerable<TAggregateRoot>> GetAllAsync()
        {
            var query = $"SELECT * FROM {typeof(TAggregateRoot).Name}s";

            using (var connection = _dapperContext.CreateConnection())
            {
                return await connection.QueryAsync<TAggregateRoot>(query);
            }
        }

        public virtual async Task<TAggregateRoot?> OfIdAsync(Guid Id)
        {
            var query = $"SELECT * FROM {typeof(TAggregateRoot).Name}s WHERE Id = @Id";

            using (var connection = _dapperContext.CreateConnection())
            {
                return await connection.QueryFirstOrDefaultAsync<TAggregateRoot>(query, new { Id });
            }
        }

        public async Task InsertAsync(TAggregateRoot aggregateRoot)
        {
            await _context.Set<TAggregateRoot>().AddAsync(aggregateRoot);
        }

        public void Update(TAggregateRoot aggregateRoot)
        {
            _context.Entry(aggregateRoot).State = EntityState.Modified;
        }

        public void Delete(TAggregateRoot aggregateRoot)
        {
            _context.Set<TAggregateRoot>().Remove(aggregateRoot);
        }
    }
}
