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
    public class LevelRepository : Repository<Level>, ILevelRepository
    {
        private readonly ApplicationDbContext _context;
        public LevelRepository(ApplicationDbContext context) : base(context)
        {
            _context= context;
        }
        public async Task UpdateAsync(Level level)
        {
            _context.Levels.Update(level);
        }
    }
}
