namespace NexusUserTest.Shared.Services
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public T? Data { get; set; }
        public string? Error { get; set; }
        public string? ErrorCode { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;

        public static ApiResponse<T> SuccessResult(T data) => new() { Success = true, Data = data };
        public static ApiResponse<T> ErrorResult(string error, string? errorCode = null) => new()
        {
            Success = false,
            Error = error,
            ErrorCode = errorCode
        };
    }
}
