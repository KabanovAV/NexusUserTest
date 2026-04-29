using NexusUserTest.Common;
using NexusUserTest.Domain.Entities;

namespace NexusUserTest.Application.Mappings
{
    public static class GroupMappingProfile
    {
        /// <summary>
        /// Маппинг из обьекта Group в GroupDTO
        /// </summary>
        /// <param name="entity">Обьект Group</param>
        /// <returns>GroupDTO</returns>
        public static GroupDTO? ToDto(this Group entity)
            => entity == null ? null : new GroupDTO
            {
                Id = entity.Id,
                Title = entity.Title,
                SpecializationId = entity.SpecializationId,
                SpecializationTitle = entity.Specialization != null ? entity.Specialization.Title : "",
                Begin = entity.Begin,
                End = entity.End
            };

        /// <summary>
        /// Маппинг списка из обьектов Group в список GroupDTO
        /// </summary>
        /// <param name="entities">Список Group</param>
        /// <returns>Список GroupDTO</returns>
        public static List<GroupDTO> ToDto(this IEnumerable<Group> entities)
            => [.. entities.Where(e => e != null).Select(e => e.ToDto())];

        /// <summary>
        /// Маппинг из обьекта Group в SelectItem
        /// </summary>
        /// <param name="entity">Обьект Group</param>
        /// <returns>SelectItem</returns>
        public static SelectItem? ToSelect(this Group entity)
            => entity == null ? null : new SelectItem
            {
                Value = entity.Id,
                Text = entity.Title
            };

        /// <summary>
        /// Маппинг списка из обьектов Group в список SelectItem
        /// </summary>
        /// <param name="entities">Список обьектов Group</param>
        /// <returns>Список SelectItem</returns>
        public static List<SelectItem> ToSelect(this IEnumerable<Group> entities)
            => [.. entities.Where(e => e != null).Select(e => e.ToSelect())];

        /// <summary>
        /// Маппинг из GroupEditDTO в обьект Group
        /// </summary>
        /// <param name="dto">GroupEditDTO</param>
        /// <returns>Обьект Group</returns>
        public static Group? ToEntity(this GroupDTO dto)
            => dto == null ? null : new Group
            {
                Id = dto.Id,
                Title = dto.Title,
                SpecializationId = dto.SpecializationId,
                Begin = dto.Begin,
                End = dto.End
            };

        /// <summary>
        /// Маппинг списка из GroupEditDTO в список обьектов Group
        /// </summary>
        /// <param name="dtos">Список GroupEditDTO</param>
        /// <returns>Список обьектов Group</returns>
        public static List<Group> ToEntity(this IEnumerable<GroupDTO> dtos)
            => [.. dtos.Where(dto => dto != null).Select(dto => dto.ToEntity())];

        /// <summary>
        /// Маппинг обновления обьекта Group
        /// </summary>
        /// <param name="entity">Обьект Group</param>
        /// <param name="dto">GroupEditDTO</param>
        public static void UpdateFromDto(this Group entity, GroupDTO dto)
        {
            if (dto == null) return;
            if (dto.Title != null && !string.IsNullOrEmpty(dto.Title) && entity.Title != dto.Title)
                entity.Title = dto.Title;
            if (entity.SpecializationId != 0 && entity.SpecializationId != dto.SpecializationId)
                entity.SpecializationId = dto.SpecializationId;
            if (entity.Begin != dto.Begin)
                entity.Begin = dto.Begin;
            if (entity.End != dto.End)
                entity.End = dto.End;
        }

        /// <summary>
        /// Маппинг из обьекта Group в GroupInfoDTO
        /// </summary>
        /// <param name="entity">Обьект Group</param>
        /// <returns>GroupInfoDTO</returns>
        public static GroupInfoDTO? ToInfoDto(this Group entity)
            => entity == null ? null : new GroupInfoDTO
            {
                Id = entity.Id,
                Title = entity.Title,
                CountOfUsers = entity.GroupUser != null ? entity.GroupUser.Count : 0,
                CountOfQuestions = entity.Specialization != null && entity.Specialization.Topics != null
                    ? entity.Specialization.Topics.Sum(t => t.TopicQuestion?.Count ?? 0) : 0
            };

        /// <summary>
        /// Маппинг списка из обьектов Group в список GroupInfoDTO
        /// </summary>
        /// <param name="entities">Список Group</param>
        /// <returns>Список GroupInfoDTO</returns>
        public static List<GroupInfoDTO> ToInfoDto(this IEnumerable<Group> entities)
            => [.. entities.Where(e => e != null).Select(e => e.ToInfoDto())];

        /// <summary>
        /// Маппинг из обьекта Group в GroupInfoDetailsDTO
        /// </summary>
        /// <param name="entity">Обьект Group</param>
        /// <returns>GroupInfoDetailsDTO</returns>
        public static GroupInfoDetailsDTO? ToInfoDetailDto(this Group entity)
            => entity == null ? null : new GroupInfoDetailsDTO
            {
                Id = entity.Id,
                Title = entity.Title,
                CountOfUsers = entity.GroupUser != null ? entity.GroupUser.Count : 0,
                CountOfQuestions = entity.Specialization != null && entity.Specialization.Topics != null
                    ? entity.Specialization.Topics.Sum(t => t.TopicQuestion?.Count ?? 0) : 0,
                Setting = new SettingDTO
                {
                    Id = entity.Setting != null ? entity.Setting.Id : 0,
                    GroupId = entity.Setting != null ? entity.Setting.GroupId : 0,
                    CountOfQuestion = entity.Setting != null ? entity.Setting.CountOfQuestion : 0,
                    Timer = entity.Setting != null ? entity.Setting.Timer : new TimeSpan()
                },
                User = entity.GroupUser != null ? [.. entity.GroupUser.Select(gu => new GroupUserInfoAdminDTO
                {
                    Id = gu.Id,
                    Status = gu.Status,
                    User = new UserInfoAdminDTO
                    {
                        Id = gu.User != null ? gu.User.Id : 0,
                        FullName = gu.User != null ? string.Join(" ", new[] { gu.User.LastName, gu.User.FirstName, gu.User.Surname }.Where(s => !string.IsNullOrEmpty(s))) : "",
                        Login = gu.User != null ? gu.User.Login : "",
                        Organization = gu.User != null ? gu.User.Organization : ""
                    },
                    Results = gu.Results != null ? [.. gu.Results.Select(r => new ResultInfoAdminDTO
                    {
                        Id = r.Id,
                        Question = r.Question != null ? new QuestionInfoAdminDTO
                        {
                            Id = r.QuestionId,
                            Title = r.Question.Title,
                            AnswerItems = r.Question.Answers != null ? [.. r.Question.Answers
                                .Select(a => new AnswerInfoDTO
                                {
                                    Id = a.Id,
                                    Title = a.Title,
                                    IsCorrect = a.IsCorrect
                                })] : []
                        } : null,
                        AnswerId = r.AnswerId
                    })] : []
                })] : []
            };

        /// <summary>
        /// Маппинг списка из обьектов Group в список GroupInfoDetailsDTO
        /// </summary>
        /// <param name="entities">Список Group</param>
        /// <returns>Список GroupInfoDetailsDTO</returns>
        public static List<GroupInfoDetailsDTO> ToInfoDetailDto(this IEnumerable<Group> entities)
            => [.. entities.Where(e => e != null).Select(e => e.ToInfoDetailDto())];      
    }
}
