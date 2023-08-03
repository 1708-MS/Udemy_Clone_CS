using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Udemy_WebApp.Application.DTO.ModelsDto;
using Udemy_WebApp.Application.Interfaces.IRepository;
using Udemy_WebApp.Domain.Models;

namespace Udemy_WebApp.WebApi.Controllers
{
    [Route("api/language")]
    [ApiController]
    public class LanguageController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public LanguageController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork= unitOfWork;
            _mapper= mapper;
        }

        /// <summary>
        /// Get all languages from the database.
        /// </summary>
        /// <returns>All available languages with a Ok response.</returns>
        [HttpGet("GetAllLanguages")]
        public async Task<ActionResult<IEnumerable<Language>>> GetAllLanguages()
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            // Fetch all languages from the database using the GetAllAsync() method of the LanguageRepository.
            var languagesFromDb = await _unitOfWork.LanguageRepository.GetAllAsync(includeproperties:"Courses");
            // If no languages found, return a NotFound response with an error message.
            if (languagesFromDb == null) return NotFound("No Language exists in the database");
            // Return the list of languages with an Ok response.
            return Ok(languagesFromDb);
        }

        /// <summary>
        /// Get a specific language by its languageId.
        /// </summary>
        /// <param name="languageId"></param>
        /// <returns>The available specific language from the database as per the entered languageId.</returns>
        [HttpGet("{languageId}")]
        public async Task<ActionResult<Language>> GetLanguageById(int languageId)
        {
            if (!ModelState.IsValid) return BadRequest();
            // Fetch the language with the given languageId from the database using the GetAsync() method of the Language repository.
            var languageFromDb = await _unitOfWork.LanguageRepository.GetAsync(languageId);
            // If no language found with the given languageId, return a NotFound response with an error message.
            if (languageFromDb == null) return NotFound($"Could not find the id: {languageId}");
            // Return the language with an Ok response.
            return Ok(languageFromDb);
        }

        /// <summary>
        /// Creates a new language using the given LanguageDto.
        /// </summary>
        /// <param name="languageDto"></param>
        /// <returns>The newly created language with Id and Name of the language.</returns>
        [HttpPost("AddNewLanguage")]
        public async Task<ActionResult> CreateNewLanguage(LanguageDto languageDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            // Check if the language name already exists in the database.
            var existingLanguage = _unitOfWork.LanguageRepository.FirstOrDefault(
                Language => Language.LanguageName.ToLower() == languageDto.LanguageName.ToLower());
            // If the language name already exists, return a BadRequest response with an error message.
            if (existingLanguage != null)
            {
                ModelState.AddModelError("LanguageName", "The Language Name already exists.");
                return BadRequest(ModelState);
            }
            // Map the LanguageDto to Language entity.
            var language = _mapper.Map<Language>(languageDto);
            // Add the new language to the database using the AddAsync() method of the Language repository.
            await _unitOfWork.LanguageRepository.AddAsync(language);
            _unitOfWork.Save();
            // Map the created language back to the LanguageDto and return it as an OK response with the details of the added new language.
            var createdLanguage = _mapper.Map<LanguageDto>(language);
            return Ok(createdLanguage);
        }

        /// <summary>
        /// Updates an existing language using the specific languageId.
        /// </summary>
        /// <param name="languageDto"></param>
        /// <returns>The full updated details of the language.</returns>
        [HttpPut]
        public async Task<IActionResult> UpdateLanguage(LanguageDto languageDto)
        {
            if (!ModelState.IsValid) return BadRequest();
            // Check if the entered languageId matches with the one in the database.
            var existingLanguage = _unitOfWork.LanguageRepository.FirstOrDefault(
                Language => Language.LanguageId == languageDto.LanguageId);
            // If the languageId does not exist in the database, return a BadRequest response with an error message.
            if (existingLanguage == null)
            {
                ModelState.AddModelError("LanguageId", "The LanguageId does not exist.");
                return BadRequest(ModelState);
            }
            // Map the LanguageDto to the existing Language entity and update it in the database using the UpdateAsync() method of the Language repository.
            var language = _mapper.Map(languageDto, existingLanguage);
            // Update the entered LanguageId details in the database using the UpdateAsync() method of the Language repository.
            await _unitOfWork.LanguageRepository.UpdateAsync(language);
            _unitOfWork.Save();
            // Map the updated language back to LanguageDto and return it as an OK response with the full details of the updated language.
            var updatedLanguage = _mapper.Map<LanguageDto>(language);
            return Ok(updatedLanguage);
        }

        /// <summary>
        /// Deletes a language from the database with the given languageId.
        /// </summary>
        /// <param name="languageId"></param>
        /// <returns>A successful message after deleting the specific language record from the database with respect to the languageId.</returns>
        [HttpDelete("{languageId}")]
        public async Task<IActionResult> DeleteLanguage(int languageId)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            // Get the language with the given languageId from the database using the GetAsync() method of the Language repository.
            var languageFromDb = await _unitOfWork.LanguageRepository.GetAsync(languageId);
            // If no language found with the given languageId, return a NotFound response with an error message.
            if (languageFromDb == null) return NotFound($"Could not find the Id : {languageId}. The entered Id is invalid!!!.");
            // Remove the language from the database using the Remove() method of the Language repository and then Save the changes to the database.
            _unitOfWork.LanguageRepository.Remove(languageFromDb);
            _unitOfWork.Save();
            // Return an Ok response with a success message indicating that the language was deleted successfully.
            return Ok("Language deleted successfully");
        }
    }
}
