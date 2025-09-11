using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NexusUserTest.Application.Mappings;
using NexusUserTest.Application.Services;
using NexusUserTest.Common.DTOs;

namespace SibCCSPETest.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupsController(IRepoServiceManager service, IMapper mapper) : ControllerBase
    {
        private readonly IRepoServiceManager _service = service;
        private readonly IMapper _mapper = mapper;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GroupDTO>>> GetAll(string? include = null)
        {
            var groups = await _service.GroupRepository.GetAllGroupAsync(includeProperties: include);
            return Ok(groups.ToDto());
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<GroupDTO>> Get(int id, string? include = null)
        {
            var group = await _service.GroupRepository.GetGroupAsync(g => g.Id == id, include);
            if (group == null)
                return NotFound(new { Message = $"Группа с id {id} не найдена." });
            return Ok(group.ToDto());
        }

        [HttpPost]
        public async Task<ActionResult<GroupDTO>> Add(GroupCreateDTO groupCreateDTO, string? include = null)
        {
            if (groupCreateDTO == null)
                return BadRequest("Данные для добавления группы пустые.");
            var group = groupCreateDTO.ToEntity();
            await _service.GroupRepository.AddGroupAsync(group, include);
            var groupDTO = group.ToDto();
            return CreatedAtAction(nameof(Get), new { id = groupDTO.Id }, groupDTO);
        }

        [HttpPut]
        public async Task<ActionResult<GroupDTO>> Update(GroupDTO groupDTO, string? include = null)
        {
            if (groupDTO == null)
                return BadRequest("Данные для обновления группы пустые.");
            var group = await _service.GroupRepository.GetGroupAsync(g => g.Id == groupDTO.Id, include);
            if (group == null)
                return NotFound(new { Message = $"Группа с id {groupDTO.Id} не найдена." });
            group.UpdateFromDto(groupDTO);
            await _service.GroupRepository.UpdateGroup(group, include);
            return Ok(group.ToDto());
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
