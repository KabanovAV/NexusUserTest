namespace NexusUserTest.Common.DTOs
{
    public record UpdateGroupDTO(int Id, string Title, int SpecializationId, DateTime Begin, DateTime End)
        : GroupBaseDTO(Title, Begin, End);
}
