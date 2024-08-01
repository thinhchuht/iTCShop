namespace iTCShop.Services.Interfaces
{
    public interface ICartDetailsServices
    {
        Task<ResponseModel> CreateCart(string id);
        Task<CartDetails> GetAllByCartId(string id);
        Task<CartDetails> GetById(string id);
        Task<CartDetails> GetCartDetailByProductTypeId(string productTypeId, string cartId);
        Task<ResponseModel> AddCartDetail(string cartId, string productTypeId);
        Task<ResponseModel> UpdateDropQuantity (string id);    
        Task<ResponseModel> DeleteCartDetail(string id);
        Task<ResponseModel> DeleteAllCartDetail(string id);
    }
}
