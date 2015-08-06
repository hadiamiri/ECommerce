using System.Data.Entity.ModelConfiguration;

namespace Shop.Models.Config
{
    public class ReviewConfig : EntityTypeConfiguration<Review>
    {
        public ReviewConfig()
        {
            ToTable("Review");
            Property(p => p.Comment).IsRequired().HasMaxLength(200);
            Property(p => p.Rating).IsRequired();
        }
    }
}