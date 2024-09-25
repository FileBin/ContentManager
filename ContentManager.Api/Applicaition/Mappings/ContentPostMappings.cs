using ContentManager.Api.Contracts.Application.Models.Requests;
using ContentManager.Api.Contracts.Application.Models.Responses;
using ContentManager.Api.Contracts.Domain.Data.Models;
using ContentManager.Api.Helpers.Extensions;
using Mapster;

namespace ContentManager.Api.Application.Mappings;

public class ContentPostMappings : IRegister {
    public void Register(TypeAdapterConfig config) {
        config.NewConfig<ContentPost, ContentPostResponse>()
            .Map(dest => dest.Tags, 
                 src => src.Tags != null ? src.Tags.GetAllTags().ToHashSet().ToArray() : new string[0])
            .Map(dest => dest.ContentVariants,
                 src => src.Attachments != null ? src.Attachments.ConvertContents() : new Dictionary<int, Guid>[0]);

        config.NewConfig<ContentPostCreateRequest, ContentPost>()
            .Ignore(dest => dest.Tags);

        config.NewConfig<ContentPostUpdateRequest, ContentPost>()
            .IgnoreNullValues(true)
            .Ignore(dest => dest.Tags);
    }
}
