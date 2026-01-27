namespace Pustok.Business.Exceptions;

public class AlreadyExistException(string message = "This item is already exist") : Exception(message)
{
}
