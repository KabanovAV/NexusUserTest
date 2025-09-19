using Microsoft.AspNetCore.Mvc;
using NexusUserTest.Application.Mappings;
using NexusUserTest.Application.Services;
using NexusUserTest.Common.DTOs;

namespace SibCCSPETest.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupsController(IRepoServiceManager service) : ControllerBase
    {
        private readonly IRepoServiceManager _service = service;

        [HttpGet("info")]
        public async Task<ActionResult<IEnumerable<GroupInfoDTO>>> GetAllInfo(string? include = null)
        {
            var groups = await _service.GroupRepository.GetAllGroupAsync(includeProperties: include);
            return Ok(groups.ToInfoDto());
        }

        [HttpGet("{id:int}/info")]
        public async Task<ActionResult<GroupInfoDetailsDTO>> GetInfo(int id, string? include = null)
        {
            var group = await _service.GroupRepository.GetGroupAsync(g => g.Id == id, include);
            if (group == null)
                return NotFound(new { Message = $"Группа с id {id} не найдена." });
            return Ok(group.ToInfoDetailsDto());
        }

        [HttpGet("edit")]
        public async Task<ActionResult<IEnumerable<GroupEditDTO>>> GetAllEdit(string? include = null)
        {
            var groups = await _service.GroupRepository.GetAllGroupAsync(includeProperties: include);
            return Ok(groups.ToEditDto());
        }

        [HttpGet("{id:int}/edit")]
        public async Task<ActionResult<GroupEditDTO>> GetEdit(int id, string? include = null)
        {
            var group = await _service.GroupRepository.GetGroupAsync(g => g.Id == id, include);
            if (group == null)
                return NotFound(new { Message = $"Группа с id {id} не найдена." });
            return Ok(group.ToEditDto());
        }

        [HttpPost]
        public async Task<ActionResult<GroupEditDTO>> Add(GroupEditCreateDTO groupCreateDTO, string? include = null)
        {
            if (groupCreateDTO == null)
                return BadRequest("Данные для добавления группы пустые.");
            var group = groupCreateDTO.ToEntity();
            await _service.GroupRepository.AddGroupAsync(group, include);
            var groupDTO = group.ToEditDto();
            return CreatedAtAction(nameof(GetEdit), new { id = groupDTO.Id }, groupDTO);
        }

        [HttpPut]
        public async Task<ActionResult<GroupEditDTO>> Update(GroupEditDTO groupDTO, string? include = null)
        {
            if (groupDTO == null)
                return BadRequest("Данные для обновления группы пустые.");
            var group = await _service.GroupRepository.GetGroupAsync(g => g.Id == groupDTO.Id, include);
            if (group == null)
                return NotFound(new { Message = $"Группа с id {groupDTO.Id} не найдена." });
            group.UpdateFromEditDto(groupDTO);
            await _service.GroupRepository.UpdateGroup(group, include);
            return Ok(group.ToEditDto());
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var group = await _service.GroupRepository.GetGroupAsync(g => g.Id == id);
            if (group == null)
                return NotFound(new { Message = $"Группа с id {id} не найдена." });
            _service.GroupRepository.DeleteGroup(group);
            return NoContent();
        }
    }
}
