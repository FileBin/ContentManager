using ContentManager.Api.Contracts.Application.Services;
using Microsoft.AspNetCore.Http;


namespace ContentManager.Api.Presentation.Services;

public class CancellationTokenObtainer(IHttpContextAccessor httpContextAccessor) : ICancellationTokenObtainer {
    public CancellationToken GetCancellationToken() {
        return httpContextAccessor.HttpContext?.RequestAborted ?? default;
    }
}
