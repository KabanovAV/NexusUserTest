//using NexusUserTest.Common.DTOs;
//using System.Net.Http.Json;

//namespace NexusUserTest.Shared.Services
//{
//    public interface ITopicQuestionService
//    {
//        void AddTopicQuestion(TopicQuestion entity);
//        void UpdateTopicQuestion(TopicQuestion entity);
//        void DeleteTopicQuestion(TopicQuestion entity);
//        void RefreshTopicQuestion(TopicQuestion entity);
//    }

//    public class TopicQuestionService : ITopicQuestionService
//    {
//        private readonly IRepositoryManager _repository;

//        public TopicQuestionService(IRepositoryManager repository)
//            => _repository = repository;

//        public void AddTopicQuestion(TopicQuestion entity)
//        {
//            _repository.TopicQuestion.AddTopicQuestion(entity);
//            _repository.Save();
//        }

//        public void UpdateTopicQuestion(TopicQuestion entity)
//        {
//            _repository.TopicQuestion.UpdateTopicQuestion(entity);
//            _repository.Save();
//        }

//        public void DeleteTopicQuestion(TopicQuestion entity)
//        {
//            _repository.TopicQuestion.DeleteTopicQuestion(entity);
//            _repository.Save();
//        }

//        public void RefreshTopicQuestion(TopicQuestion entity) => _repository.TopicQuestion.RefreshTopicQuestion(entity);
//    }
//}
