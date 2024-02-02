using Domain.Shared;

namespace Domain.ProductManagement
{
    public class Product : BaseEntity<Guid>
    {
        public Product()
        {
            Name = string.Empty;
        }

        public Product(string name, string? description)
        {
            ValidateProduct(name);

            Name = name;
            Description = description;
        }

        public void ChangeDetails(string name, string? description)
        {
            ValidateProduct(name);

            Name = name;
            Description = description;
        }

        public override Guid Id { get; set; }
        public string Name { get; private set; }
        public string? Description { get; private set; }

        private static void ValidateProduct(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(nameof(name));
            }
        }
    }
}
