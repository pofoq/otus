using FileService.Api.Dtos;
using FileService.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace FileService.Api.Controllers;

[ApiController]
[Route("[controller]")]
//[Route("api/files")]
public class FilesController(IFileService fileService) : ControllerBase
{
    private readonly IFileService _fileService = fileService;

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<FileDto>> Get(Guid id)
    {
        if (id == Guid.Empty)
            return BadRequest();

        var file = await _fileService.GetFileAsync(id);
        if (file == null)
            return NotFound();

        return Ok(file);
    }
}
