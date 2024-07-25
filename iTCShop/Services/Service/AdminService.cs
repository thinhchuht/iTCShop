

namespace iTCShop.Services.Service
{
    public class AdminService(iTCShopDbContext iTCShopDbContext, IBaseDbServices baseDbServices) : IAdminServices
    {
        public async Task<ResponseModel> AddAdmin(string userName, string password)
        {
            try
            {
                var admin = new Admin(userName, password);
                await baseDbServices.AddAsync(admin);
                return ResponseModel.SuccessResponse();
            }
            catch 
            {
                return ResponseModel.FailureResponse("Thất bại");
            }
        }

        public async Task<Admin> CheckAdmin(string user, string pass)
        {
            return await iTCShopDbContext.Admins.FirstOrDefaultAsync(a => a.UserName.Equals(user) && a.Password.Equals(pass));
        }
    }
}
