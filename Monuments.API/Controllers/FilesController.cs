using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

namespace Monuments.API.Controllers;

[ApiController]
[Authorize]
[Route("api/files")]
public class FilesController(FileExtensionContentTypeProvider fileExtensionContentTypeProvider)
    : ControllerBase
{
    private readonly FileExtensionContentTypeProvider _fileExtensionContentTypeProvider = fileExtensionContentTypeProvider
            ?? throw new System.ArgumentNullException(
                nameof(_fileExtensionContentTypeProvider));
  


    [HttpGet("{fileId}")]
    public ActionResult GetFile(string fileId)
    {
        var pathToFile = "National Monuments.pdf";

        //Check if file exists
        if (System.IO.File.Exists(pathToFile) is false)
        {
            return NotFound();
        }

        if(_fileExtensionContentTypeProvider.TryGetContentType(
            pathToFile, out var contentType) is false)
        {
            contentType = "application/octet-stream";
        }

        var bytes = System.IO.File.ReadAllBytes(pathToFile);
        return File(bytes, contentType, Path.GetFileName(pathToFile));
    }
}