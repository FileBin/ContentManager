using ContentManager.Api.Contracts.Domain.Data.Models.Auth;
using Filebin.Shared.Domain.Abstractions;

namespace ContentManager.Api.Contracts.Domain.Data.Interfaces.Auth;

public interface IAuthorizedResource : IEntity {
    public UserGroup? ReaderGroup { get; }
    public UserGroup? EditorGroup { get; }
    public UserGroup? OwnerGroup { get; }

    public bool IsPublished { get; }
}