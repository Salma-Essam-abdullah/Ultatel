

namespace Ultatel.BusinessLoginLayer.Responses
{
    public class ValidationResponse
    {
     
        public string Message { get; set; }
        public bool isSucceeded { get; set; }
        public Dictionary<string, string> Errors { get; set; }
       

    }

}
