namespace NexusUserTest.Common.DTOs
{
    public class QuestionTestDTO
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public List<AnswerTestDTO>? AnswerItems { get; set; }
    }
}
