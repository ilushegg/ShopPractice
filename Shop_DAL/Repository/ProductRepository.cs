using Microsoft.EntityFrameworkCore;
using Shop.DAL.Interface;
using Shop.DAL.Data;
using Shop.DAL.Interface;
using Shop.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.DAL.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly DataContext _dataContext;

        public ProductRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<bool> Create(Product entity)
        {
            await _dataContext.Products.AddAsync(entity);
            await _dataContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Edit(Product entity)
        {
            _dataContext.Products.Update(entity);
            await _dataContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(Product entity)
        {
            _dataContext.Products.Remove(entity);
            await _dataContext.SaveChangesAsync();
            return true;
        }

        public async Task<Product> Get(long id)
        {
            return await _dataContext.Products.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Product>> GetAllList()
        {
            return await _dataContext.Products.ToListAsync();
        }
    }
}
