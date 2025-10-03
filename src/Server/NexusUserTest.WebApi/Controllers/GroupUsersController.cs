using Microsoft.AspNetCore.Mvc;
using NexusUserTest.Application.Mappings;
using NexusUserTest.Application.Services;
using NexusUserTest.Common;
using SibCCSPETest.WebApi.MappingProfiles;

namespace SibCCSPETest.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupUsersController(IRepoServiceManager service) : ControllerBase
    {
        private readonly IRepoServiceManager _service = service;

        [HttpGet("group/{id:int}/info")]
        public async Task<ActionResult<IEnumerable<GroupUserInfoAdminDTO>>> GetInfoAll(int id, string? include = null)
        {
            var groupUser = await _service.GroupUserRepository.GetAllGroupUserAsync(gu => gu.GroupId == id, include);
            return Ok(groupUser.ToInfoAdminDto());
        }

        [HttpGet("{id:int}/info")]
        public async Task<ActionResult<GroupUserInfoAdminDTO>> GetInfo(int id, string? include = null)
        {
            var groupUser = await _service.GroupUserRepository.GetGroupUserAsync(gu => gu.Id == id, include);
            if (groupUser == null)
                return NotFound(new { Message = $"Пользователя в группе с id {id} не найден." });
            return Ok(groupUser.ToInfoAdminDto());
        }

        [HttpGet("{id:int}/test")]
        public async Task<ActionResult<GroupUserTestDTO>> GetTest(int id, string? include = null)
        {
            var groupUser = await _service.GroupUserRepository.GetGroupUserAsync(gu => gu.Id == id, include);
            if (groupUser == null)
                return NotFound(new { Message = $"Пользователя в группе с id {id} не найден." });
            return Ok(groupUser.ToTestDto());
        }

        [HttpPatch("{id:int}")]
        public async Task<IActionResult> Update(int id, GroupUserUpdateDTO groupUserUpdateDTO)
        {
            if (groupUserUpdateDTO == null)
                return BadRequest("Данные для обновления группыльзователя пустые.");
            var groupUser = await _service.GroupUserRepository.GetGroupUserAsync(gu => gu.Id == id);
            if (groupUser == null)
                return NotFound(new { Message = $"Группа пользователя с id {id} не найдена." });
            groupUser.UpdateFromDto(groupUserUpdateDTO);
            await _service.GroupUserRepository.UpdateGroupUserAsync(groupUser);
            return NoContent();
        }
    }
}
