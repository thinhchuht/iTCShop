﻿namespace iTCShop.Services.Interfaces
{
    public interface IProductDbServices
    {
        Task<List<Product>> GetAllProducts();
        Task<List<Product>> GetProductsByProductType(string productTypeId);
        Task<Product>       GetProductByImei(string imei);
        Task<ResponseModel> AddProduct(ProductRequest productRequest);
        Task<ResponseModel> UpdateProduct(ProductRequest productRequest);
        Task<ResponseModel> DeleteProduct(string imei);
        Task<ResponseModel> IsAvailableCheck(string productTypeId);
        Task<ResponseModel> DeleteProductByProductTypeID(string productTypeId);


    }
}
