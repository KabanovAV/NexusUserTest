using NexusUserTest.Domain.Common;

namespace NexusUserTest.Domain.Entities
{
    public class Specialization : AuditableEntityBase
    {
        public string Title { get; set; } = null!;
        public List<Group>? Groups { get; set; }
        public List<Topic>? Topics { get; set; }
    }
}
