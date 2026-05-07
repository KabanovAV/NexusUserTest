//using Microsoft.AspNetCore.Mvc;
//using NexusUserTest.Application.Common;
//using NexusUserTest.Common;
//using SibCCSPETest.WebApi.MappingProfiles;

//namespace SibCCSPETest.WebApi.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class QuestionsController(IQuestionService service) : ControllerBase
//    {
//        [HttpGet]
//        public async Task<ActionResult<IEnumerable<QuestionAdminDTO>>> GetAllQuestionAdmin()
//        {
//            var questions = await service.GetAllQuestionAsync();
//            return Ok(questions.ToAdminDto());
//        }

//        [HttpGet("{id:int}")]
//        public async Task<ActionResult<QuestionAdminDTO>> GetQuestionAdmin(int id)
//        {
//            var question = await service.GetQuestionByIdAsync(id);
//            if (question == null)
//                return NotFound(new { Message = $"Вопрос с id {id} не найден." });
//            return Ok(question.ToAdminDto());
//        }

//        [HttpPost]
//        public async Task<ActionResult<QuestionAdminDTO>> AddQuestion(QuestionAdminDTO questionCreateDTO)
//        {
//            if (questionCreateDTO == null)
//                return BadRequest("Данные для добавления вопроса пустые.");
//            var question = questionCreateDTO.ToEntity();
//            await service.AddQuestionAsync(question);
//            var questionDTO = question!.ToAdminDto();
//            return CreatedAtAction(nameof(GetQuestionAdmin), new { id = questionDTO!.Id }, questionDTO);
//        }

//        [HttpPut("{id:int}")]
//        public async Task<IActionResult> UpdateQuestion(int id, QuestionAdminDTO questionDTO)
//        {
//            if (questionDTO == null)
//                return BadRequest("Данные для обновления вопроса пустые.");
//            var question = await service.GetQuestionByIdAsync(id);
//            if (question == null)
//                return NotFound(new { Message = $"Вопрос с id {questionDTO.Id} не найден." });
//            question.UpdateFromDto(questionDTO);
//            await service.UpdateQuestionAsync(question);
//            return NoContent();
//        }

//        [HttpDelete("{id:int}")]
//        public async Task<IActionResult> DeleteQuestion(int id)
//        {
//            var question = await service.GetQuestionByIdAsync(id);
//            if (question == null)
//                return NotFound(new { Message = $"Вопрос с id {id} не найден." });
//            await service.DeleteQuestionAsync(question.Id);
//            return NoContent();
//        }
//    }
//}
