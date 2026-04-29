using Microsoft.AspNetCore.Mvc;
using NexusUserTest.Application.Common;
using NexusUserTest.Common;
using SibCCSPETest.WebApi.MappingProfiles;

namespace SibCCSPETest.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TopicQuestionsController(ISpecializationService service) : ControllerBase
    {
        [HttpGet("specialization/{id:int}")]
        public async Task<ActionResult<IEnumerable<QuestionTestDTO>>> GetAllQuestionTest(int id, string? include = null)
        {
            //var specialization = await service.GetSpecializationByIdAsync(id);
            //return Ok(specialization.Topics.ToTestDto());
            return NoContent();
        }
    }
}
