using static System.Runtime.InteropServices.JavaScript.JSType;

namespace shipping.Model
{
    public class BResponse
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public bool Success {  get; set; }
        public BResponse(bool success, string message, int statusCode)
        {
            Success = success;
            Message = message;
            StatusCode = statusCode;
        }
    }
}
