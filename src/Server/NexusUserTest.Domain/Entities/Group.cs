using NexusUserTest.Domain.Common;

namespace NexusUserTest.Domain.Entities
{
    public class Group : AuditableEntityBase
    {
        public string Title { get; set; } = null!;
        public int SpecializationId { get; set; }
        public DateTime Begin { get; set; }
        public DateTime End { get; set; }
        public Specialization? Specialization { get; set; }
        public List<GroupUser>? GroupUsers { get; set; }
        public TestSetting? Setting { get; set; }
    }
}
