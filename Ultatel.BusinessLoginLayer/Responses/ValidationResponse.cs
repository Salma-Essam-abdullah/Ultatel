

namespace Ultatel.BusinessLoginLayer.Responses
{
    public class ValidationResponse :ApiResponse
    {
     
        public string Message { get; set; }
        public bool isSucceeded { get; set; }
        public Dictionary<string, string> Errors { get; set; }
        public ValidationResponse(string? message = null) : base(400, message)
        {
            Message = message;
        }


    }

}
