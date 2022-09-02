using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.DAL.Interface
{
    public interface IBaseRepository<T>
    {
        Task<bool> Create(T entity);

        Task<bool> Edit(T entity);

        Task<bool> Delete(T entity);

        Task<T> Get(int id);

        Task<List<T>> GetAllList();
    }
}
