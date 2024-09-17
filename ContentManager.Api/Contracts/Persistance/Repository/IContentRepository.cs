using ContentManager.Api.Contracts.Domain.Data.Models;
using Filebin.Shared.Domain.Abstractions;

namespace ContentManager.Api.Contracts.Persistance.Repository;

public interface IContentRepository : IEntityRepository<Content> { }
