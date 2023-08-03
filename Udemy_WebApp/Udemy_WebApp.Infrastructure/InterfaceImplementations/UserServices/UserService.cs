using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Udemy_WebApp.Application.DTO;
using Udemy_WebApp.Application.DTO.UserRegisterationDto;
using Udemy_WebApp.Application.Interfaces.UserServiceContracts;
using Udemy_WebApp.Domain.Models;
using Udemy_WebApp.Domain.Utility.StandardDictionaries;


namespace Udemy_WebApp.Infrastructure.InterfaceImplementations.UserServices
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly AppSettings _appSetting;
        private readonly IMapper _mapper;
        public UserService(UserManager<ApplicationUser> userManager, IMapper mapper, SignInManager<ApplicationUser> signInManager, IOptions<AppSettings> appsetting)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _appSetting = appsetting.Value;
            _mapper = mapper;
        }

        public async Task RegisterUserAsync(UserRegistrationDto userRegistrationDto)
        {
            var user = _mapper.Map<ApplicationUser>(userRegistrationDto);

            // Check if the email is already registered
            var existingUser = await _userManager.FindByEmailAsync(userRegistrationDto.RegistrationEmail);
            if (existingUser != null) throw new Exception("Email is already registered!!!");

            // Save the user to the database using the UserManager
            var result = await _userManager.CreateAsync(user, userRegistrationDto.RegistrationPassword);

            // If registration fails, throw an exception with the error details
            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors);
                throw new Exception($"User registration failed. Errors: {errors}");
            }
        }

        public async Task<ApplicationUser> Authenticate(LoginDto loginDto)
        {
            var result = await _signInManager.PasswordSignInAsync(loginDto.LoginUserName, loginDto.LoginUserPassword, false, false);
            if (result.Succeeded)
            {
                var applicationUser = await _userManager.FindByNameAsync(loginDto.LoginUserName);
                applicationUser.PasswordHash = "";

                //JWT Token
                //if (await _userManager.IsInRoleAsync(applicationUser, SD.Role_Admin))
                //    applicationUser.Role = SD.Role_Admin;
                //if (await _userManager.IsInRoleAsync(applicationUser, SD.Role_Instructor))
                //    applicationUser.Role = SD.Role_Instructor;

                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_appSetting.Secret);
                var tokenDescriptor = new SecurityTokenDescriptor()
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Name,applicationUser.Id),
                        new Claim(ClaimTypes.Email,applicationUser.Email),
                        //new Claim(ClaimTypes.Role,applicationUser.Role)
                    }),
                    Expires = DateTime.UtcNow.AddDays(7),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)

                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                applicationUser.Token = tokenHandler.WriteToken(token);
                // applicationUser.PasswordHash = "";
                return applicationUser;
            }
            return null;
        }
    }
}
