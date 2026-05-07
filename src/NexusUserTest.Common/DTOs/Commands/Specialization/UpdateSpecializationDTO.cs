using NexusUserTest.Common.DTOs.Bases;

namespace NexusUserTest.Common.DTOs.Commands
{
    public record UpdateSpecializationDTO(int Id, string Title)
        : SpecializationBaseDTO(Title);
}
