namespace NexusUserTest.Shared.Services
{
    public class ApiResponse<T>
    {
        public bool IsSuccess { get; set; }
        public T? Data { get; set; }
        public string? Error { get; set; }

        public static ApiResponse<T> Success(T? data) => new() { IsSuccess = true, Data = data };
        public static ApiResponse<T> Errors(string error) => new() { IsSuccess = false, Error = error };
    }
}
