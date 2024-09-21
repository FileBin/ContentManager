using ContentManager.Api.Contracts.Domain.Data.Models;
using ContentManager.Api.Security.Helpers;

namespace ContentManager.Api.Security.Services.Guards;

public class ContentWriteGuard : TypedWriteGuard<Content> {
    public override bool CanCreate(Content entity) {
        return true;
    }

    public override bool CanDelete(Content entity) {
        return false;
    }

    public override bool CanUpdate(Content entity, Content original) {
        return false;
    }
}
