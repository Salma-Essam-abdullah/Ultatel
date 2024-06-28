using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace Ultatel.BusinessLoginLayer.Errors
{
    public class CustomExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            var statusCode = HttpStatusCode.InternalServerError;
            var message = "Internal Server Error";

            if (context.Exception is ValidationException )
            {
                statusCode = HttpStatusCode.BadRequest;
                message = "Validation Error";
            }
            else if (context.Exception is ArgumentNullException argNullException)
            {
                statusCode = HttpStatusCode.BadRequest;
                message = argNullException.Message;
            }
            else if (context.Exception is UnauthorizedAccessException)
            {
                statusCode = HttpStatusCode.Unauthorized;
                message = "Unauthorized";
            }
            else if (context.Exception is KeyNotFoundException)
            {
                statusCode = HttpStatusCode.NotFound;
                message = "Resource not found";
            }

            var result = new JsonResult(new { error = message })
            {
                StatusCode = (int)statusCode
            };

            context.Result = result;
            context.ExceptionHandled = true;
        }
    }
}
