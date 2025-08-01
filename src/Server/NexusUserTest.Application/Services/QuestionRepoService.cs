﻿using NexusUserTest.Domain.Entities;
using NexusUserTest.Domain.Repositories;
using System.Linq.Expressions;

namespace NexusUserTest.Application.Services
{
    public interface IQuestionRepoService
    {
        Task<IEnumerable<Question>> GetAllQuestionAsync(Expression<Func<Question, bool>>? expression = null, string? includeProperties = null);
        Task<Question> GetQuestionAsync(Expression<Func<Question, bool>> expression, string? includeProperties = null);
        Task<Question> AddQuestionAsync(Question entity, string? includeProperties = null);
        Task<Question> UpdateQuestion(Question entity, string? includeProperties = null);
        void DeleteQuestion(Question entity);
    }

    public class QuestionRepoService : IQuestionRepoService
    {
        private readonly IRepositoryManager _repository;

        public QuestionRepoService(IRepositoryManager repository)
            => _repository = repository;

        public async Task<IEnumerable<Question>> GetAllQuestionAsync(Expression<Func<Question, bool>>? expression = null, string? includeProperties = null)
            => await _repository.Question.GetAllQuestionAsync(expression, includeProperties);

        public async Task<Question> GetQuestionAsync(Expression<Func<Question, bool>> expression, string? includeProperties = null)
            => await _repository.Question.GetQuestionAsync(expression, includeProperties);

        public async Task<Question> AddQuestionAsync(Question entity, string? includeProperties = null)
        {
            entity.CreatedDate = DateTime.Now;
            entity.ChangedDate = DateTime.Now;

            if (entity.TopicQuestion != null && entity.TopicQuestion.Count != 0)
            {
                foreach (var topic in entity.TopicQuestion)
                {
                    topic.CreatedDate = DateTime.Now;
                    topic.ChangedDate = DateTime.Now;
                }
            }
            if (entity.Answers != null && entity.Answers.Count != 0)
            {
                foreach (var topic in entity.Answers)
                {
                    topic.CreatedDate = DateTime.Now;
                    topic.ChangedDate = DateTime.Now;
                }
            }

            await _repository.Question.AddQuestionAsync(entity);
            _repository.Save();
            return await _repository.Question.GetQuestionAsync(q => q.Id == entity.Id, includeProperties);
        }

        public async Task<Question> UpdateQuestion(Question entity, string? includeProperties = null)
        {
            entity.ChangedDate = DateTime.Now;

            if (entity.TopicQuestion != null && entity.TopicQuestion.Count != 0)
            {
                foreach (var topic in entity.TopicQuestion)
                {
                    if (topic.Id == 0)
                    {
                        topic.CreatedDate = DateTime.Now;
                        topic.ChangedDate = DateTime.Now;
                    }
                    else
                        topic.ChangedDate = DateTime.Now;
                }
            }
            if (entity.Answers != null && entity.Answers.Count != 0)
            {
                foreach (var topic in entity.Answers)
                {
                    if (topic.Id == 0)
                    {
                        topic.CreatedDate = DateTime.Now;
                        topic.ChangedDate = DateTime.Now;
                    }
                    else
                        topic.ChangedDate = DateTime.Now;
                }
            }

            _repository.Question.UpdateQuestion(entity);
            _repository.Save();
            return await _repository.Question.GetQuestionAsync(q => q.Id == entity.Id, includeProperties);
        }

        public void DeleteQuestion(Question entity)
        {
            _repository.Question.DeleteQuestion(entity);
            _repository.Save();
        }
    }
}
