namespace iTCShop.Services.Interfaces
{
    public interface IAdminServices
    {
        Task<Admin> CheckAdmin(string user, string pass);
        Task<ResponseModel> AddAdmin(string userName, string password);
        Task<ResponseModel> UpdateAdmin(Admin newAdmin);
    }
}
