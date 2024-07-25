namespace iTCShop.Services.Interfaces
{
    public interface ICartDetailsServices
    {
        Task<List<CartDetails>> GetAllByCartId(string id);
        Task<CartDetails> GetById(string id);
        Task<CartDetails> GetCartDetailByProductTypeId(string productTypeId, string cartId);
        Task<ResponseModel> AddCartDetail(string productTypeId, string cartId);
        Task<ResponseModel> UpdateDropQuantity (string id);    
        Task<ResponseModel> DeleteCartDetail(string id);
    }
}
