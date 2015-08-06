using System.Data.Entity.ModelConfiguration;

namespace Shop.Models.Config
{
    public class OrderConfig : EntityTypeConfiguration<Order>
    {
        public OrderConfig()
        {
            Property(x => x.Address).IsRequired().HasMaxLength(400);
        }
    }
}