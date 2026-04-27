using Microsoft.AspNetCore.Mvc;
using NexusUserTest.Application.Common;
using NexusUserTest.Application.Mappings;
using NexusUserTest.Common;
using SibCCSPETest.WebApi.MappingProfiles;

namespace SibCCSPETest.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupsController(IGroupService service) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GroupDTO>>> GetAllGroup()
        {
            var groups = await service.GetAllGroupAsync();
            return Ok(groups.ToDto());
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<GroupDTO>> GetGroup(int id)
        {
            var group = await service.GetGroupByIdAsync(id);
            if (group == null)
                return NotFound(new { Message = $"Группа с id {id} не найдена." });
            return Ok(group.ToDto());
        }

        [HttpGet("info")]
        public async Task<ActionResult<IEnumerable<GroupInfoDTO>>> GetAllGroupInfo()
        {
            var groups = await service.GetAllGroupAsync();
            return Ok(groups.ToInfoDto());
        }

        [HttpGet("{id:int}/info")]
        public async Task<ActionResult<GroupInfoDetailsDTO>> GetGroupInfoDetails(int id)
        {
            var group = await service.GetGroupByIdAsync(id);
            if (group == null)
                return NotFound(new { Message = $"Группа с id {id} не найдена." });
            return Ok(group.ToInfoDetailDto());
        }        

        [HttpGet("select")]
        public async Task<ActionResult<IEnumerable<SelectItem>>> GetSelect()
        {
            var groups = await service.GetAllGroupAsync();
            return Ok(groups.ToSelect());
        }

        [HttpPost]
        public async Task<ActionResult<GroupDTO>> AddGroup(GroupDTO groupEditDTO)
        {
            if (groupEditDTO == null)
                return BadRequest("Данные для добавления группы пустые.");
            var group = groupEditDTO.ToEntity();
            await service.AddGroupAsync(group);
            var groupDTO = group!.ToDto();
            return CreatedAtAction(nameof(GetGroup), new { id = groupDTO!.Id }, groupDTO);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateGroup(int id, GroupDTO groupDTO)
        {
            if (groupDTO == null)
                return BadRequest("Данные для обновления группы пустые.");
            var group = await service.GetGroupByIdAsync(id);
            if (group == null)
                return NotFound(new { Message = $"Группа с id {groupDTO.Id} не найдена." });
            group.UpdateFromDto(groupDTO);
            await service.UpdateGroupAsync(group);
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteGroup(int id)
        {
            var group = await service.GetGroupByIdAsync(id);
            if (group == null)
                return NotFound(new { Message = $"Группа с id {id} не найдена." });
            await service.DeleteGroupAsync(group.Id);
            return NoContent();
        }
    }
}
