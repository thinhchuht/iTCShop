using iTCShop.Services.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var services = builder.Services;
services.AddControllersWithViews();
services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(20);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
services.AddDbContext<iTCShopDbContext>(o => o.UseSqlServer(builder.Configuration.GetConnectionString("iTCDb")));
services.AddSwaggerGen();
services.AddScoped<IBaseDbServices, BaseDbServices>();
services.AddScoped<IProductsTypeServices, ProductsTypeServices>();
services.AddScoped<IProductDbServices, ProductDbServices>();
services.AddScoped<ICustomerServices, CustomerServices>();
services.AddScoped<IAdminServices, AdminService>();
services.AddHttpContextAccessor();
var app = builder.Build();

//Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
    
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();


app.UseAuthorization();
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=HomePage}/{id?}");

app.Run();
