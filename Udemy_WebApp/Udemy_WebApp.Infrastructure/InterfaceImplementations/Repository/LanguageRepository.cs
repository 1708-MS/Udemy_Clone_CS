using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Udemy_WebApp.Application.Interfaces.IRepository;
using Udemy_WebApp.Domain.Models;
using Udemy_WebApp.Infrastructure.DataAccess;

namespace Udemy_WebApp.Infrastructure.InterfaceImplementations.Repository
{
    public class LanguageRepository : Repository<Language>, ILanguageRepository
    {
        private readonly ApplicationDbContext _context;
        public LanguageRepository(ApplicationDbContext context) : base(context)
        {
            _context= context;
        }
        public async Task UpdateAsync(Language language)
        {
            _context.Languages.Update(language);
        }
    }
}
