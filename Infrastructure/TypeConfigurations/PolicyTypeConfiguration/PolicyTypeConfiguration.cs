using Domain.CustomerManagement;
using Domain.PolicyManagement;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.TypeConfigurations.PolicyTypeConfiguration
{
    public class PolicyTypeConfiguration : IEntityTypeConfiguration<Policy>
    {
        public void Configure(EntityTypeBuilder<Policy> builder)
        {
            builder.ToTable($"Policies");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Premium)
                .HasColumnType("money");

            builder.HasOne(x => x.Product)
                .WithMany()
                .HasForeignKey(x => x.ProductId)
                .OnDelete(deleteBehavior: DeleteBehavior.SetNull);

            builder.HasOne<Customer>()
                .WithMany(c => c.Policies)
                .HasForeignKey(x => x.CustomerId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
