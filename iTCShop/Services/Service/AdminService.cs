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
                return ResponseModel.ExceptionResponse();
            }
        }

        public async Task<ResponseModel> UpdateAdmin(Admin newAdmin)
        {
            try
            {
                var admin = await baseDbServices.GetById<Admin>(newAdmin.ID);
                admin.Password = newAdmin.Password;
                iTCShopDbContext.Update(admin);
                iTCShopDbContext.SaveChanges();
                return ResponseModel.SuccessResponse();
            }
            catch
            {
                return ResponseModel.ExceptionResponse();
            }


        }
        public async Task<Admin> CheckAdmin(string user, string pass)
        {
            return await iTCShopDbContext.Admins.FirstOrDefaultAsync(a => a.UserName.Equals(user) && a.Password.Equals(pass));
        }
    }
}
