using Microsoft.EntityFrameworkCore;
using Shop.DAL.Interface;
using Shop.Domain.Entity;
using Shop.Domain.Enum;
using Shop.Domain.Helper;
using Shop.Domain.Response;
using Shop.Domain.ViewModel;
using Shop.Service.Interfaces;
using Microsoft.AspNetCore.Hosting;
using System.Security.Claims;


namespace Shop.Service.Implementations
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        

        public async Task<IBaseResponse<List<Product>>> GetAllProducts()
        {
            try
            {
                var products = await _productRepository.GetAllList();
                return new BaseResponse<List<Product>>()
                {
                    Data = products,
                    StatusCode = StatusCode.OK
                };
            }
            catch(Exception ex)
            {
                return new BaseResponse<List<Product>>()
                {
                    Description = ex.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<Product>> GetProduct(long id)
        {
            var baseResponse = new BaseResponse<Product>();
            try
            {
                var product = await _productRepository.Get(id);
                if(product == null)
                {
                    baseResponse.StatusCode = StatusCode.ProductNotFound;
                    baseResponse.Description = "Product is not found";

                    return baseResponse;
                }
                baseResponse.Data = product;
                baseResponse.StatusCode = StatusCode.OK;
                return baseResponse;
            }
            catch(Exception ex)
            {
                return new BaseResponse<Product>()
                {
                    StatusCode = StatusCode.InternalServerError,
                    Description = ex.Message
                };
            }
        }

        public async Task<IBaseResponse<Product>> AddProduct(ProductViewModel model)
        {
            try
            {
                var product = new Product()
                {
                    Name = model.Name,
                    Description = model.Description,
                    Price = model.Price,
                    CreateDate = model.CreateDate,
                };

                if (model.Picture != null)
                {
                    string uniqueFileName = SaveFile(model);

                    product.Picture = uniqueFileName;
                }

                await _productRepository.Create(product);
                return new BaseResponse<Product>() 
                {
                    StatusCode = StatusCode.OK
                };
            }
            catch(Exception ex)
            {
                return new BaseResponse<Product>()
                {
                    StatusCode = StatusCode.InternalServerError,
                    Description = ex.Message
                };
            }
        }

        public async Task<IBaseResponse<Product>> EditProduct(ProductViewModel model, long id)
        {
            try
            {
                var product = await _productRepository.Get(id);

                if (model.Picture != null)
                {
                    string uniqueFileName = SaveFile(model);
                    product.Picture = uniqueFileName;
                }
                product.Name = model.Name;
                product.Description = model.Description;
                product.Price = model.Price;

                await _productRepository.Edit(product);
                return new BaseResponse<Product>()
                {
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Product>()
                {
                    StatusCode = StatusCode.InternalServerError,
                    Description = ex.Message
                };
            }
        }


        public async Task<IBaseResponse<bool>> DeleteProduct(int id)
        {
            var baseResponse = new BaseResponse<bool>();
            try
            {
                var product = await _productRepository.Get(id);
                if(product == null)
                {
                    baseResponse.StatusCode = StatusCode.ProductNotFound;
                    baseResponse.Description = "Product is not found";
                    return baseResponse;
                }
                if(product.Picture != null)
                {
                    var uploads = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");
                    var filePath = Path.Combine(uploads, product.Picture);
                    File.Delete(filePath);
                }

                await _productRepository.Delete(product);
                baseResponse.StatusCode = StatusCode.OK;
                return baseResponse;
            }
            catch(Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    StatusCode = StatusCode.InternalServerError,
                    Description = ex.Message
                };
            }
        }
        private string SaveFile(ProductViewModel model)
        {
            var uniqueFileName = GetUniqueFileName(model.Picture.FileName);
            var uploads = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");
            var filePath = Path.Combine(uploads, uniqueFileName);

            var fStream = new FileStream(filePath, FileMode.Create);
            model.Picture.CopyTo(fStream);
            fStream.Dispose();
            return uniqueFileName;
        }

        private string GetUniqueFileName(string fileName)
        {
            fileName = Path.GetFileName(fileName);
            return Path.GetFileNameWithoutExtension(fileName)
                + "_"
                + Guid.NewGuid().ToString().Substring(0, 4)
                + Path.GetExtension(fileName);
        }

    }
}
