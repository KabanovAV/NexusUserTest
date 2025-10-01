namespace NexusUserTest.Common
{
    public class GroupUserInfoTestDTO
    {
        public int Id { get; set; }
        public string GroupTitle { get; set; } = string.Empty;
        public int Status { get; set; } // 1 Недопущен; 2 Допущен; 3 Провалил; 4 Сдал
        public List<ResultInfoTestDTO>? Results { get; set; }
    }
}
