using Shop.Domain.Entity;
using Shop.Domain.Response;
using Shop.Domain.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Service.Interfaces
{
    public interface IProductService
    {
        /*Task<BaseResponse<ClaimsIdentity>> AddToCart(RegisterViewModel model);*/

        Task<IBaseResponse<List<Product>>> GetAllProducts();

        Task<IBaseResponse<Product>> GetProduct(long id);

        Task<IBaseResponse<Product>> AddProduct(ProductViewModel model);

        Task<IBaseResponse<Product>> EditProduct(ProductViewModel model, long id);

        Task<IBaseResponse<bool>> DeleteProduct(int id);
    }
}
