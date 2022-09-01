using Microsoft.EntityFrameworkCore;
using Shop_DAL.Data;
using Shop_DAL.Interface;
using Shop_Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop_DAL.Repository
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

        public async Task<User> Get(int id)
        {
            return await _dataContext.Users.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<User>> GetAll()
        {
            return await _dataContext.Users.ToListAsync();
        }

        public async Task<bool> Delete(User entity)
        {
            _dataContext.Users.Remove(entity);
            await _dataContext.SaveChangesAsync();
            return true;
        }


    }
}
