using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Udemy_WebApp.Domain.Models;
using Udemy_WebApp.Infrastructure.DataAccess;

namespace Udemy_WebApp.Infrastructure.Identity
{
    public class ApplicationUserStore : UserStore<ApplicationUser>
    {
        public ApplicationUserStore(ApplicationDbContext context,
            IdentityErrorDescriber describer = null) : base(context, describer)
        {
        }
    }
}
