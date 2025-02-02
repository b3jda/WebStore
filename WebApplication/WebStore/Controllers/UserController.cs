using Microsoft.AspNetCore.Mvc;
using WebStore.DTOs;
using WebStore.Services.Interfaces;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace WebStore.Controllers
{
    /// <summary>
    /// Manages user-related operations in the WebStore API.
    /// </summary>
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserController"/> class.
        /// </summary>
        /// <param name="userService">The service handling user-related operations.</param>
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Retrieves a user by their unique ID.
        /// </summary>
        /// <param name="id">The ID of the user.</param>
        /// <returns>The user details if found; otherwise, a 404 response.</returns>
        /// <response code="200">Returns the user details successfully.</response>
        /// <response code="401">Unauthorized. User is not authenticated.</response>
        /// <response code="403">Forbidden. User does not have permission.</response>
        /// <response code="404">User not found.</response>
        /// <response code="500">Internal server error.</response>
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(typeof(UserResponseDTO), 200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<UserResponseDTO>> GetUserById(string id)
        {
            try
            {
                var user = await _userService.GetUserById(id);
                if (user == null)
                    return NotFound(new { error = "User not found." });

                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Internal server error", message = ex.Message });
            }
        }

        /// <summary>
        /// Retrieves all users.
        /// </summary>
        /// <returns>A list of users.</returns>
        /// <response code="200">Returns the list of users successfully.</response>
        /// <response code="401">Unauthorized. User is not authenticated.</response>
        /// <response code="403">Forbidden. User does not have permission.</response>
        /// <response code="500">Internal server error.</response>
        [HttpGet]
        [Authorize(Roles = "Admin")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(typeof(IEnumerable<UserResponseDTO>), 200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<IEnumerable<UserResponseDTO>>> GetAllUsers()
        {
            try
            {
                var users = await _userService.GetAllUsers();
                return Ok(users);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Internal server error", message = ex.Message });
            }
        }

        /// <summary>
        /// Adds a new user to the system.
        /// </summary>
        /// <param name="userRequest">The user details to add.</param>
        /// <returns>The created user with a 201 response.</returns>
        /// <response code="201">User created successfully.</response>
        /// <response code="400">Invalid request. Ensure all required fields are provided.</response>
        /// <response code="401">Unauthorized. User is not authenticated.</response>
        /// <response code="403">Forbidden. User does not have permission.</response>
        /// <response code="500">Internal server error.</response>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(typeof(UserResponseDTO), 201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> AddUser([FromBody] UserResponseDTO userRequest)
        {
            try
            {
                if (userRequest == null)
                    return BadRequest(new { error = "Invalid request. User details are required." });

                var createdUser = await _userService.AddUser(userRequest);
                if (createdUser == null)
                    return BadRequest(new { error = "Unable to create user. Ensure all required fields are provided." });

                return CreatedAtAction(nameof(GetUserById), new { id = createdUser.UserName, version = "1.0" }, createdUser);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Internal server error", message = ex.Message });
            }
        }

        /// <summary>
        /// Deletes a user by their unique ID.
        /// </summary>
        /// <param name="id">The ID of the user to delete.</param>
        /// <returns>A 204 No Content response if successful; otherwise, a 404 response.</returns>
        /// <response code="204">User deleted successfully.</response>
        /// <response code="401">Unauthorized. User is not authenticated.</response>
        /// <response code="403">Forbidden. User does not have permission.</response>
        /// <response code="404">User not found.</response>
        /// <response code="500">Internal server error.</response>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(204)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> DeleteUser(string id)
        {
            try
            {
                var isDeleted = await _userService.DeleteUser(id);
                if (!isDeleted)
                    return NotFound(new { error = "User not found." });

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Internal server error", message = ex.Message });
            }
        }
    }
}