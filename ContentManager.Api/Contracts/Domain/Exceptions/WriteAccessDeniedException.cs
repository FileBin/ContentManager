using Filebin.Shared.Exceptions.Models;

namespace ContentManager.Api.Contracts.Domain.Exceptions;

public class WriteAccessDeniedException : ForbiddenException {
    public WriteAccessDeniedException() : WriteAccessDeniedException("You are not allowed to write") {}
}
