using Microsoft.AspNetCore.Mvc;
using NexusUserTest.Application.Mappings;
using NexusUserTest.Application.Services;
using NexusUserTest.Common;
using SibCCSPETest.WebApi.MappingProfiles;

namespace SibCCSPETest.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupsController(IRepoServiceManager service) : ControllerBase
    {
        private readonly IRepoServiceManager _service = service;

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? include = null)
        {
            var groups = await _service.GroupRepository.GetAllGroupAsync(includeProperties: include);
            return Ok(groups.ToDto());
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id, [FromQuery] string? include = null)
        {
            var group = await _service.GroupRepository.GetGroupAsync(g => g.Id == id, include);
            if (group == null)
                return NotFound(new { Message = $"Группа с id {id} не найдена." });
            return Ok(group.ToDto());
        }

        [HttpGet("info")]
        public async Task<IActionResult> GetAllInfo([FromQuery] string? include = null)
        {
            var groups = await _service.GroupRepository.GetAllGroupAsync(includeProperties: include);
            return Ok(groups.ToInfoDto());
        }

        [HttpGet("{id:int}/info")]
        public async Task<IActionResult> GetInfo(int id, [FromQuery] string? include = null)
        {
            var group = await _service.GroupRepository.GetGroupAsync(g => g.Id == id, include);
            if (group == null)
                return NotFound(new { Message = $"Группа с id {id} не найдена." });
            return Ok(group.ToInfoDetailDto());
        }        

        [HttpGet("select")]
        public async Task<ActionResult<IEnumerable<SelectItem>>> GetSelect([FromQuery] string? include = null)
        {
            var groups = await _service.GroupRepository.GetAllGroupAsync(includeProperties: include);
            return Ok(groups.ToSelect());
        }

        [HttpPost]
        public async Task<ActionResult<GroupDTO>> Add(GroupDTO groupEditDTO, [FromQuery] string? include = null)
        {
            if (groupEditDTO == null)
                return BadRequest("Данные для добавления группы пустые.");
            var group = groupEditDTO.ToEntity();
            await _service.GroupRepository.AddGroupAsync(group!, include);
            var groupDTO = group!.ToDto();
            return CreatedAtAction(nameof(Get), new { id = groupDTO!.Id }, groupDTO);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, GroupDTO groupDTO)
        {
            if (groupDTO == null)
                return BadRequest("Данные для обновления группы пустые.");
            var group = await _service.GroupRepository.GetGroupAsync(g => g.Id == id);
            if (group == null)
                return NotFound(new { Message = $"Группа с id {groupDTO.Id} не найдена." });
            group.UpdateFromDto(groupDTO);
            await _service.GroupRepository.UpdateGroupAsync(group);
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var group = await _service.GroupRepository.GetGroupAsync(g => g.Id == id);
            if (group == null)
                return NotFound(new { Message = $"Группа с id {id} не найдена." });
            await _service.GroupRepository.DeleteGroupAsync(group);
            return NoContent();
        }
    }
}
