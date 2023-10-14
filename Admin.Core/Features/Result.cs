namespace Admin.Core.Features
{
    public class Result<T> where T : class
    {
        public bool Success { get; set; }

        public string Message { get; set; } = string.Empty;

        public Result()
        {
            Success = true;
        }

        public Result(bool success)
        {
            Success = false;
        }
        public Result(bool success, string message)
        {
            Success = success;
            Message = message;
        }

    }
}
