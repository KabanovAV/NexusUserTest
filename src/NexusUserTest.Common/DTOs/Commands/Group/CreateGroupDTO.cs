using NexusUserTest.Common.DTOs.Bases;

namespace NexusUserTest.Common.DTOs.Commands
{
    public record CreateGroupDTO(string Title, int SpecializationId, DateTime Begin, DateTime End)
        : GroupBaseDTO(Title, Begin, End);
}
