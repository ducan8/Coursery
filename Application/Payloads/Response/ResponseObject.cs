namespace Application.Payloads.Response
{
    public class ResponseObject<T>
    {
        public int StatusCode { get; set; } = 200;
        public string Message { get; set; } = string.Empty;
        public T? Data { get; set; }
        public ResponseObject() { }

        public ResponseObject(int statusCode, string message, T? data)
        {
            StatusCode = statusCode;
            Message = message;
            Data = data;
        }
    }
}
