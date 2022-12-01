using Microsoft.AspNetCore.Mvc;

namespace LibraryApp.Controllers;

public interface IUpload
{
    IActionResult Single(IFormFile file);
    IActionResult Multiple(IFormFile[] files);
    IActionResult Post(IFormFile[] files, int id);
}

[Route("api/[controller]")]
[ApiController]
public class Upload : Controller, IUpload
{
    [HttpPost("[action]")]
    public IActionResult Single(IFormFile file)
    {
        try
        {
            // Put your code here
            return StatusCode(200);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpPost("[action]")]
    public IActionResult Multiple(IFormFile[] files)
    {
        try
        {
            // Put your code here
            return StatusCode(200);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpPost("[action]/{id}")]
    public IActionResult Post(IFormFile[] files, int id)
    {
        try
        {
            // Put your code here
            return StatusCode(200);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
}