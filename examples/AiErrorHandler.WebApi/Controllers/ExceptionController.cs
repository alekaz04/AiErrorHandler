using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;

namespace AiErrorHandler.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[SuppressMessage("Usage", "CA2201:Не порождайте исключения зарезервированных типов")]
public class ExceptionController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        throw new Exception("This is an exception in controller");
    }
}
