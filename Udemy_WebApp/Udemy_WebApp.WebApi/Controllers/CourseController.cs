using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Udemy_WebApp.Application.DTO.ModelsDto;
using Udemy_WebApp.Application.Interfaces.IFilesUploadService;
using Udemy_WebApp.Application.Interfaces.IRepository;
using Udemy_WebApp.Domain.Models;

namespace Udemy_WebApp.WebApi.Controllers
{
    [Route("api/course")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IFilesUploadService _filesUploadService;
        public CourseController(IUnitOfWork unitOfWork, IMapper mapper, IFilesUploadService filesUploadService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _filesUploadService = filesUploadService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> GetAllCourses()
        {
            var courses = await _unitOfWork.CourseRepository.GetAllAsync();
            var courseDtos = _mapper.Map<IEnumerable<CourseDto>>(courses);
            return Ok(courseDtos);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="courseDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateCourse([FromForm] CourseDto courseDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var existingCourse = _unitOfWork.CourseRepository.FirstOrDefault(
               CourseRepository => CourseRepository.CourseTitle.ToLower() == courseDto.CourseTitle.ToLower());
            if (existingCourse != null)
            {
                ModelState.AddModelError("CouseTitle", "The Course Title already exists.");
                return BadRequest(ModelState);
            }

            var imageResponse = _filesUploadService.UploadImage(courseDto.Image, "Images");
            if (imageResponse.Exception == null)
            {
                courseDto.CourseImageUrl = imageResponse.Result;
            }
            else
            {
                return BadRequest(imageResponse.Result);
            }
            var course = _mapper.Map<Course>(courseDto);

            await _unitOfWork.CourseRepository.AddAsync(course);
            _unitOfWork.Save();
            //var createdCourse = _mapper.Map<courseDto>(course);
            var createdCourse = _mapper.Map<CourseDto>(course);
            // return CreatedAtAction(nameof(GetAllCourses), new { id = course.CourseId }, course);
            return Ok(createdCourse);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="courseId"></param>
        /// <returns></returns>
        [HttpGet("{courseId}")]
        public async Task<IActionResult> GetCourseById(int courseId)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var course = await _unitOfWork.CourseRepository.GetAsync(courseId);
            if (course == null) return NotFound("The requested courseId does not exists.");
            var courseDto = _mapper.Map<CourseDto>(course);
            return Ok(courseDto);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="courseId"></param>
        /// <param name="courseDto"></param>
        /// <returns></returns>
        [HttpPut("{courseId}")]
        public async Task<IActionResult> UpdateCourse(int courseId, CourseDto courseDto)
        {
            if (courseId != courseDto.CourseId || !ModelState.IsValid) return BadRequest(ModelState);
            var courseInDb = await _unitOfWork.CourseRepository.GetAsync(courseId);
            if (courseInDb == null) return NotFound("Incorrect CourseId entered!!!.");
            var course = _mapper.Map(courseDto, courseInDb);
            await _unitOfWork.CourseRepository.UpdateAsync(courseInDb);
            _unitOfWork.Save();
            var updatedCourse = _mapper.Map<CourseDto>(course);
            return Ok(updatedCourse);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="courseId"></param>
        /// <returns></returns>
        [HttpDelete("{courseId}")]
        public async Task<IActionResult> DeleteCourse(int courseId)
        {
            var course = await _unitOfWork.CourseRepository.GetAsync(courseId);
            if (course == null) return NotFound();
            _unitOfWork.CourseRepository.Remove(course);
            _unitOfWork.Save();
            return Ok("Course Successfully deleted");
        }
    }
}
