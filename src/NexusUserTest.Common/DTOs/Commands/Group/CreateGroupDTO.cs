namespace NexusUserTest.Common.DTOs
{
    public record CreateGroupDTO(string Title, int SpecializationId, DateTime Begin, DateTime End)
        : GroupBaseDTO(Title, Begin, End);
}
