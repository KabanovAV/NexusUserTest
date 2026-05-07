//using Microsoft.AspNetCore.Mvc;
//using NexusUserTest.Application.Common;
//using NexusUserTest.Application.Mappings;
//using NexusUserTest.Common;
//using SibCCSPETest.WebApi.MappingProfiles;

//namespace SibCCSPETest.WebApi.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class TopicsController(ITopicService service) : ControllerBase
//    {
//        [HttpGet]
//        public async Task<ActionResult<IEnumerable<TopicDTO>>> GetAllTopic(string? include = null)
//        {
//            var topics = await service.GetAllTopicAsync();
//            return Ok(topics.ToDto());
//        }

//        [HttpGet("{id:int}")]
//        public async Task<ActionResult<TopicDTO>> GetTopic(int id, string? include = null)
//        {
//            var topic = await service.GetTopicByIdAsync(id);
//            if (topic == null)
//                return NotFound(new { Message = $"Тема с id {id} не найдена." });
//            return Ok(topic.ToDto());
//        }

//        [HttpGet("select")]
//        public async Task<ActionResult<IEnumerable<SelectItem>>> GetSelect()
//        {
//            var topics = await service.GetAllTopicAsync();
//            return Ok(topics.ToSelect());
//        }

//        [HttpPost]
//        public async Task<ActionResult<TopicDTO>> AddTopic(TopicCreateDTO topicCreateDTO, string? include = null)
//        {
//            if (topicCreateDTO == null)
//                return BadRequest("Данные для добавления темы пустые.");
//            var topic = topicCreateDTO.ToEntity();
//            await service.AddTopicAsync(topic);
//            var topicDTO = topic!.ToDto();
//            return CreatedAtAction(nameof(GetTopic), new { id = topicDTO!.Id }, topicDTO);
//        }

//        [HttpPut("{id:int}")]
//        public async Task<IActionResult> UpdateTopic(int id, TopicDTO topicDTO)
//        {
//            if (topicDTO == null)
//                return BadRequest("Данные для обновления темы пустые.");
//            var topic = await service.GetTopicByIdAsync(id);
//            if (topic == null)
//                return NotFound(new { Message = $"Тема с id {topicDTO.Id} не найдена." });
//            topic.UpdateFromDto(topicDTO);
//            await service.UpdateTopicAsync(topic);
//            return NoContent();
//        }

//        [HttpDelete("{id:int}")]
//        public async Task<IActionResult> DeleteTopic(int id)
//        {
//            var topic = await service.GetTopicByIdAsync(id);
//            if (topic == null)
//                return NotFound(new { Message = $"Тема с id {id} не найдена." });
//            await service.DeleteTopicAsync(topic.Id);
//            return NoContent();
//        }
//    }
//}
