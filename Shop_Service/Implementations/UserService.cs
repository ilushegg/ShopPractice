using Microsoft.EntityFrameworkCore;
using Shop.DAL.Interface;
using Shop.Domain.Entity;
using Shop.Domain.Enum;
using Shop.Domain.Helper;
using Shop.Domain.Response;
using Shop.Domain.ViewModel;
using Shop.Service.Interfaces;
using System.Security.Claims;


namespace Shop.Service.Implementations
{
    public class UserService : IUserService
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
                var user = await _userRepository.GetAll().FirstOrDefaultAsync(x => x.Email == model.Email);
                if(user != null)
                {
                    return new BaseResponse<ClaimsIdentity>()
                    {
                        StatusCode = StatusCode.CommonError,
                        Description = "A user with this email is already exists"
                    };
                }
                user = new User()
                {
                    Name = model.Name,
                    Email = model.Email,
                    Role = Role.USER,
                    Password = HashPasswordHelper.HashPassowrd(model.Password)
                };
                await _userRepository.Create(user);
                var result = Authenticate(user);

                return new BaseResponse<ClaimsIdentity>()
                {
                    Data = result,
                    Description = "Object has been added",
                    StatusCode = StatusCode.OK
                };
            }
            catch(Exception ex)
            {
                return new BaseResponse<ClaimsIdentity>()
                {
                    Description = ex.Message,
                    StatusCode = StatusCode.InternalServerError
                };

            }
        }

        public async Task<BaseResponse<ClaimsIdentity>> Login(LoginViewModel model)
        {
            try
            {
                var user = await _userRepository.GetAll().FirstOrDefaultAsync(x => x.Email == model.Email);
                if(user == null)
                {
                    return new BaseResponse<ClaimsIdentity>()
                    {
                        StatusCode = StatusCode.UserNotFound,
                        Description = "A user with this email is not found"
                    };
                }

                if(user.Password != HashPasswordHelper.HashPassowrd(model.Password))
                {
                    return new BaseResponse<ClaimsIdentity>()
                    {
                        StatusCode = StatusCode.UserNotFound,
                        Description = "Incorrect password"
                    };
                }

                var result = Authenticate(user);

                return new BaseResponse<ClaimsIdentity>()
                {
                    StatusCode = StatusCode.OK,
                    Data = result
                };
            }
            catch(Exception ex)
            {
                return new BaseResponse<ClaimsIdentity>()
                {
                    StatusCode = StatusCode.InternalServerError,
                    Description = ex.Message
                };
            }
        }

        private ClaimsIdentity Authenticate(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Name),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role.ToString())
            };
            return new ClaimsIdentity(claims, "ApplicationCookie",
                ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
        }
    }
}
