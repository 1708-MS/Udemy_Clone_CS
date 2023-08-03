using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Udemy_WebApp.Domain.Models
{
    public class ApplicationUser : IdentityUser
    {
        public String UserFullName { get; set; }
        public string Address { get; set; }
        [NotMapped]
        public string Token { get; set; }
        [NotMapped]
        public string Role { get; set; }
        public ICollection<CourseReview> CourseReviews { get; set; }
        public ICollection<Course> Courses { get; set; }
    }
}
