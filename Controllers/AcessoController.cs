using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BibliotecaAPI.Controllers;

[ApiController]
[Route("{controller}")]
public class AcessoController : ControllerBase
{
    [HttpGet]
    [Authorize(Policy = "IdadeMinima")]
    public IActionResult Get()
    {
        return Ok("Acesso permitido!");
    }
}
