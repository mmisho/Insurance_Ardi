using Domain.ProductManagement;
using Domain.ProductManagement.Repository;
using Infrastructure.DataAccess;

namespace Infrastructure.Repositories.ProductManagement
{
    public class ProductRepository : BaseRepository<EFDbContext, DapperDbContext, Product>, IProductRepository
    {
        public ProductRepository(EFDbContext eFDbContext, DapperDbContext dapperDbContext) : base(eFDbContext, dapperDbContext)
        {
        }
    }
}
