using Microsoft.AspNetCore.Mvc;

namespace LibraryApp.Controllers;

public interface IDownloadController
{
    IActionResult Single(string FileName);
}

public class DownloadControllerController : Controller, IDownloadController
{
    private readonly IWebHostEnvironment _environment;
    public DownloadControllerController(IWebHostEnvironment environment)
    {
        this._environment = environment;
    }

    [HttpGet("download/single")]
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