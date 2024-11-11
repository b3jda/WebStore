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

        [HttpPost("register")]
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
                LastName=request.LastName
            };

            var result = await _userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return Ok("User created successfully");
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] AuthRequestDTO request)
        {
            if (request == null)
            {
                return BadRequest("Request body is empty");
            }

            // Find the user by email
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                return BadRequest("Invalid email or password");
            }

            // Validate the password
            var isPasswordValid = await _userManager.CheckPasswordAsync(user, request.Password);
            if (!isPasswordValid)
            {
                return BadRequest("Invalid email or password");
            }

            // Generate token and response object
            var tokenResponse = await _tokenService.GenerateTokenResponse(user);

            return Ok(tokenResponse);
        }


        [HttpPost("role")]
        public async Task<ActionResult> CreateRoles(string roleName)
        {
            if (!await _roleManager.RoleExistsAsync(roleName))
            {
                await _roleManager.CreateAsync(new IdentityRole(roleName));
            }
            return Ok();
        }

        [HttpPost("assign")]
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
