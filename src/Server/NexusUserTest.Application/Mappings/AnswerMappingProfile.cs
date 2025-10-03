using NexusUserTest.Common;
using NexusUserTest.Domain.Entities;

namespace NexusUserTest.Application.Mappings
{
    public static class AnswerMappingProfile
    {
        /// <summary>
        /// Маппинг из обьекта Answer в AnswerDTO
        /// </summary>
        /// <param name="entity">Обьект Answer</param>
        /// <returns>AnswerDTO</returns>
        public static AnswerAdminDTO? ToDto(this Answer entity)
            => entity == null ? null : new AnswerAdminDTO
            {
                Id = entity.Id,
                Title = entity.Title,
                QuestionId = entity.QuestionId,
                QuestionTitle = entity.Question != null ? entity.Question.Title : "",
                IsCorrect = entity.IsCorrect
            };

        /// <summary>
        /// Маппинг списка из Answer в список AnswerDTO
        /// </summary>
        /// <param name="entities">Список обьектов Answer</param>
        /// <returns>Список AnswerDTO</returns>
        public static List<AnswerAdminDTO> ToDto(this IEnumerable<Answer> entities)
            => [.. entities.Where(e => e != null).Select(e => e.ToDto())];

        /// <summary>
        /// Маппинг из AnswerDTO в обьект Answer
        /// </summary>
        /// <param name="dto">AnswerDTO</param>
        /// <returns>Answer</returns>
        public static Answer? ToEntity(this AnswerAdminDTO dto)
            => dto == null ? null : new Answer
            {
                Id = dto.Id,
                Title = dto.Title,
                QuestionId = dto.QuestionId,
                IsCorrect = dto.IsCorrect
            };

        /// <summary>
        /// Маппинг списка из AnswerDTO в список обьектов Answer
        /// </summary>
        /// <param name="dtos">Список AnswerDTO</param>
        /// <returns>Список обьектов Answer</returns>
        public static List<Answer> ToEntity(this IEnumerable<AnswerAdminDTO> dtos)
            => [.. dtos.Where(dto => dto != null).Select(dto => dto.ToEntity())];

        /// <summary>
        /// Маппинг обновления обьекта Answer
        /// </summary>
        /// <param name="entity">Обьект Answer</param>
        /// <param name="dto">AnswerDTO</param>
        public static void UpdateFromDto(this Answer entity, AnswerAdminDTO dto)
        {
            if (dto == null) return;
            if (dto.Title != null && !string.IsNullOrEmpty(dto.Title) && entity.Title != dto.Title)
                entity.Title = dto.Title;
            if (entity.QuestionId != 0 && entity.QuestionId != dto.QuestionId)
                entity.QuestionId = dto.QuestionId;
            if (entity.IsCorrect != dto.IsCorrect)
                entity.IsCorrect = dto.IsCorrect;
        }
    }
}
