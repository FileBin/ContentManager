using ContentManager.Api.Contracts.Application.Models.Requests;
using ContentManager.Api.Contracts.Application.Models.Responses;
using ContentManager.Api.Contracts.Domain.Data.Models;
using Mapster;

namespace ContentManager.Api.Application.Mappings;

public class ContentPostMappings : IRegister {
    public void Register(TypeAdapterConfig config) {
        config.NewConfig<ContentPost, ContentPostResponse>()
            .Map(dest => dest.Tags, src => src.Tags.Select(t => t.Name).ToArray());

        config.NewConfig<ContentPostCreateRequest, ContentPost>()
            .Ignore(dest => dest.Tags);

        config.NewConfig<ContentPostUpdateRequest, ContentPost>()
            .Ignore(dest => dest.Tags);
    }
}
