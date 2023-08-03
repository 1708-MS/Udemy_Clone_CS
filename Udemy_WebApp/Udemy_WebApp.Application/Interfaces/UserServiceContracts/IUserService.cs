using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Udemy_WebApp.Application.DTO;
using Udemy_WebApp.Application.DTO.UserRegisterationDto;
using Udemy_WebApp.Domain.Models;

namespace Udemy_WebApp.Application.Interfaces.UserServiceContracts
{
    public interface IUserService
    {
        Task RegisterUserAsync(UserRegistrationDto userRegistrationDto);
        Task<ApplicationUser> Authenticate(LoginDto loginDto);
    }
}
