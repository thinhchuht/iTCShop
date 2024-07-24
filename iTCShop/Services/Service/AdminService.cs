
namespace iTCShop.Services.Service
{
    public class AdminService(iTCShopDbContext iTCShopDbContext) : IAdminServices
    {
        public async Task<Admin> CheckAdmin(string user, string pass)
        {
            return await iTCShopDbContext.Admins.FirstOrDefaultAsync(a => a.UserName.Equals(user) && a.Password.Equals(pass));
        }
    }
}
