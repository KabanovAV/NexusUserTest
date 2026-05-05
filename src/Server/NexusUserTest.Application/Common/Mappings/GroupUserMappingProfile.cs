using NexusUserTest.Common;
using NexusUserTest.Domain.Entities;

namespace SibCCSPETest.WebApi.MappingProfiles
{
    public static class GroupUserMappingProfile
    {
        /// <summary>
        /// Маппинг из обьекта GroupUser в GroupUserTestDTO
        /// </summary>
        /// <param name="entity">Обьект GroupUser</param>
        /// <returns>GroupUserTestDTO</returns>
        public static GroupUserInfoAdminDTO? ToInfoAdminDto(this GroupUser entity)
            => entity == null ? null : new GroupUserInfoAdminDTO
            {
                Id = entity.Id,
                Status = entity.Status,
                User = new UserInfoAdminDTO
                {
                    Id = entity.Id,
                    FullName = entity.User != null ?
                        string.Join(" ", new[] { entity.User.LastName, entity.User.FirstName, entity.User.Surname }.Where(s => !string.IsNullOrEmpty(s))) : "",
                    Login = entity.User != null ? entity.User.Login : "",
                    Organization = entity.User != null ? entity.User.Organization : ""
                },
                Results = entity.Results != null ? [.. entity.Results
                    .Select(r => new ResultInfoAdminDTO
                    {
                        Id = r.Id,
                        Question = new QuestionInfoAdminDTO
                        {
                            Id = r.QuestionId,
                            Title = r.Question != null ? r.Question.Title : "",
                            AnswerItems = r.Question != null && r.Question.Answers != null ? [.. r.Question.Answers
                                .Select(a => new AnswerInfoDTO
                                {
                                    Id = a.Id,
                                    Title = a.Title,
                                    IsCorrect = a.IsCorrect
                                })] : []
                        },
                        AnswerId = r.AnswerId
                    })] : []
            };

        /// <summary>
        /// Маппинг списка из обьектов GroupUser в список GroupUserTestDTO
        /// </summary>
        /// <param name="entities">Список GroupUser</param>
        /// <returns>Список GroupUserTestDTO</returns>
        public static List<GroupUserInfoAdminDTO> ToInfoAdminDto(this IEnumerable<GroupUser> entities)
            => [.. entities.Where(e => e != null).Select(e => e.ToInfoAdminDto())];

        /// <summary>
        /// Маппинг из обьекта GroupUser в GroupUserTestDTO
        /// </summary>
        /// <param name="entity">Обьект GroupUser</param>
        /// <returns>GroupUserTestDTO</returns>
        public static GroupUserTestDTO? ToTestDto(this GroupUser entity)
            => entity == null ? null : new GroupUserTestDTO
            {
                Id = entity.Id,
                Status = entity.Status,
                SpecializationId = entity.Group != null ? entity.Group.SpecializationId : 0,
                Results = entity.Results != null ? [.. entity.Results
                    .Select(r => new ResultTestDTO
                    {
                        Id = r.Id,
                        GroupUserId = r.GroupUserId,
                        Question = r.Question != null ? new QuestionTestDTO
                            {
                                Id = r.QuestionId,
                                Title = r.Question.Title,
                                AnswerItems = r.Question.Answers != null ? [.. r.Question.Answers
                                    .Select(a => new AnswerTestDTO
                                    {
                                        Id = a.Id,
                                        Title = a.Title
                                    })] : []
                            } : null,
                        AnswerId = r.AnswerId
                    })] : [],
                CountOfQuestion = entity.Group != null && entity.Group.Setting != null ? entity.Group.Setting.CountOfQuestion : 0,
                Timer = entity.Group != null && entity.Group.Setting != null ? entity.Group.Setting.Timer : new(),
                EndTest = entity.EndTest
            };

        /// <summary>
        /// Маппинг списка из обьектов GroupUser в список GroupUserTestDTO
        /// </summary>
        /// <param name="entities">Список GroupUser</param>
        /// <returns>Список GroupUserTestDTO</returns>
        public static List<GroupUserTestDTO> ToTestDto(this IEnumerable<GroupUser> entities)
            => [.. entities.Where(e => e != null).Select(e => e.ToTestDto())];

        /// <summary>
        /// Маппинг обновления обьекта GroupUser
        /// </summary>
        /// <param name="entity">Обьект GroupUser</param>
        /// <param name="dto">GroupUserTestDTO</param>
        public static void UpdateFromDto(this GroupUser entity, GroupUserUpdateDTO dto)
        {
            if (dto == null) return;
            if (dto.Status != 0 && entity.Status != dto.Status)
                entity.Status = dto.Status;
            if (entity.EndTest == null)
                entity.EndTest = dto.EndTest;
        }
    }
}
