using System.Data.Entity.ModelConfiguration;

namespace Shop.Models.Config
{
    public class UserConfig : EntityTypeConfiguration<User>
    {
        public UserConfig()
        {
            ToTable("User");
            Property(p => p.FirstName).HasMaxLength(50);
            Property(p => p.LastName).HasMaxLength(50);
            Property(p => p.UserName).HasMaxLength(50);
            Property(p => p.Password).HasMaxLength(200);
            Property(p => p.Email).HasMaxLength(50);
            Property(p => p.Mobile).HasMaxLength(50);
            Property(p => p.Address).HasMaxLength(500);

            HasRequired(x => x.Role).WithMany(x => x.Users).WillCascadeOnDelete(true);
            HasMany(x => x.Reviews).WithRequired(x => x.User).WillCascadeOnDelete(true);
            HasMany(x => x.Orders).WithRequired(x => x.User).WillCascadeOnDelete(false);
        }
    }
}