using NexusUserTest.Common.DTOs.Bases;

namespace NexusUserTest.Common.DTOs.Queries
{
    public record SpecializationDTO(int Id, string Title)
        : SpecializationBaseDTO(Title);
}
