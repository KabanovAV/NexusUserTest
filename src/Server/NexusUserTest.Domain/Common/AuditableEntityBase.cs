namespace NexusUserTest.Domain.Common
{
    public class AuditableEntityBase : EntityBase
    {
        public DateTime CreatedDate { get; set; }
        public DateTime ChangedDate { get; set; }
    }
}
