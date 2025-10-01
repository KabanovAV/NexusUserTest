using NexusUserTest.Common;
using NexusUserTest.Domain.Entities;

namespace SibCCSPETest.WebApi.MappingProfiles
{
    public static class TopicMappingProfile
    {
        /// <summary>
        /// Маппинг из обьекта Topic в TopicDTO
        /// </summary>
        /// <param name="entity">Обьект Topic</param>
        /// <returns>TopicDTO</returns>
        public static TopicDTO? ToAdminDto(this Topic entity)
            => entity == null ? null : new TopicDTO
            {
                Id = entity.Id,
                Title = entity.Title,
                SpecializationId = entity.SpecializationId,
                SpecializationTitle = entity.Specialization != null ? entity.Specialization.Title : ""
            };

        /// <summary>
        /// Маппинг списка из обьектов Topic в список TopicDTO
        /// </summary>
        /// <param name="entities">Список обьектов Topic</param>
        /// <returns>Список TopicDTO</returns>
        public static List<TopicDTO> ToAdminDto(this IEnumerable<Topic> entities)
            => [.. entities.Where(e => e != null).Select(e => e.ToAdminDto())];

        /// <summary>
        /// Маппинг из TopicDTO в обьект Topic
        /// </summary>
        /// <param name="dto">TopicDTO</param>
        /// <returns>Обьект Topic</returns>
        public static Topic? ToEntity(this TopicDTO dto)
            => dto == null ? null : new Topic
            {
                Id = dto.Id,
                Title = dto.Title,
                SpecializationId = dto.SpecializationId
            };

        /// <summary>
        /// Маппинг списка из TopicDTO в список обьектов Topic
        /// </summary>
        /// <param name="dtos">Список TopicDTO</param>
        /// <returns>Список обьектов Topic</returns>
        public static List<Topic> ToEntity(this IEnumerable<TopicDTO> dtos)
            => [.. dtos.Where(dto => dto != null).Select(dto => dto.ToEntity())];

        /// <summary>
        /// Маппинг из TopicCreateDTO создание в обьект Topic
        /// </summary>
        /// <param name="dto">TopicCreateDTO</param>
        /// <returns>Обьект Topic</returns>
        public static Topic? ToEntity(this TopicCreateDTO dto)
            => dto == null ? null : new Topic
            {
                Title = dto.Title,
                SpecializationId = dto.SpecializationId
            };

        /// <summary>
        /// Маппинг обновления обьекта Topic
        /// </summary>
        /// <param name="entity">Обьект Topic</param>
        /// <param name="dto">TopicDTO</param>
        public static void UpdateFromDto(this Topic entity, TopicDTO dto)
        {
            if (dto == null) return;
            if (dto.Title != null && !string.IsNullOrEmpty(dto.Title) && entity.Title != dto.Title)
                entity.Title = dto.Title;
            if (entity.SpecializationId != 0 && entity.SpecializationId != dto.SpecializationId)
                entity.SpecializationId = dto.SpecializationId;
        }

        /// <summary>
        /// Маппинг из обьекта Topic в QuestionTestDTO
        /// </summary>
        /// <param name="entity">Обьект Topic</param>
        /// <returns>Список QuestionTestDTO</returns>
        public static List<QuestionTestDTO>? ToTestDto(this Topic entity)
            => entity == null && entity!.TopicQuestion == null ? [] : entity!.TopicQuestion!.Select(t => new QuestionTestDTO
            {
                Id = t.Question != null ? t.Question.Id : 0,
                Title = t.Question != null ? t.Question.Title : "",
                AnswerItems = t.Question != null && t.Question.Answers != null ? [.. t.Question.Answers
                    .Select(a => new AnswerTestDTO
                    {
                        Id = a.Id,
                        Title = a.Title
                    })] : []
            }).ToList();

        /// <summary>
        /// Маппинг списка из обьектов Topic в список QuestionTestDTO
        /// </summary>
        /// <param name="entities">Список Topic</param>
        /// <returns>Список Topic</returns>
        public static List<QuestionTestDTO>? ToTestDto(this IEnumerable<Topic> entities)
        {
            if (entities == null)
                return null;
            List<QuestionTestDTO> questions = [];
            foreach (var entity in entities)
                questions.AddRange(entity.ToTestDto()!);
            return questions;
        }

    }
}
