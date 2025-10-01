namespace NexusUserTest.Common
{
    public class QuestionInfoAdminDTO
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public List<AnswerInfoDTO>? AnswerItems { get; set; }
    }
}
