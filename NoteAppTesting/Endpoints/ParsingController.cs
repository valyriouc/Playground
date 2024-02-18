using Microsoft.AspNetCore.Mvc;

namespace NoteAppTesting.Endpoints;

public record HtmlPayload
{
    public int Id { get; set; } 

    public string? Content { get; set; }
}

[ApiController]
[Route("api/[controller]")]
public class ParsingController : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery]int id)
    {
        HtmlPayload payload = new() { Id = id, Content = "<h1>Hello world</h1>" };
        return Ok(payload);
    }

}