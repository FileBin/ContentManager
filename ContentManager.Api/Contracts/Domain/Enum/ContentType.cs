using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace ContentManager.Api.Contracts.Domain.Enum;

[JsonConverter(typeof(StringEnumConverter))]
public enum ContentType {
    Picture,
    Gif,
    Video,
    Music
}