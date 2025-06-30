using Microsoft.AspNetCore.Mvc;

namespace Onion.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TestController: ControllerBase
{
    [HttpGet]
    public ActionResult<string> Welcome(string name)
    {
        return Ok($"Hello, {name}!, Welcome to Aramm.");
    }
}