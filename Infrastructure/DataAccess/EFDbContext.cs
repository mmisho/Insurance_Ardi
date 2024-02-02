#nullable disable
using Domain.CustomerManagement;
using Domain.PolicyManagement;
using Domain.ProductManagement;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Infrastructure.DataAccess
{
    public class EFDbContext : DbContext
    {
        public EFDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Policy> Policies { get; set; }
        public DbSet<Customer> Customer { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetAssembly(typeof(EFDbContext)));
            base.OnModelCreating(modelBuilder);
        }
    }
}
