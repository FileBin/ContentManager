using Filebin.Shared.Domain.Abstractions;

namespace ContentManager.Api.Contracts.Domain.Data.Interfaces.Auth;

public interface IAuthorizedResource : IEntity {
    public string? ReaderGroupName { get; }

    public string? EditorGroupName { get; }

    public string? OwnerGroupName { get; }

    public string OwnerUserId { get; }
    
    public bool IsPublic { get; }
    public bool IsDraft { get; }
}