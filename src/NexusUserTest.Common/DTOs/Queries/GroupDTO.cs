namespace NexusUserTest.Common.DTOs
{
    public record GroupDTO(int Id, string Title, string SpecializationTitle, int CountUsers, DateTime Begin, DateTime End)
        : GroupBaseDTO(Title, Begin, End);
}
