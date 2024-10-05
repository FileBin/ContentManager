using ContentManager.Api.Contracts.Domain.Data.Models;

namespace ContentManager.Api.Helpers.Extensions;

public static class ContentPostExtensions {
    public static void SetPreview(this ContentPost entity, int? order = null, int variant = 1) {
        if (order is not null) {
            entity.PreviewId ??=
                entity.Attachments.FirstOrDefault(c => c.PostOrder == order.Value && c.PostVariant == variant)?.Id;
        } else {
            entity.PreviewId ??= entity.Attachments.FirstOrDefault()?.Id;
        }
    }
}
