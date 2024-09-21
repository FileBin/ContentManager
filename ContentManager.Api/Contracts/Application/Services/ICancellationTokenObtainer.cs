namespace ContentManager.Api.Contracts.Application.Services;

public interface ICancellationTokenObtainer
{
    CancellationToken GetCancellationToken();
}
