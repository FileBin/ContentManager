using ContentManager.Api.Contracts.Application.Models.Responses;
using ContentManager.Api.Contracts.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ContentManager.Api.Presentation.Controllers;

[Authorize]
[Route("api/posts")]
[ApiController]
public class ContentPostContentController(IContentPostContentService service) : ControllerBase {
    [HttpGet("contents/{id:guid}")]
    [AllowAnonymous]
    public async Task<IActionResult> DownloadContent([FromRoute] Guid id, [FromQuery] string? quality) {
        var file = await service.DownloadContentAsync(id, quality);
        return File(file);
    }

    [HttpGet("{postId:guid}/contents/{postOrder:int}")]
    [AllowAnonymous]
    public async Task<IActionResult> DownloadContent(
        [FromRoute] Guid postId,
        [FromRoute] int postOrder,
        [FromQuery] int? postVariant) {

        var file = await service.DownloadContentByPostIdAsync(postId, postOrder, postVariant);
        return File(file);
    }

    [HttpPost("{postId:guid}/contents")]
    [AllowAnonymous]
    public async Task<IActionResult> UploadContent(IFormFile file, [FromRoute] Guid postId) {
        return Ok(await service.UploadContentAsync(file, postId));
    }

    private IActionResult File(FileResponse file) {
        return File(file.FileStream, file.ContentType, file.FileName);
    }
}
