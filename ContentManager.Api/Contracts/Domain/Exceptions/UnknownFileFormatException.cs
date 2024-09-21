using Filebin.Shared.Exceptions.Models;

namespace ContentManager.Api.Contracts.Domain.Exceptions;

public class UnknownFileFormatException : BadRequestException {
    public UnknownFileFormatException() : base("Unknown file format") {}
}
