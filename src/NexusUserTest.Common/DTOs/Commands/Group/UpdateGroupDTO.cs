using NexusUserTest.Common.DTOs.Bases;

namespace NexusUserTest.Common.DTOs.Commands
{
    public record UpdateGroupDTO(int Id, string Title, int SpecializationId, DateTime Begin, DateTime End)
        : GroupBaseDTO(Title, Begin, End);
}
