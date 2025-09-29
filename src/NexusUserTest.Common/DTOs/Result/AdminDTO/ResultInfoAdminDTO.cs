namespace NexusUserTest.Common.DTOs
{
    public class ResultInfoAdminDTO
    {
        public int Id { get; set; }
        public QuestionInfoAdminDTO? Question { get; set; }
        public int? AnswerId { get; set; }
    }
}
