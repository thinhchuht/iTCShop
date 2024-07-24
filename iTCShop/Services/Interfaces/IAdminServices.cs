namespace iTCShop.Services.Interfaces
{
    public interface IAdminServices
    {
        Task<Admin> CheckAdmin(string user, string pass);
        Task<ResponseModel> AddAdmin(string userName, string password);
    }
}
