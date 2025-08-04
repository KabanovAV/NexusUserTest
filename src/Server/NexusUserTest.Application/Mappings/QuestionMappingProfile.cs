using AutoMapper;
using NexusUserTest.Common.DTOs;
using NexusUserTest.Domain.Entities;

namespace SibCCSPETest.WebApi.MappingProfiles
{
    public class QuestionMappingProfile : Profile
    {
        public QuestionMappingProfile()
        {
            CreateMap<Question, QuestionDTO>().ForMember(dest => dest.AnswerItems, opt => opt.MapFrom(src => src.Answers))
                .ForMember(dest => dest.TopicQuestionItems, opt => opt.MapFrom(src => src.TopicQuestion))
                .ReverseMap()
                .ForMember(dest => dest.TopicQuestion, opt => opt.Ignore())
                .AfterMap((dto, entity) => TopicQuestionListConverter(dto, entity));

            CreateMap<QuestionCreateDTO, Question>()
                .ForMember(dest => dest.Answers, opt => opt.MapFrom(src => src.AnswerItems))
                .ForMember(dest => dest.TopicQuestion, opt => opt.MapFrom(src => src.TopicQuestionItems));
        }

        private static void TopicQuestionListConverter(QuestionDTO dto, Question entity)
        {
            var toRemove = entity.TopicQuestion
                        .Where(tq => !dto.TopicQuestionItems.Any(dtoTq => dtoTq.TopicId == tq.TopicId))
                        .ToList();

            foreach (var item in toRemove)
                entity.TopicQuestion.Remove(item);

            foreach (var item in dto.TopicQuestionItems!)
            {
                var existing = entity.TopicQuestion.FirstOrDefault(tq => tq.TopicId == item.TopicId);
                if (existing != null)
                {
                    existing.TopicId = item.TopicId;
                    existing.QuestionId = item.QuestionId;
                }
                else
                    entity.TopicQuestion.Add(new TopicQuestion { TopicId = item.TopicId, QuestionId = item.QuestionId });
            }
        }
    }
}
