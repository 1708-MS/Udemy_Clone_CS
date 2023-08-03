using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Udemy_WebApp.Domain.Models
{
    /// <summary>
    /// CourseCategory class represents the Category of the Courses
    /// </summary>
    public class CourseCategory
    {
        public int CourseCategoryId { get; set; }
        public string CourseCategoryName { get; set; }
        public ICollection<Course> Courses { get; set; }
    }
}
