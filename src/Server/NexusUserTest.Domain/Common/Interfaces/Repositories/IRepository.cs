namespace NexusUserTest.Domain.Common
{
    /// <summary>
    /// Интерфейс к репозиториям данных
    /// </summary>
    public interface IRepository
    {
        /// <summary>
        /// Ответ
        /// </summary>
        IAnswerRepository Answer { get; }

        /// <summary>
        /// Группа
        /// </summary>
        IGroupRepository Group { get; }

        /// <summary>
        /// Группа пользователя
        /// </summary>
        IGroupUserRepository GroupUser { get; }

        /// <summary>
        /// Тема вопроса
        /// </summary>
        ITopicQuestionRepository TopicQuestion { get; }

        /// <summary>
        /// Вопрос
        /// </summary>
        IQuestionRepository Question { get; }

        /// <summary>
        /// Результат
        /// </summary>
        ITestResultRepository Result { get; }

        /// <summary>
        /// Настройки теста
        /// </summary>
        ITestSettingRepository Setting { get; }

        /// <summary>
        /// Специализация
        /// </summary>
        ISpecializationRepository Specialization { get; }

        /// <summary>
        /// Тема
        /// </summary>
        ITopicRepository Topic { get; }

        /// <summary>
        /// Пользователь
        /// </summary>
        IUserRepository User { get; }
    }
}
