using NexusUserTest.Common.DTOs;
using NexusUserTest.Domain.Entities;

namespace SibCCSPETest.WebApi.MappingProfiles
{
    public static class QuestionMappingProfile
    {
        public static QuestionDTO? ToDto(this Question entity)
            => entity == null ? null : new QuestionDTO
            {
                Id = entity.Id,
                Title = entity.Title,
                AnswerItems = entity.Answers != null ? [.. entity.Answers
                    .Select(a => new AnswerDTO
                    {
                        Id = a.Id,
                        Title = a.Title,
                        QuestionId = a.QuestionId,
                        QuestionTitle = a.Question != null ? a.Question.Title : "",
                        IsCorrect = a.IsCorrect
                    })] : [],
                TopicQuestionItems = entity.TopicQuestion != null ? [.. entity.TopicQuestion
                    .Select(gu => new TopicQuestionCreateDTO { TopicId = gu.TopicId, QuestionId = gu.QuestionId })] : []
            };

        public static IEnumerable<QuestionDTO?> ToDto(this IEnumerable<Question> entities)
            => entities.Select(e => e.ToDto()) ?? [];

        public static Question? ToEntity(this QuestionDTO dto)
            => dto == null ? null : new Question
            {
                Id = dto.Id,
                Title = dto.Title,
                Answers = dto.AnswerItems != null ? AnswerListConverter(dto.AnswerItems) : [],
                TopicQuestion = dto.TopicQuestionItems != null ? TopicQuestionListConverter(dto.TopicQuestionItems) : []
            };

        public static IEnumerable<Question?> ToEntity(this IEnumerable<QuestionDTO> entities)
            => entities.Select(e => e.ToEntity()) ?? [];

        public static Question? ToEntity(this QuestionCreateDTO dto)
            => dto == null ? null : new Question
            {
                Title = dto.Title,
                Answers = dto.AnswerItems != null ? AnswerListConverter(dto.AnswerItems) : [],
                TopicQuestion = dto.TopicQuestionItems != null ? TopicQuestionListConverter(dto.TopicQuestionItems) : []
            };

        public static void UpdateFromDto(this Question entity, QuestionDTO dto)
        {
            if (dto == null) return;


            if (dto.Title != null && !string.IsNullOrEmpty(dto.Title) && entity.Title != dto.Title)
                entity.Title = dto.Title;
            if (dto.AnswerItems != null)
                AnswerListConverter(entity, dto);
            if (dto.TopicQuestionItems != null)
                TopicQuestionListConverter(entity, dto);
        }

        private static List<Answer> AnswerListConverter(List<AnswerDTO> items)
        {
            List<Answer> answer = [];
            foreach (var item in items!)
                answer.Add(new Answer
                {
                    Id = item.Id,
                    Title = item.Title,
                    QuestionId = item.QuestionId,
                    IsCorrect = item.IsCorrect
                });
            return answer;
        }

        private static void AnswerListConverter(this Question entity, QuestionDTO dto)
        {
            if (entity.Answers != null && dto.AnswerItems != null)
            {
                var toRemove = entity.Answers
                        .Where(a => !dto.AnswerItems.Any(dtoA => dtoA.Id == a.Id))
                        .ToList();

                foreach (var item in toRemove)
                    entity.Answers.Remove(item);

                foreach (var item in dto.AnswerItems!)
                {
                    var existing = entity.Answers.FirstOrDefault(a => a.Id == item.Id);
                    if (existing != null)
                    {
                        existing.Title = item.Title;
                        existing.QuestionId = item.QuestionId;
                        existing.IsCorrect = item.IsCorrect;
                    }
                    else
                        entity.Answers.Add(new Answer { Title = item.Title, QuestionId = item.QuestionId, IsCorrect = item.IsCorrect });
                }
            }
        }

        private static List<TopicQuestion> TopicQuestionListConverter(List<TopicQuestionCreateDTO> items)
        {
            List<TopicQuestion> topicQuestion = [];
            foreach (var item in items!)
                topicQuestion.Add(new TopicQuestion { TopicId = item.TopicId, QuestionId = item.QuestionId });
            return topicQuestion;
        }

        private static void TopicQuestionListConverter(this Question entity, QuestionDTO dto)
        {
            if (entity.TopicQuestion != null && dto.TopicQuestionItems != null)
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
}
