using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Udemy_WebApp.Application.Interfaces.IRepository;
using Udemy_WebApp.Domain.Models;
using Udemy_WebApp.Infrastructure.DataAccess;

namespace Udemy_WebApp.Infrastructure.InterfaceImplementations.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            CourseCategoryRepository = new CourseCategoryRepository(_context);
            LanguageRepository = new LanguageRepository(_context);
            LevelRepository = new LevelRepository(_context);
            CourseRepository = new CourseRepository(_context);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public ICourseCategoryRepository CourseCategoryRepository { get; private set; }
        public ILanguageRepository LanguageRepository { get; private set; }
        public ILevelRepository LevelRepository { get; private set; }
        public ICourseRepository CourseRepository { get; private set; }
    }
}