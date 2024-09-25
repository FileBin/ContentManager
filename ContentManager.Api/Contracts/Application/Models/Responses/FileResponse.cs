namespace ContentManager.Api.Contracts.Application.Models.Responses;

public record FileResponse {
    public required Stream FileStream { get; init; }
    public required string FileName { get; init; }
    public string ContentType { get; init; } = "application/octet-stream";
}
