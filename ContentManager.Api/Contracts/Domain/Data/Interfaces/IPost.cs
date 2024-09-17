using Filebin.Shared.Domain.Abstractions;

namespace ContentManager.Api.Contracts.Domain.Data.Interfaces;

public interface IPost : IEntity {
    public string Name { get; }
    public string Description { get; }
}