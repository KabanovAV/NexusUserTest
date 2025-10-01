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

        [HttpPut("info")]
        public async Task<ActionResult<GroupUserInfoAdminDTO>> UpdateInfo(GroupUserInfoAdminDTO groupUserDTO, string? include = null)
        {
            if (groupUserDTO == null)
                return BadRequest("Данные для обновления группыльзователя пустые.");
            var groupUser = await _service.GroupUserRepository.GetGroupUserAsync(gu => gu.Id == groupUserDTO.Id, include);
            if (groupUser == null)
                return NotFound(new { Message = $"Группа пользователя с id {groupUserDTO.Id} не найдена." });
            groupUser.UpdateFromInfoDto(groupUserDTO);
            await _service.GroupUserRepository.UpdateGroupUserAsync(groupUser, include);
            return Ok(groupUser.ToInfoAdminDto());
        }

        [HttpPut("test")]
        public async Task<ActionResult<GroupUserTestDTO>> UpdateTest(GroupUserTestDTO groupUserDTO, string? include = null)
        {
            if (groupUserDTO == null)
                return BadRequest("Данные для обновления группыльзователя пустые.");
            var groupUser = await _service.GroupUserRepository.GetGroupUserAsync(gu => gu.Id == groupUserDTO.Id, include);
            if (groupUser == null)
                return NotFound(new { Message = $"Группа пользователя с id {groupUserDTO.Id} не найдена." });
            groupUser.UpdateFromTestDto(groupUserDTO);
            await _service.GroupUserRepository.UpdateGroupUserAsync(groupUser, include);
            return Ok(groupUser.ToTestDto());
        }
    }
}
