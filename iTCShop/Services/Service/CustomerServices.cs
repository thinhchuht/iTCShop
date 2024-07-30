namespace iTCShop.Services.Service
{
    public class CustomerServices(IBaseDbServices baseDbServices, iTCShopDbContext iTCShopDbContext) : ICustomerServices
    {
        public async Task<ResponseModel> AddCustomer(Customer customer)
        {
           try
            {
                var customers = await GetAll();
                if (customer.DateOfBirth > DateTime.Now) return ResponseModel.FailureResponse("That not your bithday!");
                foreach (var cus in customers)
                {
                    if(customer.Email == cus.Email) return ResponseModel.FailureResponse("This Email is already being used !");
                    if(customer.UserName == cus.UserName) return ResponseModel.FailureResponse("This UserName is already being used !");
                }
                await baseDbServices.AddAsync(customer);
                return ResponseModel.SuccessResponse();
            }
            catch
            {
                return ResponseModel.ExceptionResponse();
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
