using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Udemy_WebApp.Application.DTO.ModelsDto;
using Udemy_WebApp.Application.Interfaces.IRepository;
using Udemy_WebApp.Domain.Models;
using Udemy_WebApp.Infrastructure.InterfaceImplementations.Repository;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Udemy_WebApp.WebApi.Controllers
{
    [Route("api/CourseCategory")]
    [ApiController]
    // [Authorize(Policy = "Admin")]
    public class CourseCategoryController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CourseCategoryController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        /// <summary>
        /// Get all of the Categories of the Courses from the database 
        /// </summary>
        /// <returns>All of the available Categories from the database with a Ok response</returns>
        [HttpGet("GetAllCourseCategories")]
        public async Task<ActionResult<IEnumerable<CourseCategory>>> GetCourseCategories()
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            // Gets all of the Course Categories from the database using the GetAllAsync() method of the CourseCategoryRepository 
            var categoryFromDb = await _unitOfWork.CourseCategoryRepository.GetAllAsync(includeproperties: "Courses" );
            // If no Category found then return NotFound error with message
            if (categoryFromDb == null) return NotFound("No Course Categories exists in the database.");
            var categoryDtos = _mapper.Map<IEnumerable<CourseCategoryDto>>(categoryFromDb);
            return Ok(categoryDtos);
        }

        /// <summary>
        ///  Gets a single CourseCategory by using the CourseCategoryID.
        /// </summary>
        /// <param name="courseCategoryId"></param>
        /// <returns>The available specific CourseCategory from the database as per the entered CourseCategoryId</returns>
        [HttpGet("{CourseCategoryId}")]
        public async Task<ActionResult> GetCourseCategoryById(int courseCategoryId)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            // Get the specific Course Category with the given CourseCategoryId from the database using the GetAsync() method of the CourseCategoryRepository.
            var categoryFromDb = await _unitOfWork.CourseCategoryRepository.GetAsync(courseCategoryId);
            // If no Category found with respect to the entered courseCategoryId, then return NotFound error with message        
            if (categoryFromDb == null) return NotFound($"Could not find the id : {courseCategoryId}");
            // var categoryDto = _mapper.Map<CourseCategoryDto>(category);
            return Ok(categoryFromDb);
        }

        /// <summary>
        /// Creates a new Category by using the given CourseCategoryDto.
        /// </summary>
        /// <param name="courseCategoryDto"></param>
        /// <returns>The newly created CourseCategory with Id and Name of the Category</returns>
        [HttpPost("CreateCourseCategory")]
        public async Task<ActionResult> CreateCourseCategory(CourseCategoryDto courseCategoryDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            // Check if the CourseCategory name already exists
            var existingCategory = _unitOfWork.CourseCategoryRepository.FirstOrDefault(
                CourseCategory => CourseCategory.CourseCategoryName.ToLower() == courseCategoryDto.CourseCategoryName.ToLower());
            // If the entered CourseCategory name already exists in the database, then return the error with message
            if (existingCategory != null)
            {
                ModelState.AddModelError("CourseCategoryName", "The CourseCategory name already exists.");
                return BadRequest(ModelState);
            }
            // Map the CourseCategoryDto to CourseCategory entity.
            var courseCategory = _mapper.Map<CourseCategory>(courseCategoryDto);
            // If the entered CourseCategory name is unique, then add it to the database using the AddAsync() method of the CourseCategoryRepository
            await _unitOfWork.CourseCategoryRepository.AddAsync(courseCategory);
            _unitOfWork.Save();
            // Map the created CourseCategory back to the CourseCategoryDto and return it as an OK response with the details of the added new CourseCategory Name
            var createdCategory = _mapper.Map<CourseCategoryDto>(courseCategory);
            //return CreatedAtAction(nameof(GetCourseCategories), new { id = createdCategoryDto.CourseCategoryId }, createdCategoryDto);
            return Ok(createdCategory);
        }

        /// <summary>
        /// Updates an existing CourseCategory by using the specific courseCategoryId.
        /// </summary>
        /// <param name="courseCategoryDto"></param>
        /// <returns>The full updated details of the CourseCategory</returns>
        [HttpPut]
        public async Task<IActionResult> UpdateCourseCategory(CourseCategoryDto courseCategoryDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            // Check if the entered courseCategoryId matches with the one in the database.
            var existingCategory = _unitOfWork.CourseCategoryRepository.FirstOrDefault(
                CourseCategory => CourseCategory.CourseCategoryId == courseCategoryDto.CourseCategoryId);
            if (existingCategory == null) 
            {
                ModelState.AddModelError("CourseCategoryId", "The CourseCategory Id does not exists.");
                return BadRequest(ModelState);
            }
            // Map the CourseCategoryDto to CourseCategory entity update it in the database using the UpdateAsync() method of the CourseCategoryRepository
            var category = _mapper.Map(courseCategoryDto, existingCategory);
            // Update the entered courseCategory Id details in the database using the UpdateAsync() method of the CourseCategoryRepository
            await _unitOfWork.CourseCategoryRepository.UpdateAsync(category);
            _unitOfWork.Save();
            // Map the updated CourseCategory back to CourseCategoryDto and return it as an OK response with the full details of the updated CourseCategory.
            var updatedCourseCategory = _mapper.Map<CourseCategoryDto>(category);
            return Ok(updatedCourseCategory);
        }

        /// <summary>
        /// Deletes a CourseCategory from the database with the given courseCategoryId
        /// </summary>
        /// <param name="courseCategoryId"></param>
        /// <returns>A successful message after deleting the specific Category record from the database with respect to the CourseID </returns>
        [HttpDelete("{courseCategoryId}")]
        public async Task<IActionResult> DeleteCourseCategory(int courseCategoryId)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            // Get the Course Category with the given CourseCategoryId from the database using the GetAsync() method of the CourseCategoryRepository.
            var categoryFromDb = await _unitOfWork.CourseCategoryRepository.GetAsync(courseCategoryId);
            // If no course category found with the given courseCategoryId, return NotFound response with the error message.
            if (categoryFromDb == null) return NotFound($"Could not find the Id : {courseCategoryId}. The entered Id is invalid!!!.");
            // Remove the Course Category from the database using the Remove() method of the CourseCategoryRepository and then Save the changes to the database.
            _unitOfWork.CourseCategoryRepository.Remove(categoryFromDb);
            _unitOfWork.Save();
            return Ok("Category deleted successfully");
        }
    }
}
