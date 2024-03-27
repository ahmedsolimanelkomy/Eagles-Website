using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Eagles_Website.Models
{
        public class Context : IdentityDbContext<ApplicationUser, ApplicationRole, int>
        {
            public DbSet<ApplicationUser> applicationUsers { get; set; }
            public DbSet<ApplicationRole> applicationRole { get; set; }

            public DbSet<Product> Products { get; set; }
            public DbSet<Category> Categories { get; set; }
            public DbSet<Cart> Carts { get; set; }
            public DbSet<CartItem> cartItems { get; set; }
            public DbSet<Order> orders { get; set; }
            public DbSet<OrderDetails> OrderDetails { get; set; }
            public DbSet<Payment> Payment { get; set; }

            public Context()
            {
            
            }
            public Context(DbContextOptions<Context> Options) : base(Options)
            {

            }

            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                base.OnConfiguring(optionsBuilder);
            }

            protected override void OnModelCreating(ModelBuilder builder)
            {
                base.OnModelCreating(builder);
            }
        }
    }
