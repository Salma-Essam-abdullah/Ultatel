using Microsoft.AspNetCore.Mvc;
using Ultatel.BusinessLoginLayer.Errors;

namespace Ultatel.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ServiceFilter(typeof(CustomExceptionFilter))]
    public class BaseController : ControllerBase
    {
    }
}
