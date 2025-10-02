using Microsoft.AspNetCore.Mvc;
using NexusUserTest.Application.Services;
using NexusUserTest.Common;
using SibCCSPETest.WebApi.MappingProfiles;

namespace SibCCSPETest.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpecializationsController(IRepoServiceManager service) : ControllerBase
    {
        private readonly IRepoServiceManager _service = service;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SpecializationDTO>>> GetAll([FromQuery] string? include = null)
        {
            var specializations = await _service.SpecializationRepository.GetAllSpecializationAsync(includeProperties: include);
            return Ok(specializations.ToDto());
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<SpecializationDTO>> Get(int id, [FromQuery] string? include = null)
        {
            var specialization = await _service.SpecializationRepository.GetSpecializationAsync(s => s.Id == id, include);
            if (specialization == null)
                return NotFound(new { Message = $"Специализация с id {id} не найдена." });
            return Ok(specialization.ToDto());
        }

        [HttpGet("select")]
        public async Task<ActionResult<IEnumerable<SelectItem>>> GetSelect()
        {
            var specializations = await _service.SpecializationRepository.GetAllSpecializationAsync();
            return Ok(specializations.ToSelect());
        }

        [HttpPost]
        public async Task<ActionResult<SpecializationDTO>> Add(SpecializationDTO specializationCreateDTO, [FromQuery] string? include = null)
        {
            if (specializationCreateDTO == null)
                return BadRequest("Данные для добавления специализации пустые.");
            var specialization = specializationCreateDTO.ToEntity();
            await _service.SpecializationRepository.AddSpecializationAsync(specialization!, include);
            var specializationDTO = specialization!.ToDto();
            return CreatedAtAction(nameof(Get), new { id = specializationDTO!.Id }, specializationDTO);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, SpecializationDTO specializationDTO)
        {
            if (specializationDTO == null)
                return BadRequest("Данные для обновления специализации пустые.");
            var specialization = await _service.SpecializationRepository.GetSpecializationAsync(s => s.Id == id);
            if (specialization == null)
                return NotFound(new { Message = $"Специализация с id {specializationDTO.Id} не найдена." });
            specialization.UpdateFromDto(specializationDTO);
            await _service.SpecializationRepository.UpdateSpecializationAsync(specialization);
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            var specialization = await _service.SpecializationRepository.GetSpecializationAsync(s => s.Id == id, "Groups,Topics");
            if (specialization == null)
                return NotFound(new { Message = $"Специализация с id {id} не найдена." });
            var result = await _service.SpecializationRepository.DeleteSpecializationAsync(specialization);
            return Ok(result);
        }
    }
}
