using Filebin.Shared.Exceptions.Models;

namespace ContentManager.Api.Contracts.Domain.Exceptions;

public class TagDepthException : BadRequestException {
    public TagDepthException(int maxDepth) : base($"Tag depth is too big, it must be less than {maxDepth}") {}
}
