using ContentManager.Api.Contracts.Domain.Data.Models.Auth;
using Filebin.Shared.Domain.Abstractions;

namespace ContentManager.Api.Contracts.Domain.Data.Interfaces.Auth;

public interface IAuthorizedResource : IEntity {
    public Guid? ReaderGroupId { get; }
    public UserGroup? ReaderGroup { get; }

    public Guid? EditorGroupId { get; }
    public UserGroup? EditorGroup { get; }

    public Guid? OwnerGroupId { get; }
    public UserGroup? OwnerGroup { get; }

    public string OwnerUserId { get; }
    public User Owner { get; }
    
    public bool IsPublic { get; }
    public bool IsDraft { get; }
}