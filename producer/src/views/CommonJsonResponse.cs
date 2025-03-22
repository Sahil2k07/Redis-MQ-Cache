namespace producer.views;

public class CommonJsonResponse<T>(bool success, T data, string message = "Request Successfull")
{
    public bool Success { get; set; } = success;

    public string Message { get; set; } = message;

    public T Data { get; set; } = data;
}
