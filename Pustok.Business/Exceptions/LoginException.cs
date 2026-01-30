using Pustok.Business.Abstractions;

namespace Pustok.Business.Exceptions;

public class LoginException(string message = "Some Credeantials are wrong") : Exception(message), IBaseException
{
    public int StatusCode { get; set; } = 400;
}