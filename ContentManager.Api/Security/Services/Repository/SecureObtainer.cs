using ContentManager.Api.Contracts.Domain.Data.Models;
using ContentManager.Api.Contracts.Persistance.Repository;
using ContentManager.Api.Contracts.Security.Repository;
using ContentManager.Api.Contracts.Security.Services;
using ContentManager.Api.Security.Services.Guards;
using Filebin.Shared.Domain.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace ContentManager.Api.Security.Services.Repository;

internal class SecureObtainer(IEntityObtainer obtainer,
IEntityReadGuard<Content> contentGuard,
IEntityReadGuard<ContentCollection> contentCollectionGuard,
IEntityReadGuard<ContentPost> contentPostGuard) : ISecureEntityObtainer {
    public IQueryable<T> StartQuery<T>() where T : class {
        IQueryable<T> query = obtainer.StartQuery<T>();

        if (query is IQueryable<Content> qContent)
            query = (IQueryable<T>)contentGuard.FilterQuery(qContent);

        if (query is IQueryable<ContentPost> qContentPost)
            query = (IQueryable<T>)contentPostGuard.FilterQuery(qContentPost);

        if (query is IQueryable<ContentCollection> qContentCollection)
            query = (IQueryable<T>)contentCollectionGuard.FilterQuery(qContentCollection);

        return query;
    }

    public static void RegisterReadGuards(IServiceCollection services) {
        services.AddScoped<IEntityReadGuard<Content>, ContentReadGuard>();
        services.AddScoped<IEntityReadGuard<ContentPost>, AuthorizedResourceReadGuard<ContentPost>>();
        services.AddScoped<IEntityReadGuard<ContentCollection>, AuthorizedResourceReadGuard<ContentCollection>>();
    }

    public static void RegisterContainers(IServiceCollection services) {
        services.AddScoped<ISecureRepoContainer<IContentRepository, Content>,
            SecureRepoContainer<IContentRepository, Content>>();

        services.AddScoped<ISecureRepoContainer<IContentPostRepository, ContentPost>,
            SecureRepoContainer<IContentPostRepository, ContentPost>>();
            
        services.AddScoped<ISecureRepoContainer<IContentCollectionRepository, ContentCollection>,
            SecureRepoContainer<IContentCollectionRepository, ContentCollection>>();
    }
}