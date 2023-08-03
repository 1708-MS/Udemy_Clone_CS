using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Udemy_WebApp.Domain.Models;

namespace Udemy_WebApp.Application.DTO.UserRegisterationDto
{
    public class UserRegistrationDto
    {
        public string RegistrationUserName { get; set; }
        public string UserAddress { get; set; }
        public string RegistrationEmail { get; set; }
        public string RegistrationPassword { get; set; }
    }
}
