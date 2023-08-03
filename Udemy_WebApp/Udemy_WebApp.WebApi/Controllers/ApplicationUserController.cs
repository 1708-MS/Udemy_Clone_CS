using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Udemy_WebApp.Application.DTO;
using Udemy_WebApp.Application.DTO.RoleInitializeDto;
using Udemy_WebApp.Application.DTO.UserRegisterationDto;
using Udemy_WebApp.Application.Interfaces.RoleServiceContract;
using Udemy_WebApp.Application.Interfaces.UserServiceContracts;
using Udemy_WebApp.Domain.Models;

namespace Udemy_WebApp.WebApi.Controllers
{
    [Route("api/ApplicationUser")]
    [ApiController]
    public class ApplicationUserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IRoleInitializeService _roleInitializeService;

        public ApplicationUserController(IUserService userService, SignInManager<ApplicationUser> signInManager,
            RoleManager<IdentityRole> roleManager, IRoleInitializeService roleInitializeService)
        {
            _userService = userService;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _roleInitializeService = roleInitializeService;
        }

        /// <summary>
        /// Register a new user.
        /// </summary>
        /// <param name="userRegistrationDto">The UserRegistrationDto contains User Registration details entered by the user during Registeration</param>
        /// <returns>Ok if registration is successful or BadRequest with error message if registration fails</returns>
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegistrationDto userRegistrationDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                // Call the IUserService for user registration
                await _userService.RegisterUserAsync(userRegistrationDto);
                return Ok("Registration successful.");
            }
            // Registration failed and return exception message
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Log in a user with the provided credentials.
        /// </summary>
        /// <param name="loginDto">The LoginDto containing user login details entered by the User while Signing In</param>
        /// <returns>Ok if login is successful or BadRequest with error message if login fails</returns>
        [HttpPost("Login")]
        public async Task<ActionResult> Login([FromBody] LoginDto loginDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            // Use the SignInManager to attempt user login
            var result = await _signInManager.PasswordSignInAsync(loginDto.LoginUserName, loginDto.LoginUserPassword, isPersistent: false, lockoutOnFailure: false);
            // User successfully logged in
            if (result.Succeeded) return Ok();
            // If login failed, returns an error message
            return BadRequest("Invalid login attempt.");
        }

        /// <summary>
        /// Create new roles for the application.
        /// </summary>
        /// <param name="roleInitializeDto"></param>
        /// <returns>Ok if roles are created successfully or BadRequest if role creation fails</returns>
        [HttpPost("CreateRole")]
        public async Task<IActionResult> CreateRoles([FromBody] RoleInitializeDto roleInitializeDto)
        {
            if (!ModelState.IsValid) return BadRequest();
            // Call the IRoleInitializeService to create roles
            await _roleInitializeService.InitializeRolesAsync(roleInitializeDto);
            return Ok("Roles created successfully.");
        }

        /// <summary>
        /// Authenticate user and generate an authentication token
        /// </summary>
        /// <param name="loginDto"></param>
        /// <returns>Ok with the authentication token if login is successful or BadRequest with error message if login fails</returns>
        [HttpPost("Authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] LoginDto loginDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            // Authenticate user using the IUserService
            var user = await _userService.Authenticate(loginDto);
            // If user authentication fails, return BadRequest with an error message
            if (user == null) return BadRequest(new { message = "Wrong User/Password" });
            return Ok(user);
        }
    }
}
