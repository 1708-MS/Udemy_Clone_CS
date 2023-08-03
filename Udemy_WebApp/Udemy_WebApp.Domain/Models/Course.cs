using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Udemy_WebApp.Domain.Models
{
    public class Course
    {
        public int CourseId { get; set; }
        public string CourseTitle { get; set; }
        public string CourseDescription { get; set; }
        public decimal CourseListPrice { get; set; }
        public DateTime CourseCreatedAt { get; set; }
        public decimal CourseDurationTime { get; set; }
        public string CourseImageUrl { get; set; }
        public int CourseCategoryId { get; set; }
        public CourseCategory CourseCategory { get; set; }
        public int LevelId { get; set; }
        public Level Level { get; set; }
        public int LanguageId { get; set; }
        public Language Language { get; set; }
        public ICollection<CourseVideo> CourseVideos { get; set; }  
        public ICollection<CourseReview> CourseReviews { get; set; }
    }
}
