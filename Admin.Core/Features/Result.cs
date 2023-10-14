namespace BoxCar.Admin.Core.Features
{
    public class Result<T> where T : class
    {
        public T Value { get; set; }

        public bool Success { get; set; }

        public string Message { get; set; } = string.Empty;

        public Result(T t)
        {
            Success = true;
            Value = t;
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
