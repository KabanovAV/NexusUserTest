using Microsoft.AspNetCore.Mvc;
using NexusUserTest.Application.Common;
using NexusUserTest.Common;
using SibCCSPETest.WebApi.MappingProfiles;

namespace SibCCSPETest.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpecializationsController(ISpecializationService service) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SpecializationDTO>>> GetAllSpecialization([FromQuery] string? include = null)
        {
            var specializations = await service.GetAllSpecializationAsync();
            return Ok(specializations.ToDto());
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<SpecializationDTO>> GetSpecialization(int id, [FromQuery] string? include = null)
        {
            var specialization = await service.GetSpecializationByIdAsync(id);
            if (specialization == null)
                return NotFound(new { Message = $"Специализация с id {id} не найдена." });
            return Ok(specialization.ToDto());
        }

        [HttpGet("select")]
        public async Task<ActionResult<IEnumerable<SelectItem>>> GetSelect()
        {
            var specializations = await service.GetAllSpecializationAsync();
            return Ok(specializations.ToSelect());
        }

        [HttpPost]
        public async Task<ActionResult<SpecializationDTO>> AddSpecialization(SpecializationDTO specializationCreateDTO, [FromQuery] string? include = null)
        {
            if (specializationCreateDTO == null)
                return BadRequest("Данные для добавления специализации пустые.");
            var specialization = specializationCreateDTO.ToEntity();
            await service.AddSpecializationAsync(specialization);
            var specializationDTO = specialization!.ToDto();
            return CreatedAtAction(nameof(GetSpecialization), new { id = specializationDTO!.Id }, specializationDTO);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateSpecialization(int id, SpecializationDTO specializationDTO)
        {
            if (specializationDTO == null)
                return BadRequest("Данные для обновления специализации пустые.");
            var specialization = await service.GetSpecializationByIdAsync(id);
            if (specialization == null)
                return NotFound(new { Message = $"Специализация с id {specializationDTO.Id} не найдена." });
            specialization.UpdateFromDto(specializationDTO);
            await service.UpdateSpecializationAsync(specialization);
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<bool>> DeleteSpecialization(int id)
        {
            //var specialization = await _service.SpecializationRepository.GetSpecializationAsync(s => s.Id == id, "Groups,Topics");
            var specialization = await service.GetSpecializationByIdAsync(id);
            if (specialization == null)
                return NotFound(new { Message = $"Специализация с id {id} не найдена." });
            //var result = await service.DeleteSpecializationAsync(specialization.Id);
            //return Ok(result);
            await service.DeleteSpecializationAsync(specialization.Id);
            return NoContent();
        }
    }
}
