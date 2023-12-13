namespace BonTech.Product.Domain.Result;

public class Result
{
    public bool IsSuccess => ErrorMessage == null;
    public string ErrorMessage { get; set; }
    public int? ErrorCode { get; set; }
}

public class Result<T> : Result
{
    public Result(string errorMessage, int errorCode, T data)
    {
        ErrorMessage = errorMessage;
        ErrorCode = errorCode;
        Data = data;
    }
    
    public Result(string errorMessage, int errorCode)
    {
        ErrorMessage = errorMessage;
        ErrorCode = errorCode;
    }
    
    public Result(T data)
    {
        Data = data;
    }

    public Result() {  }
    public T Data { get; set; }
}