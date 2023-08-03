using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Udemy_WebApp.Application.DTO.RoleInitializeDto;

namespace Udemy_WebApp.Application.Interfaces.RoleServiceContract
{
    public interface IRoleInitializeService
    {
        Task InitializeRolesAsync(RoleInitializeDto roleInitializeDto);
    }
}
