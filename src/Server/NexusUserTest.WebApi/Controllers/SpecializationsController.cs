using Microsoft.AspNetCore.Mvc;
using NexusUserTest.Application.Services;
using NexusUserTest.Common.DTOs;
using SibCCSPETest.WebApi.MappingProfiles;

namespace SibCCSPETest.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpecializationsController(IRepoServiceManager service) : ControllerBase
    {
        private readonly IRepoServiceManager _service = service;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SpecializationDTO>>> GetAll(string? include = null)
        {
            var specializations = await _service.SpecializationRepository.GetAllSpecializationAsync(includeProperties: include);
            return Ok(specializations.ToDto());
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<SpecializationDTO>> Get(int id, string? include = null)
        {
            var specialization = await _service.SpecializationRepository.GetSpecializationAsync(s => s.Id == id, include);
            if (specialization == null)
                return NotFound(new { Message = $"Специализация с id {id} не найдена." });
            return Ok(specialization.ToDto());
        }

        [HttpPost]
        public async Task<ActionResult<SpecializationDTO>> Add(SpecializationCreateDTO specializationCreateDTO, string? include = null)
        {
            if (specializationCreateDTO == null)
                return BadRequest("Данные для добавления специализации пустые.");
            var specialization = specializationCreateDTO.ToEntity();
            await _service.SpecializationRepository.AddSpecializationAsync(specialization, include);
            var specializationDTO = specialization.ToDto();
            return CreatedAtAction(nameof(Get), new { id = specializationDTO.Id }, specializationDTO);
        }

        [HttpPut]
        public async Task<ActionResult<SpecializationDTO>> Update(SpecializationDTO specializationDTO, string? include = null)
        {
            if (specializationDTO == null)
                return BadRequest("Данные для обновления специализации пустые.");
            var specialization = await _service.SpecializationRepository.GetSpecializationAsync(s => s.Id == specializationDTO.Id, include);
            if (specialization == null)
                return NotFound(new { Message = $"Специализация с id {specializationDTO.Id} не найдена." });
            specialization.UpdateFromDto(specializationDTO);
            await _service.SpecializationRepository.UpdateSpecialization(specialization, include);
            return Ok(specialization.ToDto());
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            var specialization = await _service.SpecializationRepository.GetSpecializationAsync(s => s.Id == id);
            if (specialization == null)
                return NotFound(new { Message = $"Специализация с id {id} не найдена." });
            var result = _service.SpecializationRepository.DeleteSpecialization(specialization);
            return Ok(result);
        }
    }
}
