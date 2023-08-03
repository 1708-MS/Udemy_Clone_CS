using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Udemy_WebApp.Domain.Models;

namespace Udemy_WebApp.Application.DTO.ModelsDto
{
    public class CourseCategoryDto
    {
        public int CourseCategoryId { get; set; }
        public string CourseCategoryName { get; set; }
        public ICollection<Course> Courses { get; set; }
    }
}
