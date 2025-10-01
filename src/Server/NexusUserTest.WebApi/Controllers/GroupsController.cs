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
        public async Task<IActionResult> GetAll([FromQuery] string view = "info", [FromQuery] string? include = null)
        {
            var groups = await _service.GroupRepository.GetAllGroupAsync(includeProperties: include);
            return view.ToLower() switch
            {
                "edit" => Ok(groups.ToEditDto()),
                _ => Ok(groups.ToInfoDto())
            };
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id, [FromQuery] string view = "detailed", [FromQuery] string? include = null)
        {
            var group = await _service.GroupRepository.GetGroupAsync(g => g.Id == id, include);
            if (group == null)
                return NotFound(new { Message = $"Группа с id {id} не найдена." });
            return view.ToLower() switch
            {
                "edit" => Ok(group.ToEditDto()),
                _ => Ok(group.ToInfoDetailDto())
            };
        }

        [HttpGet("select")]
        public async Task<ActionResult<IEnumerable<SelectItem>>> GetSelect([FromQuery] string? include = null)
        {
            var groups = await _service.GroupRepository.GetAllGroupAsync(includeProperties: include);
            return Ok(groups.ToSelect());
        }

        [HttpPost]
        public async Task<ActionResult<GroupEditDTO>> Add(GroupEditDTO groupEditDTO, [FromQuery] string? include = null)
        {
            if (groupEditDTO == null)
                return BadRequest("Данные для добавления группы пустые.");
            var group = groupEditDTO.ToEntity();
            await _service.GroupRepository.AddGroupAsync(group!, include);
            var groupDTO = group!.ToEditDto();
            return CreatedAtAction(nameof(Get), new { id = groupDTO!.Id, view = "edit" }, groupDTO);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<GroupEditDTO>> Update(int id, GroupEditDTO groupDTO, [FromQuery] string? include = null)
        {
            if (groupDTO == null)
                return BadRequest("Данные для обновления группы пустые.");
            var group = await _service.GroupRepository.GetGroupAsync(g => g.Id == id, include);
            if (group == null)
                return NotFound(new { Message = $"Группа с id {groupDTO.Id} не найдена." });
            group.UpdateFromEditDto(groupDTO);
            await _service.GroupRepository.UpdateGroupAsync(group, include);
            return Ok(group.ToEditDto());
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
