using Microsoft.IdentityModel.Tokens;

namespace iTCShop.Controllers.Response
{
    public class ProductTypeController(IProductsTypeServices productTypesServices, IProductDbServices productDbServices) : Controller
    {
        public async Task<IActionResult> ProductPartial()
        {
            var productTypes = await productTypesServices.GetAllProductTypes();
            return PartialView(productTypes);
        }

        public async Task<IActionResult> GetProductTypeById(string id)
        {
            try
            {
                var product = await productTypesServices.GetProductTypeById(id);
                return Ok(product);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public async Task<IActionResult> Search(string search, string sort)
        {
            var productTypes = await productTypesServices.GetAllProductTypes();

            if (!string.IsNullOrEmpty(search))
            {
                switch (sort)
                {
                    case "typeID":
                        productTypes = productTypes.Where(p => p.ID.Contains(search.Trim(), StringComparison.OrdinalIgnoreCase)).ToList();
                        TempData.Put("productTypes", productTypes);
                        return RedirectToAction("HomeAdminProductType", "Admin");
                    default:
                        productTypes = productTypes.Where(p => p.Name.Contains(search.Trim(), StringComparison.OrdinalIgnoreCase)).ToList();
                        break;
                }
            }
            switch (sort)
            {
                case "nameAZ":
                    productTypes = productTypes.OrderBy(p => p.Name).ToList();
                    break;
                case "nameZA":
                    productTypes = productTypes.OrderByDescending(p => p.Name).ToList();
                    break;
                case "priceDes":
                    productTypes = productTypes.OrderByDescending(p => p.Price).ToList();
                    break;
                case "priceAcs":
                    productTypes = productTypes.OrderBy(p => p.Price).ToList();
                    break;
            }
            TempData.Put("productTypes", productTypes);
            TempData["Search"] = search;
            TempData["Sort"] = sort;
            return RedirectToAction("HomePage", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> AddProductType(ProductTypesRequest productTypesRequest)
        {
            try
            {
                var uploadsDir = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/ProductImages");
                if (!Directory.Exists(uploadsDir))
                {
                    Directory.CreateDirectory(uploadsDir);
                }
                var filePath = Path.Combine(uploadsDir, productTypesRequest.Picture.FileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await productTypesRequest.Picture.CopyToAsync(stream);
                }
                var product = new ProductType(productTypesRequest.Name, productTypesRequest.Price, productTypesRequest.Description,
                                          productTypesRequest.Size, productTypesRequest.Battery, productTypesRequest.Memory, productTypesRequest.Color,
                                          productTypesRequest.RAM, productTypesRequest.Picture.FileName);
                var result = await productTypesServices.AddProductType(product);
                if (!result.IsSuccess()) TempData.Put("response", result);
                TempData.Keep();
                return RedirectToAction("HomeAdminProductType", "Admin");
            }
            catch 
            {
                TempData.PutResponse(ResponseModel.ExceptionResponse());
                return RedirectToAction("HomeAdminProductType", "Admin");
            }
        }

        //[HttpPost]
        //public async Task<IActionResult> ImportExcel(IFormFile importFile)
        //{
        //    var productTypes = new List<ProductType>();
        //    var strData = new StringBuilder();
        //    using (var stream = new MemoryStream())
        //    {
        //        await importFile.CopyToAsync(stream);
        //        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        //        using (var package = new ExcelPackage(stream))
        //        {
        //            var worksheet = package.Workbook.Worksheets[0];
        //            var rowCount = worksheet.Dimension.Rows;

        //            for (int row = 2; row <= rowCount; row++)
        //            {
        //                try
        //                {
        //                    var productType = new ProductType
        //                    {
        //                        Name = worksheet.Cells[row, 1].Value.ToString(),
        //                        Price = Convert.ToDecimal(worksheet.Cells[row, 2].Value),
        //                        Description = worksheet.Cells[row, 3].Value.ToString(),
        //                        Size = Convert.ToDecimal(worksheet.Cells[row, 4].Value),
        //                        Battery = Convert.ToInt32(worksheet.Cells[row, 5].Value),
        //                        Memory = Convert.ToInt32(worksheet.Cells[row, 6].Value),
        //                        Color = worksheet.Cells[row, 7].Value.ToString(),
        //                        RAM = Convert.ToInt32(worksheet.Cells[row, 8].Value),
        //                    };
        //                    productType.ID  = string.Concat(Regex.Matches(productType.Name, @"\b(\d+|\w)")) + productType.Memory + string.Concat(Regex.Matches(productType.Color, @"\b(\d+|\w)")) + "-" + DateTimeOffset.Now.ToString("ddMMyyyyHHmmssffffff");
        //                    productTypes.Add(productType);
        //                }
        //                catch
        //                {
        //                    strData.Append($"{row},");
        //                    continue;
        //                }
        //            }
        //        }
        //    }
        //    var str = new StringBuilder();
        //    foreach (var productType in productTypes)
        //    {
        //        var rs = await productTypesServices.AddProductType(productType);
        //        if (!rs.IsSuccess()) str.AppendLine($"{productType.Name} - {productType.Memory}GB - {productType.Color} can not be saved  due to: {rs.Message}");
        //    }
        //    if (str.Length > 0) TempData.PutResponse(ResponseModel.FailureResponse($"Cannot add these product types:\n{str}"));
        //    if (strData.Length > 0 && str.Length == 0) TempData.PutResponse(ResponseModel.FailureResponse($"Data ta at rows : {strData} is invalid"));
        //    if (strData.Length > 0 && str.Length > 0) TempData.PutResponse(ResponseModel.FailureResponse($"Data ta at rows : {strData} is invalid \nCannot add these product types:\n{str}"));
        //    return RedirectToAction("HomeAdminProductType", "Admin");
        //}


        [HttpPost]
        public async Task<IActionResult> DeleteProductType(string id)
        {
            try
            {
                var result = await productTypesServices.DeleteProductType(id);
                if (!result.IsSuccess()) TempData.PutResponse(result);
                return RedirectToAction("HomeAdminProductType", "Admin");
            }
            catch 
            {
                TempData.PutResponse(ResponseModel.ExceptionResponse());
                return RedirectToAction("HomeAdminProductType", "Admin");
            }
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProductType(ProductTypesRequest productTypesRequest, string id)
        {
            var product = await productDbServices.GetProductsByProductType(id);
            if(!product.IsNullOrEmpty())
            {
                TempData.PutResponse(ResponseModel.FailureResponse("This product is already on the market, you can not apply change!"));
                return RedirectToAction("HomeAdminProductType", "Admin");
            }
            var rs = await productTypesServices.DeleteProductType(id);
            if (!rs.IsSuccess())
            {
                TempData.PutResponse(rs);
                return RedirectToAction("HomeAdminProductType", "Admin");
            }
            var uploadsDir = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/ProductImages");
            if (!Directory.Exists(uploadsDir))
            {
                Directory.CreateDirectory(uploadsDir);
            }
            var filePath = Path.Combine(uploadsDir, productTypesRequest.Picture.FileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await productTypesRequest.Picture.CopyToAsync(stream);
            }
            var newProduct = new ProductType(productTypesRequest.Name, productTypesRequest.Price, productTypesRequest.Description, productTypesRequest.Size,
                                     productTypesRequest.Battery, productTypesRequest.Memory, productTypesRequest.Color, productTypesRequest.RAM, productTypesRequest.Picture.FileName);
            var result = await productTypesServices.AddProductType(newProduct);
            if (!result.IsSuccess()) TempData.PutResponse(rs);
            return RedirectToAction("HomeAdminProductType", "Admin");
        }
    }
}

