using iTCShop.Data;
using iTCShop.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace iTCShop.Services.Service
{
    public class BaseDbServices(iTCShopDbContext iTCShopDbContext) : IBaseDbServices
    {
        public async Task AddAsync<T>(T entity) where T : class
        {
           await iTCShopDbContext.Set<T>().AddAsync(entity);
           await iTCShopDbContext.SaveChangesAsync();
        }

        public Task<List<T>> GetAll<T>() where T : class
        {
           return iTCShopDbContext.Set<T>().ToListAsync();
        }

        public Task<T> GetById<T>(string id) where T : class
        {
            throw new NotImplementedException();
        }
    }
}
