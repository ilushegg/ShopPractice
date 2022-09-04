using Microsoft.EntityFrameworkCore;
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
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _dataContext;

        public UserRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<bool> Create(User entity)
        {
            await _dataContext.Users.AddAsync(entity);
            await _dataContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Edit(User entity)
        {
            _dataContext.Users.Update(entity);
            await _dataContext.SaveChangesAsync();
            return true;
        }

        public async Task<User> Get(long id)
        {
            return await _dataContext.Users.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<User>> GetAllList()
        {
            return await _dataContext.Users.ToListAsync();
        }

        public IQueryable<User> GetAll()
        {
            return _dataContext.Users;
        }

        public async Task<bool> Delete(User entity)
        {
            _dataContext.Users.Remove(entity);
            await _dataContext.SaveChangesAsync();
            return true;
        }


    }
}
