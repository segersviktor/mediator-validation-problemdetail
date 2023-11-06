namespace Handling.Common.ResponseWrapper;

public class Result : IResult
{
    public Result()
    {
    }

    public List<string> Messages { get; set; } = new List<string>();

    public bool Succeeded { get; set; }

    public static Result Failed()
    {
        return new Result { Succeeded = false };
    }

    public static Result Failed(string message)
    {
        return new Result { Succeeded = false, Messages = new List<string> { message } };
    }


    public static Result Failed(List<string> messages)
    {
        return new Result { Succeeded = false, Messages = messages };
    }

    public static Task<Result> FailedAsync()
    {
        return Task.FromResult(Failed());
    }

    public static Task<Result> FailedAsync(string message)
    {
        return Task.FromResult(Failed(message));
    }


    public static Task<Result> FailedAsync(List<string> messages)
    {
        return Task.FromResult(Failed(messages));
    }

    public static Result Success()
    {
        return new Result { Succeeded = true };
    }

    public static Result Success(string message)
    {
        return new Result { Succeeded = true, Messages = new List<string> { message } };
    }

    public static Task<Result> SuccessAsync()
    {
        return Task.FromResult(Success());
    }

    public static Task<Result> SuccessAsync(string message)
    {
        return Task.FromResult(Success(message));
    }
}

public class Result<T> : Result, IResult<T> where T : class
{
    public Result()
    {
    }

    public T? Data { get; set; }

    public new static Result<T> Failed()
    {
        return new Result<T> { Succeeded = false };
    }

    public new static Result<T> Failed(string message)
    {
        return new Result<T> { Succeeded = false, Messages = new List<string> { message } };
    }


    public new static Result<T> Failed(List<string> messages)
    {
        return new Result<T> { Succeeded = false, Messages = messages };
    }

    public new static Task<Result<T>> FailedAsync()
    {
        return Task.FromResult(Failed());
    }

    public new static Task<Result<T>> FailedAsync(string message)
    {
        return Task.FromResult(Failed(message));
    }


    public new static Task<Result<T>> FailedAsync(List<string> messages)
    {
        return Task.FromResult(Failed(messages));
    }

    public new static Result<T> Success()
    {
        return new Result<T> { Succeeded = true };
    }

    public new static Result<T> Success(string message)
    {
        return new Result<T> { Succeeded = true, Messages = new List<string> { message } };
    }

    public static Result<T> Success(T data)
    {
        return new Result<T> { Succeeded = true, Data = data };
    }

    public static Result<T> Success(T data, string message)
    {
        return new Result<T> { Succeeded = true, Data = data, Messages = new List<string> { message } };
    }

    public static Result<T> Success(T data, List<string> messages)
    {
        return new Result<T> { Succeeded = true, Data = data, Messages = messages };
    }

    public new static Task<Result<T>> SuccessAsync()
    {
        return Task.FromResult(Success());
    }

    public new static Task<Result<T>> SuccessAsync(string message)
    {
        return Task.FromResult(Success(message));
    }

    public static Task<Result<T>> SuccessAsync(T data)
    {
        return Task.FromResult(Success(data));
    }

    public static Task<Result<T>> SuccessAsync(T data, string message)
    {
        return Task.FromResult(Success(data, message));
    }
}