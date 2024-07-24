namespace iTCShop.Services.Interfaces
{
    public interface IProductDbServices
    {
        Task<List<Product>> GetAllProducts();
        Task<Product>       GetProductByImei(string imei);
        Task<ResponseModel> AddProduct(ProductRequest productRequest);
        Task<ResponseModel> UpdateProduct(ProductRequest productRequest);
        Task<ResponseModel> DeleteProduct(string imei);
        
    }
}
