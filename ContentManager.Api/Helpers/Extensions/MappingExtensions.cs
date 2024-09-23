using ContentManager.Api.Contracts.Domain.Data.Models;

namespace ContentManager.Api.Helpers.Extensions;

public static class MappingExtensions
{
    public static IEnumerable<string> GetAllTags(this ISet<Tag> tags) {
        foreach (var tag in tags) {
            yield return tag.Name;
            var tagParent = tag.Parent;
            while(tagParent != null) {
                yield return tagParent.Name;
                tagParent = tagParent.Parent;
            }
        }
    }
}