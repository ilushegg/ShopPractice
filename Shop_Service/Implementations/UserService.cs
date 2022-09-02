using Microsoft.EntityFrameworkCore;
using Shop_DAL.Interface;
using Shop_Domain.Entity;
using Shop_Domain.Enum;
using Shop_Domain.Helper;
using Shop_Domain.Response;
using Shop_Domain.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Shop_Service.Implementations
{
    public class UserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<BaseResponse<ClaimsIdentity>> Register(RegisterViewModel model)
        {
            try
            {
                var user = await _userRepository.GetAll().FirstOrDefaultAsync(x => x.Name == model.Name);
                if(user != null)
                {
                    return new BaseResponse<ClaimsIdentity>()
                    {
                        StatusCode = StatusCode.CommonError,
                        Description = "A user with this username is already exists"
                    };
                }
                user = new User()
                {
                    Name = model.Name,
                    Role = Role.USER,
                    Password = HashPasswordHelper.HashPassowrd(model.Password)
                };
                await _userRepository.Create(user);
                return new BaseResponse<ClaimsIdentity>()
                {
                    StatusCode = StatusCode.OK,
                };
            }
            catch(Exception ex)
            {
                return new BaseResponse<ClaimsIdentity>()
                {
                    Description = "An internal error",
                    StatusCode = StatusCode.InternalServerError
                };

            }
        }
    }
}
