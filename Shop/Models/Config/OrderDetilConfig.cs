using System.Data.Entity.ModelConfiguration;

namespace Shop.Models.Config
{
    public class OrderDetilConfig : EntityTypeConfiguration<OrderDetail>
    {
        public OrderDetilConfig()
        {
            Property(x => x.ProductName).IsRequired().HasMaxLength(50);
        }
    }
}