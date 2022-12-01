using Microsoft.AspNetCore.Mvc;

namespace LibraryApp.Controllers;

public interface IDownload
{
    IActionResult Single(string FileName);
}

[Route("api/[controller]")]
[ApiController]
public class Download : Controller, IDownload
{
    private readonly IWebHostEnvironment _environment;
    public Download(IWebHostEnvironment environment)
    {
        this._environment = environment;
    }

    [HttpGet("[action]")]
    public IActionResult Single(string FileName)
    {
        string path = Path.Combine(
            _environment.WebRootPath,
            "files",
            FileName);

        var stream = new FileStream(path, FileMode.Open);

        var result = new FileStreamResult(stream, "text/plain");
        result.FileDownloadName = FileName;
        return result;
    }
}