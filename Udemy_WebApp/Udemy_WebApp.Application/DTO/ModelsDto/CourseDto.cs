using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Udemy_WebApp.Application.DTO.ModelsDto
{
    /// <summary>
    /// The CourseDto class represents a DTO for Course entities in the Domain class Library
    /// and is used to transfer information related to Course objects between different parts of the application..
    /// </summary>
    public class CourseDto
    {
        public int CourseId { get; set; } 
        public string CourseTitle { get; set; }
        public string CourseDescription { get; set; }
        public decimal CourseListPrice { get; set; }
        public DateTime CourseCreatedAt { get; set; }
        public decimal CourseDurationTime { get; set; }
        public string CourseImageUrl { get; set; }
        public int CourseCategoryId { get; set; }
        public int LevelId { get; set; }
        public int LanguageId { get; set; }
        [DataType(DataType.Upload)]
        public IFormFile Image { get; set; }
    }
}
