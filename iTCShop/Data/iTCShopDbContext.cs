using iTCShop.Models;
using Microsoft.EntityFrameworkCore;

namespace iTCShop.Data
{
    public class iTCShopDbContext : DbContext
    {
        public iTCShopDbContext(DbContextOptions<iTCShopDbContext> options) : base(options) { }
        public DbSet<AuthorizeUser> AuthorizeUsers { get; set; }
        public DbSet<Customer>      Customers      { get; set; }
        public DbSet<Admin>         Admins         { get; set; }    
        public DbSet<Product>       Products       { get; set; }
        public DbSet<Order>         Orders         { get; set; }
        public DbSet <OrderDetail>  OrderDetails   { get; set; }
        public DbSet<Inventory> Inventories { get; set; }
        public DbSet<StockIn> StockIns { get; set; }
        public DbSet<StockOut>    StockOuts { get; set;}
        public DbSet<Supplier>  Suppliers { get; set; }
    }
}
