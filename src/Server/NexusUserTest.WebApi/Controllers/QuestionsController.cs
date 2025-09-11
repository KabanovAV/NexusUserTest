using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NexusUserTest.Application.Services;
using NexusUserTest.Common.DTOs;
using SibCCSPETest.WebApi.MappingProfiles;

namespace SibCCSPETest.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionsController(IRepoServiceManager service, IMapper mapper) : ControllerBase
    {
        private readonly IRepoServiceManager _service = service;
        private readonly IMapper _mapper = mapper;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<QuestionDTO>>> GetAll(string? include = null)
        {
            var questions = await _service.QuestionRepository.GetAllQuestionAsync(includeProperties: include);
            return Ok(questions.ToDto());
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<QuestionDTO>> Get(int id, string? include = null)
        {
            var question = await _service.QuestionRepository.GetQuestionAsync(q => q.Id == id, include);
            if (question == null)
                return NotFound(new { Message = $"Вопрос с id {id} не найден." });
            return Ok(question.ToDto());
        }

        [HttpPost]
        public async Task<ActionResult<QuestionDTO>> Add(QuestionCreateDTO questionCreateDTO, string? include = null)
        {
            if (questionCreateDTO == null)
                return BadRequest("Данные для добавления вопроса пустые.");
            var question = questionCreateDTO.ToEntity();
            await _service.QuestionRepository.AddQuestionAsync(question, include);
            var questionDTO = question.ToDto();
            return CreatedAtAction(nameof(Get), new { id = questionDTO.Id }, questionDTO);
        }

        [HttpPut]
        public async Task<ActionResult<QuestionDTO>> Update(QuestionDTO questionDTO, string? include = null)
        {
            if (questionDTO == null)
                return BadRequest("Данные для обновления вопроса пустые.");
            var question = await _service.QuestionRepository.GetQuestionAsync(q => q.Id == questionDTO.Id, include);
            if (question == null)
                return NotFound(new { Message = $"Вопрос с id {questionDTO.Id} не найден." });
            question.UpdateFromDto(questionDTO);
            await _service.QuestionRepository.UpdateQuestion(question, include);
            return Ok(question.ToDto());
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var question = await _service.QuestionRepository.GetQuestionAsync(q => q.Id == id);
            if (question == null)
                return NotFound(new { Message = $"Вопрос с id {id} не найден." });
            _service.QuestionRepository.DeleteQuestion(question);
            return NoContent();
        }
    }
}
