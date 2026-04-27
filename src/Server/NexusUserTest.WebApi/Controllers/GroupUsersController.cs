using Microsoft.AspNetCore.Mvc;
using NexusUserTest.Application.Common;
using NexusUserTest.Application.Mappings;
using NexusUserTest.Common;
using SibCCSPETest.WebApi.MappingProfiles;

namespace SibCCSPETest.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupUsersController(IGroupUserService service) : ControllerBase
    {
        [HttpGet("group/{id:int}/info")]
        public async Task<ActionResult<IEnumerable<GroupUserInfoAdminDTO>>> GetAllGroupUserInfoAdmin(int id)
        {
            //var groupUser = await _service.GroupUserRepository.GetAllGroupUserAsync(gu => gu.GroupId == id, include);
            var groupUser = await service.GetAllGroupUserAsync();
            return Ok(groupUser.ToInfoAdminDto());
        }

        [HttpGet("{id:int}/info")]
        public async Task<ActionResult<GroupUserInfoAdminDTO>> GetGroupUserInfoAdmin(int id)
        {
            var groupUser = await service.GetGroupUserByIdAsync(id);
            if (groupUser == null)
                return NotFound(new { Message = $"Пользователя в группе с id {id} не найден." });
            return Ok(groupUser.ToInfoAdminDto());
        }

        [HttpGet("{id:int}/test")]
        public async Task<ActionResult<GroupUserTestDTO>> GetGroupUserTest(int id)
        {
            var groupUser = await service.GetGroupUserByIdAsync(id);
            if (groupUser == null)
                return NotFound(new { Message = $"Пользователя в группе с id {id} не найден." });
            return Ok(groupUser.ToTestDto());
        }

        [HttpPatch("{id:int}")]
        public async Task<IActionResult> UpdateGroupUser(int id, GroupUserUpdateDTO groupUserUpdateDTO)
        {
            if (groupUserUpdateDTO == null)
                return BadRequest("Данные для обновления группыльзователя пустые.");
            var groupUser = await service.GetGroupUserByIdAsync(id);
            if (groupUser == null)
                return NotFound(new { Message = $"Группа пользователя с id {id} не найдена." });
            groupUser.UpdateFromDto(groupUserUpdateDTO);
            await service.UpdateGroupUserAsync(groupUser);
            return NoContent();
        }
    }
}
