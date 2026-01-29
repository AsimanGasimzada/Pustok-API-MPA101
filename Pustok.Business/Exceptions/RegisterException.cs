using Pustok.Business.Abstractions;

namespace Pustok.Business.Exceptions;

public class RegisterException(string message = "Registration failed") : Exception(message), IBaseException
{
    public int StatusCode { get; set; } = 400;
}