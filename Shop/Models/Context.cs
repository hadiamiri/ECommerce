using System;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using Shop.Infrastructure;
using Shop.Models.Config;

namespace Shop.Models
{
    public class Context : DbContext
    {
        public Context()
            : base("ShopDB")
        { }

        public DbSet<Review> Reviews { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        //public DbSet<About> Abouts { get; set; }
        public DbSet<Content> Contents { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new CategoryConfig());
            modelBuilder.Configurations.Add(new OrderConfig());
            modelBuilder.Configurations.Add(new OrderDetilConfig());
            modelBuilder.Configurations.Add(new ProductConfig());
            modelBuilder.Configurations.Add(new ReviewConfig());
            modelBuilder.Configurations.Add(new UserConfig());

            base.OnModelCreating(modelBuilder);
        }
    }

    public class MyDbInitializer : DropCreateDatabaseIfModelChanges<Context>
    {
        protected override void Seed(Context context)
        {
            try
            {
                context.Categories.Add(new Category { Name = "ورزشی" });
                context.Categories.Add(new Category { Name = "خوراکی" });
                context.Categories.Add(new Category { Name = "فرهنگی" });
                context.Categories.Add(new Category { Name = "پزشکی" });
                context.Categories.Add(new Category { Name = "الکترونیکی" });

                context.Roles.Add(new Role
                {
                    Description = "آدمین سایت",
                    Name = "admin"
                });

                context.Roles.Add(new Role
                {
                    Description = "کاربر عادی سایت",
                    Name = "customer"
                });
                context.SaveChanges();

                var admin = new User
                {
                    FirstName = "hadi",
                    LastName = "amiri",
                    UserName = "admin",
                    Password = Encryption.EncryptingPassword("secret"),
                    CreateDate = DateTime.Now,
                    Email = "email@site.com",
                    Mobile = "222222",
                    Address = "Iran",
                    Role = context.Roles.FirstOrDefault(x => x.Name == "admin")
                };

                context.Users.Add(admin);
                context.SaveChanges();
                base.Seed(context);

                context.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
        }
    }
}