using ContentManager.Api.Contracts.Application.Models.Requests;
using ContentManager.Api.Contracts.Application.Services;
using Filebin.Shared.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ContentManager.Api.Presentation.Controllers;

[Authorize]
[Route("api/posts")]
[ApiController]
public class ContentPostController(IContentPostService service) : ControllerBase {
    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> GetPage([FromQuery] PageDesc pageDesc) {
        return Ok(await service.GetPageAsync(pageDesc));
    }

    [HttpGet("/count")]
    [AllowAnonymous]
    public async Task<IActionResult> GetCount() {
        return Ok(await service.GetCountAsync());
    }

    [HttpGet("{id:guid}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetPost([FromRoute] Guid id) {
        return Ok(await service.GetByIdAsync(id));
    }

    [HttpPost]
    public async Task<IActionResult> CreatePost([FromBody] ContentPostCreateRequest createRequest) {
        return Ok(await service.CreateAsync(createRequest));
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdatePost([FromRoute] Guid id, [FromBody] ContentPostUpdateRequest updateRequest) {
        await service.UpdateAsync(id, updateRequest);
        return Ok();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeletePost([FromRoute] Guid id) {
        await service.DeleteAsync(id);
        return Ok();
    }
}
