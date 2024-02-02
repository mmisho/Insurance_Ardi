using Dapper;
using Domain.PolicyManagement;
using Domain.PolicyManagement.Enums;
using Domain.PolicyManagement.Repository;
using Domain.ProductManagement;
using Infrastructure.DataAccess;

namespace Infrastructure.Repositories.PolicyManagement
{
    public class PolicyRepository : BaseRepository<EFDbContext, DapperDbContext, Policy>, IPolicyRepository
    {
        private const string ConstQuery = $@" SELECT Policies.*,  Products.* 
                                              FROM 
                                              Policies
                                              LEFT JOIN 
                                              Products ON Policies.ProductId = Products.Id
                                              WHERE 1=1";
        public PolicyRepository(EFDbContext eFDbContext, DapperDbContext dapperDbContext) : base(eFDbContext, dapperDbContext)
        {
        }

        public async Task<IEnumerable<Policy>> GetAllAsync(Status? status = null)
        {
            var query = ConstQuery;

            if (status.HasValue)
            {
                query += " AND Status = @Status";
            }

            using (var connection = _dapperContext.CreateConnection())
            {
                var results = await connection.QueryAsync<Policy, Product, Policy>(query, (policy, product) =>
            {
                policy.Product = product;
                return policy;
            },
                new { Status = status },
                splitOn: "Id"
            );

                return results;
            }
        }

        public new async Task<Policy?> OfIdAsync(Guid id)
        {
            var query = ConstQuery;
            query += " AND Policies.Id = @Id";

            using (var connection = _dapperContext.CreateConnection())
            {
                var result = await connection.QueryAsync<Policy, Product, Policy>(query, (policy, product) =>
                {
                    policy.Product = product;
                    return policy;
                },
                new { Id = id },
                splitOn: "Id"
            );

                return result.FirstOrDefault();
            }
        }
    }
}
