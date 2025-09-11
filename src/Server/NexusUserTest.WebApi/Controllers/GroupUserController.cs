using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NexusUserTest.Application.Services;
using NexusUserTest.Common.DTOs;

namespace SibCCSPETest.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupUserController(IRepoServiceManager service, IMapper mapper) : ControllerBase
    {
        private readonly IRepoServiceManager _service = service;
        private readonly IMapper _mapper = mapper;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GroupUserDTO>>> GetAll(string? include = null)
        {
            var groupUser = await _service.GroupUserRepository.GetAllGroupUserAsync(includeProperties: include);
            var groupUserDTOs = _mapper.Map<IEnumerable<GroupUserDTO>>(groupUser);
            return Ok(groupUserDTOs);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<GroupUserDTO>> Get(int id, string? include = null)
        {
            var groupUser = await _service.GroupUserRepository.GetGroupUserAsync(gu => gu.Id == id, include);
            if (groupUser == null)
                return NotFound(new { Message = $"Пользователя в группе с id {id} не найден." });
            var groupUserDTO = _mapper.Map<GroupUserDTO>(groupUser);
            return Ok(groupUserDTO);
        }        
    }
}
