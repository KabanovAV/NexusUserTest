using Microsoft.AspNetCore.Mvc;
using NexusUserTest.Application.Mappings;
using NexusUserTest.Application.Services;
using NexusUserTest.Common;

namespace NexusUserTest.WebApi
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnswersController(IRepoServiceManager service) : ControllerBase
    {
        private readonly IRepoServiceManager _service = service;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AnswerAdminDTO>>> GetAllAnswerAdmin(string? include = null)
        {
            var answers = await _service.AnswerRepository.GetAllAnswerAsync(includeProperties: include);
            return Ok(answers.ToDto());
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<AnswerAdminDTO>> GetAnswerAdmin(int id, string? include = null)
        {
            var answer = await _service.AnswerRepository.GetAnswerAsync(a => a.Id == id, include);
            if (answer == null)
                return NotFound(new { Message = $"Ответ с id {id} не найден." });
            return Ok(answer.ToDto());
        }

        [HttpPost]
        public async Task<ActionResult<AnswerAdminDTO>> AddAnswer(AnswerAdminDTO answerCreateDTO, string? include = null)
        {
            if (answerCreateDTO == null)
                return BadRequest("Данные для добавления ответа пустые.");
            var answer = answerCreateDTO.ToEntity();
            await _service.AnswerRepository.AddAnswerAsync(answer!, include);
            var answerDTO = answer!.ToDto();
            return CreatedAtAction(nameof(GetAnswerAdmin), new { id = answerDTO!.Id }, answerDTO);
        }

        [HttpPost("batch")]
        public async Task<ActionResult<IEnumerable<AnswerAdminDTO>>> AddRangeAnswer(IEnumerable<AnswerAdminDTO> answerCreateDTOs, string? include = null)
        {
            if (answerCreateDTOs == null)
                return BadRequest("Данные для добавления ответов пустые.");
            var answers = answerCreateDTOs.ToEntity();
            await _service.AnswerRepository.AddRangeAnswerAsync([.. answers], include);
            var answerDTOs = answers.ToDto();
            return CreatedAtAction(nameof(GetAllAnswerAdmin), answerDTOs);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateAnswer(int id, AnswerAdminDTO answerDTO)
        {
            if (answerDTO == null)
                return BadRequest("Данные для обновления ответа пустые.");
            var answer = await _service.AnswerRepository.GetAnswerAsync(a => a.Id == id);
            if (answer == null)
                return NotFound(new { Message = $"Ответ с id {answerDTO.Id} не найден." });
            answer.UpdateFromDto(answerDTO);
            await _service.AnswerRepository.UpdateAnswerAsync(answer);
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteAnswer(int id)
        {
            var answer = await _service.AnswerRepository.GetAnswerAsync(a => a.Id == id);
            if (answer == null)
                return NotFound(new { Message = $"Ответ с id {id} не найден." });
            await _service.AnswerRepository.DeleteAnswerAsync(answer);
            return NoContent();
        }
    }
}
