namespace NexusUserTest.Application.Common.Errors
{
    public static class UserErrors
    {
        public static Error NotFound(int id)
            => Error.NotFound("User.NotFound", $"Пользователь с идентификатором '{id}' не найдена");

        public static Error Conflict(int entityId, int dtoId)
            => Error.Conflict("User.NotEqualId", $"Несовпадение идентификаторов: ожидался '{entityId}', получен '{dtoId}'");
    }
}
