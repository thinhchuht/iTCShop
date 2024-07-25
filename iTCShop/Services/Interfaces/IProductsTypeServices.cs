namespace iTCShop.Services.Interfaces
{
    public interface IProductsTypeServices
    {
        Task<List<ProductType>> GetAllProductTypes();
        Task<ProductType>       GetProductTypeById(string id);
        Task<ResponseModel>     AddProductType(ProductType product);
        Task<ResponseModel>     DeleteProductType(string id);
        Task<ResponseModel>     UpdateProductType(ProductType newProductType);
    }
}
