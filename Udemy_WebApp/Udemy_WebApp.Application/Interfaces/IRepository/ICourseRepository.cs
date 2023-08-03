using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Udemy_WebApp.Application.DTO.ModelsDto;
using Udemy_WebApp.Domain.Models;

namespace Udemy_WebApp.Application.Interfaces.IRepository
{
    public interface ICourseRepository : IRepository<Course>
    {
        Task UpdateAsync(Course course);
    }
}