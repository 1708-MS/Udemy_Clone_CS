using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Udemy_WebApp.Application.DTO.ModelsDto;
using Udemy_WebApp.Application.Interfaces.IRepository;
using Udemy_WebApp.Domain.Models;
using Udemy_WebApp.Infrastructure.DataAccess;
using Udemy_WebApp.Infrastructure.Migrations;

namespace Udemy_WebApp.Infrastructure.InterfaceImplementations.Repository
{
    public class CourseRepository : Repository<Course>, ICourseRepository
    {
        private readonly ApplicationDbContext _context;
        public CourseRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task UpdateAsync(Course course)
        {
            var courseInDb = _context.Courses.FirstOrDefault(c => c.CourseId == course.CourseId);
            if (courseInDb != null)
            {
                courseInDb.CourseTitle = course.CourseTitle;
                courseInDb.CourseDescription = course.CourseDescription;
                courseInDb.CourseListPrice = course.CourseListPrice;
                courseInDb.CourseCreatedAt = course.CourseCreatedAt;
                courseInDb.CourseDurationTime = course.CourseDurationTime;
                if (courseInDb.CourseImageUrl != null)
                {
                    courseInDb.CourseImageUrl = courseInDb.CourseImageUrl;
                }
                courseInDb.CourseCategoryId = course.CourseCategoryId;
                courseInDb.LevelId = course.LevelId;
                courseInDb.LanguageId = course.LanguageId;
            }
        }
    }
}
