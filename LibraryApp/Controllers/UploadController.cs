using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace LibraryApp.Controllers;

public class UploadController : Controller
{
    [HttpPost("upload/image/{imgType}/{bookId}")]
    public IActionResult Post(IFormFile[] files, string imgType, ObjectId bookId)
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