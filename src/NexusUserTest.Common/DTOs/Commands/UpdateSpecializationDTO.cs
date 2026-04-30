namespace NexusUserTest.Common.DTOs
{
    public record UpdateSpecializationDTO(int Id, string Title)
        : SpecializationBaseDTO(Title);
}
