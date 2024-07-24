﻿

using iTCShop.Models;

namespace iTCShop.Services.Service
{
    public class CustomerServices(IBaseDbServices baseDbServices, iTCShopDbContext iTCShopDbContext) : ICustomerServices
    {
        public async Task<ResponseModel> AddCustomer(Customer customer)
        {
           try
            {
                await baseDbServices.AddAsync<Customer>(customer);
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
            return await iTCShopDbContext.Customers.Include(p=> p.Auth).Include(p=>p.Orders).ToListAsync();
        }
    }
}
