using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Udemy_WebApp.Application.DTO.RoleInitializeDto;
using Udemy_WebApp.Application.Interfaces.RoleServiceContract;
using Udemy_WebApp.Domain.Utility.StandardDictionaries;

namespace Udemy_WebApp.Infrastructure.InterfaceImplementations.RoleService
{
    public class RoleInitializeService : IRoleInitializeService
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleInitializeService(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task InitializeRolesAsync(RoleInitializeDto roleInitializeDto)
        {
            if(roleInitializeDto.RoleName=="Admin")
            {
                if (await _roleManager.RoleExistsAsync(SD.Role_Admin)) throw new Exception("Admin Role Already Exists");
                var adminRole = new IdentityRole();
                adminRole.Name = SD.Role_Admin;
                await _roleManager.CreateAsync(adminRole);
            }
            else if (roleInitializeDto.RoleName == "Instructor")
            {  // Create the Instructor Role
                if (await _roleManager.RoleExistsAsync(SD.Role_Instructor)) throw new Exception("Instructor already Exists!!!");
                var instructorRole = new IdentityRole();
                instructorRole.Name = SD.Role_Instructor;
                await _roleManager.CreateAsync(instructorRole);

            }
            else if (roleInitializeDto.RoleName == "Individual")
            {
                //Create the Individual Role
                if (await _roleManager.RoleExistsAsync(SD.Roles_Individual)) throw new Exception("Individual already Exists!!!");
                var IndividualUserRole = new IdentityRole();
                IndividualUserRole.Name = SD.Roles_Individual;
                await _roleManager.CreateAsync(IndividualUserRole);
            }
            else
            {
                throw new Exception("Wrong Role Name!!!. Use Admin, Instructor or Individual");
            }
        }
    }
}



