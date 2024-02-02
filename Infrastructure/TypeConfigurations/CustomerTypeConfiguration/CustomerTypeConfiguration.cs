using Domain.CustomerManagement;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.TypeConfigurations.CustomerTypeConfiguration
{
    public class CustomerTypeConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable($"{nameof(Customer)}s");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.FirstName)
                .IsRequired();

            builder.Property(x => x.LastName)
                .IsRequired();

            builder.Property(x => x.Email)
                .IsRequired();

            builder.HasIndex(x => x.Email)
                .IsUnique();

            builder.Property(x => x.PersonalNumber)
                .IsRequired();

            builder.HasIndex(x => x.PersonalNumber)
                .IsUnique();
        }
    }
}
