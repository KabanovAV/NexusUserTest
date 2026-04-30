namespace NexusUserTest.Common.DTOs
{
    public record SpecializationDTO(int Id, string Title)
        : SpecializationBaseDTO(Title);
}
