using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Udemy_WebApp.Domain.Models
{
    public class Language
    {
        public int LanguageId { get; set; }
        public string LanguageName { get; set; }
        public ICollection<Course> Courses { get; set; }
    }
}
