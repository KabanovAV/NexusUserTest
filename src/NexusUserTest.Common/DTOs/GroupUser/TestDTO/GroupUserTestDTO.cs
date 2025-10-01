namespace NexusUserTest.Common
{
    public class GroupUserTestDTO
    {
        public int Id { get; set; }
        public int Status { get; set; } // 1 Недопущен; 2 Допущен; 3 Сдал 
        public int SpecializationId { get; set; }
        public List<ResultTestDTO>? Results { get; set; }
        public int CountOfQuestion { get; set; }
        public TimeSpan Timer { get; set; }
        public DateTime EndTest { get; set; }
    }
}
