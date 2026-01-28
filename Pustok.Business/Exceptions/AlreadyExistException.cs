using Pustok.Business.Abstractions;

namespace Pustok.Business.Exceptions;

public class AlreadyExistException(string message = "This item is already exist") : Exception(message), IBaseException
{
    public int StatusCode { get; set; } = 409;
}


//exception =>System.Exception=>ApplicationException