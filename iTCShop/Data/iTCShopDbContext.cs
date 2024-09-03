
namespace iTCShop.Data
{
    public class iTCShopDbContext : DbContext
    {
        public iTCShopDbContext(DbContextOptions<iTCShopDbContext> options) : base(options) { }

        public DbSet<AuthorizeUser> AuthorizeUsers { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        //public DbSet<Cart> Carts { get; set; }
        public DbSet<CartDetails> CartDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductType>()
                 .HasKey(p => p.ID);

            modelBuilder.Entity<Product>()
                .HasKey(p => p.IMEI);
            modelBuilder.Entity<Product>()
                .Property(o => o.Status)
                .HasConversion<int>();

            modelBuilder.Entity<Customer>()
                .HasIndex(c => c.Email).IsUnique();

            modelBuilder.Entity<Customer>()
            .HasIndex(c => c.UserName).IsUnique();


            modelBuilder.Entity<Admin>()
                .HasIndex(a => a.UserName).IsUnique();

            modelBuilder.Entity<Order>()
           .Property(o => o.Status)
           .HasConversion<int>();

            modelBuilder.Entity<Order>()
                .Property(o => o.PayMethod)
                .HasConversion<int>();

            modelBuilder.Entity<Customer>()
           .Property(c => c.Status)
           .HasConversion<int>();
        }
     
    }
}
