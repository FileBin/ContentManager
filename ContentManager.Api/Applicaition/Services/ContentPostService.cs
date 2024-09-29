using ContentManager.Api.Contracts.Application.Models.Requests;
using ContentManager.Api.Contracts.Application.Models.Responses;
using ContentManager.Api.Contracts.Application.Services;
using ContentManager.Api.Contracts.Domain.Data.Models;
using ContentManager.Api.Contracts.Persistance.Repository;
using ContentManager.Api.Contracts.Security.Repository;
using ContentManager.Api.Contracts.Security.Services;
using ContentManager.Api.Helpers.Extensions;
using Filebin.Shared.Domain.Abstractions;
using Filebin.Shared.Misc.Repository;
using Mapster;

namespace ContentManager.Api.Application.Services;

internal class ContentPostService(
    ICancellationTokenObtainer cancellationTokenObtainer,
    ISecureRepoContainer<IContentPostRepository, ContentPost> postContainer,
    ITagRepository tagRepository,
    ISecureUnitOfWork secureUnitOfWork,
    IUserContextAccessor userContextAccessor)
: IContentPostService {
    public async Task<ContentPostResponse> GetByIdAsync(Guid id) {
        return (await postContainer.Repo.GetByIdOrThrow(id, CancellationToken)).Adapt<ContentPostResponse>();
    }

    public async Task<IReadOnlyCollection<ContentPostResponse>> GetPageAsync(IPageDesc pageDesc) {
        var page = await postContainer.Repo
            .GetPageAsync(pageDesc, CancellationToken);

        return page.Adapt<List<ContentPostResponse>>();
    }

    public async Task<Guid> CreateAsync(ContentPostCreateRequest createRequest) {
        var entity = createRequest.Adapt<ContentPost>();
        entity.OwnerUserId = userContextAccessor.GetUserIdOrThrow();

        postContainer.Repo.Create(entity);

        await SetTags(entity, createRequest.Tags);

        await secureUnitOfWork.SaveChangesAsync(CancellationToken);

        return entity.Id;
    }

    public async Task DeleteAsync(Guid id) {
        await postContainer.Repo.DeleteByIdAsync(id, CancellationToken);
        await secureUnitOfWork.SaveChangesAsync(CancellationToken);
    }

    public async Task UpdateAsync(Guid id, ContentPostUpdateRequest updateRequest) {
        var entity = await postContainer.Repo.GetByIdOrThrow(id, CancellationToken);

        updateRequest.Adapt(entity);

        if (updateRequest.Tags is not null) {
            await SetTags(entity, updateRequest.Tags);
        }

        await secureUnitOfWork.SaveChangesAsync(CancellationToken);
    }

    private async Task SetTags(ContentPost entity, string[] tagsList) {
        entity.Tags ??= new HashSet<Tag>();
        entity.Tags.Clear();
        foreach (var tag in tagsList) {
            var tagEntity = await tagRepository.GetOrCreateByNameAsync(tag, CancellationToken);

            tagEntity.ContentPosts ??= new HashSet<ContentPost>();
            tagEntity.ContentPosts.Add(entity);

            entity.Tags.Add(tagEntity);
        }
    }

    private CancellationToken CancellationToken => cancellationTokenObtainer.GetCancellationToken();

}
