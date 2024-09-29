using ContentManager.Api.Contracts.Domain.Data.Models;
using ContentManager.Api.Contracts.Persistance.Repository;
using ContentManager.Api.Persistence.Data;
using Filebin.Shared.Domain.Abstractions;
using Filebin.Shared.Misc.Repository;
using Microsoft.EntityFrameworkCore;

namespace ContentManager.Api.Persistence.Repository;

internal class ContentCollectionRepository(IEntityAccessor accessor, IEntityObtainer obtainer)
: EntityCrudRepositoryBase<ContentCollection>(accessor, obtainer), IContentCollectionRepository {}
