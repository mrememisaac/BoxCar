namespace BoxCar.Admin.Core.Features
{
    public interface IResult<T>
    {
        public T Value { get; }

        public bool Success { get; }

        public string Message { get; }
    }

    public class Result<T> : IResult<T>
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
