namespace iTCShop.Services.Interfaces
{
    public interface ICartDetailsServices
    {
        //Task<ResponseModel> CreateCart(string id);
        Task<List<CartDetails>> GetAllByCartId(string customersID);
        Task<CartDetails> GetById(string id);
        //Task<CartDetails> GetCartDetailByProductTypeId(string productTypeId, string cartId);
        Task<ResponseModel> AddCartDetail(string productTypeId, string cartId);
        Task<ResponseModel> UpdateDropQuantity (string id);    
        Task<ResponseModel> DeleteCartDetail(string id);
        Task<ResponseModel> DeleteAllCartDetail(string id);
    }
}
