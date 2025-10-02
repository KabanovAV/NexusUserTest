namespace NexusUserTest.Shared.Services
{
    /// <summary>
    /// Специальный тип, обозначающий "ничего" (аналог void для дженериков).
    /// Используется в ApiResponse<Unit>, когда данные не возвращаются.
    /// </summary>
    public readonly struct Unit
    {
        public static readonly Unit Value = new();
    }

    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public T? Data { get; set; }
        public string? Error { get; set; }
        public string? ErrorCode { get; set; }
        public string RequestId { get; set; } = Guid.NewGuid().ToString();
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;

        public static ApiResponse<T> SuccessResult(T? data = default) =>
            new() { Success = true, Data = data };

        public static ApiResponse<T> ErrorResult(string error, string? errorCode = null) =>
            new()
            {
                Success = false,
                Error = error,
                ErrorCode = errorCode
            };
    }
}
