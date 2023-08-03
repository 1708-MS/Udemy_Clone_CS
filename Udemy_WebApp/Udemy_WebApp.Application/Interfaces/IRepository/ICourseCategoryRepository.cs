using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Udemy_WebApp.Domain.Models;

namespace Udemy_WebApp.Application.Interfaces.IRepository
{
    public interface ICourseCategoryRepository : IRepository<CourseCategory>
    {
        Task UpdateAsync(CourseCategory courseCategory);
    }
}
