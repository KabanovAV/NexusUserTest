using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NexusUserTest.Application.Services;
using NexusUserTest.Common.DTOs;
using NexusUserTest.Domain.Entities;

namespace SibCCSPETest.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TopicsController(IRepoServiceManager service, IMapper mapper) : ControllerBase
    {
        private readonly IRepoServiceManager _service = service;
        private readonly IMapper _mapper = mapper;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TopicDTO>>> GetAll(string? include = null)
        {
            var topics = await _service.TopicRepository.GetAllTopicAsync(includeProperties: include);
            var topicDTOs = _mapper.Map<IEnumerable<TopicDTO>>(topics);
            return Ok(topicDTOs);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<TopicDTO>> Get(int id, string? include = null)
        {
            var topic = await _service.TopicRepository.GetTopicAsync(t => t.Id == id, include);
            if (topic == null)
                return NotFound(new { Message = $"Тема с id {id} не найдена." });
            var topicDTO = _mapper.Map<TopicDTO>(topic);
            return Ok(topicDTO);
        }

        [HttpPost]
        public async Task<ActionResult<TopicDTO>> Add(TopicCreateDTO topicCreateDTO, string? include = null)
        {
            if (topicCreateDTO == null)
                return BadRequest("Данные для добавления темы пустые.");
            var topic = _mapper.Map<Topic>(topicCreateDTO);
            await _service.TopicRepository.AddTopicAsync(topic, include);
            var topicDTO = _mapper.Map<TopicDTO>(topic);
            return CreatedAtAction(nameof(Get), new { id = topicDTO.Id }, topicDTO);
        }

        [HttpPut]
        public async Task<ActionResult<TopicDTO>> Update(TopicDTO topicDTO, string? include = null)
        {
            if (topicDTO == null)
                return BadRequest("Данные для обновления темы пустые.");
            var topic = await _service.TopicRepository.GetTopicAsync(t => t.Id == topicDTO.Id, include);
            if (topic == null)
                return NotFound(new { Message = $"Тема с id {topicDTO.Id} не найдена." });
            _mapper.Map(topicDTO, topic);
            await _service.TopicRepository.UpdateTopic(topic, include);
            topicDTO = _mapper.Map<TopicDTO>(topic);
            return Ok(topicDTO);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var topic = await _service.TopicRepository.GetTopicAsync(t => t.Id == id);
            if (topic == null)
                return NotFound(new { Message = $"Тема с id {id} не найдена." });
            _service.TopicRepository.DeleteTopic(topic);
            return NoContent();
        }
    }
}
