using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Udemy_WebApp.Domain.Models
{
    public class Level
    {
        public int LevelId { get; set; }
        public string LevelName { get; set; }
        public ICollection<Course> Courses { get; set; }
    }
}
