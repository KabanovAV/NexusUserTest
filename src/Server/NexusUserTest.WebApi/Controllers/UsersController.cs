using Microsoft.AspNetCore.Mvc;
using NexusUserTest.Application.Mappings;
using NexusUserTest.Application.Services;
using NexusUserTest.Common;
using SibCCSPETest.WebApi.MappingProfiles;

namespace SibCCSPETest.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController(IRepoServiceManager service) : ControllerBase
    {
        private readonly IRepoServiceManager _service = service;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserAdminDTO>>> GetAll([FromQuery] string? include = null)
        {
            var users = await _service.UserRepository.GetAllUserAsync(includeProperties: include);
            return Ok(users.ToAdminDto());
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id, [FromQuery] string view = "info", [FromQuery] string? include = null)
        {
            var user = await _service.UserRepository.GetUserAsync(u => u.Id == id, include);
            if (user == null)
                return NotFound(new { Message = $"Пользователь с id {id} не найден." });
            return view.ToLower() switch
            {
                "test" => Ok(user.ToTestDto()),
                _ => Ok(user.ToAdminDto())
            };
        }

        [HttpPost]
        public async Task<ActionResult<UserAdminDTO>> Add(UserAdminDTO userAdminDTO, [FromQuery] string? include = null)
        {
            if (userAdminDTO == null)
                return BadRequest("Данные для добавления пользователя пустые.");
            var user = userAdminDTO.ToEntity();
            await _service.UserRepository.AddUserAsync(user!, include);
            var userDTO = user!.ToAdminDto();
            return CreatedAtAction(nameof(Get), new { id = userDTO!.Id }, userDTO);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<UserAdminDTO>> Update(int id, UserAdminDTO userDTO, [FromQuery] string? include = null)
        {
            if (userDTO == null)
                return BadRequest("Данные для обновления пользователя пустые.");
            var user = await _service.UserRepository.GetUserAsync(u => u.Id == id, include);
            if (user == null)
                return NotFound(new { Message = $"Пользователь с id {userDTO.Id} не найден." });
            user.UpdateFromAdminDto(userDTO);
            await _service.UserRepository.UpdateUserAsync(user, include);
            return Ok(user.ToAdminDto());
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _service.UserRepository.GetUserAsync(u => u.Id == id);
            if (user == null)
                return NotFound(new { Message = $"Пользователь с id {id} не найден." });
            await _service.UserRepository.DeleteUserAsync(user);
            return NoContent();
        }
    }
}
