namespace NexusUserTest.Application.Common
{
    public static class SpecializationErrors
    {
        public static Error NotFound(int id)
            => Error.NotFound("Specialization.NotFound", $"Специализация с идентификатором '{id}' не найдена");

        public static Error Conflict(int entityId, int dtoId)
            => Error.Conflict("Specialization.NotEqualId", $"Несовпадение идентификаторов: ожидался '{entityId}', получен '{dtoId}'");

        public static Error Connection(int id)
            => Error.Conflict("Specialization.Connection", $"Специализация с идентификатором '{id}' имеет связи с другими таблицами");
    }
}
