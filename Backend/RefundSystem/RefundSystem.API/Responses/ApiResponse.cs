using System.Text.Json.Serialization;

namespace RefundSystem.API.Responses
{
    public class ApiResponse<T>
    {
        public bool Success { get; private set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public T? Data { get; private set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<string>? Errors { get; private set; }

        public static ApiResponse<T> CreateSuccess(T data)
        {
            return new ApiResponse<T> { Success = true, Data = data };
        }
        public static ApiResponse<T> CreateError(List<string> errors)
        {
            return new ApiResponse<T> { Success = false, Errors = errors };
        }
        public static ApiResponse<T> CreateError(string error)
        {
            return new ApiResponse<T> { Success = false, Errors = new List<string> { error } };
        }
    }
}
