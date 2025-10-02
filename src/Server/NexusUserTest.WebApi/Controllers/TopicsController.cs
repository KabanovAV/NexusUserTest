using Microsoft.AspNetCore.Mvc;
using NexusUserTest.Application.Mappings;
using NexusUserTest.Application.Services;
using NexusUserTest.Common;
using SibCCSPETest.WebApi.MappingProfiles;

namespace SibCCSPETest.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TopicsController(IRepoServiceManager service) : ControllerBase
    {
        private readonly IRepoServiceManager _service = service;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TopicDTO>>> GetAll(string? include = null)
        {
            var topics = await _service.TopicRepository.GetAllTopicAsync(includeProperties: include);
            return Ok(topics.ToDto());
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<TopicDTO>> Get(int id, string? include = null)
        {
            var topic = await _service.TopicRepository.GetTopicAsync(t => t.Id == id, include);
            if (topic == null)
                return NotFound(new { Message = $"Тема с id {id} не найдена." });
            return Ok(topic.ToDto());
        }

        [HttpGet("select")]
        public async Task<ActionResult<IEnumerable<SelectItem>>> GetSelect()
        {
            var topics = await _service.TopicRepository.GetAllTopicAsync();
            return Ok(topics.ToSelect());
        }

        [HttpPost]
        public async Task<ActionResult<TopicDTO>> Add(TopicCreateDTO topicCreateDTO, string? include = null)
        {
            if (topicCreateDTO == null)
                return BadRequest("Данные для добавления темы пустые.");
            var topic = topicCreateDTO.ToEntity();
            await _service.TopicRepository.AddTopicAsync(topic!, include);
            var topicDTO = topic!.ToDto();
            return CreatedAtAction(nameof(Get), new { id = topicDTO!.Id }, topicDTO);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, TopicDTO topicDTO)
        {
            if (topicDTO == null)
                return BadRequest("Данные для обновления темы пустые.");
            var topic = await _service.TopicRepository.GetTopicAsync(t => t.Id == id);
            if (topic == null)
                return NotFound(new { Message = $"Тема с id {topicDTO.Id} не найдена." });
            topic.UpdateFromDto(topicDTO);
            await _service.TopicRepository.UpdateTopicAsync(topic);
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var topic = await _service.TopicRepository.GetTopicAsync(t => t.Id == id);
            if (topic == null)
                return NotFound(new { Message = $"Тема с id {id} не найдена." });
            await _service.TopicRepository.DeleteTopicAsync(topic);
            return NoContent();
        }
    }
}
