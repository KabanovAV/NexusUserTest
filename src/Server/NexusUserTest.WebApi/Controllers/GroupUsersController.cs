using Microsoft.AspNetCore.Mvc;
using NexusUserTest.Application.Mappings;
using NexusUserTest.Application.Services;
using NexusUserTest.Common.DTOs;
using SibCCSPETest.WebApi.MappingProfiles;

namespace SibCCSPETest.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupUsersController(IRepoServiceManager service) : ControllerBase
    {
        private readonly IRepoServiceManager _service = service;

        [HttpGet("group/{id:int}")]
        public async Task<ActionResult<IEnumerable<GroupUserDTO>>> GetAll(int id, string? include = null)
        {
            var groupUser = await _service.GroupUserRepository.GetAllGroupUserAsync(gu => gu.GroupId == id, include);
            return Ok(groupUser.ToDto());
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<GroupUserDTO>> Get(int id, string? include = null)
        {
            var groupUser = await _service.GroupUserRepository.GetGroupUserAsync(gu => gu.Id == id, include);
            if (groupUser == null)
                return NotFound(new { Message = $"Пользователя в группе с id {id} не найден." });
            return Ok(groupUser.ToDto());
        }

        [HttpPut]
        public async Task<ActionResult<GroupUserDTO>> Update(GroupUserDTO groupUserDTO, string? include = null)
        {
            if (groupUserDTO == null)
                return BadRequest("Данные для обновления группыльзователя пустые.");
            var groupUser = await _service.GroupUserRepository.GetGroupUserAsync(gu => gu.Id == groupUserDTO.Id, include);
            if (groupUser == null)
                return NotFound(new { Message = $"Группа пользователя с id {groupUserDTO.Id} не найдена." });
            groupUser.UpdateFromDto(groupUserDTO);
            await _service.GroupUserRepository.UpdateGroupUser(groupUser, include);
            return Ok(groupUser.ToDto());
        }
    }
}
