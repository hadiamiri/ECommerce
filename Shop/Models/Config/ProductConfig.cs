using System.Data.Entity.ModelConfiguration;

namespace Shop.Models.Config
{
    public class ProductConfig : EntityTypeConfiguration<Product>
    {
        public ProductConfig()
        {
            ToTable("Product");
            Property(x => x.Name).IsRequired().HasMaxLength(50);
            Property(x => x.Description).IsRequired().HasMaxLength(100);

            HasMany(x => x.Reviews).WithRequired(x => x.Product).WillCascadeOnDelete(true);
        }
    }
}