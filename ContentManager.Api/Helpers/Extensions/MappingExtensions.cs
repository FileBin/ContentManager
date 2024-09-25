using ContentManager.Api.Contracts.Domain.Data.Models;

namespace ContentManager.Api.Helpers.Extensions;

public static class MappingExtensions {
    public static IEnumerable<string> GetAllTags(this ISet<Tag> tags) {
        foreach (var tag in tags) {
            yield return tag.Name;
            var tagParent = tag.Parent;
            while (tagParent != null) {
                yield return tagParent.Name;
                tagParent = tagParent.Parent;
            }
        }
    }

    public static Dictionary<int, Guid>[] ConvertContents(this ICollection<Content> contents) {
        contents = [.. contents.OrderBy(x => x.PostOrder)];
        var n = contents.Last().PostOrder;

        var result = new Dictionary<int, Guid>[n];

        foreach (var content in contents) {
            var i = content.PostOrder - 1;
            result[i] ??= [];
            result[i].Add(content.PostVariant, content.Id);
        }

        return result;
    }
}