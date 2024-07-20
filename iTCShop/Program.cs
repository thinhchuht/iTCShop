using iTCShop.Data;
using iTCShop.Services.Interfaces;
using iTCShop.Services.Service;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var services = builder.Services;
services.AddControllersWithViews();
services.AddDbContext<iTCShopDbContext>(o => o.UseSqlServer(builder.Configuration.GetConnectionString("iTCDb")));
services.AddSwaggerGen();
services.AddScoped<IBaseDbServices, BaseDbServices>();
services.AddScoped<IProductsTypeServices, ProductsTypeServices>();
services.AddScoped<IProductDbServices, ProductDbServices>();
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
app.MapControllers();
app.Run();
