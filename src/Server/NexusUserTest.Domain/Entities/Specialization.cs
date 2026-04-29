using NexusUserTest.Domain.Common;

namespace NexusUserTest.Domain.Entities
{
    public class Specialization : AuditableEntityBase
    {
        public string Title { get; set; } = null!;
        public List<Group>? Groups { get; set; }
        public List<Topic>? Topics { get; set; }

        public bool ApplyUpdate(string? title)
        {
            var hasChanges = false;
            if (title != null && !Title.Equals(title.Trim(), StringComparison.OrdinalIgnoreCase))
            {
                Title = title;
                hasChanges = true;
            }            
            return hasChanges;
        }
    }
}
