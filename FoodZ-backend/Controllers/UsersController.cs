using Foodz.API.DTOs.User;
using Foodz.API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoodZ_backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] // Require JWT for all endpoints
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: api/Users
        [HttpGet]
        [Authorize(Roles = "Admin")] // Only admin can get all users
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")] // Admin can get any user by ID
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            return Ok(user);
        }

        // POST: api/Users
        [HttpPost]
        [Authorize(Roles = "Admin")] // Only admin can create users
        public async Task<IActionResult> CreateUser([FromBody] UserCreateDto dto)
        {
            var createdUser = await _userService.CreateUserAsync(dto);
            return CreatedAtAction(nameof(GetUserById), new { id = createdUser.Id }, createdUser);
        }

        // PUT: api/Users/5
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")] // Only admin can update users
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UserCreateDto dto)
        {
            var updatedUser = await _userService.UpdateUserAsync(id, dto);
            return Ok(updatedUser);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")] // Only admin can delete users
        public async Task<IActionResult> DeleteUser(int id)
        {
            var deleted = await _userService.DeleteUserAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}
