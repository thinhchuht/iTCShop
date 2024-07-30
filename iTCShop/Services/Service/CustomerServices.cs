namespace iTCShop.Services.Service
{
    public class CustomerServices(IBaseDbServices baseDbServices, iTCShopDbContext iTCShopDbContext) : ICustomerServices
    {
        public async Task<ResponseModel> AddCustomer(Customer customer)
        {
           try
            {
                await baseDbServices.AddAsync(customer);
                return ResponseModel.SuccessResponse();
            }
            catch (Exception ex)
            {
                return ResponseModel.FailureResponse(ex.ToString());
            }
        }

        public async Task<Customer> CheckCustomerAccount(string userName, string password)
        {
           return await iTCShopDbContext.Customers.SingleOrDefaultAsync(c => c.UserName.Equals(userName) && c.Password.Equals(password));
        }

        public async Task<List<Customer>> GetAll()
        {
            return await iTCShopDbContext.Customers.Include(p=> p.Auth).ToListAsync();
        }

        public async Task<Customer> GetCustomerById(string id)
        {
            return await baseDbServices.GetById<Customer>(id);
        }

        public  ResponseModel UpdateCustomer(Customer customer)
        {
            try
            {
                iTCShopDbContext.Update(customer);
                iTCShopDbContext.SaveChanges();
                return ResponseModel.SuccessResponse();
            }
            catch (Exception ex)
            {
                return ResponseModel.FailureResponse(ex.ToString());
            }
        }
    }
}
