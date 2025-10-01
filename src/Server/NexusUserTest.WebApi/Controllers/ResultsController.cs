using Microsoft.AspNetCore.Mvc;
using NexusUserTest.Application.Mappings;
using NexusUserTest.Application.Services;
using NexusUserTest.Common;

namespace NexusUserTest.WebApi
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResultsController(IRepoServiceManager service) : ControllerBase
    {
        private readonly IRepoServiceManager _service = service;

        [HttpGet("groupuser/{id:int}/info")]
        public async Task<ActionResult<IEnumerable<ResultInfoAdminDTO>>> GetInfoAll(int id, string? include = null)
        {
            var results = await _service.ResultRepository.GetAllResultAsync(r => r.GroupUserId == id, includeProperties: include);
            return Ok(results.ToInfoAdminDto());
        }

        [HttpGet("groupuser/{id:int}/test")]
        public async Task<ActionResult<IEnumerable<ResultTestDTO>>> GetTestAll(int id, string? include = null)
        {
            var results = await _service.ResultRepository.GetAllResultAsync(r => r.GroupUserId == id, includeProperties: include);
            return Ok(results.ToTestDto());
        }

        [HttpGet("info/groupuser/{id:int}/test")]
        public async Task<ActionResult<IEnumerable<ResultTestDTO>>> GetTestInfoAll(int id, string? include = null)
        {
            var results = await _service.ResultRepository.GetAllResultAsync(r => r.GroupUserId == id, includeProperties: include);
            return Ok(results.ToTestInfoDto());
        }

        [HttpGet("{id:int}/test")]
        public async Task<ActionResult<ResultTestDTO>> GetTest(int id, string? include = null)
        {
            var result = await _service.ResultRepository.GetResultAsync(a => a.Id == id, include);
            if (result == null)
                return NotFound(new { Message = $"Результат с id {id} не найден." });
            return Ok(result.ToTestDto());
        }

        [HttpPost("test")]
        public async Task<ActionResult<ResultTestDTO>> Add(ResultTestDTO resultTestDTO, string? include = null)
        {
            if (resultTestDTO == null)
                return BadRequest("Данные для добавления результата пустые.");
            var result = resultTestDTO.ToTestEntity();
            await _service.ResultRepository.AddResultAsync(result, include);
            var resultDTO = result.ToTestDto();
            return CreatedAtAction(nameof(GetTest), new { id = resultDTO.Id }, resultDTO);
        }

        [HttpPost("test/batch")]
        public async Task<ActionResult<List<ResultTestDTO>>> Add(IEnumerable<ResultTestDTO> resultTestDTOs, string? include = null)
        {
            if (resultTestDTOs == null)
                return BadRequest("Данные для добавления результатов пустые.");
            var results = resultTestDTOs.ToTestEntity();
            await _service.ResultRepository.AddRangeResultAsync(results, include);
            var resultDTOs = results.ToTestDto();
            return CreatedAtAction(nameof(GetTestAll), new { id = resultDTOs.First().GroupUserId }, resultDTOs);
        }

        [HttpPut("test")]
        public async Task<ActionResult<ResultTestDTO>> Update(ResultTestDTO resultDTO, string? include = null)
        {
            if (resultDTO == null)
                return BadRequest("Данные для обновления результата пустые.");
            var result = await _service.ResultRepository.GetResultAsync(r => r.Id == resultDTO.Id, include);
            if (result == null)
                return NotFound(new { Message = $"Результат с id {resultDTO.Id} не найден." });
            result.UpdateFromDto(resultDTO);
            await _service.ResultRepository.UpdateResultAsync(result, include);
            return Ok(result.ToTestDto());
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var results = await _service.ResultRepository.GetAllResultAsync(r => r.GroupUserId == id);
            if (results == null)
                return NotFound(new { Message = $"Результаты с id пользователя группы {id} не найдены." });
            await _service.ResultRepository.DeleteResultAsync([.. results]);
            return NoContent();
        }
    }
}
