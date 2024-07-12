namespace iTCShop.Services.Interfaces
{
    public interface IBaseDbServices
    {
        Task<List<T>> GetAll<T>() where T : class;
        Task<T> GetById<T>(string id) where T: class; 
        Task AddAsync<T>(T entity) where T: class;
        Task DeleteAsync<T>(string id) where T: class;
        Task UpdateAsync<T>(T entity, string id) where T:class;
    }
}
