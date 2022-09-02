using Shop.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.DAL.Interface
{
    public interface IProductRepository : IBaseRepository<Product>
    {
    }
}
