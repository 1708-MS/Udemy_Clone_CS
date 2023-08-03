using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Udemy_WebApp.Domain.Models;

namespace Udemy_WebApp.Application.Interfaces.IRepository
{
    /// <summary>
    /// The IUnitOfWork Interface is a container for multiple repositories which manages multiple Repositories as a single unit.
    /// </summary>
    public interface IUnitOfWork
    { 
        ICourseCategoryRepository CourseCategoryRepository { get; }
        ILanguageRepository LanguageRepository { get; }
        ILevelRepository LevelRepository { get; }
        ICourseRepository CourseRepository { get; }
        IRepository<CourseVideo> CourseVideosRepository { get; }
        void Save();
    }
}
