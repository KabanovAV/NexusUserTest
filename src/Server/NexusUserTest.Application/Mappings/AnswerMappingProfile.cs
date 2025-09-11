using NexusUserTest.Common.DTOs;
using NexusUserTest.Domain.Entities;

namespace NexusUserTest.Application.Mappings
{
    public static class AnswerMappingProfile
    {
        public static AnswerDTO? ToDto(this Answer entity)
            => entity == null ? null : new AnswerDTO
            {
                Id = entity.Id,
                Title = entity.Title,
                QuestionId = entity.QuestionId,
                QuestionTitle = entity.Question != null ? entity.Question.Title : "",
                IsCorrect = entity.IsCorrect
            };

        public static IEnumerable<AnswerDTO?> ToDto(this IEnumerable<Answer> entities)
            => entities.Select(e => e.ToDto()) ?? [];

        public static Answer? ToEntity(this AnswerDTO dto)
            => dto == null ? null : new Answer
            {
                Id = dto.Id,
                Title = dto.Title,
                QuestionId = dto.QuestionId,
                IsCorrect = dto.IsCorrect
            };

        public static IEnumerable<Answer?> ToEntity(this IEnumerable<AnswerDTO> entities)
            => entities.Select(e => e.ToEntity()) ?? [];

        public static Answer? ToEntity(this AnswerCreateDTO dto)
            => dto == null ? null : new Answer
            {
                Title = dto.Title,
                QuestionId = dto.QuestionId,
                IsCorrect = dto.IsCorrect
            };

        public static IEnumerable<Answer?> ToEntity(this IEnumerable<AnswerCreateDTO> entities)
            => entities.Select(e => e.ToEntity()) ?? [];

        public static void UpdateFromDto(this Answer entity, AnswerDTO dto)
        {
            if (dto == null) return;
            if (dto.Title != null && !string.IsNullOrEmpty(dto.Title) && entity.Title != dto.Title)
                entity.Title = dto.Title;
            if (entity.QuestionId != 0 && entity.QuestionId != dto.QuestionId)
                entity.QuestionId = dto.QuestionId;
            if (entity.IsCorrect != dto.IsCorrect)
                entity.IsCorrect = dto.IsCorrect;
        }


        //public AnswerMappingProfile()
        //{
        //    CreateMap<Answer, AnswerDTO>().ForMember(dest => dest.QuestionTitle, opt =>
        //    {
        //        opt.PreCondition(src => src.Question != null);
        //        opt.MapFrom(src => src.Question!.Title);
        //    }).ReverseMap();
        //    CreateMap<AnswerCreateDTO, Answer>();
        //}
    }
}
