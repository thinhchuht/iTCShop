namespace iTCShop.Services.Service
{
    public class BaseDbServices(iTCShopDbContext iTCShopDbContext) : IBaseDbServices
    {

        public Task<List<T>> GetAll<T>() where T : class
        {
           return iTCShopDbContext.Set<T>().ToListAsync();
        }

        public async Task<T> GetById<T>(string id) where T : class
        {
           return await iTCShopDbContext.Set<T>().FindAsync(id);
        }
        public async Task AddAsync<T>(T entity) where T : class
        {
            await iTCShopDbContext.Set<T>().AddAsync(entity);
            await iTCShopDbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync<T>(string id) where T : class
        {
            var entity = await GetById<T>(id);
            iTCShopDbContext.Remove<T>(entity);
            await iTCShopDbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync<T>(T newEntity,T entity) where T : class
        {
            var entry = iTCShopDbContext.Entry(entity);
            var newEntry = iTCShopDbContext.Entry(newEntity);
            foreach (var property in entry.Properties)
            {
                if (property.Metadata.IsPrimaryKey())
                {
                    continue;
                }

                var newValue = newEntry.Property(property.Metadata.Name).CurrentValue;

                property.CurrentValue = newValue;
            }
            await iTCShopDbContext.SaveChangesAsync();
        }
    }
}
