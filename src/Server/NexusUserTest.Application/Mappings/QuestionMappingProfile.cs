using NexusUserTest.Common;
using NexusUserTest.Domain.Entities;

namespace SibCCSPETest.WebApi.MappingProfiles
{
    public static class QuestionMappingProfile
    {
        /// <summary>
        /// Маппинг из обьекта Question в QuestionDTO
        /// </summary>
        /// <param name="entity">Обьект Question</param>
        /// <returns>QuestionDTO</returns>
        public static QuestionDTO? ToAdminDto(this Question entity)
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
                    .Select(gu => new TopicQuestionCreateDTO { TopicId = gu.TopicId })] : []
            };

        /// <summary>
        /// Маппинг списка из обьектов Question в список QuestionDTO
        /// </summary>
        /// <param name="entities">Список обьектов Question</param>
        /// <returns>Список QuestionDTO</returns>
        public static List<QuestionDTO> ToAdminDto(this IEnumerable<Question> entities)
            => [.. entities.Where(e => e != null).Select(e => e.ToAdminDto())];

        /// <summary>
        /// Маппинг из QuestionDTO в обьект Question
        /// </summary>
        /// <param name="dto">QuestionDTO</param>
        /// <returns>Question</returns>
        public static Question? ToEntity(this QuestionDTO dto)
            => dto == null ? null : new Question
            {
                Id = dto.Id,
                Title = dto.Title,
                Answers = dto.AnswerItems != null ? AnswerListConverter(dto.AnswerItems) : [],
                TopicQuestion = dto.TopicQuestionItems != null ? TopicQuestionListConverter(dto.TopicQuestionItems) : []
            };

        /// <summary>
        /// Маппинг списка из QuestionDTO в список обьектов Question
        /// </summary>
        /// <param name="dtos">Список QuestionDTO</param>
        /// <returns>Список Question</returns>
        public static List<Question> ToEntity(this IEnumerable<QuestionDTO> dtos)
            => [.. dtos.Where(dto => dto != null).Select(dto => dto.ToEntity())];

        /// <summary>
        /// Маппинг из QuestionCreateDTO создание в обьект Question
        /// </summary>
        /// <param name="dto">QuestionCreateDTO</param>
        /// <returns>Обьект Question</returns>
        public static Question? ToEntity(this QuestionCreateDTO dto)
            => dto == null ? null : new Question
            {
                Title = dto.Title,
                Answers = dto.AnswerItems != null ? AnswerListConverter(dto.AnswerItems) : [],
                TopicQuestion = dto.TopicQuestionItems != null ? TopicQuestionListConverter(dto.TopicQuestionItems) : []
            };

        /// <summary>
        /// Маппинг обновления обьекта Question
        /// </summary>
        /// <param name="entity">Обьект Question</param>
        /// <param name="dto">QuestionDTO</param>
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

        /// <summary>
        /// Конвертирование списка из AnswerDTO в список обьектов Answer
        /// </summary>
        /// <param name="items">AnswerDTO</param>
        /// <returns>Список Answer</returns>
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

        /// <summary>
        /// Обьединение и конвертирование в обьекта Question
        /// </summary>
        /// <param name="entity">Обьект Question</param>
        /// <param name="dto">QuestionDTO</param>
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
                        if (item.Title != null && !string.IsNullOrEmpty(item.Title) && existing.Title != item.Title)
                            existing.Title = item.Title;
                        if (existing.QuestionId != item.QuestionId && item.QuestionId != 0)
                            existing.QuestionId = item.QuestionId;
                        if (existing.IsCorrect != item.IsCorrect)
                            existing.IsCorrect = item.IsCorrect;
                    }
                    else
                        entity.Answers.Add(new Answer { Title = item.Title, QuestionId = item.QuestionId, IsCorrect = item.IsCorrect });
                }
            }
        }

        /// <summary>
        /// Конвертирование списка из TopicQuestionCreateDTO в список обьектов TopicQuestion
        /// </summary>
        /// <param name="items">TopicQuestionCreateDTO</param>
        /// <returns>Список TopicQuestion</returns>
        private static List<TopicQuestion> TopicQuestionListConverter(List<TopicQuestionCreateDTO> items)
        {
            List<TopicQuestion> topicQuestion = [];
            foreach (var item in items!)
                topicQuestion.Add(new TopicQuestion { TopicId = item.TopicId });
            return topicQuestion;
        }

        /// <summary>
        /// Обьединение и конвертирование в обьект Question
        /// </summary>
        /// <param name="entity">Обьект Question</param>
        /// <param name="dto">QuestionDTO</param>
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
                        if (existing.TopicId != item.TopicId && item.TopicId != 0)
                            existing.TopicId = item.TopicId;
                    }
                    else
                        entity.TopicQuestion.Add(new TopicQuestion { TopicId = item.TopicId });
                }
            }
        }
    }
}
