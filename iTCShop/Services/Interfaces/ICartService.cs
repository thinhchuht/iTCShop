namespace iTCShop.Services.Interfaces
{
    public interface ICartService
    {
        Task<ResponseModel> AddToCart();
    }
}
