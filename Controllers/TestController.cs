using Microsoft.AspNetCore.Mvc;

namespace Project1.Controllers;

[ApiController]
[Route("[controller]")]
public class TestController : ControllerBase
{
    [HttpGet("testing")]
    public IActionResult Get([FromQuery] string message)
    {
        return Ok(message);
    }
}
