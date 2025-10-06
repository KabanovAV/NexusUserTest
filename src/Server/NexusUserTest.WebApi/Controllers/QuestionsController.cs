using Microsoft.AspNetCore.Mvc;
using NexusUserTest.Application.Services;
using NexusUserTest.Common;
using SibCCSPETest.WebApi.MappingProfiles;

namespace SibCCSPETest.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionsController(IRepoServiceManager service) : ControllerBase
    {
        private readonly IRepoServiceManager _service = service;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<QuestionAdminDTO>>> GetAllQuestionAdmin(string? include = null)
        {
            var questions = await _service.QuestionRepository.GetAllQuestionAsync(includeProperties: include);
            return Ok(questions.ToAdminDto());
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<QuestionAdminDTO>> GetQuestionAdmin(int id, string? include = null)
        {
            var question = await _service.QuestionRepository.GetQuestionAsync(q => q.Id == id, include);
            if (question == null)
                return NotFound(new { Message = $"Вопрос с id {id} не найден." });
            return Ok(question.ToAdminDto());
        }

        [HttpPost]
        public async Task<ActionResult<QuestionAdminDTO>> AddQuestion(QuestionAdminDTO questionCreateDTO, string? include = null)
        {
            if (questionCreateDTO == null)
                return BadRequest("Данные для добавления вопроса пустые.");
            var question = questionCreateDTO.ToEntity();
            await _service.QuestionRepository.AddQuestionAsync(question!, include);
            var questionDTO = question!.ToAdminDto();
            return CreatedAtAction(nameof(GetQuestionAdmin), new { id = questionDTO!.Id }, questionDTO);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateQuestion(int id, QuestionAdminDTO questionDTO, string? include = null)
        {
            if (questionDTO == null)
                return BadRequest("Данные для обновления вопроса пустые.");
            var question = await _service.QuestionRepository.GetQuestionAsync(q => q.Id == id, include);
            if (question == null)
                return NotFound(new { Message = $"Вопрос с id {questionDTO.Id} не найден." });
            question.UpdateFromDto(questionDTO);
            await _service.QuestionRepository.UpdateQuestionAsync(question, include);
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteQuestion(int id)
        {
            var question = await _service.QuestionRepository.GetQuestionAsync(q => q.Id == id);
            if (question == null)
                return NotFound(new { Message = $"Вопрос с id {id} не найден." });
            await _service.QuestionRepository.DeleteQuestionAsync(question);
            return NoContent();
        }
    }
}
