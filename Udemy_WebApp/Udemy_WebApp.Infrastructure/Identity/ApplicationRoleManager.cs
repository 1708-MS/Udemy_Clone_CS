using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Udemy_WebApp.Infrastructure.Identity
{
    internal class ApplicationRoleManager : RoleManager<ApplicationRole>
    {
        public ApplicationRoleManager(IRoleStore<ApplicationRole> store,
            IEnumerable<IRoleValidator<ApplicationRole>> roleValidators,
            ILookupNormalizer keyNormalizer, IdentityErrorDescriber errors,
            ILogger<RoleManager<ApplicationRole>> logger) :
            base(store, roleValidators, keyNormalizer, errors, logger)
        {
        }
    }
}
