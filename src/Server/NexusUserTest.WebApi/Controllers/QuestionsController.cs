using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NexusUserTest.Application.Services;
using NexusUserTest.Common.DTOs;
using NexusUserTest.Domain.Entities;

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
            var questionDTOs = _mapper.Map<IEnumerable<QuestionDTO>>(questions);
            return Ok(questionDTOs);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<QuestionDTO>> Get(int id, string? include = null)
        {
            var question = await _service.QuestionRepository.GetQuestionAsync(q => q.Id == id, include);
            if (question == null)
                return NotFound(new { Message = $"Вопрос с id {id} не найден." });
            var questionDTO = _mapper.Map<QuestionDTO>(question);
            return Ok(questionDTO);
        }

        [HttpPost]
        public async Task<ActionResult<QuestionDTO>> Add(QuestionCreateDTO questionCreateDTO, string? include = null)
        {
            if (questionCreateDTO == null)
                return BadRequest("Данные для добавления вопроса пустые.");
            var question = _mapper.Map<Question>(questionCreateDTO);
            await _service.QuestionRepository.AddQuestionAsync(question, include);
            var questionDTO = _mapper.Map<QuestionDTO>(question);
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
            _mapper.Map(questionDTO, question);
            await _service.QuestionRepository.UpdateQuestion(question, include);
            questionDTO = _mapper.Map<QuestionDTO>(question);
            return Ok(questionDTO);
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
