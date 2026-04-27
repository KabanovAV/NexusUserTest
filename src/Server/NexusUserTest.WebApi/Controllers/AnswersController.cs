using Microsoft.AspNetCore.Mvc;
using NexusUserTest.Application.Common;
using NexusUserTest.Application.Mappings;
using NexusUserTest.Common;

namespace NexusUserTest.WebApi
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnswersController(IAnswerService service) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AnswerAdminDTO>>> GetAllAnswerAdmin()
        {
            var answers = await service.GetAllAnswerAsync();
            return Ok(answers.ToDto());
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<AnswerAdminDTO>> GetAnswerAdmin(int id)
        {
            var answer = await service.GetAnswerByIdAsync(id);
            if (answer == null)
                return NotFound(new { Message = $"Ответ с id {id} не найден." });
            return Ok(answer.ToDto());
        }

        [HttpPost]
        public async Task<ActionResult<AnswerAdminDTO>> AddAnswer(AnswerAdminDTO answerCreateDTO)
        {
            if (answerCreateDTO == null)
                return BadRequest("Данные для добавления ответа пустые.");
            var answer = answerCreateDTO.ToEntity();
            await service.AddAnswerAsync(answer);
            var answerDTO = answer!.ToDto();
            return CreatedAtAction(nameof(GetAnswerAdmin), new { id = answerDTO!.Id }, answerDTO);
        }

        [HttpPost("batch")]
        public async Task<ActionResult<IEnumerable<AnswerAdminDTO>>> AddRangeAnswer(IEnumerable<AnswerAdminDTO> answerCreateDTOs)
        {
            if (answerCreateDTOs == null)
                return BadRequest("Данные для добавления ответов пустые.");
            var answers = answerCreateDTOs.ToEntity();
            await service.AddRangeAnswerAsync([.. answers]);
            var answerDTOs = answers.ToDto();
            return CreatedAtAction(nameof(GetAllAnswerAdmin), answerDTOs);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateAnswer(int id, AnswerAdminDTO answerDTO)
        {
            if (answerDTO == null)
                return BadRequest("Данные для обновления ответа пустые.");
            var answer = await service.GetAnswerByIdAsync(id);
            if (answer == null)
                return NotFound(new { Message = $"Ответ с id {answerDTO.Id} не найден." });
            answer.UpdateFromDto(answerDTO);
            await service.UpdateAnswerAsync(answer);
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteAnswer(int id)
        {
            var answer = await service.GetAnswerByIdAsync(id);
            if (answer == null)
                return NotFound(new { Message = $"Ответ с id {id} не найден." });
            await service.DeleteAnswerAsync(answer.Id);
            return NoContent();
        }
    }
}
