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

        [HttpGet("groupuser/{groupUserId:int}/info")]
        public async Task<ActionResult<IEnumerable<ResultInfoAdminDTO>>> GetAllResultInfoAdmin(int groupUserId, string? include = null)
        {
            var results = await _service.ResultRepository.GetAllResultAsync(r => r.GroupUserId == groupUserId, includeProperties: include);
            return Ok(results.ToInfoAdminDto());
        }

        [HttpGet("groupuser/{groupUserId:int}/test-info")]
        public async Task<ActionResult<IEnumerable<ResultInfoTestDTO>>> GetAllResultInfoTest(int groupUserId, string? include = null)
        {
            var results = await _service.ResultRepository.GetAllResultAsync(r => r.GroupUserId == groupUserId, includeProperties: include);
            return Ok(results.ToTestInfoDto());
        }

        [HttpGet("groupuser/{groupUserId:int}/test")]
        public async Task<ActionResult<IEnumerable<ResultTestDTO>>> GetAllResultTest(int groupUserId, string? include = null)
        {
            var results = await _service.ResultRepository.GetAllResultAsync(r => r.GroupUserId == groupUserId, includeProperties: include);
            return Ok(results.ToTestDto());
        }        

        [HttpGet("{id:int}/test")]
        public async Task<ActionResult<ResultTestDTO>> GetResultTest(int id, string? include = null)
        {
            var result = await _service.ResultRepository.GetResultAsync(a => a.Id == id, include);
            if (result == null)
                return NotFound(new { Message = $"Результат с id {id} не найден." });
            return Ok(result.ToTestDto());
        }

        [HttpPost("test")]
        public async Task<ActionResult<ResultTestDTO>> AddResult(ResultTestDTO resultTestDTO, string? include = null)
        {
            if (resultTestDTO == null)
                return BadRequest("Данные для добавления результата пустые.");
            var result = resultTestDTO.ToTestEntity();
            await _service.ResultRepository.AddResultAsync(result, include);
            var resultDTO = result.ToTestDto();
            return CreatedAtAction(nameof(GetResultTest), new { id = resultDTO.Id }, resultDTO);
        }

        [HttpPost("test/batch")]
        public async Task<ActionResult<List<ResultTestDTO>>> AddRangeResult(IEnumerable<ResultTestDTO> resultTestDTOs, string? include = null)
        {
            if (resultTestDTOs == null)
                return BadRequest("Данные для добавления результатов пустые.");
            var results = resultTestDTOs.ToTestEntity();
            await _service.ResultRepository.AddRangeResultAsync(results, include);
            var resultDTOs = results.ToTestDto();
            return CreatedAtAction(nameof(GetAllResultTest), new { groupUserId = resultDTOs.First().GroupUserId }, resultDTOs);
        }

        [HttpPatch("{id:int}/test")]
        public async Task<IActionResult> UpdateResult(int id, ResultTestDTO resultDTO)
        {
            if (resultDTO == null)
                return BadRequest("Данные для обновления результата пустые.");
            var result = await _service.ResultRepository.GetResultAsync(r => r.Id == id);
            if (result == null)
                return NotFound(new { Message = $"Результат с id {resultDTO.Id} не найден." });
            result.UpdateFromDto(resultDTO);
            await _service.ResultRepository.UpdateResultAsync(result);
            return NoContent();
        }

        [HttpDelete("{groupUserId:int}")]
        public async Task<IActionResult> DeleteResult(int groupUserId)
        {
            var results = await _service.ResultRepository.GetAllResultAsync(r => r.GroupUserId == groupUserId);
            if (results == null)
                return NotFound(new { Message = $"Результаты с id пользователя группы {groupUserId} не найдены." });
            await _service.ResultRepository.DeleteResultAsync([.. results]);
            return NoContent();
        }
    }
}
