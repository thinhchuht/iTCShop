namespace iTCShop.Services.Interfaces
{
    public interface ICustomerServices
    {
        Task<Customer> GetCustomerById(string id);
        Task<List<Customer>> GetAll();
        Task<ResponseModel> AddCustomer(Customer customer);
        Task<Customer> CheckCustomerAccount(string userName, string password);
        ResponseModel UpdateCustomer(Customer customer);
    }
}
