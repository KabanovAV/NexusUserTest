using NexusUserTest.Common;
using NexusUserTest.Domain.Entities;

namespace NexusUserTest.Application.Mappings
{
    public static class ResultMappingProfile
    {
        /// <summary>
        /// Маппинг из обьекта Result в ResultInfoDTO
        /// </summary>
        /// <param name="entity">Обьект Result</param>
        /// <returns>ResultInfoDTO</returns>
        public static ResultInfoAdminDTO? ToInfoAdminDto(this Result entity)
            => entity == null ? null : new ResultInfoAdminDTO
            {
                Id = entity.Id,
                Question = entity.Question != null ? new QuestionInfoAdminDTO
                {
                    Id = entity.QuestionId,
                    Title = entity.Question.Title,
                    AnswerItems = entity.Question.Answers != null ? [.. entity.Question.Answers
                        .Select(a => new AnswerInfoDTO
                        {
                            Id = a.Id,
                            Title = a.Title,
                            IsCorrect = a.IsCorrect
                        })] : []
                } : null,
                AnswerId = entity.AnswerId
            };

        /// <summary>
        /// Маппинг списка из Result в список ResultInfoDTO
        /// </summary>
        /// <param name="entities">Список обьектов Result</param>
        /// <returns>Список ResultInfoDTO</returns>
        public static List<ResultInfoAdminDTO> ToInfoAdminDto(this IEnumerable<Result> entities)
            => [.. entities.Where(e => e != null).Select(e => e.ToInfoAdminDto())];

        /// <summary>
        /// Маппинг из обьекта Result в ResultTestDTO
        /// </summary>
        /// <param name="entity">Обьект Result</param>
        /// <returns>ResultTestDTO</returns>
        public static ResultTestDTO? ToTestDto(this Result entity)
            => entity == null ? null : new ResultTestDTO
            {
                Id = entity.Id,
                GroupUserId = entity.GroupUserId,
                Question = entity.Question != null ? new QuestionTestDTO
                {
                    Id = entity.QuestionId,
                    Title = entity.Question.Title,
                    AnswerItems = entity.Question.Answers != null ? [.. entity.Question.Answers
                        .Select(a => new AnswerTestDTO
                        {
                            Id = a.Id,
                            Title = a.Title
                        })] : []
                } : null,
                AnswerId = entity.AnswerId
            };

        /// <summary>
        /// Маппинг списка из Result в список ResultTestDTO
        /// </summary>
        /// <param name="entities">Список обьектов Result</param>
        /// <returns>Список ResultTestDTO</returns>
        public static List<ResultTestDTO> ToTestDto(this IEnumerable<Result> entities)
            => [.. entities.Where(e => e != null).Select(e => e.ToTestDto())];

        /// <summary>
        /// Маппинг из ResultTestDTO в обьект Result
        /// </summary>
        /// <param name="dto">ResultTestDTO</param>
        /// <returns>Result</returns>
        public static Result? ToTestEntity(this ResultTestDTO dto)
            => dto == null ? null : new Result
            {
                Id = dto.Id,
                GroupUserId = dto.GroupUserId,
                QuestionId = dto.Question != null ? dto.Question.Id : 0,
                AnswerId = dto.AnswerId
            };

        /// <summary>
        /// Маппинг списка из ResultTestDTO в список обьектов Result
        /// </summary>
        /// <param name="dtos">Список ResultTestDTO</param>
        /// <returns>Список обьектов Result</returns>
        public static List<Result> ToTestEntity(this IEnumerable<ResultTestDTO> dtos)
            => [.. dtos.Where(dto => dto != null).Select(e => e.ToTestEntity())];

        /// <summary>
        /// Маппинг обновления обьекта Result
        /// </summary>
        /// <param name="entity">Обьект Result</param>
        /// <param name="dto">ResultTestDTO</param>
        public static void UpdateFromDto(this Result entity, ResultTestDTO dto)
        {
            if (dto == null) return;
            if (entity.GroupUserId != 0 && entity.GroupUserId != dto.GroupUserId)
                entity.GroupUserId = dto.GroupUserId;
            if (entity.QuestionId != 0 && dto.Question != null && entity.QuestionId != dto.Question.Id)
                entity.QuestionId = dto.Question.Id;
            if (entity.AnswerId != dto.AnswerId)
                entity.AnswerId = dto.AnswerId;
        }

        /// <summary>
        /// Маппинг из обьекта Result в ResultInfoTestDTO
        /// </summary>
        /// <param name="entity">Обьект Result</param>
        /// <returns>ResultInfoTestDTO</returns>
        public static ResultInfoTestDTO? ToTestInfoDto(this Result entity)
            => entity == null ? null : new ResultInfoTestDTO { IsCorrect = entity.Answer!.IsCorrect };

        /// <summary>
        /// Маппинг списка из Result в список ResultInfoTestDTO
        /// </summary>
        /// <param name="entities">Список обьектов Result</param>
        /// <returns>Список ResultInfoTestDTO</returns>
        public static List<ResultInfoTestDTO> ToTestInfoDto(this IEnumerable<Result> entities)
            => [.. entities.Where(e => e != null).Select(e => e.ToTestInfoDto())];
    }
}
