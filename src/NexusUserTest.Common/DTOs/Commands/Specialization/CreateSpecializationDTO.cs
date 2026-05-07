using NexusUserTest.Common.DTOs.Bases;

namespace NexusUserTest.Common.DTOs.Commands
{
    public record CreateSpecializationDTO(string Title)
        : SpecializationBaseDTO(Title);
}
