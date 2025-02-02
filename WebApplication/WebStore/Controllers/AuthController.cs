using WebStore.Data;
using WebStore.DTOs;
using WebStore.Services.AuthenticationService;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebStore.Models;

namespace WebStore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [ApiVersion("1.0")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly AppDbContext _context;
        private readonly TokenService _tokenService;

        public AuthController(UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager,
            AppDbContext context,
            TokenService tokenService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
            _tokenService = tokenService;
        }

        /// <summary>
        /// Registers a new user.
        /// </summary>
        /// <param name="request">User registration details.</param>
        /// <returns>Success or failure message.</returns>
        /// <response code="200">User created successfully.</response>
        /// <response code="400">User already exists or invalid request.</response>
        [HttpPost("register")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult> Register(RegistrationRequestDTO request)
        {
            var userExists = await _userManager.FindByEmailAsync(request.Email);
            if (userExists != null)
            {
                return BadRequest("User already exists");
            }

            var user = new User
            {
                Email = request.Email,
                UserName = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName
            };

            var result = await _userManager.CreateAsync(user, request.Password);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return Ok("User created successfully");
        }

        /// <summary>
        /// Authenticates a user and returns a JWT token.
        /// </summary>
        /// <param name="request">User login credentials.</param>
        /// <returns>JWT token for authenticated users.</returns>
        /// <response code="200">Returns JWT token.</response>
        /// <response code="400">Invalid email or password.</response>
        [HttpPost("login")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult> Login([FromBody] AuthRequestDTO request)
        {
            if (request == null)
            {
                return BadRequest("Request body is empty");
            }

            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                return BadRequest("Invalid email or password");
            }

            var isPasswordValid = await _userManager.CheckPasswordAsync(user, request.Password);
            if (!isPasswordValid)
            {
                return BadRequest("Invalid email or password");
            }

            var tokenResponse = await _tokenService.GenerateTokenResponse(user);
            return Ok(tokenResponse);
        }

        /// <summary>
        /// Creates a new role.
        /// </summary>
        /// <param name="roleName">The name of the role.</param>
        /// <returns>Success message.</returns>
        /// <response code="200">Role created successfully.</response>
        [HttpPost("role")]
        [ProducesResponseType(200)]
        public async Task<ActionResult> CreateRoles(string roleName)
        {
            if (!await _roleManager.RoleExistsAsync(roleName))
            {
                await _roleManager.CreateAsync(new IdentityRole(roleName));
            }
            return Ok();
        }

        /// <summary>
        /// Assigns a role to a user.
        /// </summary>
        /// <param name="username">User's username.</param>
        /// <param name="roleName">Role name to assign.</param>
        /// <returns>Success message.</returns>
        /// <response code="200">Role assigned successfully.</response>
        /// <response code="400">User or role does not exist.</response>
        [HttpPost("assign")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult> AssignRoleToUser(string username, string roleName)
        {
            var user = await _userManager.FindByNameAsync(username)
                ?? throw new ApplicationException($"User with username '{username}' not found.");
            if (!await _roleManager.RoleExistsAsync(roleName))
            {
                throw new ApplicationException($"Role '{roleName}' does not exist.");
            }

            if (!await _userManager.IsInRoleAsync(user, roleName))
            {
                await _userManager.AddToRoleAsync(user, roleName);
            }
            return Ok();
        }
    }
}