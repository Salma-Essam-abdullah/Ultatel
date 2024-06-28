using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ultatel.BusinessLoginLayer.Responses
{
  
        public class ApiResponse
        {
            public int StatusCode { get; set; }

            public string? Message { get; set; }

            public ApiResponse(int statusCode, string? message = null)
            {
                StatusCode = statusCode;
                Message = message ?? getDefaultMessageForStatusCode(statusCode);
            }

            private string? getDefaultMessageForStatusCode(int statusCode)
            {
                return statusCode switch
                {
                    400 => "bad request",
                    401 => "unAuthorized",
                    404 => "resources was not found",
                    500 => "internal server error",
                    _ => null
                };
            }
        }
    
}
