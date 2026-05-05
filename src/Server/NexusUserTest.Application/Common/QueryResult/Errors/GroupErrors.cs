namespace NexusUserTest.Application.Common
{
    public static class GroupErrors
    {
        public static Error NotFound(int id)
            => Error.NotFound("Group.NotFound", $"Группа с идентификатором '{id}' не найдена");

        public static Error Conflict(int entityId, int dtoId)
            => Error.Conflict("Group.NotEqualId", $"Несовпадение идентификаторов: ожидался '{entityId}', получен '{dtoId}'");
    }
}
