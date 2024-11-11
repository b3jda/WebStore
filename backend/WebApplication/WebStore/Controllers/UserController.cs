using Microsoft.AspNetCore.Mvc;
using WebStore.DTOs;
using WebStore.Services.Interfaces;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace WebStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]

        public async Task<ActionResult<UserResponseDTO>> GetUserById(string id)
        {
            var user = await _userService.GetUserById(id);
            if (user == null)
                return NotFound();
            return Ok(user);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]

        public async Task<ActionResult<IEnumerable<UserResponseDTO>>> GetAllUsers()
        {
            var users = await _userService.GetAllUsers();
            return Ok(users);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> AddUser([FromBody] UserResponseDTO userRequest)
        {
            var createdUser = await _userService.AddUser(userRequest);
            if (createdUser == null)
                return BadRequest("Unable to create user. Ensure all required fields are provided.");

            return CreatedAtAction(nameof(GetUserById), new { id = createdUser.UserName }, createdUser);
        }
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var isDeleted = await _userService.DeleteUser(id);
            if (!isDeleted)
                return NotFound();

            return NoContent();
        }

    }
 }
