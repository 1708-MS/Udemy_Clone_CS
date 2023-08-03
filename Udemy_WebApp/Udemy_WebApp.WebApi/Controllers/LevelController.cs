using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Udemy_WebApp.Application.DTO.ModelsDto;
using Udemy_WebApp.Application.Interfaces.IRepository;
using Udemy_WebApp.Domain.Models;

namespace Udemy_WebApp.WebApi.Controllers
{
    [Route("api/level")]
    [ApiController]
    public class LevelController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public LevelController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        /// <summary>
        /// Get all course levels from the database.
        /// </summary>
        /// <returns>All available course levels with a Ok response</returns>
        [HttpGet("GetAllLevels")]
        public async Task<ActionResult<IEnumerable<Level>>> GetAllLevels()
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            // Gets all of the Course Levels from the database using the GetAllAsync() method of the LevelRepository 
            var levelsFromDb = await _unitOfWork.LevelRepository.GetAllAsync();
            // If no Level found then return NotFound error with message
            if (levelsFromDb == null) return NotFound("No Course Levels found in the database.");
            return Ok(levelsFromDb);
        }

        /// <summary>
        /// Get a specific course level by its levelId
        /// </summary>
        /// <param name="levelId">This is the Id of the Course Level which is to be fetched</param>
        /// <returns>The available specific course level from the database as per the entered levelId</returns>
        [HttpGet("{levelId}")]
        public async Task<ActionResult<Level>> GetLevelById(int levelId)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            // Get the specific Course Level with the given LevelId from the database using the GetAsync() method of the LevelRepository.
            var levelFromDb = await _unitOfWork.LevelRepository.GetAsync(levelId);
            // If no Level found with respect to the entered LevelId, then return NotFound error with message        
            if (levelFromDb == null) return NotFound($"Could not find the id : {levelId}");
            return Ok(levelFromDb);
        }

        /// <summary>
        /// Create a new course level using the given LevelDto.
        /// </summary>
        /// <param name="levelDto">The LevelDto contains the details of the New Course Level whcih is to be created</param>
        /// <returns>The newly created course level with Id and Name of the level</returns>
        [HttpPost("CreateNewLevel")]
        public async Task<ActionResult> CreateNewLevel(LevelDto levelDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            // Check if the Level name already exists
            var existingLevel = _unitOfWork.LevelRepository.FirstOrDefault(
                Level => Level.LevelName.ToLower() == levelDto.LevelName.ToLower());
            // If the entered Level Name already exists in the database, then return the error with message
            if (existingLevel != null)
            {
                ModelState.AddModelError("LevelName", "The Level name already exists.");
                return BadRequest(ModelState);
            }
            // Map the levelDto to Level entity.
            var level = _mapper.Map<Level>(levelDto);
            // If the entered Level Name is unique, then add it to the database using the AddAsync() method of the LevelRepository
            await _unitOfWork.LevelRepository.AddAsync(level);
            _unitOfWork.Save();
            // Map the created Level back to the levelDto and return it as an OK response with the details of the added new Level Name
            var createdLevelDto = _mapper.Map<LevelDto>(level);
            return Ok(createdLevelDto);
        }

        /// <summary>
        /// Update an existing course level using specific levelId.
        /// </summary>
        /// <param name="levelDto">Contains the details of the level entered by the user to be updated</param>
        /// <returns>The fully updated details of the course level</returns>
        [HttpPut]
        public async Task<IActionResult> UpdateLevel(LevelDto levelDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            // Check if the entered LevelId matches with the one in the database.
            var existingLevel = _unitOfWork.LevelRepository.FirstOrDefault(
                Level => Level.LevelId == levelDto.LevelId);
            if (existingLevel == null)
            {
                ModelState.AddModelError("LevelId", "The Level Id does not exists.");
                return BadRequest(ModelState);
            }
            // Map the LevelDto to existing Level entity and  update it in the database using the UpdateAsync() method of the LevelRepository
            var level = _mapper.Map(levelDto, existingLevel);
            // Update the entered LevelId details in the database using the UpdateAsync() method of the LevelRepository
            await _unitOfWork.LevelRepository.UpdateAsync(level);
            _unitOfWork.Save();
            // Map the updated Level back to levelDto and return it as an OK response with the full details of the updated Level.
            var updatedLevelDto = _mapper.Map<LevelDto>(level);
            return Ok(updatedLevelDto);
        }

        /// <summary>
        /// Deletes a Course Level from the database as per the specific levelId entered by the user.
        /// </summary>
        /// <param name="levelId"></param>
        /// <returns>A successful message after deleting the specific course level record from the database with respect to the levelId</returns>
        [HttpDelete("{levelId}")]
        public async Task<IActionResult> DeleteLevel(int levelId)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            // Get the Course level with the given levelId from the database using the GetAsync() method of the LevelRepository.
            var levelFromDb = await _unitOfWork.LevelRepository.GetAsync(levelId);
            // If no course level found with the given levelId, return NotFound response with the error message.
            if (levelFromDb == null) return NotFound($"Could not find the Id : {levelId}. The entered Id is invalid!!!.");
            // Remove the Course Level from the database using the Remove() method of the LevelRepository and then Save the changes to the database and then returns a message
            _unitOfWork.LevelRepository.Remove(levelFromDb);
            _unitOfWork.Save();
            return Ok("Level deleted successfully");
        }
    }
}
