namespace NexusUserTest.Common.DTOs
{
    public class ResultTestDTO
    {
        public int Id { get; set; }
        public int GroupUserId { get; set; }
        public QuestionTestDTO? Question { get; set; }
        public int? AnswerId { get; set; }
    }
}
