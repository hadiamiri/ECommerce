using System.Data.Entity.ModelConfiguration;

namespace Shop.Models.Config
{
    public class CategoryConfig : EntityTypeConfiguration<Category>
    {
        public CategoryConfig()
        {
            ToTable("Category");
            Property(x => x.Name).IsRequired().HasMaxLength(50);
        }
    }
}