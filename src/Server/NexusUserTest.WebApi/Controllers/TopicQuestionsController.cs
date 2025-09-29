using Microsoft.AspNetCore.Mvc;
using NexusUserTest.Application.Services;
using NexusUserTest.Common.DTOs;
using SibCCSPETest.WebApi.MappingProfiles;

namespace SibCCSPETest.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TopicQuestionsController(IRepoServiceManager service) : ControllerBase
    {
        private readonly IRepoServiceManager _service = service;

        [HttpGet("specialization/{id:int}")]
        public async Task<ActionResult<IEnumerable<QuestionTestDTO>>> GetAll(int id, string? include = null)
        {
            var specialization = await _service.SpecializationRepository.GetSpecializationAsync(s => s.Id == id, include);
            return Ok(specialization.Topics.ToTestDto());
        }
    }
}
