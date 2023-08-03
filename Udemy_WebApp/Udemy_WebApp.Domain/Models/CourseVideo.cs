using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Udemy_WebApp.Domain.Models
{
    public class CourseVideo
    {
        public int CourseVideoId { get; set; }
        public string CourseVideoTitle { get; set; }
        public string CourseVideoUrl { get; set; }
        public int CourseId { get; set; }
        public Course Course { get; set; }
    }
}
