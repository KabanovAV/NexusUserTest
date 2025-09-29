using Microsoft.AspNetCore.Mvc;
using NexusUserTest.Application.Mappings;
using NexusUserTest.Application.Services;
using NexusUserTest.Common.DTOs;
using SibCCSPETest.WebApi.MappingProfiles;

namespace SibCCSPETest.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController(IRepoServiceManager service) : ControllerBase
    {
        private readonly IRepoServiceManager _service = service;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetAll(string? include = null)
        {
            var users = await _service.UserRepository.GetAllUserAsync(includeProperties: include);
            return Ok(users.ToAdminDto());
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<UserDTO>> Get(int id, string? include = null)
        {
            var user = await _service.UserRepository.GetUserAsync(u => u.Id == id, include);
            if (user == null)
                return NotFound(new { Message = $"Пользователь с id {id} не найден." });
            return Ok(user.ToAdminDto());
        }

        [HttpGet("{id:int}/test")]
        public async Task<ActionResult<UserInfoTestDTO>> GetTest(int id, string? include = null)
        {
            var user = await _service.UserRepository.GetUserAsync(u => u.Id == id, include);
            if (user == null)
                return NotFound(new { Message = $"Пользователь с id {id} не найден." });
            return Ok(user.ToTestDto());
        }

        [HttpPost]
        public async Task<ActionResult<UserDTO>> Add(UserCreateDTO userCreateDTO, string? include = null)
        {
            if (userCreateDTO == null)
                return BadRequest("Данные для добавления пользователя пустые.");
            var user = userCreateDTO.ToEntity();
            await _service.UserRepository.AddUserAsync(user!, include);
            var userDTO = user!.ToAdminDto();
            return CreatedAtAction(nameof(Get), new { id = userDTO!.Id }, userDTO);
        }

        [HttpPut]
        public async Task<ActionResult<UserDTO>> Update(UserDTO userDTO, string? include = null)
        {
            if (userDTO == null)
                return BadRequest("Данные для обновления пользователя пустые.");
            var user = await _service.UserRepository.GetUserAsync(u => u.Id == userDTO.Id, include);
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
